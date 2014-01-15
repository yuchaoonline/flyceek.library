using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Enquiry:IDAL.IEnquiry
    {
        public int Add(Model.Enquiry model)
        {
            return new DAL.Db.Enquiry().Add(model);
        }
    }
}
