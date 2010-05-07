//#include "StdAfx.h"
//#include "InputPin.h"
#include "Stdafx.h"
#include "guids.h"
#include "RendererWriter.h"
//#include "Filter.h"

CInputPin::CInputPin(TCHAR *pObjectName, CFilter *pRenderer, 
					 CCritSec *pInterfaceLock, HRESULT *phr, LPCWSTR pPinName):
			CRendererInputPin((CBaseRenderer*)pRenderer,phr,pPinName),
				m_pRenderer(pRenderer),
				m_pInterfaceLock(pInterfaceLock)
{
}

EXTERN_C const GUID CLSID_RendererSkelet;

CFilter::CFilter(TCHAR *tszName, LPUNKNOWN punk, HRESULT *phr) :
    CBaseRenderer(CLSID_RendererSkelet, tszName, punk, phr),
		m_InputPin(NAME("Input Pin"),this,&m_InterfaceLock,phr,L"Input")
{
}
CFilter::~CFilter()
{
}

HRESULT CFilter::OnStopStreaming()
{
	CloseHandle(m_hFile);
return NOERROR;
}

HRESULT	CFilter::OnStartStreaming()
{
 //ResetStreamingTimes();

 m_hFile = CreateFile("c:/NewFile.mp3",       // name of the write
                       GENERIC_WRITE,          // open for writing
                       0,                      // do not share
                       NULL,                   // default security
                       CREATE_NEW,             // create new file only
                       FILE_ATTRIBUTE_NORMAL,  // normal file
                       NULL);                  // no attr. template
	if (m_hFile == INVALID_HANDLE_VALUE) 
    { 
	m_hFile = CreateFile("c:/NewFile1.mp3",    // name of the write
                       GENERIC_WRITE,          // open for writing
                       0,                      // do not share
                       NULL,                   // default security
                       CREATE_NEW,             // create new file only
                       FILE_ATTRIBUTE_NORMAL,  // normal file
                       NULL);                  // no attr. template
    }   
 return NOERROR;
}


CUnknown * WINAPI CFilter::CreateInstance(LPUNKNOWN punk, HRESULT *phr)
{
	ASSERT(phr);    
	CFilter *pNewObject = new CFilter(NAME("fv Add Graph To ROT Sample Filter"), punk, phr);
    if (pNewObject == NULL) {
        if (phr)
            *phr = E_OUTOFMEMORY;
    }

    return pNewObject;
}

HRESULT CFilter::CheckMediaType(const CMediaType *pmt)
{
	return NOERROR;
}

HRESULT CFilter::DoRenderSample(IMediaSample *pMediaSample)
{
    DWORD lpCount;
    BYTE *buff;
    pMediaSample->GetPointer(&buff);
    if (m_hFile == NULL)
        return NOERROR;

    if (FALSE == WriteFile(m_hFile, buff, pMediaSample->GetActualDataLength(), &lpCount, NULL))
    {
        return NOERROR;
    }

    return NOERROR;
}
