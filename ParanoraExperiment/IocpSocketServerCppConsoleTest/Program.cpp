#include "stdafx.h"  
#include <winsock2.h>  
#include <mswsock.h>  
#include <windows.h>  
#include <iostream> 
#include <process.h>
#include <atlstr.h>
#pragma comment(lib,"ws2_32.lib") 
#define DATA_BUFSIZE	8192

enum IO_OPERATION
{
	IO_READ,IO_WRITE
};

typedef struct
{
	OVERLAPPED Overlapped;
	WSABUF DataBuf;
	CHAR Buffer[DATA_BUFSIZE];
	DWORD BytesSend;
	DWORD BytesRecv;
	DWORD TotalSendBytes;
	IO_OPERATION OperationType;
	SOCKET ActiveSocket;
}PER_IO_OPERATION_DATA, *LPPER_IO_OPERATION_DATA;

typedef struct
{
	SOCKET Socket;
}PER_HANDLE_DATA,*LPPER_HANDLE_DATA;

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

int IsHttpRequest(char * requestHeadString)
{
	int isHttpRequest=0;
	int index=StrIndexOf(requestHeadString,"HTTP/1.1",0);
	if(index>-1)
	{
		char * str=StrSubstr(requestHeadString,0,index-8);
		index=StrIndexOf(str,"GET",0);
		if(index>-1)
		{
			isHttpRequest=1;
		}
		if(isHttpRequest<1)
		{
			index=StrIndexOf(str,"GET",0);
			if(index>-1)
			{
				isHttpRequest=1;
			}
		}
	}

	return isHttpRequest;
}

unsigned __stdcall IocpProc(LPVOID ComlpetionPortID)
{
	HANDLE ComplectionPort = (HANDLE)ComlpetionPortID;
	DWORD BytesTransferred;
	LPPER_HANDLE_DATA PerHandleData;
	LPPER_IO_OPERATION_DATA PerIOData; 
	DWORD dwRecvNumBytes = 0;  
    DWORD dwSendNumBytes = 0;  
    DWORD dwFlags = 0;  
	int iRev=0;
	WSABUF buffSend;
	while (1)
	{
		if (GetQueuedCompletionStatus(ComplectionPort,
			&BytesTransferred,
			(LPDWORD)&PerHandleData,
			(LPOVERLAPPED*)&PerIOData,INFINITE) == 0)
		{
			printf("GetQueuedCompletionStatus failed with error%d\r\n",GetLastError());
			break;
		}

		if (BytesTransferred == 0)
		{
			printf("Closing Socket %d\r\n",PerHandleData->Socket);
			if (closesocket(PerHandleData->Socket) == SOCKET_ERROR)
			{
				printf("closesocket failed with error %d\r\n",WSAGetLastError());

			}
			delete PerIOData; 
			continue;
		}

		if(PerIOData->OperationType==IO_READ)
		{
			std::cout<<"From Socket"<<PerIOData->ActiveSocket<<" Message:"<< PerIOData->Buffer<<std::endl;

			
			PerIOData->DataBuf.len=BytesTransferred;
			PerIOData->TotalSendBytes = BytesTransferred;
			PerIOData->BytesSend = 0;  
			PerIOData->OperationType=IO_WRITE;
            dwFlags = 0;  
			iRev = WSASend(PerIOData->ActiveSocket,
				&PerIOData->DataBuf,
				1,
				&dwSendNumBytes,  
                dwFlags,  
                &(PerIOData->Overlapped),
				NULL);	//
            if( iRev == SOCKET_ERROR && (ERROR_IO_PENDING != WSAGetLastError()) ) {  
                    std::cout << "WASSend Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
                    closesocket(PerIOData->ActiveSocket);  
                    delete PerIOData;  
                    continue;  
            }
		}
		else if(PerIOData->OperationType==IO_WRITE)
		{
			dwFlags = 0;  
			PerIOData->BytesSend+=BytesTransferred;
			if(PerIOData->BytesSend<PerIOData->TotalSendBytes)
			{
				PerIOData->OperationType=IO_WRITE;
				buffSend.buf = PerIOData->DataBuf.buf + PerIOData->BytesSend;
				buffSend.len = PerIOData->TotalSendBytes - PerIOData->BytesSend;
				iRev = WSASend(PerIOData->ActiveSocket,
					&buffSend,
					1,
					&dwSendNumBytes,  
					dwFlags,  
					&(PerIOData->Overlapped),
					NULL);
				if( iRev == SOCKET_ERROR && (ERROR_IO_PENDING != WSAGetLastError()) ) {  
						std::cout << "WASSend Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
						closesocket(PerIOData->ActiveSocket);  
						delete PerIOData;  
						continue;  
				}
			}
			else
			{
				ZeroMemory(&(PerIOData->Overlapped),sizeof(OVERLAPPED));
				ZeroMemory(PerIOData->DataBuf.buf,DATA_BUFSIZE);
				PerIOData->DataBuf.len = DATA_BUFSIZE; 
				PerIOData->BytesRecv=0;
				PerIOData->BytesSend=0;
				PerIOData->TotalSendBytes=0;
				PerIOData->OperationType=IO_READ;
				dwRecvNumBytes = 0;  
				dwFlags = 0;
		
				iRev=WSARecv(PerHandleData->Socket,
					&(PerIOData->DataBuf),
					1,
					&dwRecvNumBytes,
					&dwFlags,
					&(PerIOData->Overlapped),
					NULL);

				if( iRev == SOCKET_ERROR && (ERROR_IO_PENDING != WSAGetLastError()) ) 
				{  
					std::cout << "WASRecv Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
					closesocket(PerIOData->ActiveSocket);  
					delete PerIOData; 
					continue;  
				}
			}
		}
	}
	return 0; 
}

