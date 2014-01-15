using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Model
{
    /// <summary>
    /// RetailStore:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RetailStore
    {
        public RetailStore()
        { }
        #region Model
        private int _retailstoreid;
        private string _imgpath;
        private string _storename;
        private string _address;
        private string _firstcommunity;
        private decimal? _firstprice;
        private string _secondcommunity;
        private decimal? _secondprice;
        private string _linkurl;
        private DateTime? _logdate;
        /// <summary>
        /// 
        /// </summary>
        public int RetailStoreID
        {
            set { _retailstoreid = value; }
            get { return _retailstoreid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreName
        {
            set { _storename = value; }
            get { return _storename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstCommunity
        {
            set { _firstcommunity = value; }
            get { return _firstcommunity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FirstPrice
        {
            set { _firstprice = value; }
            get { return _firstprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SecondCommunity
        {
            set { _secondcommunity = value; }
            get { return _secondcommunity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SecondPrice
        {
            set { _secondprice = value; }
            get { return _secondprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LogDate
        {
            set { _logdate = value; }
            get { return _logdate; }
        }
        #endregion Model

    }
}
