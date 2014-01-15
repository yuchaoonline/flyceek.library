

using System;
using System.Net.Sockets;
public class UdpServer
{
    /// <summary>
    /// 接收新客户端的端口
    /// </summary>
    private int listenPort;
    /// <summary>
    /// 数据通讯的端口
    /// </summary>
    private int CommunicationPort;
    /// <summary>
    /// 最大的客户端数
    /// </summary>
    private int numClient;

    /// <summary>
    /// 负责接收新的客户端的数据
    /// </summary>
    private UdpReceiveSocket listen;
    /// <summary>
    /// 负责接收旧客户端的数据
    /// </summary>
    private UdpReceiveSocket communicationRec;
    /// <summary>
    /// 负责向客户端发送数据
    /// </summary>
    private UdpSendSocket communicationSend;
    private bool isStartSend;


    public event EventHandler<SocketAsyncEventArgs> OnNewClient;
    public event EventHandler<SocketAsyncEventArgs> OnReceivedData;
    public event EventHandler<SocketAsyncEventArgs> OnSentData;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="acceptPort">新客户端接收端口</param>
    /// <param name="dataComPort">数据通讯端口</param>
    /// <param name="maxNumClient">最大客户端数</param>
    public UdpServer(int acceptPort, int dataComPort, int maxNumClient)
    {
        listenPort = acceptPort;
        CommunicationPort = dataComPort;
        numClient = maxNumClient;
        isStartSend = false;
         

        listen = new UdpReceiveSocket(listenPort);
        listen.OnDataReceived += new EventHandler<SocketAsyncEventArgs>(listen_OnDataReceived);

        communicationRec = new UdpReceiveSocket(CommunicationPort);
        communicationRec.OnDataReceived += new EventHandler<SocketAsyncEventArgs>(communicationRec_OnDataReceived);

        communicationSend = new UdpSendSocket(numClient);
        communicationSend.DataSent += new EventHandler<SocketAsyncEventArgs>(communicationSend_DataSent);
        communicationSend.Init();
    }


    public void Start()
    {
        //接收新的客户端
        listen.StartReceive();

        //接收数据
        communicationRec.StartReceive();      
    }

    #region 事件
    void communicationRec_OnDataReceived(object sender, SocketAsyncEventArgs e)
    {            
        if (OnReceivedData != null)
        {
            OnReceivedData(sender, e);
        }
        //向客户端发送数据
        communicationSend.Send(e.RemoteEndPoint);
    }
        
    void listen_OnDataReceived(object sender, SocketAsyncEventArgs e)
    {          
        if (OnNewClient != null)
        {
            OnNewClient(sender, e);
        }
    }

    void communicationSend_DataSent(object sender, SocketAsyncEventArgs e)
    {          
        if (OnSentData != null)
        {
            OnSentData(sender, e);
        }         
    }

    #endregion
}