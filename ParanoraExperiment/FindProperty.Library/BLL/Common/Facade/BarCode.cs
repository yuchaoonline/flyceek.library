using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using System.Drawing;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class BarCode
    {
        private readonly IBarCode dal = DataAccessFactoryCreator.Create().BarCode();

        public byte[] CreateCode(string content)
        {
            return dal.CreateCode(content);
        }
        public byte[] CreateCode(int size, Brush darkBrush, Brush lightBrush, string content)
        {
            return dal.CreateCode(size, darkBrush, lightBrush, content);
        }
    }
}
