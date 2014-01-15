using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.ViewModel
{
    [Serializable]
    public class TagMain
    {
        public TagMain()
        { }
        #region Model

        private Guid _tagcode;
        private string _tag;
        private int _taghitcount;
        private Guid _tagcommoncode;
        private string _tagcommon;
        private string _tagpysx;
        private string _tagpy;
        private string _tchinese;
        private int _aboutnum;
        private string _tagcategory;
        private int _categoryrank;
        private int _tagrank;
        private int? _snum;
        private int? _rnum;
        /// <summary>
        /// 
        /// </summary>
        public Guid TagCode
        {
            set { _tagcode = value; }
            get { return _tagcode; }
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
        public int TagHitCount
        {
            set { _taghitcount = value; }
            get { return _taghitcount; }
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
        public string TagCommon
        {
            set { _tagcommon = value; }
            get { return _tagcommon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TagPYSX
        {
            set { _tagpysx = value; }
            get { return _tagpysx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TagPY
        {
            set { _tagpy = value; }
            get { return _tagpy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TChinese
        {
            set { _tchinese = value; }
            get { return _tchinese; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AboutNum
        {
            set { _aboutnum = value; }
            get { return _aboutnum; }
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
        public int CategoryRank
        {
            set { _categoryrank = value; }
            get { return _categoryrank; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TagRank
        {
            set { _tagrank = value; }
            get { return _tagrank; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SNum
        {
            set { _snum = value; }
            get { return _snum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RNum
        {
            set { _rnum = value; }
            get { return _rnum; }
        }
        #endregion Model

        public MongoDB.Bson.ObjectId _id { get; set; }
    }
}
