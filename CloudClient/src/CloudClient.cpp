// Boost
#include <boost/asio.hpp>
#include <boost/date_time/gregorian/gregorian.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>
#include <boost/lexical_cast.hpp>
#include <boost/random.hpp>
#include <boost/thread.hpp>
#include <boost/timer.hpp>

// OpenAL
#include <AL/al.h>
#include <AL/alc.h>

// OpenCV
#include <opencv2/opencv.hpp>

#include "filters/audio_encoder.h"
#include "filters/video_encoder.h"
#include "filters/multiplexer.h"
#include "filters/transmitter.h"

#include "list.h"
#include "3rdparty/LSD/LSD.h"

using namespace std;
using boost::asio::ip::tcp;
using namespace boost::posix_time;
using boost::posix_time::ptime;
using boost::posix_time::time_duration;

int audio_capture_device;
int audio_sample_rate;
std::string container;
bool flag_disable_audio;
bool flag_disable_video;
bool flag_generate_audio;
bool flag_generate_video;
bool flag_lsd;
std::string server;
int stream_bitrate;
std::string username;
int video_capture_device;
int video_width;
int video_height;
int video_frame_rate;

bool has_audio;
bool has_video;

audio_encoder* audio_encoder_block;
video_encoder* video_encoder_block;
multiplexer* multiplexer_block;
transmitter* transmitter_block;

// Boost
boost::mt19937 rng;
boost::uniform_int<> six(-128, 127);
boost::variate_generator<boost::mt19937&, boost::uniform_int<> >die(rng, six);

// FFmpeg
AVFrame* frame;
AVFrame* readyFrame;
int nSampleSize;
char* sample;
URLContext* StreamToUrl;

// OpenAL
ALCdevice* dev[2];
ALCcontext* ctx;
ALuint source, buffers[3];
ALchar* Buffer;
ALuint buf;
ALint val;
ALint iSamplesAvailable;
int nBlockAlign;
ALCdevice* pDevice;
ALCcontext* pContext;
ALCdevice* pCaptureDevice;
const ALCchar* szDefaultCaptureDevice;

// OpenCV
CvCapture* capture;
IplImage* CVframe;
IplImage* CVframeWithText;
IplImage* destination;
IplImage* redchannel;
IplImage* greenchannel;
IplImage* bluechannel;
CvFont font;
CvPoint UL;
CvPoint LR;

int64_t desiredTimeForCaptureFame;
int64_t spendedTimeForCaptureFame;

int64_t desiredTimeForMain;
int64_t spendedTimeForMain;

boost::timer timerForCaptureFame;
boost::timer timerForMain;

void init_opencv()
{
	if (flag_disable_video)
	{
		has_video = false;
		return;
	}

	if (flag_generate_video)
	{
		has_video = true;
		cvInitFont(&font, CV_FONT_HERSHEY_DUPLEX, 2, 1, 0.0, 3, CV_AA);
		CvPoint UL = { 0, 0 };
		CvPoint LR = { video_width, video_height };
		CVframe = cvCreateImage(cvSize(video_width, video_height), 8, 4);
		CVframeWithText = cvCreateImage(cvSize(video_width, video_height), 8, 4);
		cvRectangle(CVframe, UL, LR, CV_RGB(0, 254, 53), CV_FILLED);
		cvPutText(CVframe, username.c_str(), cvPoint(0, video_height - 10), &font, CV_RGB(1, 1, 1));
	}
	else
	{
		CamerasListNamespace::CamerasList* CamList = new CamerasListNamespace::CamerasList();
		video_capture_device = CamList->SelectFromList();

		if (video_capture_device == 999)
			has_video = false;
		else
		{
			capture = cvCaptureFromCAM(video_capture_device);
			cvSetCaptureProperty(capture, CV_CAP_PROP_FPS, video_frame_rate);
			cvSetCaptureProperty(capture, CV_CAP_PROP_FRAME_WIDTH, (double)video_width);
			cvSetCaptureProperty(capture, CV_CAP_PROP_FRAME_HEIGHT, (double)video_height);
		}

		if (!capture)
		{
			has_video = false;
			fprintf(stderr, "Cannot initialize selected webcam!\n");
			cout << endl;
			return;
		}

		CVframe = cvQueryFrame(capture);
		destination = cvCreateImage(cvSize(video_width, video_height), CVframe->depth, CVframe->nChannels);
		redchannel = cvCreateImage(cvGetSize(destination), 8, 1);
		greenchannel = cvCreateImage(cvGetSize(destination), 8, 1);
		bluechannel = cvCreateImage(cvGetSize(destination), 8, 1);
		has_video = true;
	}
}

