
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;

namespace SocketLib.Core
{
    public interface ISession
    {
        void Initize(Socket socket);
        void ProcessRecv(SocketAsyncEventArgs e);
        void ProcessSend(SocketAsyncEventArgs e);
        IPEndPoint LocalEndPoint { get; }
        IPEndPoint RemoteEndPoint { get; }
        event EventHandler<SocketAsyncEventArgs> Closed;
    }

    public abstract class Session : ISession
    {
        public Context Context { get; private set; }
        public Socket Client { get; private set; }
        public IPEndPoint LocalEndPoint { get { return (IPEndPoint)Client.LocalEndPoint; } }
        public IPEndPoint RemoteEndPoint { get { return (IPEndPoint)Client.RemoteEndPoint; } }
        public event EventHandler<SocketAsyncEventArgs> Closed;

        //public virtual void RecvResponse(SocketContext context, byte[] data) { }
        //public virtual void RecvResponse(SocketContext context, byte[] data) { }

        //public virtual void SendResponse(SocketContext context, byte[] data) { }
        //public virtual void SendResponse(SocketContext context, byte[] data) { }

        public void Initize(Socket socket)
        {
            this.Client = socket;
        }

        public virtual void ProcessRecv(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success)
            {
                OnClosed(e);
                return;
            }

            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            e.SetBuffer(e.Offset, e.BytesTransferred);

            string str = "Hello World";
            byte[] buffer = System.Text.Encoding.Default.GetBytes(str);
            e.SetBuffer(buffer, 0, buffer.Length);


            bool willRaiseEvent = token.Socket.SendAsync(e);
            if (!willRaiseEvent)
            {
                ProcessSend(e);
            }
        }

        public virtual void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                OnClosed(e);
                return;
            }

            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            bool willRaiseEvent = token.Socket.ReceiveAsync(e);
            if (!willRaiseEvent)
            {
                ProcessRecv(e);
            }
        }

        public virtual void OnClosed(SocketAsyncEventArgs e)
        {
            if (Closed != null)
                Closed(this, e);
        }
    }
}