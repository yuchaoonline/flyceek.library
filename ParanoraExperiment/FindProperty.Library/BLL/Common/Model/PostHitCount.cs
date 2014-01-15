using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.BLL.Common.Model
{
    [Serializable]
    public class PostHitCount
    {
        public PostHitCount()
        { }
        #region Model
        private Guid _id;
        private string _cuntcode;
        private string _cestcode;
        private string _costcent=string.Empty;
        private decimal? _price;
        private decimal? _rental;
        private string _website="L";
        private string _session;
        private DateTime _hittime=DateTime.Now;
        private string _agentno;
        private string _ip;
        private string _operation;
        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string cuntcode
        {
            set { _cuntcode = value; }
            get { return _cuntcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cestcode
        {
            set { _cestcode = value; }
            get { return _cestcode; }
        }
        /// <summary>
        /// 可以不赋值
        /// </summary>
        public string CostCent
        {
            set { _costcent = value; }
            get { return _costcent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Rental
        {
            set { _rental = value; }
            get { return _rental; }
        }
        /// <summary>
        /// 可以不赋值
        /// </summary>
        public string WebSite
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Session
        {
            set { _session = value; }
            get { return _session; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime HitTime
        {
            set { _hittime = value; }
            get { return _hittime; }
        }
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
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 租售类型
        /// </summary>
        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        #endregion Model
        public string ToInsertString()
        {
            var str = new StringBuilder();
            var type = this.GetType();

            str.Append(string.Format(@"INSERT INTO pub.{0} (", type.Name));

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
                if (p.PropertyType.Name == "Int32")
                    str.Append(string.Format("{0},", p.GetValue(this, null)));
                else if (p.PropertyType.Name == "Nullable`1")
                {
                    var value = p.GetValue(this, null);
                    if (value!=null)
                        str.Append(string.Format("{0},", p.GetValue(this, null)));
                    else
                        str.Append(string.Format("null,"));
                }
                else if (p.PropertyType.Name == "DateTime")
                    str.Append(string.Format("'{0:yyyy-MM-dd HH:mm:ss.fff}',", (DateTime)p.GetValue(this, null)));
                else
                    str.Append(string.Format("'{0}',", p.GetValue(this, null)));
            }
            str.Remove(str.Length - 1, 1);
            str.Append(");");
            return str.ToString();
        }
    }

    [Serializable]
    public class PostHitCountW
    {
        public PostHitCountW()
        { }
        #region Model
        private Guid _id;
        private string _cuntcode;
        private string _cestcode;
        private string _costcent = string.Empty;
        private decimal? _price;
        private decimal? _rental;
        private string _website = "L";
        private string _session;
        private DateTime _hittime = DateTime.Now;
        private string _agentno;
        private string _ClientIP;
      
        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Seq
        {
            get { return ID.ToString().GetHashCode(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cuntcode
        {
            set { _cuntcode = value; }
            get { return _cuntcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cestcode
        {
            set { _cestcode = value; }
            get { return _cestcode; }
        }
        /// <summary>
        /// 可以不赋值
        /// </summary>
        public string CostCent
        {
            set { _costcent = value; }
            get { return _costcent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Rental
        {
            set { _rental = value; }
            get { return _rental; }
        }
        /// <summary>
        /// 可以不赋值
        /// </summary>
        public string WebSite
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Session
        {
            set { _session = value; }
            get { return _session; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime HitTime
        {
            set { _hittime = value; }
            get { return _hittime; }
        }
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
        public string ClientIP
        {
            set { _ClientIP = value; }
            get { return _ClientIP; }
        }
       
        #endregion Model
        public string ToInsertString()
        {
            var str = new StringBuilder();
            var type = this.GetType();

            str.Append(string.Format(@"INSERT INTO dbo.{0} (", "PostHitCount"));

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
                if (p.PropertyType.Name == "Int32")
                    str.Append(string.Format("{0},", p.GetValue(this, null)));
                else if (p.PropertyType.Name == "Nullable`1")
                {
                    var value = p.GetValue(this, null);
                    if (value != null)
                        str.Append(string.Format("{0},", p.GetValue(this, null)));
                    else
                        str.Append(string.Format("null,"));
                }
                else if (p.PropertyType.Name == "DateTime")
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