//SOCKADDR_IN GetHttpRequestAddr(char * requestHead)
//{
//	sockaddr_in service;  
//	service.sin_family = AF_INET;
//
//}

int GetNumberOfProcessors()
{
	int num=1;
	SYSTEM_INFO sysInfo;  
    ZeroMemory(&sysInfo,sizeof(SYSTEM_INFO));  
    GetSystemInfo(&sysInfo);  
    num = sysInfo.dwNumberOfProcessors;
	return num;
}

int _tmain (int argc, _TCHAR * argv[])  
{
	int g_ThreadCount;  
	HANDLE g_hIOCP = INVALID_HANDLE_VALUE;  
	SOCKET g_ServerSocket = INVALID_SOCKET; 
     // Init winsock2  
    WSADATA wsaData;  
    ZeroMemory(&wsaData,sizeof(WSADATA));  
    int retVal = -1;  
    if( (retVal = WSAStartup(MAKEWORD(2,2), &wsaData)) != 0 ) {  
        std::cout << "WSAStartup Failed::Reason Code::"<< retVal << std::endl;  
        return 0;  
    }

    //Create socket  
    g_ServerSocket = WSASocket(AF_INET,SOCK_STREAM, IPPROTO_TCP, NULL,0,WSA_FLAG_OVERLAPPED);  
    if( g_ServerSocket == INVALID_SOCKET ) {  
        std::cout << "Server Socket Creation Failed::Reason Code::" << WSAGetLastError() << std::endl;  
        return 0;  
    }
	
    //bind  
    sockaddr_in service;  
    service.sin_family = AF_INET;  
    service.sin_addr.s_addr = htonl(INADDR_ANY);
    service.sin_port = htons(8899);  
    retVal = bind(g_ServerSocket,(SOCKADDR *)&service,sizeof(service));  
    if( retVal == SOCKET_ERROR ) {  
        std::cout << "Server Soket Bind Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
        return 0;  
    }
	
    //listen  
    retVal = listen(g_ServerSocket,SOMAXCONN);  
    if( retVal == SOCKET_ERROR ) {  
        std::cout << "Server Socket Listen Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
        return 0;  
    }

    g_ThreadCount = GetNumberOfProcessors()*2+2;
    g_hIOCP = CreateIoCompletionPort(INVALID_HANDLE_VALUE,NULL,0,g_ThreadCount);  
    if (g_hIOCP == NULL) {  
        std::cout << "CreateIoCompletionPort() Failed::Reason::"<< GetLastError() << std::endl;  
        return 0;              
    } 

    if (CreateIoCompletionPort((HANDLE)g_ServerSocket,g_hIOCP,0,0) == NULL){  
		std::cout << "Binding Server Socket to IO Completion Port Failed::Reason Code::"<< GetLastError() << std::endl;  
		return 0;
    }

    for( DWORD dwThread=0; dwThread < g_ThreadCount; dwThread++ )  
    {  
        HANDLE  hThread;
		unsigned   dwThreadId;
        hThread = (HANDLE)_beginthreadex(NULL, 0, IocpProc, g_hIOCP, 0, &dwThreadId);
		if(hThread==NULL)
		{
			printf("CreateThread failed with error %d\r\n" ,GetLastError());
			return 0;
		}
        CloseHandle(hThread);
	}

	while(1)
	{
		SOCKADDR_IN accpetSocketAddrIn;
		SOCKET  acceptSocket=WSAAccept(g_ServerSocket,(sockaddr*)&accpetSocketAddrIn,NULL,NULL,0);
		if (acceptSocket == SOCKET_ERROR)
		{
			printf("WSAAccept failed with error %d\r\n",WSAGetLastError());
			return 0;
		}
		printf("Socket number %d connected\rn",acceptSocket);

		LPPER_HANDLE_DATA PerHandleData=new PER_HANDLE_DATA;
		PerHandleData->Socket=acceptSocket;

		if ((CreateIoCompletionPort((HANDLE)acceptSocket,g_hIOCP,(DWORD)PerHandleData,0)) == NULL)
		{
			printf("CreateIoCompletionPort failed with error%d\rn",GetLastError());
			return 0;
		}

		LPPER_IO_OPERATION_DATA PerIOData=new PER_IO_OPERATION_DATA;
		ZeroMemory(&(PerIOData->Overlapped),sizeof(OVERLAPPED));
		ZeroMemory(PerIOData->Buffer,sizeof(PerIOData->Buffer));
		PerIOData->BytesRecv = 0;
		PerIOData->BytesSend = 0;
		PerIOData->TotalSendBytes=0;
		PerIOData->DataBuf.len = DATA_BUFSIZE;
		PerIOData->DataBuf.buf = PerIOData->Buffer;
		PerIOData->ActiveSocket=acceptSocket;
		PerIOData->OperationType=IO_READ;

		DWORD dwRecvNumBytes=0;
		DWORD dwFlags=0;  

		int iRev=WSARecv(acceptSocket,
			&(PerIOData->DataBuf),
			1,
			&dwRecvNumBytes,
			&dwFlags,
			&(PerIOData->Overlapped),
			NULL);

		if (iRev == SOCKET_ERROR)
		{
			if (WSAGetLastError() != ERROR_IO_PENDING)
			{
				printf("WSARecv() failed with error %d\r\n",WSAGetLastError());
				closesocket(acceptSocket);
				delete PerIOData;
				delete PerHandleData;
				return 0;
			}
		}
	}

    closesocket(g_ServerSocket);  
    WSACleanup();

	return 0;
}