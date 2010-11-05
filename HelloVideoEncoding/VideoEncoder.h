/*
  FFmpeg simple Encoder
 */


#ifndef __VIDEO_ENCODER_H__
#define __VIDEO_ENCODER_H__
#include "stdafx.h"
#include "ffmpegInclude.h"
#include <Windows.h>
#include <string>

class VideoEncoder
{
  private:

  // output file name
  std::string     outputFilename;
  // output format.
  AVOutputFormat  *pOutFormat;
  // format context
  AVFormatContext *pFormatContext;
  // video stream context
  AVStream * pVideoStream;
  // audio streams context
  AVStream * pAudioStream;
  // convert context context
  struct SwsContext *pImgConvertCtx;
  // encode buffer and size
  uint8_t * pVideoEncodeBuffer;
  int nSizeVideoEncodeBuffer;

  // audio buffer and size
  uint8_t * pAudioEncodeBuffer;
  int nSizeAudioEncodeBuffer;
	

  // count of sample
  int audioInputSampleSize;
  // current picture
  AVFrame *pCurrentPicture;

  // audio buffer
  char* audioBuffer;
  int   nAudioBufferSize;
  int   nAudioBufferSizeCurrent;

  public:
  int fps;
  VideoEncoder() 
  {
    pOutFormat = NULL;
    pFormatContext = NULL;
    pVideoStream = NULL;
    pImgConvertCtx = NULL;
    pCurrentPicture = NULL;
    pVideoEncodeBuffer = NULL;
    nSizeVideoEncodeBuffer = 0;
    pAudioEncodeBuffer = NULL;
    nSizeAudioEncodeBuffer = 0;
    nAudioBufferSize = 1024 * 1024 * 4;
    audioBuffer      = new char[nAudioBufferSize];
    nAudioBufferSizeCurrent = 0;
	fps = 7;
  }
  
  virtual ~VideoEncoder() 
  {
    Finish();
  }
  //set fps
  void SetFps(int UserFps);
  // init output file
  bool InitFile(std::string& inputFile, std::string& container);
  // Add video and audio data
  bool AddFrame(AVFrame* frame, const char* soundBuffer, int soundBufferSize);
  // end of output
  bool Finish();

  private: 
  
  // Add video stream
  AVStream *AddVideoStream(AVFormatContext *pContext, CodecID codec_id);
  // Open Video Stream
  bool OpenVideo(AVFormatContext *oc, AVStream *pStream);
  // Allocate memory
  AVFrame * CreateFFmpegPicture(int pix_fmt, int nWidth, int nHeight);
  // Close video stream
  void CloseVideo(AVFormatContext *pContext, AVStream *pStream);
  // Add audio stream
  AVStream * AddAudioStream(AVFormatContext *pContext, CodecID codec_id);
  // Open audio stream
  bool OpenAudio(AVFormatContext *pContext, AVStream *pStream);
  // close audio stream
  void CloseAudio(AVFormatContext *pContext, AVStream *pStream);
  // Add video frame
  bool AddVideoFrame(AVFrame * frame, AVCodecContext *pVideoCodec);
  // Add audio samples
  bool AddAudioSample(AVFormatContext *pFormatContext, 
    AVStream *pStream, const char* soundBuffer, int soundBufferSize);
  // Free resourses.
  void Free();
  bool NeedConvert();
};

#endif // __VIDEO_ENCODER_H__
