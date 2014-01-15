using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IBarCode
    {
        byte[] CreateCode(string content);
        byte[] CreateCode(int size, Brush darkBrush, Brush lightBrush, string content);
    }
}
