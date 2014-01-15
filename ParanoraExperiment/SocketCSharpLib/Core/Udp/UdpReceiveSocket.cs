using System;
using System.Net;
using System.Net.Sockets;

namespace SocketLib.Core.Udp
{
    public class UdpReceiveSocket
    {
        private Socket receiveSocket;

        private SocketAsyncEventArgs receiveSocketArgs;

        private IPEndPoint localEndPoint;

        private byte[] receivebuffer;

        public  event EventHandler<SocketAsyncEventArgs> OnDataReceived;

        public UdpReceiveSocket(IPAddress ip, int port)
        {
            receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            localEndPoint = new IPEndPoint(ip, port);
            receiveSocket.Bind(localEndPoint);

            receivebuffer = new byte[1024];
            receiveSocketArgs = new SocketAsyncEventArgs();
            receiveSocketArgs.RemoteEndPoint = localEndPoint;
            receiveSocketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveSocket_Completed);
            receiveSocketArgs.SetBuffer(receivebuffer, 0, receivebuffer.Length);
        }

        public UdpReceiveSocket(string ip,int port):this(IPAddress.Parse(ip),port)
        {

        }

        public void StartReceive()
        {
            //if (!receiveSocket.AcceptAsync(receiveSocketArgs))
            //{
            //    ProcessAccept(receiveSocketArgs);
            //}

            if (!receiveSocket.ReceiveFromAsync(receiveSocketArgs))
            {
                ProcessReceived(receiveSocketArgs);
            }
        }

        void ReceiveSocket_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.ReceiveFrom:
                    this.ProcessReceived(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive");
            }
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Socket acceptSocket = e.AcceptSocket;
            if (acceptSocket.Connected)
            {
                if (!acceptSocket.ReceiveFromAsync(e))
                {
                    ProcessReceived(e);
                }
            }
        }

        private  void ProcessReceived(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                if (OnDataReceived != null)
                {
                    //不要进行耗时操作
                    OnDataReceived(receiveSocket , e);
                }
            }
            StartReceive();
        }
    }
}
