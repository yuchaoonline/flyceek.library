using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingInterface
{
    public interface IAdder
    {
        double Add(double a, double b);
    }

    public interface IFatory
    {
        IAdder BuilderANewAdder();
    }
}
