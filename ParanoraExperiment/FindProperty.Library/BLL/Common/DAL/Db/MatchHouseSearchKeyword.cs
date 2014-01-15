using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace FindProperty.Lib.BLL.Common.DAL.Db
{
    public class MatchHouseSearchKeyword:IDAL.IMatchHouseSearchKeyword
    {
        public ViewModel.MatchHouseSearchKeyword Match(string keyWord, string ip, string session)
        {
            ViewModel.MatchHouseSearchKeyword match = new ViewModel.MatchHouseSearchKeyword();

            Database db = DbContextFactory.SHTagToSalesBlog;
            DbCommand dbc = db.GetStoredProcCommand("USP_GetSearchConditionIncludeTag");
            db.AddInParameter(dbc, "@Tag", DbType.String, keyWord);
            db.AddInParameter(dbc, "@IP", DbType.String, ip);
            db.AddInParameter(dbc, "@Session", DbType.String, session);
            DataSet ds = db.ExecuteDataSet(dbc);

            #region macth
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string value = dr["searchValue"].ToString();
                switch (dr["searchType"].ToString().ToLower())
                {
                    case "area":
                        if (value != "")
                        {
                            match.ScopeMkt = value;
                            match.Code = value;
                            match.Type = "22";
                        }
                        break;
                    case "minprice":
                        break;
                    case "maxprice":
                        break;
                    case "maxsize":
                        break;
                    case "minsize":
                        break;
                    case "unitprice":
                        break;
                    case "housetypes":
                        break;
                    case "housetypet":
                        break;
                    case "housetypew":
                        break;
                    case "housetypey":
                        break;
                    case "address":
                        if (value != null && value != "")
                            match.KeyWord = value;
                        break;
                    case "refno":
                        if (value != null && value != "")
                        {
                            match.RefNo = value;
                        }
                        break;

                    case "agentmobile":
                        if (value != null && value != "")
                        {
                            match.AgentMobile = value;
                        }
                        break;
                    case "agentcname":
                        if (value != null && value != "")
                        {
                            match.AgentCName = value;
                        }
                        break;
                    case "buyrent":
                        //TODO: 
                        break;
                }
            }
            #endregion

            List<String> list = new List<string>();
            String newKeyword = "";
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                String value = dr["TagCommonCode"].ToString();
                if (value != null && value != "")
                {
                    list.Add(value);
                }
                else
                {
                    newKeyword += dr["Tag"].ToString() + " ";
                }

            }

            match.KeyWord = newKeyword.Trim();
            //}

            if (list.Count > 0)
            {
                match.TagCommonCodes = list;
            }

            return match;;
        }
    }
}