void init_openal(int fps)
{
	if (flag_disable_audio)
	{
		has_audio = false;
		return;
	}

	if (flag_generate_audio)
	{
		has_audio = true;
		nSampleSize = (int)(2.0f * audio_sample_rate / fps);
		nBlockAlign = 1 * 16 / 8;
		Buffer = new ALchar[nSampleSize];
	}
	else
	{
		nSampleSize = (int)(2.0f * audio_sample_rate / fps);
		nBlockAlign = 1 * 16 / 8;
		Buffer = new ALchar[nSampleSize];
		dev[0] = alcOpenDevice(NULL);
		if (NULL == dev[0])
		{
			fprintf(stderr, "No microphone found, please restart application , or continue streaming with out sound\n");
			boost::this_thread::sleep(boost::posix_time::seconds(9999999));
			cin.get();
			return;
		}

		ctx = alcCreateContext(dev[0], NULL);
		alcMakeContextCurrent(ctx);
		int i = -1;
		string bufferString[99];
		const ALchar *pDeviceList = alcGetString(NULL, ALC_CAPTURE_DEVICE_SPECIFIER);
		const ALCchar *bufferList[99];
		if (pDeviceList)
		{
			printf("\nLet us select audio device\n");
			printf("Available Capture Devices are:\n");
			i = 0;
			while (*pDeviceList)
			{
				bufferList[i] = pDeviceList;
				bufferString[i] += pDeviceList;
				cout << i <<") " << bufferString[i] << endl;
				pDeviceList += strlen(pDeviceList) + 1;
				i++;
			}
		}

		//Get the name of the 'default' capture device
		//szDefaultCaptureDevice = alcGetString(NULL, ALC_CAPTURE_DEFAULT_DEVICE_SPECIFIER);
		//printf("\nDefault Capture Device is '%s'\n\n", szDefaultCaptureDevice);

		int SelectedIndex = 999;
		if (i <= 0)
		{
			cout <<"No devices found. \n " << endl;
			//cout <<"Please restart application." << endl;
			//cin.get();
			//Sleep(999999);
			SelectedIndex = 999;
		}
		else
			if (i == 1)
			{
				cout <<"Default device will be used" << std::endl;
				SelectedIndex = 0;
				has_audio = true;
			}
			else
			{
				while(SelectedIndex > i-1 || SelectedIndex < 0)
				{
					try
					{
						std::cout << "please input index from 0 to " << i - 1 << std::endl;
						std::string s;
						std::getline(cin, s, '\n');
						SelectedIndex = boost::lexical_cast<int>(s);
					}
					catch(std::exception&)
					{
						SelectedIndex = 999;
					}
				}
			}

		if (SelectedIndex == 999)
		{
			has_audio = false;
		}
		else
		{
			has_audio = true;
			alDistanceModel(AL_NONE);
			dev[0] = alcCaptureOpenDevice(bufferList[SelectedIndex], audio_sample_rate, AL_FORMAT_MONO16, nSampleSize/2);
			alcCaptureStart(dev[0]);
		}
	}
}

void init_ffmpeg(string container, int w, int h, int fps)
{
	int bufferImgSize = avpicture_get_size(PIX_FMT_BGR24, w, h);

	frame = avcodec_alloc_frame();
	uint8_t* frameBuffer = (uint8_t*)av_mallocz(bufferImgSize);
	avpicture_fill((AVPicture*)frame, frameBuffer, PIX_FMT_BGR24, w, h);

	readyFrame = avcodec_alloc_frame();
	uint8_t* readyFrameBuffer = (uint8_t*)av_mallocz(bufferImgSize);
	avpicture_fill((AVPicture*)readyFrame, readyFrameBuffer, PIX_FMT_BGR24, w, h);

	sample = new char[nSampleSize];
}

