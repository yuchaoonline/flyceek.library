// SocketWin32Console.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"  

#define PORT 5150
#define DATA_BUFSIZE 8192

#define _LINK_PORT_ 8088
#define _BUFFER_LENGTH_ 512
#define _DEFAULT_PORT_ "12345"
//关键项
typedef struct
{
	OVERLAPPED Overlapped;
	WSABUF DataBuf;
	CHAR Buffer[DATA_BUFSIZE];
	DWORD BytesSEND;
	DWORD BytesRECV;
} PER_IO_OPERATION_DATA, * LPPER_IO_OPERATION_DATA;

typedef struct 
{
	SOCKET Socket;
} PER_HANDLE_DATA, * LPPER_HANDLE_DATA;

unsigned __stdcall ThreadSocketRecv(LPVOID param)
{
    SOCKET sockClient = (SOCKET)param;

    char recvBuf[_BUFFER_LENGTH_] = {0};
    int nRecv = 0;
    while( SOCKET_ERROR != nRecv)
    {
        nRecv = recv(sockClient, recvBuf, _BUFFER_LENGTH_, 0);
        printf("ClinetSocket=%d, received count:%d,recevied string:%s\n", sockClient, nRecv,recvBuf);
        if(SOCKET_ERROR == nRecv)
        {
            //标记当前线程为有信号

            _endthreadex(0);
            printf("recv failed! current thread's state is signaled! error=%d, clientsocket=%d", WSAGetLastError(), sockClient);
        }
    }

    //关闭Socket

    printf("ClientSocket=%d, thread closing...\n", sockClient);
    shutdown(sockClient, SD_BOTH);
    closesocket( sockClient );
    return 0;
}


int SocketTest()
{
	struct addrinfo *result = NULL;
    struct addrinfo *ptr = NULL;
	struct addrinfo hints;
    int iResult;

	int rev=0;
	WSADATA wsa;
	/*初始化socket资源*/
	if (WSAStartup(MAKEWORD(2,2),&wsa) != 0)
	{
		WSACleanup();
		return 0;   //代表失败
	}


	//SOCKET socketServer = socket(AF_INET, SOCK_STREAM, 0);

	ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_INET;
    hints.ai_socktype = SOCK_STREAM;
    //hints.ai_protocol = IPPROTO_IP;
	hints.ai_protocol = IPPROTO_IP;
    hints.ai_flags = AI_PASSIVE;

	SOCKADDR_IN serverAddr;
	ZeroMemory((char *)&serverAddr,sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_port = htons(8899);					/*本地监听端口:1234*/
	serverAddr.sin_addr.S_un.S_addr=inet_addr("127.0.0.1");
	//serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);		/*有IP*/

	//hints.ai_addr=(struct sockaddr *)&serverAddr;

	iResult = getaddrinfo(NULL, _DEFAULT_PORT_, &hints, &result);
    if(iResult != 0)
    {
        printf("getaddrinfo failed: %d\n", iResult);
        WSACleanup();
        return 1;
    }

	/*SOCKET socketServer =WSASocket(AF_INET,
		SOCK_STREAM,
		0,
		NULL,
		0,
		NULL);*/

	SOCKET socketServer=socket(result->ai_family, result->ai_socktype, result->ai_protocol);

	iResult = bind(socketServer, (struct sockaddr *)&serverAddr,sizeof(serverAddr));
	//iResult = bind(socketServer, result->ai_addr, result->ai_addrlen);
    if(iResult == SOCKET_ERROR)
    {
        printf("bind failed: %d\n", WSAGetLastError());
        freeaddrinfo(result);
        closesocket(socketServer);
        WSACleanup();
        return 1;
    }

	//rev =bind(socketServer,(struct sockaddr *)&serverAddr,sizeof(serverAddr));

	//if (SOCKET_ERROR == rev)
	//{
	//	closesocket(socketServer); //关闭套接字
	//	return 0;
	//}

	rev=listen(socketServer,SOMAXCONN);

	if(SOCKET_ERROR ==rev)
	{
		printf("Error at bind():%d \n", WSAGetLastError());
        closesocket(socketServer);
        WSACleanup();
        return 1;
	}
	

	//SOCKADDR_IN addrClient;
	//int len=sizeof(SOCKADDR);

	while(true)
    {
		SOCKET sockClient = INVALID_SOCKET;
		printf("start accept...\n");
		sockClient = accept(socketServer, NULL, NULL);
		printf("end accept...\n");
		if(INVALID_SOCKET == sockClient)
		{
			printf("accept failed:%d\n", WSAGetLastError());
			closesocket(socketServer);
			WSACleanup();
			return 1;
		}
		else
		{
			unsigned threadId;
			HANDLE hThread = (HANDLE)_beginthreadex(NULL, 0, &ThreadSocketRecv,(void*)sockClient, 0, &threadId);
			//WaitForSingleObject(hThread, INFINITE);
			CloseHandle(hThread);
		}
	}
    
	WSACleanup();
	closesocket(socketServer);

	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	

    return 0;


	/*while(1)
	{
		std::cout<<"begin accept.\r\n";
		SOCKET sockConn=accept(socketServer,(SOCKADDR*)&addrClient,&len);
		char sendBuf[50];
		sprintf_s(sendBuf,"Welcome %s to here!\r\n",inet_ntoa(addrClient.sin_addr));

		send(sockConn,sendBuf,strlen(sendBuf)+1,0);
		char recvBuf[50];

		recv(sockConn,recvBuf,strlen(recvBuf),0);

		printf("%s\n",recvBuf);

		closesocket(sockConn);
	}

	std::cout<<"server end.\r\n";

	WSACleanup();
	closesocket(socketServer);

	return 0;*/
}

