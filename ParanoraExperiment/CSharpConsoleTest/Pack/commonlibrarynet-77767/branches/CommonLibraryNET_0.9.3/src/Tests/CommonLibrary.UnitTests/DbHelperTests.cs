using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;


using NUnit.Framework;
using ComLib;
using ComLib.Database;


namespace CommonLibrary.Tests
{
    //[TestFixture]
    public class DbHelperTests
    {
        [Test]
        public void CanGetTable()
        {
            DBHelper helper = new DBHelper(ConnectionInfo.Default2);
            DataTable table = helper.ExecuteDataTable("select * from wk_adminqueries", System.Data.CommandType.Text);

            Assert.IsNotNull(table);
        }


        [Test]
        public void CanGetScalar()
        {
            DBHelper helper = new DBHelper(ConnectionInfo.Default2);
            object totalRecords = helper.ExecuteScalar("select count(*) from wk_adminqueries", System.Data.CommandType.Text);

            Assert.AreEqual(8, totalRecords);
        }


        [Test]
        public void CanUpdateData()
        {
            DBHelper helper = new DBHelper(ConnectionInfo.Default2);
            object maxId = helper.ExecuteScalar("select max(id) from wk_adminqueries", System.Data.CommandType.Text);
            int rowsAffected = helper.ExecuteNonQuery("update wk_adminqueries set description = 'unit test update' where id = " + maxId, CommandType.Text);

            Assert.AreEqual(1, rowsAffected);
        }


        [Test]
        public void Can1()
        {
            DBHelper helper = new DBHelper(ConnectionInfo.Default2);
            DbParameter[] dbparams = new DbParameter[2];
            dbparams[0] = helper.BuildInParam("@groupid", DbType.Int32, 1);
            dbparams[1] = helper.BuildInParam("@name", DbType.String, "aboutus");
            DataTable table = helper.ExecuteDataTable("Kd_PageContent_Retrieve", CommandType.StoredProcedure, dbparams);
            string content = table.Rows[0]["Description"].ToString();
            Assert.IsNotNull(content);
         
        }
    }
}
