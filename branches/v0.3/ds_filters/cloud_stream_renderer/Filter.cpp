#include "stdafx.h"
#include "guids.h"
#include "Filter.h"
#include "dll.h"
#include <winsock.h>

CFilter::CFilter(TCHAR *tszName, LPUNKNOWN punk, HRESULT *phr) :
    CBaseRenderer(CLSID_Filter, tszName, punk, phr),
		m_InputPin(NAME("Input Pin"),this,&m_InterfaceLock,phr,L"Input")
{
	// Class Constructor makes all routines for 
	// preparation to samples sendings via sockets

	//m_serverAddress = LPOLESTR("127.0.0.1");
	//m_portNumber= 8001;
	SetAddress(LPCOLESTR("127.0.0.1"), 8001);
	//init socket lib
	WORD wVersionRequested;	
	WSADATA wsaData;
	wVersionRequested = MAKEWORD(2, 2);
	WSAStartup(wVersionRequested, &wsaData);
}

CFilter::~CFilter()
{
	Disconnect();
	WSACleanup ();
}

HRESULT CFilter::OnStartStreaming()
{
	Connect(m_serverAddress, m_portNumber);

	return NOERROR;
}
HRESULT CFilter::OnStopStreaming()
{
	Disconnect();

    return NOERROR;
}

CUnknown * WINAPI CFilter::CreateInstance(LPUNKNOWN punk, HRESULT *phr)
{
	ASSERT(phr); 


	CFilter *pNewObject = new CFilter(NAME(FILTER_TCHAR_NAME), punk, phr);
    if (pNewObject == NULL) 
	{
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
	if (NULL == m_socket)
	{
		//drop  this frame (we haven't conneted yet)
		return NOERROR;
	}
	BYTE * buff;
	pMediaSample->GetPointer(&buff);

	if (NULL == buff)
	{
		return NOERROR;
	}

	if(SOCKET_ERROR == send (m_socket,(const char *) buff, pMediaSample->GetActualDataLength(), 0 ))
	{
		Disconnect();
		return ErrorInfo();
	}
	
	return NOERROR;
}

int CFilter::ErrorInfo()
{
	return -1;
}

int CFilter::Connect(LPCOLESTR addr, int port)
{
	char blin[100];

	CharToOem(m_serverAddress, blin);

	struct sockaddr_in socketaddr;
	memset((char *)&socketaddr, NULL, sizeof(socketaddr));
	socketaddr.sin_family = AF_INET;
	socketaddr.sin_addr.s_addr = inet_addr((const char *)blin);
	socketaddr.sin_port = htons(m_portNumber);
	m_socket = socket(AF_INET,SOCK_STREAM, IPPROTO_TCP);

	if (m_socket == INVALID_SOCKET)
	{
		m_socket= NULL;
		return ErrorInfo();
	}
	
	if (connect(m_socket,(LPSOCKADDR)&socketaddr, sizeof(socketaddr)))
	{
		Disconnect();
		return ErrorInfo();
	}

	return 0;
}

void CFilter::Disconnect()
{
	if (NULL != m_socket) {
		closesocket (m_socket);
		m_socket = NULL; 
	}	
}

STDMETHODIMP CFilter::SetAddress( LPCOLESTR pszAddress, int port) 
{
	CheckPointer(pszAddress,E_POINTER);
    if(wcslen(pszAddress) > MAX_PATH)
        return ERROR_FILENAME_EXCED_RANGE;

    // Take a copy of the filename

    size_t len = 1+lstrlenW(pszAddress);
    m_serverAddress = new WCHAR[len];
    if (m_serverAddress == 0)
        return E_OUTOFMEMORY;

    HRESULT hr = StringCchCopyW(m_serverAddress, len, pszAddress);

	m_portNumber = port;
	return S_OK;
}

STDMETHODIMP CFilter::GetAddress(/* [out] */ LPOLESTR *pszAddress, int *port)
{
	CheckPointer(pszAddress, E_POINTER);
    *pszAddress = NULL;

    if (m_serverAddress != NULL) 
    {
        size_t len = 1+lstrlenW(m_serverAddress);
        *pszAddress = (LPOLESTR)QzTaskMemAlloc(sizeof(WCHAR) * (len));

        if (*pszAddress != NULL) 
        {
            HRESULT hr = StringCchCopyW(*pszAddress, len, m_serverAddress);
        }
    }

	*port = m_portNumber;

    return S_OK;
}

//
// NonDelegatingQueryInterface
//
// Override this to say what interfaces we support where
//
STDMETHODIMP CFilter::NonDelegatingQueryInterface(REFIID riid, void ** ppv)
{
    CheckPointer(ppv,E_POINTER);
 //   CAutoLock lock(&m_Lock);

    // Do we have this interface


    if (riid == IID_ICloudInetControl ) {
        return GetInterface((ICloudInetControl *) this, ppv);
    }
	if (riid == IID_IBaseFilter)
	{
		return GetInterface((IBaseFilter *) this, ppv);
	}

	if (riid == IID_IMediaFilter) 
	{
		return GetInterface((IMediaFilter *) this, ppv);
	}
	/*we must return the same way || riid == IID_IMediaFilter || riid == IID_IMediaPosition */
	/*if (riid == IID_IBaseFilter)
	{
		return GetInterface((IBaseFilter *) this, ppv);
	}
	*/
	/* return all not support interfaces */
	return CBaseRenderer::NonDelegatingQueryInterface(riid,ppv);
}