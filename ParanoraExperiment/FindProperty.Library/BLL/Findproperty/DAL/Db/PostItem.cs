using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Findproperty.IDAL;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class PostItem : IPostItem
    {
        public List<ViewModel.PostItem> SelectPostItem(string postId)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(postId))
            {
                where += " and id='" + postId + "'";
            }
            return SelectPostItemWhere(where);
        }

        private List<ViewModel.PostItem> SelectPostItemWhere(string where)
        {
            string sql = "SELECT * FROM pub.PostItem where ";
            sql += where;

            List<ViewModel.PostItem> result = new List<ViewModel.PostItem>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.PostItem>().Convert(ds.Tables[0]);
            }

            return result;
        }
    }
}
