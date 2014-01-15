using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic ;
using System.Threading;

namespace SocketLib.Core.Udp
{
    public  class UdpSendSocket
    {

       private SocketAsyncEventArgsPool socketArgsPool;

       private BufferManager bfManager;

       private Socket socket;

       private SocketAsyncEventArgs socketArgs;

       private int numClient;

       public event EventHandler<SocketAsyncEventArgs> OnDataSented;

       private static readonly object asyncLock = new object();
       /// <summary>
       /// 最大客户端数
       /// </summary>
       /// <param name="numClient"></param>
       public UdpSendSocket(int numClient):this(numClient,1024)
       {
       }

       public UdpSendSocket(int numClient, int bufferSize)
       {
           socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
           this.numClient = numClient;
           bfManager = new BufferManager(numClient * bufferSize * 2, bufferSize);
           socketArgsPool = new SocketAsyncEventArgsPool(numClient);
       }
       /// <summary>
       /// 初始化
       /// </summary>
       public void Init()
       {
           //初始化数据池
           bfManager.InitBuffer();
           //生成一定数量的对象池
           for (int i = 0; i < numClient; i++)
           {
               socketArgs = new SocketAsyncEventArgs();
               socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SendSocket_Completed);
              //设置SocketAsyncEventArgs的Buffer
               bfManager.SetBuffer(socketArgs);
               socketArgsPool.Push(socketArgs);
           }
       }


       /// <summary>
       /// 发送数据包
       /// </summary>
       /// <param name="data">要发送的数据包</param>
       public void Send(EndPoint remoteEndPoint)
       {
           //每次发送前都取一个新的SocketAsyncEventArgs对象。
           socketArgs = socketArgsPool.Pop();
           socketArgs.RemoteEndPoint = remoteEndPoint;
           if (socketArgs.RemoteEndPoint != null)
           {
               if (!socket.SendToAsync(socketArgs))
               {
                   ProcessSent(socketArgs);
               }
           }
       }

       public void Send(byte[] content, EndPoint remoteEndPoint)
       {
           socketArgs = socketArgsPool.Pop();
           socketArgs.RemoteEndPoint = remoteEndPoint;
           //设置发送的内容
           bfManager.SetBufferValue(socketArgs, content);
           if (socketArgs.RemoteEndPoint != null)
           {
               if (!socket.SendToAsync(socketArgs))
               {
                   ProcessSent(socketArgs);
               }
           }
       }

       private void SendSocket_Completed(object sender, SocketAsyncEventArgs e)
       {
           switch (e.LastOperation)
           {
               case SocketAsyncOperation.SendTo:
                   this.ProcessSent(e);
                   break;
               default:
                   throw new ArgumentException("The last operation completed on the socket was not a send");
           }
       }

       private void ProcessSent(SocketAsyncEventArgs e)
       {
           if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
           {
               if (OnDataSented != null)
               {
                   //用于统计发送了多少数据
                   OnDataSented(socket, e);
               }
           }
           //发送完成后将SocketAsyncEventArgs对象放回对象池中
           socketArgsPool.Push(e);         
       }
    }
}
