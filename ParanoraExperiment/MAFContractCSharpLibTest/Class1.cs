using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAFContractCSharpLibTest
{
    [AddInContract]
    public interface IContract1 : IContract
    {
        string Operate(string param);
    }
}
