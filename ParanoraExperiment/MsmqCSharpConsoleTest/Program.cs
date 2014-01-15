using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Messaging;
using System.Threading;

namespace MSMQ_Test_001
{
    [Serializable]
    public class VideoPath
    {
        public string SourceFilePath{get;set;}
        public string TargetFilePath { get; set; }

        public VideoPath()
        {
        }

        public VideoPath(string sourceFilePath, string targetFilePath)
        {
            this.SourceFilePath = sourceFilePath;
            this.TargetFilePath = targetFilePath;
        }
    }

    class Program
    {
        public static string path = @".\private$\MyMSMQ_Test_001";

        static void Main(string[] args)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
            //SendMq();
            //ReciveMq();

            //异步接收ms
            //AsynReceiveCallBak();

            



            Console.ReadKey();
        }

        static void Fun1()
        {
            int tc = 100;
            Thread[] threads = new Thread[tc];

            for (int i = 0; i < tc; i++)
            {
                threads[i] = new Thread(delegate()
                {
                    MessageQueue MQ = new MessageQueue(path);

                    for (var n = 0; n < 100; n++)
                    {
                        Message message = new Message();
                        message.Label = "消息" + i.ToString() + "_" + n.ToString();
                        message.Body = "消息" + i.ToString() + "_" + n.ToString();

                        MQ.Send(message);
                        //Console.WriteLine("消息" + i.ToString() + "_" + n.ToString());
                    }
                });
                threads[i].Start();
            }
        }

        static void ReciveMq()
        {
            MessageQueue MQ = new MessageQueue(path);
            Message message = MQ.Receive(TimeSpan.FromSeconds(5));

            if (message != null)
            {
                message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[]{typeof(VideoPath)});
                VideoPath Vpath = (VideoPath)message.Body;
                Console.Write(Vpath.TargetFilePath);
            }
            else
            {
                Console.Write("没有找到消息！");
            }
        }
        static void SendMq()
        {
            MessageQueue MQ = new MessageQueue(path);

            VideoPath VPath = new VideoPath("a", "b");
            for (var i = 10; i < 100; i++)
            {
                Message message = new Message();
                message.Label = "消息"+i.ToString();
                message.Body = VPath;

                MQ.Send(message);
                Console.Write(message.Id);
            }
        }

        static ManualResetEvent signal = new ManualResetEvent(false);
        static Mutex mtx1 = new Mutex(false);

        static void AsyncReceiveMessage()
        {
            MessageQueue myQueue = new MessageQueue(path);
            //if (myQueue.Transactional)
            //{
                //MessageQueueTransaction myTransaction = new MessageQueueTransaction();
                //这里使用了委托,当接收消息完成的时候就执行MyReceiveCompleted方法
                myQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MyReceiveCompleted);
                myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(VideoPath) });
                //myTransaction.Begin();
                myQueue.BeginReceive();//启动一个没有超时时限的异步操作
                signal.WaitOne();
                //myTransaction.Commit();
            //}
        }

        static void MyReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            try
            {
                MessageQueue myQueue = (MessageQueue)source;
                //完成指定的异步接收操作
                Message message = myQueue.EndReceive(asyncResult.AsyncResult);
                signal.Set();
                VideoPath book = message.Body as VideoPath;
                Console.WriteLine("参数1：{0},参数2：{1}",
                    book.SourceFilePath,
                    book.TargetFilePath);
                //myQueue.BeginReceive();
            }
            catch (MessageQueueException me)
            {
                Console.WriteLine("异步接收出错,原因：" + me.Message);

            }
        } 

        static void AsynReceiveCallBak()
        {
            try
            {
                //this.MessageType=msgType;
                MessageQueueTransaction myTransaction = new MessageQueueTransaction();
                MessageQueue anycMQ = new MessageQueue(path);
                anycMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(VideoPath) });
                AsyncCallback cb = new AsyncCallback (callback);
                
                IAsyncResult asyncResult=anycMQ.BeginReceive(new TimeSpan(0,0,5,0),DateTime.Now,cb);
                //return MQ_OutBox;
            }
            catch(Exception e)
            {
                throw;
            }
        }
 
        static void callback(IAsyncResult handle)
        {
            MessageQueue anycMQ = new MessageQueue(path);
            
            Message msg=anycMQ.EndReceive(handle);
            try
            {
                VideoPath video = msg.Body as VideoPath;
                Console.WriteLine(video.SourceFilePath);
            }
            catch
            {
                throw;
            }
        }
    }
}
