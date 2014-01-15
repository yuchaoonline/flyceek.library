using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Model
{
    /// <summary>
    /// TagMkt:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TagMkt
    {
        public TagMkt()
        { }
        #region Model
        private string _scpmkt;
        private Guid _tagcommoncode;
        private string _tag;
        private string _tagcategory;
        private int? _tagcount;
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
        public int? TagCount
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

    }
}
