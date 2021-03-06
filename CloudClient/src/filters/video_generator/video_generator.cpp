#include "video_generator.h"

using namespace ffmpeg;

video_generator::video_generator(int video_width, int video_height, int video_frame_rate, std::string username)
{
	this->width = video_width;
	this->height = video_height;
	this->frame_rate = video_frame_rate;

	cvInitFont(&font, CV_FONT_HERSHEY_DUPLEX, 2, 1, 0.0, 3, CV_AA);

	this->base_frame = cvCreateImage(cvSize(this->width, this->height), 8, 3);
	cvSet(this->base_frame, CV_RGB(0, 254, 53));
	cvPutText(this->base_frame, username.c_str(), cvPoint(0, this->height - 10), &font, CV_RGB(1, 1, 1));

	this->current_frame = cvCreateImage(cvSize(this->width, this->height), 8, 3);
	this->frame = avcodec_alloc_frame();
	avpicture_fill((AVPicture*)frame, (uint8_t*)this->current_frame->imageData, PIX_FMT_RGB24, this->width, this->height);

	this->start_time = boost::date_time::second_clock<boost::posix_time::ptime>::local_time();

	this->video_encoder_block = NULL;
}

video_generator::~video_generator()
{
	av_free(this->frame);
}

void video_generator::connect(video_encoder* video_encoder_block)
{
	this->video_encoder_block = video_encoder_block;
}

void video_generator::disconnect()
{
	this->video_encoder_block = NULL;
}

void video_generator::send()
{
	cvCopy(this->base_frame, this->current_frame);
	boost::posix_time::ptime now = boost::date_time::second_clock<boost::posix_time::ptime>::local_time();
	cvPutText(this->current_frame, boost::posix_time::to_simple_string(now.time_of_day()).c_str(),
		cvPoint(0, this->height / 2 + 10), &font, CV_RGB(1, 1, 1));
	cvPutText(this->current_frame, ("Uptime: " + boost::posix_time::to_simple_string(now - start_time)).c_str(),
		cvPoint(2 * this->width / 3, this->height - 10), &font, CV_RGB(1, 1, 1));

	this->video_encoder_block->send(this->frame);
}
