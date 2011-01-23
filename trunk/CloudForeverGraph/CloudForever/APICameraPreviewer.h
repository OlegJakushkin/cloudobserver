#ifndef _APICameraPreviewer_h_
#define _APICameraPreviewer_h_

#include <iostream>

#include <cv.h>
#include <cxcore.h>
#include <highgui.h>

using namespace std;

class APICameraPreviewer
{
public:
	int key;

	void createPreviewWindow(string WindowName, int width, int height)
	{
		key = 0;
		windowName = WindowName;
	//cvNamedWindow(windowName.c_str(), CV_WINDOW_AUTOSIZE);
	destination = cvCreateImage(cvSize(width, height),IPL_DEPTH_8U, 3);  //��������� �������� ������ ��� �����
	}

	void OpenCVShowFrame(char * frame)
	{
//		IplImage* destination = cvCreateImage(cvSize(320, 240),IPL_DEPTH_8U, 3); 
		destination->imageData = (char *)frame;
		//IplImage* destination2 = cvCreateImage(cvSize(320, 240), destination->depth, destination->nChannels); 
		//cvResize(destination, destination2);
		//cvSaveImage("test.jpg" ,destination);
		
		cvShowImage(windowName.c_str(), destination);
		delete[] frame;	
		

		//destination->imageData = (char*)frame; // �������� ����������� ������ ����������������� ������ // ������ ����� ������� �����?
	//	IplImage* destination2 = cvCreateImage(cvSize(width, width), destination->depth, destination->nChannels);  // ������ �� ����������� ����
	//	cvResize(destination, destination2);
	//	cvSaveImage("test.jpg" ,destination);// ���� ������ ������ ���� ���� ��������� ��������...
		//cvReleaseImage(&destination);
	//	cvShowImage(windowName.c_str(), destination);
		// ������ ���� ������� �����? // � ����� � ���� � ���� ����...
		key = cvWaitKey(1);
	}

	void CleanUp()
	{
	//cvReleaseImage(&destination);
	cvDestroyWindow(windowName.c_str());
	cvReleaseImage(&destination);
	}
	
private:
	string windowName;
	IplImage* destination;
	
};

#endif // _APICameraPreviewer_h_