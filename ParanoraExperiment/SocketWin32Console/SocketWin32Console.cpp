// SocketWin32Console.cpp : 定义控制台应用程序的入口点。
//
#include "stdafx.h"
#include <stdio.h>
#include <winsock2.h>  
#include <process.h>
#include <iostream>
#include <string.h>
#include <fstream>
#include <string>
#include <regex>

#pragma comment(lib,"ws2_32.lib")
using namespace std;

CRITICAL_SECTION gCriticalSection;

unsigned __stdcall ThreadProc(LPVOID param)
{
	std::cout<<"ThreadProc exit"<<std::endl;
	//Sleep(10000);
	
	_endthreadex(0);
	return 0;
}

int getHostInfo()
{
	WSADATA wsaData;  
	ZeroMemory(&wsaData,sizeof(WSADATA));  
	int retVal = -1;  
	if( (retVal = WSAStartup(MAKEWORD(2,2), &wsaData)) != 0 ) {  
		std::cout << "WSAStartup Failed::Reason Code::"<< retVal << std::endl;  
		return 0;  
	}

	char * hostName="sh.centanet.com";
	/*int iRev=0;
	iRev=gethostname(hostName,128);
	if(iRev==0)
	{

	}*/
	HOSTENT * host;

	host=gethostbyname(hostName);
	printf("%s\n", hostName);
	if(host!=0)
	{
		sockaddr_in service;  
		service.sin_family = AF_INET;  
		//service.sin_addr.s_addr = htonl(INADDR_ANY);
		service.sin_port = htons(8899);  

		memcpy(&(service.sin_addr),host->h_addr,host->h_length);

		
		printf("解析IP地址: ");
		printf("%d.%d.%d.%d\r\n",
			(host->h_addr_list[0][0]&0x00ff),
			(host->h_addr_list[0][1]&0x00ff),
			(host->h_addr_list[0][2]&0x00ff),
			(host->h_addr_list[0][3]&0x00ff));

	}
	
	WSACleanup();

	return 0;
}
int StrIndexOf(char * mainStr,char * subStr,int pos)
{
	int index=-1;
	int i = pos, j = 0;
	int mainStrLen=strlen(mainStr);
	int subStrLen=strlen(subStr);
	while (i <mainStrLen && j < subStrLen)
	{
		if (mainStr[i] == subStr[j])
		{
			i++;
			j++;
		}
		else
		{
			i = i - j + 1;
			j = 0;
		}
	}
	if (j == subStrLen) 
	{
		index=i -subStrLen;
	}
	return index;
}

char * StrSubstr(char * mainStr,int startIndex,int length)
{
	char * reStr=(char *)malloc(sizeof(char));
	int mainStrLen=strlen(mainStr);
	if(startIndex+length<mainStrLen)
	{
		reStr=(char *)malloc(sizeof(char)*length);
		strncpy_s(reStr,strlen(reStr),&mainStr[startIndex], length);

	}
	return reStr;
}

wchar_t * ANSIToUnicode(const char * str)
{
	int  len = 0;
	len =strlen(str);
	int  unicodeLen = MultiByteToWideChar(CP_UTF8,
		0,
		str,
		-1,
		NULL,
		0 );  
	wchar_t * pUnicode=(wchar_t *)malloc((unicodeLen+1)*sizeof(wchar_t));  
	MultiByteToWideChar( CP_UTF8,
		0,
		str,
		-1,
		pUnicode,
		unicodeLen ); 

	return  pUnicode;  
}

char * UnicodeToANSI(const wchar_t * str)
{
	char* pElementText;
	int    ansiLen;
	// wide char to multi char
	ansiLen = WideCharToMultiByte( CP_ACP,
		0,
		str,
		-1,
		NULL,
		0,
		NULL,
		NULL );
	char * pAnsi = (char *)malloc(sizeof( char ) * ( ansiLen + 1 ) );
	::WideCharToMultiByte( CP_ACP,
		0,
		str,
		-1,
		pAnsi,
		ansiLen,
		NULL,
		NULL );
	return pAnsi;
}

