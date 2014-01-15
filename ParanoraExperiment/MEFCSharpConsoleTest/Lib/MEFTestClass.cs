using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFCSharpConsoleTest.Lib
{
    public interface IBookService
    {
        void GetBookName();
    }

    public interface ISay
    {
        void SayHello();
    }

    
    public class MySay : ISay
    {

        public void SayHello()
        {
            Console.WriteLine("I am Paranora.");
        }
    }
    [Export(typeof(ISay))]
    public class SheSay : ISay
    {

        public void SayHello()
        {
            Console.WriteLine("She is my welf.");
        }
    }

    [Export(typeof(IBookService))]
    public class ComputerBookService : IBookService
    {
        public void GetBookName()
        {
            Console.WriteLine("Hello MEF.");
        }
    }

    public class MEFTestClass
    {
        [Import]
        public IBookService Service { get; set; }

        [Import]
        public ISay SayOperate { get; set; }

        public void Test()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
