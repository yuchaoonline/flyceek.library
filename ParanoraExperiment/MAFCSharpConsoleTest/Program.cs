using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAFCSharpConsoleTest.Lib;
using MAFHostViewCSharpLibTest;

namespace MAFCSharpConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            String addInRoot = Environment.CurrentDirectory;
            AddInStore.Update(addInRoot);

            Collection<AddInToken> tokens = AddInStore.FindAddIns(typeof(View1), addInRoot);

            AddInToken calcToken = tokens[0];

            AddInProcess process = new AddInProcess();
            process.KeepAlive = false;

            var o = calcToken.Activate<View1>(process, AddInSecurityLevel.Internet);

            Console.WriteLine(o.Operate("Flyceek is come!"));

            Console.ReadKey();
        }
    }
}

