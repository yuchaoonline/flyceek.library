using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.BLL.Common.Model
{
    [Serializable]
    public partial class AgentHitCount
    {
        public AgentHitCount()
        { }
        #region Model
        private string _agentno;
        private string _agentname;
        private string _agentcname;
        private string _clientip;
        private string _sessionid;
        private DateTime _dateaccess=DateTime.Now;
        private string _agentlicense;
        private string _scpid="NA";
        private string _scpmkt="NA";
        private string _source="F";
        /// <summary>
        /// 
        /// </summary>
        public string AgentNo
        {
            set { _agentno = value; }
            get { return _agentno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AgentName
        {
            set { _agentname = value; }
            get { return _agentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AgentCName
        {
            set { _agentcname = value; }
            get { return _agentcname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientip
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SessionID
        {
            set { _sessionid = value; }
            get { return _sessionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime dateAccess
        {
            set { _dateaccess = value; }
            get { return _dateaccess; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AgentLicense
        {
            set { _agentlicense = value; }
            get { return _agentlicense; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string scpID
        {
            set { _scpid = value; }
            get { return _scpid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string scpMkt
        {
            set { _scpmkt = value; }
            get { return _scpmkt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string source
        {
            set { _source = value; }
            get { return _source; }
        }
        #endregion Model
        public string ToInsertString()
        {
            var str = new StringBuilder();
            var type = this.GetType();

            str.Append(string.Format(@"INSERT INTO dbo.{0} (", type.Name));

            var query = from p in type.GetProperties()
                        select p;
            foreach (var p in query)
            {
                str.Append(string.Format("{0},", p.Name));
            }
            str.Remove(str.Length - 1, 1);
            str.Append(") VALUES (");
            foreach (var p in query)
            {
             
                if (p.PropertyType.Name == "DateTime")
                    str.Append(string.Format("'{0:yyyy-MM-dd HH:mm:ss.fff}',", (DateTime)p.GetValue(this, null)));
                else
                    str.Append(string.Format("'{0}',", p.GetValue(this, null)));
            }
            str.Remove(str.Length - 1, 1);
            str.Append(");");
            return str.ToString();
        }
    }
}
