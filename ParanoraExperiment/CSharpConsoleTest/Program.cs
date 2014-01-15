using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Text.RegularExpressions;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Reflection.Emit;

namespace ConsoleAppTest
{
    interface IUser
    {
        void say();
        void write();
    }

    class Program
    {
        private static Mutex _m = new Mutex();
        private static Queue<string> queue = new Queue<string>();
        private static Mutex mutex = new Mutex();

        private delegate void HelloWorldDelegate();
        
        static void Main(string[] args)
        {
            //EmitHelloWorldTest();

            //string regStr=@"\/esf\/(\S)+\/(\S)+\/(\S)*";

            //MatchCollection mc = Regex.Matches(@"/esf/sslkdfsd/ljm1lk23/lsd", regStr, RegexOptions.IgnoreCase);

            //foreach (Match m in mc)
            //{

            //}
            string reg = @"^[A-Za-z]+[0-9]+$";
            string str = "zhongyuanliangwancheng";
            Match m = Regex.Match(str, reg, RegexOptions.IgnoreCase);

            Console.ReadKey();
        }

        static void Test1()
        {
            string reg = @"((esf)|(zf))\/([A-Za-z\/]+)?([A-Za-z0-9\/]+)?";

            string houseSaleListReg1 = @"^((esf)|(zf))\/[A-Za-z]+\/[A-Za-z0-9]+\/";

            string houseSaleListReg2 = @"^((esf)|(zf))\/[A-Za-z0-9]+\/";

            string houseSaleListReg3 = @"^((esf)|(zf))\/(g\d+)?(m\d+)?[A-Za-z0-9]*\/";

            string houseSaleListReg4 = @"^((esf)|(zf))\/(([A-Za-z]+)|(((g\d+)?(m\d+)?)+[a-zA-Z0-9]*))\/";

            Match m = Regex.Match(@"zf///d1///", houseSaleListReg4, RegexOptions.IgnoreCase);


            string url = "zf/d1";

            m = Regex.Match(@"d1", @"^(g\d+)?(m\d+)?[A-Za-z0-9]*$", RegexOptions.IgnoreCase);

            m = Regex.Match(@"d1", @"^[A-Za-z]+$", RegexOptions.IgnoreCase);

            string result = m.Value;
        }

        static void EmitHelloWorldTest()
        {
            var asmName = new AssemblyName("Test");

            var asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                asmName,
                AssemblyBuilderAccess.RunAndSave);

            var mdlBldr = asmBuilder.DefineDynamicModule("Main", "Main.exe");
            var typeBldr = mdlBldr.DefineType("Hello", TypeAttributes.Public);
            var methodBldr = typeBldr.DefineMethod(
                "SayHello",
                MethodAttributes.Public | MethodAttributes.Static,
                null,//return type
                null//parameter type
                );

            var il = methodBldr.GetILGenerator();//获取il生成器
            il.Emit(OpCodes.Ldstr, "Hello, World");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            il.Emit(OpCodes.Pop);//读入的值会被推送至evaluation stack，而本方法是没有返回值的，因此，需要将栈上的值抛弃
            il.Emit(OpCodes.Ret);

            var t = typeBldr.CreateType();
            asmBuilder.SetEntryPoint(t.GetMethod("SayHello"));
            asmBuilder.Save("Main.exe");
        }

        static void MainTest1()
        {
            //定义一个名为HelloWorld的动态方法，没有返回值，没有参数
            DynamicMethod helloWorldMethod = new DynamicMethod("HelloWorld", null, null);

            //创建一个MSIL生成器，为动态方法生成代码
            ILGenerator helloWorldIL = helloWorldMethod.GetILGenerator();

            //将要输出的Hello World!字符创加载到堆栈上
            helloWorldIL.Emit(OpCodes.Ldstr, "Hello World!");
            //调用Console.WriteLine(string)方法输出Hello World!
            helloWorldIL.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            //方法结束，返回
            helloWorldIL.Emit(OpCodes.Ret);

            //完成动态方法的创建，并且获取一个可以执行该动态方法的委托
            HelloWorldDelegate HelloWorld = (HelloWorldDelegate)helloWorldMethod.CreateDelegate(typeof(HelloWorldDelegate));

            //执行动态方法，将在屏幕上打印Hello World!
            HelloWorld();
        }

        
        



