using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketClientConsoleTest.Lib;
using SocketLib.Core.Udp;

namespace SocketClientConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string input=string.Empty;

            SocketClient socketClient = new SocketClient("127.0.0.1", 8899);

            socketClient.Connect();

            while (true)
            {
                input = Console.ReadLine();
                if(input.ToLower()!="exit")
                {
                    socketClient.Send(input);
                }
            }
        }
    }
    //class Program
    //{
    //    static string serverIp;
    //    static string name = string.Empty;
    //    static int port1, port2;
    //    static UdpClient client;
    //    static void Main(string[] args)
    //    {
    //        //Console.WriteLine("输入服务器IP地址");
    //        //serverIp = Console.ReadLine();
    //        //Console.WriteLine("输入接入端口");
    //        //port1 = int.Parse(Console.ReadLine());
    //        //Console.WriteLine("输入通信端口");
    //        //port2 = int.Parse(Console.ReadLine());

    //        serverIp = "127.0.0.1";
    //        port1 = 8898;
    //        port2 = 8899;

    //        while (running()) ;

    //        Console.ReadKey();
    //    }


    //    static bool running()
    //    {
    //        int txtNum;
    //        //Console.WriteLine("输入最大连接数");
    //        //txtNum = int.Parse(Console.ReadLine());

    //        txtNum = 2000;

    //        client = new UdpClient(port1, port2, serverIp, txtNum);

    //        for (int i = 0; i < txtNum; i++)
    //        {
    //            client.Start();

    //            Thread.Sleep(30);
    //        }

    //        return txtNum > 0;
    //    }
    //}
}
