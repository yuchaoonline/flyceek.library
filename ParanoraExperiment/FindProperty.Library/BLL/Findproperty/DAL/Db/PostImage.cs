using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class PostImage:IDAL.IPostImage
    {
        public List<ViewModel.PostImage> SelectPostImage(string postId)
        {
            string where = " 1=1";
            if (!string.IsNullOrEmpty(postId))
            {
                where += " and id='" + postId + "'";
            }

            return SelectPostImageWhere(where);
        }

        private List<ViewModel.PostImage> SelectPostImageWhere(string where)
        {
            string sql = "SELECT * FROM pub.PostImage where ";
            sql += where;

            List<ViewModel.PostImage> result = new List<ViewModel.PostImage>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.PostImage>().Convert(ds.Tables[0]);
            }

            return result;
        }
    }
}