        public static void TestFun2()
        {
            TypeCreator tc = new TypeCreator(typeof(IUser));
            Type t = tc.build();
            IUser user = (IUser)Activator.CreateInstance(t);
            user.say();
            user.write();
            Console.ReadKey();
        }

        public static void TestFun1()
        {
            //string httpRequestHead = "GET http://sh.centanet.com/ HTTP/1.1\r\nAccept: text/html, application/xhtml+xml, */*\r\nAccept-Language: zh-CN\r\nUser-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)\r\nAccept-Encoding: gzip, deflate\r\nProxy-Connection: Keep-Alive\r\nHost: sh.centanet.com\r\nPragma: no-cache\r\n\r\n";

            //Regex reg = new Regex("((GET)|(POST))");

            //Match match = reg.Match(httpRequestHead);
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            Thread thread1 = new Thread(new ThreadStart(Fun1));
            Thread thread2 = new Thread(new ThreadStart(Fun2));
            Thread thread3 = new Thread(new ThreadStart(Fun3));

            //thread1.Start();
            //thread2.Start();
            //thread3.Start();

            //thread1.Join();
            //thread2.Join();

            //Thread thread1 = new Thread(new ThreadStart(thread1Func));
            //Thread thread2 = new Thread(new ThreadStart(thread2Func));
            //thread1.Start();
            //thread2.Start();

            string key = "db626011a15b450485acc692da57151f";

            string dk = DesEncrypt("123456", key);
        }

        public static string DesEncrypt(string srcString, string key)
        {
            if ((key == null) || (key.Length < 8))
            {
                key = key + "00000000";
            }
            key = key.Substring(0, 8);
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = bytes;
            byte[] buffer = Encoding.UTF8.GetBytes(srcString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }

        private static void thread1Func()
        {
            for (int count = 0; count < 10; count++)
            {
                mutex.WaitOne();
                TestFunc("Thread1 have run " + count.ToString() + " times");
                mutex.ReleaseMutex();
            }
        }

        private static void thread2Func()
        {
            for (int count = 0; count < 10; count++)
            {
                mutex.WaitOne();
                TestFunc("Thread2 have run " + count.ToString() + " times");
                mutex.ReleaseMutex();
            }
        }

        private static void TestFunc(string str)
        {
            Console.WriteLine("{0} {1}", str, System.DateTime.Now.Millisecond.ToString());
            Thread.Sleep(50);
        }

        public static void Fun1()
        {
            _m.WaitOne();
            for (var i = 0; i < 10; i++)
            {
                
                queue.Enqueue(i.ToString());
                Console.WriteLine("write thread: {0}, total item:{1},current item text:{2}", Thread.CurrentThread.ManagedThreadId, queue.Count,i.ToString());
              
            }
            
            _m.ReleaseMutex();
            Thread.Sleep(1);
        }

        public static void Fun3()
        {
            _m.WaitOne();
            while (queue.Count() > 0)
            {
                
                Console.WriteLine("read thread: {0}, total item:{1},current item text:{2}", Thread.CurrentThread.ManagedThreadId, queue.Count, queue.Dequeue());
                
                Thread.Sleep(1);
            }
            _m.ReleaseMutex();
        }

        public static void Fun2()
        { 
            _m.WaitOne();
            while (queue.Count() > 0)
            {               
                Console.WriteLine("read thread: {0}, total item:{1},current item text:{2}",Thread.CurrentThread.ManagedThreadId, queue.Count, queue.Dequeue());
              
                Thread.Sleep(1);
            }
            _m.ReleaseMutex();
            
        }

    }

    
    public class Foo: IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Release managed resources
                }
  
                // Release unmanaged resources
  
                m_disposed = true;
            }
        }
  
        ~Foo()
        {
            Dispose(false);
        }
  
        private bool m_disposed;
    }
}
