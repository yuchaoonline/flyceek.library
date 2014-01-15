using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Globalization;

namespace SocketAsyncServer
{
    delegate void ProcessData(SocketAsyncEventArgs args);

    /// <summary>
    /// Token for use with SocketAsyncEventArgs.
    /// </summary>
    public sealed class Token : IDisposable
    {
        private Socket connection;

        private StringBuilder buffer;

        private Int32 currentIndex;

        private int bufferSize;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="connection">Socket to accept incoming data.</param>
        /// <param name="bufferSize">Buffer size for accepted data.</param>
        public Token(Socket connection, Int32 bufferSize)
        {
            this.connection = connection;
            Init(bufferSize);
        }

        public void Init(int bufferSize)
        {
            this.buffer = new StringBuilder(bufferSize);
            this.bufferSize = bufferSize;
        }

        /// <summary>
        /// Accept socket.
        /// </summary>
        public Socket Connection
        {
            get { return this.connection; }
        }

        public int BufferSize
        {
            get { return bufferSize; }
        }

        public string Buffer
        {
            get { return buffer.ToString(); }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
        }

        /// <summary>
        /// Process data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        public void ProcessData(SocketAsyncEventArgs args)
        {
            // Get the message received from the client.
            String received = this.buffer.ToString();

            int handle = (int)this.Connection.Handle;
            //int handle=args.
            //TODO Use message received to perform a specific operation.
            Console.WriteLine("Received From Handle[{2}] Message: {0. The server has read {1} bytes.", received, received.Length,handle.ToString());

            Byte[] sendBuffer = Encoding.Unicode.GetBytes("Return "+ received);
            args.SetBuffer(sendBuffer, 0, sendBuffer.Length);

            // Clear StringBuffer, so it can receive more data from a keep-alive connection client.
            buffer.Length = 0;
            this.currentIndex = 0;
        }

        /// <summary>
        /// Set data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        public void SetData(SocketAsyncEventArgs args)
        {
            Int32 count = args.BytesTransferred;

            if ((this.currentIndex + count) > this.buffer.Capacity)
            {
                throw new ArgumentOutOfRangeException("count",
                    String.Format(CultureInfo.CurrentCulture, "Adding {0} bytes on buffer which has {1} bytes, the listener buffer will overflow.", count, this.currentIndex));
            }

            buffer.Append(Encoding.Unicode.GetString(args.Buffer, args.Offset, count));
            this.currentIndex += count;
        }

        #region IDisposable Members

        /// <summary>
        /// Release instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.connection.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                // Throw if client has closed, so it is not necessary to catch.
            }
            finally
            {
                this.connection.Close();
            }
        }

        #endregion
    }
}