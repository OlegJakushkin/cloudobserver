#include "audio_capturer.h"

audio_capturer::audio_capturer(int audio_sample_rate, int audio_format, int capture_size)
{
#if defined(WIN) && defined(AL_LIBTYPE_STATIC)
	alcInit();
#endif

	this->sample_rate = (ALCuint)audio_sample_rate;
	this->format = (ALCenum)audio_format;
	this->capture_size = capture_size;

	switch (this->format)
	{
	case AL_FORMAT_MONO8:
		this->format_multiplier = 1;
		break;
	case AL_FORMAT_MONO16:
	case AL_FORMAT_STEREO8:
		this->format_multiplier = 2;
		break;
	case AL_FORMAT_STEREO16:
		this->format_multiplier = 4;
		break;
	}
	this->buffer = new ALchar[this->sample_rate * this->format_multiplier];

	this->capture_device = alcCaptureOpenDevice(NULL, this->sample_rate, this->format, this->sample_rate);
	alcCaptureStart(this->capture_device);

	this->audio_player_block = NULL;
	this->audio_encoder_block = NULL;
}

audio_capturer::~audio_capturer()
{
	alcCaptureStop(this->capture_device);

#if defined(WIN) && defined(AL_LIBTYPE_STATIC)
	alcRelease();
#endif
}

void audio_capturer::connect(audio_player* audio_player_block)
{
	this->audio_player_block = audio_player_block;
}

void audio_capturer::connect(audio_encoder* audio_encoder_block)
{
	this->audio_encoder_block = audio_encoder_block;
}

void audio_capturer::disconnect()
{
	this->audio_player_block = NULL;
	this->audio_encoder_block = NULL;
}

void audio_capturer::send()
{
	int captured_samples;
	do
	{
		alcGetIntegerv(this->capture_device, ALC_CAPTURE_SAMPLES, 1, &captured_samples);
	} while (captured_samples < this->capture_size);

	alcCaptureSamples(this->capture_device, this->buffer, this->capture_size);

	if (this->audio_player_block != NULL)
		this->audio_player_block->send(this->buffer, this->capture_size * this->format_multiplier);

	if (this->audio_encoder_block != NULL)
		this->audio_encoder_block->send(this->buffer, this->capture_size * this->format_multiplier);
}

void audio_capturer::set_capture_device(int capture_device_index)
{
	std::vector<std::string> capture_devices = get_capture_devices();

	alcCaptureStop(this->capture_device);
	alcCaptureCloseDevice(this->capture_device);

	if (capture_device_index == 0)
		this->capture_device = alcCaptureOpenDevice(NULL, this->sample_rate, this->format, this->sample_rate);
	else
		this->capture_device = alcCaptureOpenDevice((ALCchar*)capture_devices[capture_device_index - 1].c_str(), this->sample_rate, this->format, this->sample_rate);
	alcCaptureStart(this->capture_device);
}

void audio_capturer::set_capture_size(int capture_size)
{
	this->capture_size = capture_size;

	// clear the internal buffer
	int captured_samples;
	alcGetIntegerv(this->capture_device, ALC_CAPTURE_SAMPLES, 1, &captured_samples);
	alcCaptureSamples(this->capture_device, this->buffer, captured_samples);
}

std::vector<std::string> audio_capturer::get_capture_devices()
{
#if defined(WIN) && defined(AL_LIBTYPE_STATIC)
	alcInit();
#endif

	const ALCchar* devices = alcGetString(NULL, ALC_CAPTURE_DEVICE_SPECIFIER);
	if (devices == NULL)
	{
		std::cout << "Audio capturer: cannot enumerate audio capture devices." << std::endl;
		throw internal_exception();
	}

	if (*devices == 0)
	{
		std::cout << "Audio capturer: no audio capture devices found." << std::endl;
		throw internal_exception();
	}

	std::vector<std::string> capture_devices;
	while (*devices)
	{
		capture_devices.push_back(std::string(devices));
		devices += strlen(devices) + 1;
	}

#if defined(WIN) && defined(AL_LIBTYPE_STATIC)
	alcRelease();
#endif

	return capture_devices;
}

void audio_capturer::set_synchronization_callback(boost::function<void ()> synchronization_callback)
{
	this->synchronization_callback = synchronization_callback;
}

void audio_capturer::set_synchronization_period(int synchronization_period) { }

void audio_capturer::start()
{
	this->synchronization_thread = boost::shared_ptr<boost::thread>(new boost::thread(&audio_capturer::synchronization_loop, this));
}

void audio_capturer::stop()
{
	this->synchronization_thread->interrupt();
}

void audio_capturer::synchronization_loop()
{
	int captured_samples;
	while (true)
	{
		do
		{
			alcGetIntegerv(this->capture_device, ALC_CAPTURE_SAMPLES, 1, &captured_samples);
		} while (captured_samples < this->capture_size);
		this->synchronization_callback();
	}
}
