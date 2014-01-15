using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace SocketLib.Core
{
    public class AsyncUserToken
    {
        Socket m_socket;
        public AsyncUserToken() : this(null) { }

        public AsyncUserToken(Socket socket)
        {
            m_socket = socket;
        }
        public Socket Socket
        {
            get { return m_socket; }
            set { m_socket = value; }
        }
        public ISession Session { get; set; }
        public Context Context { get; set; }
    }
}