void capture_frame(int w, int h, char* buffer, int bytespan)
{
	if (flag_generate_video)
	{
		cvResize(CVframe, CVframeWithText);
		ptime now = second_clock::local_time();
		cvPutText(CVframeWithText, to_simple_string(now.time_of_day()).c_str(), cvPoint(0, (h / 2 + 10)), &font, CV_RGB(1, 1, 1));
		for (int i = 0; i < w * 4 * h; i = i + 4)
		{
			buffer[0] = CVframeWithText->imageData[i];
			buffer[1] = CVframeWithText->imageData[i + 1];
			buffer[2] = CVframeWithText->imageData[i + 2];
			buffer += 3;
		}
		//if (rainbow)
		//{
		//	int wxh = w * h;
		//	static float seed = 1.0;
		//	for (int i = 0; i < h; i++)
		//	{
		//		char* line = buffer + i * bytespan;
		//		for (int j = 0; j < w; j ++)
		//		{
		//			// RGB
		//			line[0] = 255 * sin(((float)i / wxh * seed) * 3.14);
		//			line[1] = 255 * cos(((float)j / wxh * seed) * 3.14);
		//			line[2] = 255 * sin(((float)(i + j) / wxh * seed) * 3.14);
		//			line += 3;
		//		}
		//	}
		//	seed = seed + 2.2;
		//}
	}
	else
	{
		CVframe = cvQueryFrame(capture);
		if (!CVframe)
		{
			printf("No CV frame captured!\n");
			cin.get();
		}

		cvResize(CVframe, destination);
		if (flag_lsd)
		{
			IplImage *destinationForLSD = cvCreateImage(cvSize(w, h), IPL_DEPTH_8U, 1);
			cvCvtColor(destination, destinationForLSD, CV_RGB2GRAY);

			image_double lsdImage;
			ntuple_list lsdOut;
			lsdImage = new_image_double(w, h);

			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					lsdImage->data[x + y * lsdImage->xsize] = cvGetReal2D(destinationForLSD, y, x);

			// call LSD
			lsdOut = lsd(lsdImage);

			for (unsigned int i = 0; i < lsdOut->size; i++)
			{
				CvPoint pt1 = { (int)lsdOut->values[i * lsdOut->dim + 0], (int)lsdOut->values[i * lsdOut->dim + 1] };
				CvPoint pt2 = { (int)lsdOut->values[i * lsdOut->dim + 2], (int)lsdOut->values[i * lsdOut->dim + 3] };
				cvLine(destination, pt1, pt2, CV_RGB(240, 255, 255), 1, CV_AA, 0);
			}
			cvReleaseImage(&destinationForLSD);
			free_image_double(lsdImage);
			free_ntuple_list(lsdOut);
		}

		for (int i = 0; i < destination->imageSize; i = i + 3)
		{
			buffer[2] = destination->imageData[i];
			buffer[1] = destination->imageData[i + 1];
			buffer[0] = destination->imageData[i + 2];
			buffer += 3;
		}

		//cvSplit(destination, bluechannel, greenchannel, redchannel, NULL);
		//for(int y = 0; y < destination->height; y++)
		//{
		//	char* line = buffer + y * bytespan;
		//	for(int x = 0; x < destination->width; x++)
		//	{
		// 		line[0] = cvGetReal2D(redchannel, y, x);
		// 		line[1] = cvGetReal2D(greenchannel, y, x);
		// 		line[2] = cvGetReal2D(bluechannel, y, x);
		// 		line += 3;
		//	}
		//}

		//for (int i = 0; i < w * h * 3; ++i) {
		// 		buffer[i] = destination->imageData;
		//}
	}
}

