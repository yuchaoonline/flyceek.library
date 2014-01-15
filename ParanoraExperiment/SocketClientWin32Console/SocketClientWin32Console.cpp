// SocketClientWin32Console.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <stdio.h>
#include <iostream>
#include <winsock2.h>
#include <mswsock.h>

#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "mswsock.lib")

#define _BUFFER_LENGTH_ 512
#define _DEFAULT_PORT_ 12345

int _tmain(int argc, _TCHAR* argv[])
{
	WORD wVersionRequested;
	WSADATA wsaData;
	int err;
	int iResult;
	wVersionRequested = MAKEWORD( 2, 2 );

	err = WSAStartup( wVersionRequested, &wsaData );
	if( NO_ERROR != err)
	{
		printf("Error at WSAStartup()\n");
		WSACleanup();
		return 0;
	}

	/*if (LOBYTE( wsaData.wVersion ) != 1 ||
		HIBYTE( wsaData.wVersion ) != 1 ) 
	{
			WSACleanup( );
			return 0;
	}*/

	SOCKET socketClient=socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);

	if(socketClient == INVALID_SOCKET)
    {
        printf("Error at socket():%ld\n", WSAGetLastError());
        WSACleanup();
        return 0;
    }


	sockaddr_in clientService;
    clientService.sin_family = AF_INET;
    clientService.sin_addr.s_addr = inet_addr("127.0.0.1");
    clientService.sin_port = htons(8899);


	//connect(socketClient,(SOCKADDR*)&addrSrv,sizeof(SOCKADDR));

	int connRv = connect(socketClient, (SOCKADDR*)&clientService, sizeof(clientService));
    if( SOCKET_ERROR == connRv )
    {
        printf("Failed to connect.\n");
        WSACleanup();
        return 0;
    }

	std::cout<<"Connected server."<<std::endl;	

	while (1)
	{
		char recvBuf[50]={0};
		char inputStr[50]={0};
		//scanf_s("%s",&inputStr);

		std::cin>>inputStr;
		if(inputStr=="exit")
		{
			break;
		}

		iResult = send(socketClient, inputStr, strlen(inputStr), 0);
		if(iResult == SOCKET_ERROR)
		{
			printf("send failed: %d\n", WSAGetLastError());
			closesocket(socketClient);
			WSACleanup();
			return 0;
		}

		printf("Bytes Sended:%d\n", iResult);


		iResult = recv(socketClient, recvBuf, _BUFFER_LENGTH_, 0);
		if(iResult > 0)
		{
			printf("Bytes received: %d,Byte :%s\n", iResult,recvBuf);
		}
		else if(iResult == 0)
		{
			printf("Connection closed\n");
		}
		else
		{
			printf("recv failed: %d\n", WSAGetLastError());
		}

		/*do{
			iResult = recv(socketClient, recvBuf, _BUFFER_LENGTH_, 0);
			if(iResult > 0)
			{
				printf("Bytes received: %d\n", iResult);
			}
			else if(iResult == 0)
			{
				printf("Connection closed\n");
			}
			else
			{
				printf("recv failed: %d\n", WSAGetLastError());
			}

		}while(iResult > 0);*/

		//send(socketClient,inputStr,strlen(inputStr)+1,0);

		//char recvBuf[50];
		//ZeroMemory(recvBuf,50);
		//recv(socketClient,recvBuf,50,0);
		//printf("%s\n",recvBuf);
		//std::cout<<recvBuf;
	}

	iResult = shutdown(socketClient, SD_SEND);
    if(iResult == SOCKET_ERROR)
    {
        printf("shutdown failed:%d\n", WSAGetLastError());
        closesocket(socketClient);
        WSACleanup();
        return 0;
    }

    WSACleanup();

	closesocket(socketClient);
	WSACleanup();

	return 0;
}

