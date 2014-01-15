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

namespace SocketClientConsoleTest.Lib
{
    public class SocketClient:IDisposable
    {
        Socket m_socket;

        IPEndPoint m_ipEndPoint;

        AutoResetEvent m_autoResetEvent = new AutoResetEvent(false); 

        public SocketClient(string ip,int port):this(new IPEndPoint(IPAddress.Parse(ip),port))
        {
        }

        public SocketClient(IPEndPoint ip)
        {
            m_ipEndPoint = ip;
            m_socket = new Socket(m_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        void SocketAsyncEvent_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(e);
                        break;
                    case SocketAsyncOperation.Connect:
                        ProcessConnect(e);
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSent(e);
                        break;
                }
            }
        }

        public void Connect()
        {
            SocketAsyncEventArgs socketAsyncEventArg = new SocketAsyncEventArgs();
            socketAsyncEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
            socketAsyncEventArg.RemoteEndPoint = m_ipEndPoint;
            socketAsyncEventArg.UserToken = m_socket;
            m_socket.ConnectAsync(socketAsyncEventArg);
            
            m_autoResetEvent.WaitOne();
            SocketError errorCode = socketAsyncEventArg.SocketError;
            if (errorCode != SocketError.Success)
            {
                throw new SocketException((Int32)errorCode);
            }
        }

        public void Send(string content)
        {
            if (m_socket.Connected)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(content);
                SocketAsyncEventArgs socketAsyncEventArg = new SocketAsyncEventArgs();
                socketAsyncEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
                socketAsyncEventArg.RemoteEndPoint = m_ipEndPoint;
                socketAsyncEventArg.UserToken = new Token(m_socket, bytes.Length);
                socketAsyncEventArg.SetBuffer(bytes, 0, bytes.Length);
                if (!m_socket.SendAsync(socketAsyncEventArg))
                {
                    ProcessSent(socketAsyncEventArg);
                }
            }
        }

        void ProcessSent(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                Token token=e.UserToken as Token;

                SocketAsyncEventArgs socketAsyncEventArg = new SocketAsyncEventArgs();
                socketAsyncEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
                socketAsyncEventArg.SetBuffer(new byte[token.BufferSize], 0, token.BufferSize);
                socketAsyncEventArg.UserToken = token;


                //e.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEvent_Completed);
                //e.SetBuffer(new byte[token.BufferSize], 0, token.BufferSize);

                if (!m_socket.ReceiveAsync(socketAsyncEventArg))
                {
                    ProcessReceive(e);
                }
            }
        }

        void ProcessReceive(SocketAsyncEventArgs e)
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
                        Console.WriteLine("服务器收到{0}消息:{1}", token.Connection.RemoteEndPoint.ToString(), token.Buffer);
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
        }

        void ProcessConnect(SocketAsyncEventArgs e)
        {
            m_autoResetEvent.Set();
            if (e.SocketError == SocketError.Success)
            {
                ;
            }
        }

        public void Dispose()
        {
            if (m_socket.Connected)
            {
                m_socket.Close();
            }
            m_autoResetEvent.Close();
        }
    }
}
