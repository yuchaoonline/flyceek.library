using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.ResultExaminer
{
    public interface IResultExaminer
    {
        bool IsNullOrEmpty(object v);
    }
}
