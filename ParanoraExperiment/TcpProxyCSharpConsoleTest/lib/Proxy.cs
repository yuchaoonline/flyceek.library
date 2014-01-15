using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TcpProxy
{
    /// <summary>
    /// by 路过秋天
    /// http://www.cnblogs.com/cyq1162
    /// </summary>
    public class Proxy
    {
        Socket clientSocket;//接收和返回
        byte[] read = null;//存储来自客户端请求数据包
        byte[] sendBytes = null;//存储中转请求发送的数据
        byte[] recvBytes = null;//存储中转请求返回的数据
        bool isConnect = false;
        byte[] qqSendBytes=new byte[4096];
        byte[] qqRecvBytes = new byte[4096];
        public Proxy(Socket socket)
        {
            clientSocket = socket;
            recvBytes = new Byte[1024 * 1024];
            clientSocket.ReceiveBufferSize = recvBytes.Length;
            clientSocket.SendBufferSize = recvBytes.Length;
            // clientSocket.SendTimeout = 5000;
            //clientSocket.ReceiveTimeout = 10000;
        }
        int sendLength = 0, recvLength = 0;
        public void Run()
        {
            #region 获取客户端请求数据
            Write("-----------------------------请求开始---------------------------");

            read = new byte[clientSocket.Available];
            IPAddress ipAddress = IPAddress.Any;
            string host = "";//主机
            int port = 80;//端口

            int bytes = ReadMessage(read, ref clientSocket, ref ipAddress, ref host, ref port);
            if (bytes == 0)
            {
                Write("读取不到数据!");
                CloseSocket(clientSocket);
                return;
            }
            #endregion
            #region 创建中转Socket及建立连接
            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);
            Socket IPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPsocket.Connect(ipEndpoint); Write("-----Socket 建立连接！ IP地址：" + ipAddress + "网址：http://" + host);
            }
            catch (Exception err)
            {
                Write("连接失败 ：" + err.Message);
                Write("退出请求!!!");
                CloseSocket(IPsocket, false);
                return;
            }


            
            #endregion
            if (isConnect)
            {
                byte[] qqOkData = QQokProxyData();
                clientSocket.Send(qqOkData, 0, qqOkData.Length, 0);
            }
            else
            {
                IPsocket.Send(sendBytes, 0);
            }
          
            //IPsocket.ReceiveTimeout = 5000;
            //创建连接Web服务器端的Socket对象
           

           
            #region 发送/接收中转请求
            
            int length = 0, count = 0;
            if (isConnect)
            {
                System.Threading.Thread.Sleep(100);//关键
                //循环发送客户端请求,接收服务器返回
                while (IPsocket.Available != 0 || clientSocket.Available != 0 || isConnect)
                {
                    if (!IPsocket.Connected || !clientSocket.Connected)
                    {
                        clientSocket.Close();
                        IPsocket.Close();
                        return;
                    }
                    try
                    {
                        while (clientSocket.Available != 0)
                        {
                            sendLength = clientSocket.Receive(qqSendBytes, qqSendBytes.Length, 0);
                            IPsocket.Send(qqSendBytes, sendLength, 0);
                            Console.WriteLine("发送字节数: " + sendLength.ToString());
                        }

                        System.Threading.Thread.Sleep(500);
                        while (IPsocket.Available != 0)
                        {
                            recvLength = IPsocket.Receive(qqRecvBytes, qqRecvBytes.Length, 0);

                            clientSocket.Send(qqRecvBytes, recvLength, 0);
                            Console.WriteLine("接收字节数: " + recvLength.ToString());
                            //System.Threading.Thread.Sleep(100);
                        }
                    }
                    catch
                    {

                    }

                }

            }
            else
            {
                try
                {
                    do
                    {

                        length = IPsocket.Receive(recvBytes, count, IPsocket.Available, 0);
                        count = count + length;
                        Write("接收转发请求返回的数据中..." + length);
                        System.Threading.Thread.Sleep(200);//关键点,请求太快数据接收不全
                    }

                    while (IPsocket.Available > 0);

                    clientSocket.Send(recvBytes, 0, count, 0);
                }
                catch(Exception err)
                {
                    Write(err.Message);
                }

            }
            #endregion

            #region 结束请求,关闭客户端Socket
            Write("接收完成。返回客户端数据..." + count);
            CloseSocket(IPsocket);
            CloseSocket(clientSocket);
            recvBytes = null;
            Write("本次请求完成,已关闭连接...");
            Write("-----------------------------请求结束---------------------------");
            #endregion
            //}
            //catch (Exception err)
            //{
            //    System.Console.WriteLine(err.Message + err.Source);
            //}

        }
        //从请求头里解析出url和端口号
        private string GetUrl(string clientmessage, ref int port)
        {
            if (clientmessage.IndexOf("CONNECT") != -1)
            {
                isConnect = true;
            }
            int index1 = clientmessage.IndexOf(' ');
            int index2 = clientmessage.IndexOf(' ', index1 + 1);
            if ((index1 == -1) || (index2 == -1))
            {
                return "";
            }
            string part1 = clientmessage.Substring(index1 + 1, index2 - index1).Trim();
            string url = string.Empty;
            if (!part1.Contains("http://"))
            {
                if (part1.Substring(0, 1) == "/")
                {
                    part1 = "127.0.0.1" + part1;
                }
                part1 = "http://" + part1;
            }
            Uri uri = null;
            try
            {
                uri = new Uri(part1);
            }
            catch
            {
                return "";
            }
            url = uri.Host;
            port = uri.Port;
            return url;
        }
        //接收客户端的HTTP请求数据
        private int ReadMessage(byte[] readByte, ref Socket s, ref IPAddress ipAddress, ref string host, ref int port)
        {
            try
            {

                int bytes = s.Receive(readByte, readByte.Length, 0);
                Write("收到原始请求数据：" + readByte.Length);
                string header = Encoding.ASCII.GetString(readByte);


                host = GetUrl(header, ref port);
                if (Filter(host))
                {
                    Write("系统过滤:" + host);
                    return 0;
                }
                Write(header);
                ipAddress = Dns.GetHostAddresses(host)[0];
                if (!isConnect)
                {
                    header = header.Replace("http://" + host, "");
                }
                sendBytes = Encoding.ASCII.GetBytes(header);
                System.Threading.Thread.Sleep(50);
                Write("转发请求数据：" + sendBytes.Length);
                Write(Encoding.ASCII.GetString(sendBytes));
                return bytes;
            }
            catch
            {
                System.Threading.Thread.Sleep(300);
                return 0;
            }
        }

        private void Write(string msg)
        {
            // return;
            System.Console.WriteLine(msg);
        }
        private void CloseSocket(Socket socket)
        {
            CloseSocket(socket, true);
        }
        private void CloseSocket(Socket socket, bool shutdown)
        {
            if (socket != null)
            {
                if (shutdown)
                {
                    socket.Shutdown(SocketShutdown.Both);
                }
                socket.Close();
            }
        }
        private byte[] QQokProxyData()
        {
            string data = "HTTP/1.0 200 Connection established";//  Proxy-agent: CCProxy 2010";
            return System.Text.Encoding.ASCII.GetBytes(data);
        }
        private bool Filter(string url)
        {
            switch (url.ToLower())
            {
                case "fffocus.cn":
                    return true;
            }
            return false;
        }


    }
}
