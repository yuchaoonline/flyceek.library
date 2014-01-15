using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.ViewModel
{
    /// <summary>
	/// TagGscp:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class TagGscp
    {
        public TagGscp()
        { }
        #region Model
        private string _scpmkt;
        private string _gscpid;
        private Guid _tagcommoncode;
        private string _tag;
        private string _tagcategory;
        private decimal? _tagcount;
        private long? _seq;
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
        public string gscpID
        {
            set { _gscpid = value; }
            get { return _gscpid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid TagCommonCode
        {
            set { _tagcommoncode = value; }
            get { return _tagcommoncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag
        {
            set { _tag = value; }
            get { return _tag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TagCategory
        {
            set { _tagcategory = value; }
            get { return _tagcategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TagCount
        {
            set { _tagcount = value; }
            get { return _tagcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? seq
        {
            set { _seq = value; }
            get { return _seq; }
        }
        #endregion Model

        public MongoDB.Bson.ObjectId _id { get; set; }
    }
}
