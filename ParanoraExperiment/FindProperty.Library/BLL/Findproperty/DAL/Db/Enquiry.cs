using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Enquiry:IDAL.IEnquiry
    {
        public int Add(Model.Enquiry model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into enquiry(");
            strSql.Append("name,email,contact,propertyId,detail,clientIp,AgentNo,enquirydate,RefNo,ManagerNo,scpID,scpMkt,source,cestcode,codeType)");
            strSql.Append(" values (");
            strSql.Append("@name,@email,@contact,@propertyId,@detail,@clientIp,@AgentNo,@enquirydate,@RefNo,@ManagerNo,@scpID,@scpMkt,@source,@cestcode,@codeType)");
            strSql.Append(";select @@IDENTITY");

            SqlCommand dbc = new SqlCommand(strSql.ToString());

            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NChar,50),
					new SqlParameter("@email", SqlDbType.NChar,50),
					new SqlParameter("@contact", SqlDbType.NChar,50),
					new SqlParameter("@propertyId", SqlDbType.NChar,50),
					new SqlParameter("@detail", SqlDbType.NChar,255),
					new SqlParameter("@clientIp", SqlDbType.NChar,50),
					new SqlParameter("@AgentNo", SqlDbType.NChar,10),
					new SqlParameter("@enquirydate", SqlDbType.DateTime),
					new SqlParameter("@RefNo", SqlDbType.NChar,50),
					new SqlParameter("@ManagerNo", SqlDbType.NChar,10),
					new SqlParameter("@scpID", SqlDbType.NVarChar,10),
					new SqlParameter("@scpMkt", SqlDbType.NVarChar,3),
					new SqlParameter("@source", SqlDbType.NVarChar,3),
					new SqlParameter("@cestcode", SqlDbType.NVarChar,50),
					new SqlParameter("@codeType", SqlDbType.NVarChar,2)};

            dbc.Parameters.AddRange(parameters);

            dbc.Parameters["@name"].Value = model.name;
            dbc.Parameters["@email"].Value = model.email;
            dbc.Parameters["@contact"].Value = model.contact;
            dbc.Parameters["@propertyId"].Value = model.propertyId;
            dbc.Parameters["@detail"].Value = model.detail;
            dbc.Parameters["@clientIp"].Value = model.clientIp;
            dbc.Parameters["@AgentNo"].Value = model.AgentNo;
            dbc.Parameters["@enquirydate"].Value = model.enquirydate;
            dbc.Parameters["@RefNo"].Value = model.RefNo;
            dbc.Parameters["@ManagerNo"].Value = model.ManagerNo;
            dbc.Parameters["@scpID"].Value = model.scpID;
            dbc.Parameters["@scpMkt"].Value = model.scpMkt;
            dbc.Parameters["@source"].Value = model.source;
            dbc.Parameters["@cestcode"].Value = model.cestcode;
            dbc.Parameters["@codeType"].Value = model.codeType;

            object obj = DbContextFactory.Findproperty.ExecuteScalar(dbc);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}