char* capture_sample()
{
	if (flag_generate_audio)
	{
		for (int i = 0; i < nSampleSize / nBlockAlign; i++)
			Buffer [i] = die();
	}
	else
	{
		// Check how much audio data has been captured (note that 'val' is the number of frames, not bytes)
		alcGetIntegerv(dev[0], ALC_CAPTURE_SAMPLES, 1, &iSamplesAvailable);
		// When we would have enough data to fill our BUFFERSIZE byte buffer, will grab the samples, so now we should wait
		while (iSamplesAvailable < (nSampleSize / nBlockAlign) - 1) // -1 was added to make code run on Mac OS X, potential bug
			alcGetIntegerv(dev[0], ALC_CAPTURE_SAMPLES, 1, &iSamplesAvailable);
		// Consume Samples
		alcCaptureSamples(dev[0], Buffer, (nSampleSize / nBlockAlign) - 1);
	}
	return (char*)Buffer;
}

void release_opencv()
{
	if (!flag_generate_video)
	{
		cvReleaseCapture(&capture);
		//cvReleaseImage(&destination);
		//cvReleaseImage(&CVframe);
	}
}

void release_openal()
{
}

void release_ffmpeg()
{
	av_free(frame->data[0]);
	av_free(frame);

	av_free(readyFrame->data[0]);
	av_free(readyFrame);

	delete[] sample;
	sample = NULL;
}

void capture_frame_loop()
{
	if (has_video)
	{
		while (true)
		{
			timerForCaptureFame.restart();
			capture_frame(video_width, video_height, (char*)frame->data[0], frame->linesize[0]);
			AVFrame* swap = frame;
			frame = readyFrame;
			readyFrame = swap;
			spendedTimeForCaptureFame = (int64_t)timerForCaptureFame.elapsed();
			if (spendedTimeForCaptureFame < desiredTimeForCaptureFame)
				boost::this_thread::sleep(boost::posix_time::milliseconds(desiredTimeForCaptureFame - spendedTimeForCaptureFame));
		}
	}
}

void save_frame_loop()
{
	while (true)
	{
		timerForMain.restart();
		if (!has_video)
			try
			{
				audio_encoder_block->send(capture_sample(), nSampleSize);
			}
			catch (std::exception)
			{
				printf("Cannot write frame!\n");
			}

		if (!has_audio)
			try
			{
				video_encoder_block->send(readyFrame);
			}
			catch (std::exception)
			{
				printf("Cannot write frame!\n");
			}
		
		if (has_audio && has_video)
			try
			{
				audio_encoder_block->send(capture_sample(), nSampleSize);
				video_encoder_block->send(readyFrame);
			}
			catch (std::exception)
			{
				printf("Cannot write frame!\n");
			}
		
		if (!has_audio && !has_video)
		{
			printf("No data to encode");
			break;
		}

		spendedTimeForMain = (int64_t)timerForMain.elapsed();
		if(spendedTimeForMain < desiredTimeForMain)
			boost::this_thread::sleep(boost::posix_time::milliseconds(desiredTimeForMain - spendedTimeForMain));
	}
}

