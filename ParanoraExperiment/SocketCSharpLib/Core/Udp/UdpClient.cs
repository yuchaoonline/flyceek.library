using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketLib.Core.Udp
{
    public class UdpClient
    {
        /// <summary>
        /// 连接请求地址
        /// </summary>
        IPEndPoint remoteAccpetPort;
        /// <summary>
        /// 数据通信地址
        /// </summary>
        IPEndPoint remoteCommunicationPort;

        private SocketAsyncEventArgsPool pool;

        private BufferManager bfManager;

        private int bufferSize = 1024;
        Random r = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port1">第一次通讯端口，用于确认连接</param>
        /// <param name="port2">数据通信端口</param>
        /// <param name="ServerIP">服务器IP地址</param>
        public UdpClient(int port1, int port2, string ServerIP, int numClient)
        {
            remoteAccpetPort = new IPEndPoint(IPAddress.Parse(ServerIP), port1);
            remoteCommunicationPort = new IPEndPoint(IPAddress.Parse(ServerIP), port2);

            pool = new SocketAsyncEventArgsPool(numClient);
            bfManager = new BufferManager(numClient * bufferSize * 2, bufferSize);
            bfManager.InitBuffer();

            SocketAsyncEventArgs args;
            for (int i = 0; i < numClient; i++)
            {
                args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(SendSocket_Completed);
                bfManager.SetBuffer(args);
                pool.Push(args);
            }


        }

        void SendSocket_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.SendTo:
                    ProcessSent(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a send");
            }

        }


        public void Start()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            SocketAsyncEventArgs args = pool.Pop();
            args.RemoteEndPoint = remoteAccpetPort;
            args.UserToken = socket;
            StartSend(args);
        }

        private void StartSend(SocketAsyncEventArgs args)
        {
            Socket socket = args.UserToken as Socket;
            if (!socket.SendToAsync(args))
            {
                ProcessSent(args);
            }
        }


        void ProcessSent(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {

                e.RemoteEndPoint = remoteCommunicationPort;
            }
            Thread.Sleep(1000);
            StartSend(e);
        }

    }
}
