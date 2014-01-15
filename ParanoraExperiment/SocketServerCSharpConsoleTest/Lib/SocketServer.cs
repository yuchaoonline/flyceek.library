using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketAsyncServer;
using SocketLib.Core;

namespace SocketServerConsoleTest.Lib
{
    public class SocketServer
    {
        private int m_numConnections;
        private int m_receiveBufferSize;

        BufferManager m_bufferManager;

        const int opsToPreAlloc = 2;

        Socket listenSocket;
        SocketAsyncEventArgsPool m_socketAsyncEventPool;

        int m_totalBytesRead;
        int m_numConnectedSockets;

        Semaphore m_maxNumberAcceptedClients;

        public SocketServer(int numConnections, int receiveBufferSize)
        {
            m_totalBytesRead = 0;
            m_numConnectedSockets = 0;
            m_numConnections = numConnections;
            m_receiveBufferSize = receiveBufferSize;

            m_bufferManager = new BufferManager(receiveBufferSize * numConnections * opsToPreAlloc, receiveBufferSize);

            m_socketAsyncEventPool = new SocketAsyncEventArgsPool(numConnections);
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        public void Init()
        {
            m_bufferManager.InitBuffer();
            SocketAsyncEventArgs readWriteEventArg;

            for (int i = 0; i < m_numConnections; i++)
            {
                readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
                m_bufferManager.SetBuffer(readWriteEventArg);
                m_socketAsyncEventPool.Push(readWriteEventArg);
            }
        }

        public void Start(string ip,int port)
        {
            Start(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public void Start(IPEndPoint localEndPoint)
        {
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(localEndPoint);
            listenSocket.Listen(100);

            StartAccept(null);
        }

        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }

            //m_maxNumberAcceptedClients.WaitOne();

            if (!listenSocket.AcceptAsync(acceptEventArg))
            {
                ProcessAccept(acceptEventArg);
            }
        }
        
        void SocketAsyncEvent_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
            }
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Interlocked.Increment(ref m_numConnectedSockets);

            Socket acceptSocket = e.AcceptSocket;

            SocketAsyncEventArgs sockentEventArg = m_socketAsyncEventPool.Pop();

            if (sockentEventArg != null)
            {
                sockentEventArg.UserToken = new Token(acceptSocket, m_receiveBufferSize);
                if (!acceptSocket.ReceiveAsync(sockentEventArg))
                {
                    ProcessReceive(sockentEventArg);
                }
            }

            StartAccept(e);
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            Token token = e.UserToken as Token;
            Socket socket = token.Connection;
            if (e.BytesTransferred > 0)
            {
                if (e.SocketError == SocketError.Success)
                {
                    token.SetData(e);

                    if (socket.Available == 0)
                    {
                        byte[] reciveBytes=Encoding.Unicode.GetBytes(token.Buffer);
                        e.SetBuffer(reciveBytes, 0, reciveBytes.Length);
                        if (!socket.SendAsync(e))
                        {
                            ProcessSend(e);
                        }
                    }
                    else if (!socket.ReceiveAsync(e))
                    {
                        ProcessReceive(e);
                    }
                }
                else 
                {
                    ;
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Token token=e.UserToken as Token;
                Socket socket = token.Connection;

                token.Init(m_receiveBufferSize);

                if (!socket.ReceiveAsync(e))
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            Socket socket = (e.UserToken as Token).Connection;

            try
            {
                socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception) 
            { }
            
            socket.Close();

            Interlocked.Decrement(ref m_numConnectedSockets);

            m_socketAsyncEventPool.Push(e);
        }

    }
}
