#include "stdafx.h"  
#include <winsock2.h>  
#include <mswsock.h>  
#include <windows.h>  
#include <iostream> 
#include <process.h>
#pragma comment(lib,"ws2_32.lib")  

 
#define DATA_BUFSIZE	8192

typedef struct
{
	OVERLAPPED OVerlapped;
	WSABUF DATABuf;
	CHAR Buffer[DATA_BUFSIZE];
	DWORD BytesSend,BytesRecv;
}PER_IO_OPERATION_DATA, *LPPER_IO_OPERATION_DATA;

typedef struct
{	SOCKET Socket;
}PER_HANDLE_DATA,*LPPER_HANDLE_DATA;


unsigned __stdcall IocpProc(LPVOID ComlpetionPortID)
{
	HANDLE ComplectionPort = (HANDLE)ComlpetionPortID;
	DWORD BytesTransferred;
	LPOVERLAPPED Overlapped;
	LPPER_HANDLE_DATA PerHandleData;
	LPPER_IO_OPERATION_DATA PerIOData;
	DWORD SendBytes,RecvBytes;
	DWORD Flags; 

	while (TRUE)
	{
		if (GetQueuedCompletionStatus(ComplectionPort,&BytesTransferred,(LPDWORD)&PerHandleData,(LPOVERLAPPED*)&PerIOData,INFINITE) == 0)
		{
			printf("GetQueuedCompletionStatus failed with error%d\r\n",GetLastError());
			return 0;
		}

		//首先检查套接字上是否发生错误，如果发生了则关闭套接字并且清除同套节字相关的SOCKET_INFORATION 结构体
		if (BytesTransferred == 0)
		{
			printf("Closing Socket %d\r\n",PerHandleData->Socket);
			if (closesocket(PerHandleData->Socket) == SOCKET_ERROR)
			{
				printf("closesocket failed with error %d\r\n",WSAGetLastError());
				return 0;
			}
			GlobalFree(PerHandleData);
			GlobalFree(PerIOData);
			continue;
		}

		//检查BytesRecv域是否等于0，如果是，说明WSARecv调用刚刚完成，可以用从己完成的WSARecv调用返回的BytesTransferred值更新BytesRecv域
		if (PerIOData->BytesRecv == 0)
		{
			PerIOData->BytesRecv = BytesTransferred;
			PerIOData->BytesSend = 0;
		}
		else
		{
			PerIOData->BytesRecv +=BytesTransferred;
		}
		
		if (PerIOData->BytesRecv > PerIOData->BytesSend)
		{
			//发布另一个WSASend()请求，因为WSASendi 不能确保发送了请的所有字节，继续WSASend调用直至发送完所有收到的字节
			ZeroMemory(&(PerIOData->OVerlapped),sizeof(OVERLAPPED));
			PerIOData->DATABuf.buf = PerIOData->Buffer + PerIOData->BytesSend;
			PerIOData->DATABuf.len = PerIOData->BytesRecv - PerIOData->BytesSend;     

			if (WSASend(PerHandleData->Socket,&(PerIOData->DATABuf),1,&SendBytes,0,&(PerIOData->OVerlapped),NULL) ==SOCKET_ERROR )
			{
				if (WSAGetLastError() != ERROR_IO_PENDING)
				{
					printf("WSASend() fialed with error %d\r\n",WSAGetLastError());
					return 0;
				}
			}
		}
		else
		{
			PerIOData->BytesRecv = 0;
			//Now that is no more bytes to send post another WSARecv() request
			//现在己经发送完成
			Flags = 0;
			ZeroMemory(&(PerIOData->OVerlapped),sizeof(OVERLAPPED));
			PerIOData->DATABuf.buf = PerIOData->Buffer;
			PerIOData->DATABuf.len = DATA_BUFSIZE;
			if (WSARecv(PerHandleData->Socket,&(PerIOData->DATABuf),1,&RecvBytes,&Flags,&(PerIOData->OVerlapped),NULL) == SOCKET_ERROR)
			{
				if (WSAGetLastError() != ERROR_IO_PENDING)
				{
					printf("WSARecv() failed with error %d\r\n",WSAGetLastError());
					return 0;
				}
			}
		}    
	}
	return 0; 
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
    retVal = listen(g_ServerSocket, 8);  
    if( retVal == SOCKET_ERROR ) {  
        std::cout << "Server Socket Listen Failed::Reason Code::"<< WSAGetLastError() << std::endl;  
        return 0;  
    }

    // Create IOCP  
    SYSTEM_INFO sysInfo;  
    ZeroMemory(&sysInfo,sizeof(SYSTEM_INFO));  
    GetSystemInfo(&sysInfo);  
    g_ThreadCount = sysInfo.dwNumberOfProcessors * 1;
    g_hIOCP = CreateIoCompletionPort(INVALID_HANDLE_VALUE,NULL,0,g_ThreadCount);  
    if (g_hIOCP == NULL) {  
        std::cout << "CreateIoCompletionPort() Failed::Reason::"<< GetLastError() << std::endl;  
        return 0;              
    } 

    if (CreateIoCompletionPort((HANDLE)g_ServerSocket,g_hIOCP,0,0) == NULL){  
		std::cout << "Binding Server Socket to IO Completion Port Failed::Reason Code::"<< GetLastError() << std::endl;  
		return 0;
    }

    //Create worker threads  
    for( DWORD dwThread=0; dwThread < g_ThreadCount; dwThread++ )  
    {  
        HANDLE  hThread;  
        //DWORD   dwThreadId;
		unsigned   dwThreadId;
        hThread = (HANDLE)_beginthreadex(NULL, 0, IocpProc, g_hIOCP, 0, &dwThreadId);
		if(hThread==NULL)
		{
			printf("CreateThread failed with error %d\r\n" ,GetLastError());
			return 0;
		}
        CloseHandle(hThread);
	}


	DWORD Flags;
	DWORD RecvBytes;
	while (TRUE)
	{
		SOCKET  Accept=WSAAccept(g_ServerSocket,NULL,NULL,NULL,0);
		if (Accept == SOCKET_ERROR)
		{
			printf("WSAAccept failed with error %d\r\n",WSAGetLastError());
			return 0;
		}

		////创建与套接字相关的套接字信息结构
		//if ((PerHandleData = (LPPER_HANDLE_DATA)GlobalAlloc(GPTR,sizeof(PER_HANDLE_DATA))) == NULL)
		//{
		//	printf("GlobalAlloc failed with error %d\r\n",GetLastError());
		//	return 0;
		//}
		// Associate the accepted socket with the original completion port.
		printf("Socket number %d connected\rn",Accept);
		//结构中存入接收的套接字

		

		//// 创建同下面的WSARecv调用相关的IO套接字信息结构体
		//if ((PerIOData = (LPPER_IO_OPERATION_DATA)GlobalAlloc(GPTR,sizeof(PER_IO_OPERATION_DATA))) = NULL)
		//{
		//	printf("GlobalAloc failed with error %d\r\n",GetLastError());
		//	return 0;
		//}

		LPPER_HANDLE_DATA PerHandleData=(LPPER_HANDLE_DATA)malloc(sizeof(LPPER_HANDLE_DATA));
		ZeroMemory(&PerHandleData,sizeof(PerHandleData));
		PerHandleData->Socket=Accept;

		if ((CreateIoCompletionPort((HANDLE)Accept,g_hIOCP,(DWORD)PerHandleData,0)) == NULL)
		{
			printf("CreateIoCompletionPort failed with error%d\rn",GetLastError());
			return 0;
		}

		LPPER_IO_OPERATION_DATA PerIOData=(LPPER_IO_OPERATION_DATA)malloc(sizeof(LPPER_IO_OPERATION_DATA));
		ZeroMemory(&PerIOData,sizeof(PerIOData));
		ZeroMemory(&(PerIOData->OVerlapped),sizeof(OVERLAPPED));
		PerIOData->BytesRecv = 0;
		PerIOData->BytesSend = 0;
		PerIOData->DATABuf.len = DATA_BUFSIZE;
		PerIOData->DATABuf.buf = PerIOData->Buffer;
		Flags = 0;

		if (WSARecv(Accept,&(PerIOData->DATABuf),1,&RecvBytes,&Flags,&(PerIOData->OVerlapped),NULL) == SOCKET_ERROR)
		{
			if (WSAGetLastError() != ERROR_IO_PENDING)
			{
				printf("WSARecv() failed with error %d\r\n",WSAGetLastError());
				return 0;
			}
		}
	}

    closesocket(g_ServerSocket);  
    WSACleanup();

	return 0;
}