int SocketSendHttpRequest()
{
	WSADATA wsaData;  
	ZeroMemory(&wsaData,sizeof(WSADATA));  
	int retVal = -1;  
	if( (retVal = WSAStartup(MAKEWORD(2,2), &wsaData)) != 0 ) {  
		std::cout << "WSAStartup Failed::Reason Code::"<< retVal << std::endl;  
		return 0;  
	}
	sockaddr_in service;  
	service.sin_family = AF_INET;  
	service.sin_addr.s_addr = inet_addr("61.172.202.195");
	service.sin_port = htons(80);  

	SOCKET requestSocket = WSASocket(AF_INET,SOCK_STREAM, IPPROTO_TCP, NULL,0,WSA_FLAG_OVERLAPPED);  
	if( requestSocket == INVALID_SOCKET ) {  
		std::cout << "Server Socket Creation Failed::Reason Code::" << WSAGetLastError() << std::endl; 
		WSACleanup();
		return 0;  
	}
	int connRev = connect(requestSocket, (SOCKADDR*)&service, sizeof(service));
	if( SOCKET_ERROR == connRev )
	{
		std::cout<<"Failed to connect."<<WSAGetLastError()<<std::endl;
		closesocket(requestSocket);
		WSACleanup();
		return 0;
	}	
	char *httpRequestHead="GET http://sh.centanet.com/ HTTP/1.1\r\nAccept: text/html, application/xhtml+xml, */*\r\nAccept-Language: zh-CN\r\nUser-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)\r\nAccept-Encoding: gzip, deflate\r\nProxy-Connection: Keep-Alive\r\nHost: sh.centanet.com\r\nPragma: no-cache\r\n\r\n";

	int iRev = send(requestSocket, httpRequestHead, strlen(httpRequestHead), 0);

	if(iRev == SOCKET_ERROR)
	{
		std::cout<<"send failed:"<<WSAGetLastError()<<std::endl;
		closesocket(requestSocket);
		WSACleanup();
		return 0;
	}
	iRev=0;
	char recvBuf[10240]={0};
	iRev = recv(requestSocket, recvBuf, sizeof(recvBuf), 0);
	char * m_saveFileName="c:\\test.txt";
	
	while (true)
	{
		if(iRev > 0)
		{
			wchar_t * wRecvBuf=ANSIToUnicode(recvBuf);
			std::ofstream of;
			of.open("d:\\test.txt");
			of<<recvBuf;
			of.close();
			std::cout<<recvBuf;
			iRev = recv(requestSocket, recvBuf, sizeof(recvBuf), 0);
		}
		else if(iRev == 0)
		{
			break;
		}
		else
		{
			printf("recv failed: %d\n", WSAGetLastError());
			break;
		}
	}
	return 0;
}

int IsHttpRequest(char * requestHeadString,int * requestType)
{
	int isHttpRequest=0;
	int index=StrIndexOf(requestHeadString,"HTTP/1.1",0);
	*requestType=-1;
	if(index>-1)
	{
		char * str=StrSubstr(requestHeadString,0,index);
		index=StrIndexOf(str,"GET",0);
		if(index>-1)
		{
			*requestType=0;
			isHttpRequest=1;
		}
		if(isHttpRequest<1)
		{
			index=StrIndexOf(str,"POST",0);
			if(index>-1)
			{
				*requestType=1;
				isHttpRequest=1;
			}
		}
	}
	return isHttpRequest;
}

int GetHttpRequestAddr(char * requestHeadStr,int requestType)
{
	int state=0;
	regex pattern("Host: (.*)\r\n");

	string str=string(requestHeadStr);
	
	std:: match_results<std::string::const_iterator> result;
	std::smatch m;
	bool valid =  regex_search(str, m, pattern);

	if(valid)
	{
		string s;
		for(int i=0;i<m.size();i++)
		{
			s=m[i];
		}
		state=1;
	}

	return state;
}

int _tmain(int argc, _TCHAR* argv[])
{
	unsigned threadId;
	HANDLE hThread = (HANDLE)_beginthreadex(NULL, 0, &ThreadProc,(void*)ThreadProc, 0, &threadId);
	//WaitForSingleObject(hThread, INFINITE);

	HANDLE mutex=CreateMutex(NULL,FALSE,(LPCWSTR)"My_Mutex_0001");

	HANDLE openMutex=::OpenMutex(MUTEX_ALL_ACCESS,FALSE,(LPCWSTR)"My_Mutex_0001");

	//getHostInfo();
	char *httpRequestHead="GET http://sh.centanet.com/ HTTP/1.1\r\nAccept: text/html, application/xhtml+xml, */*\r\nAccept-Language: zh-CN\r\nUser-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)\r\nAccept-Encoding: gzip, deflate\r\nProxy-Connection: Keep-Alive\r\nHost: sh.centanet.com:6060\r\nPragma: no-cache\r\n\r\n";
	
	int index=StrIndexOf(httpRequestHead,"GET",0);
	int requestType;
	IsHttpRequest(httpRequestHead,&requestType);
	
	GetHttpRequestAddr(httpRequestHead,requestType);

	Sleep(10000);

	return 0;
}