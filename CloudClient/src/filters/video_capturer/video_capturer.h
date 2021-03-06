#ifndef VIDEO_CAPTURER_H
#define VIDEO_CAPTURER_H

// Boost
#include <boost/asio.hpp>
#include <boost/date_time.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/thread.hpp>

// OpenCV
#include <opencv2/opencv.hpp>

#include "../line_segment_detector/line_segment_detector.h"
#include "../video_player/video_player.h"
#include "../video_encoder/video_encoder.h"

#include "../../3rdparty/ffmpeg/ffmpeg.h"

#include "../../utils/timer/timer.h"

#include <exception>
#include <iostream>
#include <string>
#include <vector>

#ifdef WIN
#pragma comment(lib, "strmiids")
#include <comdef.h>
#include <dshow.h>
#include <windows.h>
#endif

#ifdef LIN
#include <fcntl.h>
#include <unistd.h>
#include <sys/ioctl.h>
#include <linux/videodev2.h>
#endif

class video_capturer
{
public:
	video_capturer(int video_width, int video_height, int video_frame_rate);
	~video_capturer();
	void connect(line_segment_detector* line_segment_detector_block);
	void connect(video_player* video_player_block);
	void connect(video_encoder* video_encoder_block);
	void disconnect();
	void send();
	void start();
	void stop();
	void set_capture_device(int capture_device_index);

	static std::vector<std::string> get_capture_devices();

	class internal_exception: public std::exception { };
private:
	void capture_loop();
	int width;
	int height;
	int frame_rate;

	IplImage* captured_frame;
	IplImage* resized_frame;
	IplImage* ready_resized_frame;

	ffmpeg::AVFrame* frame;
	ffmpeg::AVFrame* ready_frame;

	CvCapture* capture_device;
	boost::shared_ptr<boost::thread> capture_thread;

	line_segment_detector* line_segment_detector_block;
	video_player* video_player_block;
	video_encoder* video_encoder_block;
};

#endif // VIDEO_CAPTURER_H