// Application entry point.
int main(int argc, char* argv[])
{
	// Initialize default settings.
	audio_capture_device = -1;
	audio_sample_rate = 44100;
	container = "flv";
	flag_disable_audio = false;
	flag_disable_video = false;
	flag_generate_audio = false;
	flag_generate_video = false;
	flag_lsd = false;
	server = "";
	stream_bitrate = 1048576;
	username = "";
	video_capture_device = -1;
	video_frame_rate = 15;
	video_height = 720;
	video_width = 1280;

	// Parse command line arguments.
	for (int i = 1; i < argc; i++)
	{
		std::string arg = string(argv[i]);
		int pos = arg.find("=");
		if (std::string::npos != pos)
		{
			string key = arg.substr(0, pos);
			string value = arg.substr(pos + 1, arg.length() - pos - 1);

			try
			{
				if (key == "--audio-capture-device")
					audio_capture_device = boost::lexical_cast<int>(value);
				if (key == "--audio-sample-rate")
					audio_sample_rate = boost::lexical_cast<int>(value);
				if (key == "--container")
					container = value;
				if (key == "--server")
					server = value;
				if (key == "--stream-bitrate")
					stream_bitrate = boost::lexical_cast<int>(value);
				if (key == "--username")
					username = value;
				if (key == "--video-capture-device")
					video_capture_device = boost::lexical_cast<int>(value);
				if (key == "--video-frame-rate")
					video_frame_rate = boost::lexical_cast<int>(value);
				if (key == "--video-height")
					video_height = boost::lexical_cast<int>(value);
				if (key == "--video-width")
					video_width = boost::lexical_cast<int>(value);
			}
			catch (boost::bad_lexical_cast const&)
			{
				std::cout << "Error while parsing argument '" << arg << "': value is not valid. Argument skipped." << std::endl;
			}
		}
		else
		{
			if (arg == "--disable-audio")
				flag_disable_audio = true;
			if (arg == "--disable-video")
				flag_disable_video = true;
			if (arg == "--generate-audio")
				flag_generate_audio = true;
			if (arg == "--generate-video")
				flag_generate_video = true;
			if (arg == "--lsd")
				flag_lsd = true;
			if (arg == "--robot")
			{
				flag_generate_audio = true;
				flag_generate_video = true;
			}
		}
	}

	// Align video width and height so that each dimension divides by 4.
	video_width -= video_width % 4;
	video_height -= video_height % 4;

	// Ask for the username if one wasn't read from command line arguments.
	if (username.empty())
	{
		std::cout << "Please, select a username: ";
		std::cin >> username;
	}

	// Ask for the server URL if one wasn't read from command line arguments.
	if (server.empty())
	{
		std::cout << "Please, specify the server URL: ";
		std::cin >> server;
	}

	// Initialize the transmitter block.
	transmitter_block = new transmitter();

	// Repeat asking for the username and the server URL until connection is successfully established.
	bool succeed = false;
	while (!succeed)
	{
		try
		{
			// Try to connect to the server.
			transmitter_block->connect(username, server);
			// Connection succeeded.
			succeed = true;
		}
		catch (transmitter::invalid_username_exception&)
		{
			// Server rejected the username. Ask for another username.
			std::cout << "Please, select another username: ";
			std::cin >> username;
		}
		catch (transmitter::server_connection_exception&)
		{
			// Failed to connect to the server. Ask for another server URL.
			std::cout << "Please, specify another server URL: ";
			std::cin >> server;
		}
	}

	init_opencv();
	init_openal(video_frame_rate);
	init_ffmpeg(container, video_width, video_height, video_frame_rate);

	if (!has_audio && !has_video)
	{
		std::cout << "No input devices selected. Closing application..." << std::endl;
		return 0;
	}

	multiplexer_block = new multiplexer(container);
	AVFormatContext* format_context = multiplexer_block->get_format_context();

	if (has_audio)
	{
		audio_encoder_block = new audio_encoder(audio_sample_rate);
		audio_encoder_block->connect(multiplexer_block);
	}
	
	if (has_video)
	{
		video_encoder_block = new video_encoder(stream_bitrate, video_frame_rate, video_width, video_height);
		video_encoder_block->connect(multiplexer_block);
	}
	
	if (av_set_parameters(format_context, NULL) < 0)
		throw std::runtime_error("av_set_parameters failed.");

	multiplexer_block->connect(transmitter_block);

	desiredTimeForCaptureFame = (int64_t)(1000.0f / video_frame_rate);
	desiredTimeForMain = (int64_t)(1000.0f / video_frame_rate);

	boost::thread workerThread(capture_frame_loop);
	boost::this_thread::sleep(boost::posix_time::milliseconds(200));
	boost::thread workerThread2(save_frame_loop);

	std::cout << "Type 'exit' and hit enter to stop broadcasting and close the application..." << std::endl;
	std::string exit;
	do
	{
		cin >> exit;
		boost::this_thread::sleep(boost::posix_time::milliseconds(250));
	} while (exit != "exit");

	workerThread2.interrupt();
	workerThread.interrupt();
	boost::this_thread::sleep(boost::posix_time::milliseconds(250));

	release_opencv();
	release_openal();
	release_ffmpeg();

	if (has_audio)
	{
		audio_encoder_block->disconnect();
		delete audio_encoder_block;
	}

	if (has_video)
	{
		video_encoder_block->disconnect();
		delete video_encoder_block;
	}

	multiplexer_block->disconnect();
	delete multiplexer_block;

	transmitter_block->disconnect();
	delete transmitter_block;

	return 0;
}
