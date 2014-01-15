using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib.Core
{
    public class ReceiveSocket
    {
        private Socket m_receiveSocket;

        private SocketAsyncEventArgs receiveSocketArgs;

        private IPEndPoint localEndPoint;

        private byte[] receivebuffer;

        public event EventHandler<SocketAsyncEventArgs> OnDataReceived;

        public ReceiveSocket(Socket socket)
        {
            m_receiveSocket = socket;

            receivebuffer = new byte[1024];
            receiveSocketArgs = new SocketAsyncEventArgs();
            receiveSocketArgs.RemoteEndPoint = localEndPoint;
            receiveSocketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveSocket_Completed);
            receiveSocketArgs.SetBuffer(receivebuffer, 0, receivebuffer.Length);
        }

        void ReceiveSocket_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.ReceiveFrom:
                    ProcessReceived(e);
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

        public void StartReceive()
        {
            if (!m_receiveSocket.AcceptAsync(receiveSocketArgs))
            {
                ProcessAccept(receiveSocketArgs);
            }
        }

        private void ProcessReceived(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                if (OnDataReceived != null)
                {
                    //不要进行耗时操作
                    OnDataReceived(m_receiveSocket, e);
                }
            }
            StartReceive();
        }
    }
}
