using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
    public class PostDetail
    {
        public Guid ID { get; set; }
        private string _id;
        public Guid PosId{get;set;}
		private string _cuntcode;
		private DateTime? _cblgopdate;
		private string _cestcode;
		private string _cestcname;
		private string _bigestcode;
		private string _bigestcname;
             

        //private string _scpid;
        //private string _scpcname;
        private string _scpmktc;
        private string _scpmkt;
        private string _gscpid;
        private string _gscpcname;
		//private string _refno;
		//private string _caddress1;
		private string _caddress2;
		private string _cnestate;
        private string _cestate;

		//private string _cdisplay;
		//private string _cpropertytype;
		//private string _propertytypetag;
		private string _cfloor;
		//private string _floortag;
		private string _cdirection;
		//private string _directiontag;
		private int? _sittingroomcount;
		private int _bedroomcount;
		private int? _toiletcount;
        //private int? _balconycount;
        //private int? _ensuitecount;
		private string _posttype;
        /// <summary>
        /// 笋盘
        /// </summary>
		private bool _selection;
		private bool _soleagent;

        private decimal? _price;
		private bool? _withlease;
        private decimal? _rental;
		private string _agentname;
		private string _agentcname;
		private string _agentno;

		private string _agentmobile;
		private string _thumbpath;
        private DateTime _createdate;
        private DateTime _updatedate;
        //private bool _follow;

        private int? balconycount;
        private int? ensuiteCount;


        public int? BalconyCount
        {
            get { return balconycount; }
            set { balconycount = value; }
        }
        public int? EnsuiteCount
        {
            get { return ensuiteCount; }
            set { ensuiteCount = value; }
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string RefNo
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Characteristic { get; set; }
        public string CCharacteristic { get; set; }
        public string BranchCName { get; set; }
        public string ManagerNo { get; set; }
        public string ManagerMobile { get; set; }
        public string ManagerCName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string Title { get; set; }

        public List<ViewModel.PostImage> Images { get; set; }
        public List<ViewModel.PostItem> Items { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string cuntcode
		{
			set{ _cuntcode=value;}
			get{return _cuntcode;}
		}
		
		/// <summary>
		/// 
		/// </summary>
       [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime? cblgOpDate
		{
			set{ _cblgopdate=value;}
			get{return _cblgopdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cestcode
		{
			set{ _cestcode=value;}
			get{return _cestcode;}
		}
       
		public string cestCName
		{
			set{ _cestcname=value;}
			get{return _cestcname;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string bigestcode
		{
			set{ _bigestcode=value;}
			get{return _bigestcode;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string bigestCName
		{
			set{ _bigestcname=value;}
			get{return _bigestcname;}
		}

        public string CEstate
		{
			set{ _cestate=value;}
			get{return _cestate;}
		}
        /// <summary>
        /// 
        /// </summary>
        //public string RefNo
        //{
        //    set { _refno = value; }
        //    get { return _refno; }
        //}
		
		/// <summary>
		/// 
		/// </summary>
        //public string scpID
        //{
        //    set{ _scpid=value;}
        //    get{return _scpid;}
        //}
		
        ///// <summary>
        ///// 
        ///// </summary>
        //public string scpCName
        //{
        //    set{ _scpcname=value;}
        //    get{return _scpcname;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        public string scpMkt
        {
            set{ _scpmkt=value;}
            get{return _scpmkt;}
        }

        public string scpMktc
        {
             set{ _scpmktc=value;}
            get{return _scpmktc;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string gscpID
        {
            set{ _gscpid=value;}
            get{return _gscpid;}
        }

        /// <summary>
        /// 
        /// </summary>
        public string gscpCName
        {
            set{ _gscpcname=value;}
            get{return _gscpcname;}
        }
		
		
        ///// <summary>
        ///// 
        ///// </summary>
        //public string CAddress1
        //{
        //    set{ _caddress1=value;}
        //    get{return _caddress1;}
        //}
		/// <summary>
		/// 
		/// </summary>
		public string CAddress2
		{
			set{ _caddress2=value;}
			get{return _caddress2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CnEstate
		{
			set{ _cnestate=value;}
			get{return _cnestate;}
		}
		
        ///// <summary>
        ///// 
        ///// </summary>
        //public string CDisplay
        //{
        //    set{ _cdisplay=value;}
        //    get{return _cdisplay;}
        //}
		
        ///// <summary>
        ///// 
        ///// </summary>
        //public string CPropertyType
        //{
        //    set{ _cpropertytype=value;}
        //    get{return _cpropertytype;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string PropertyTypeTag
        //{
        //    set{ _propertytypetag=value;}
        //    get{return _propertytypetag;}
        //}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string CFloor
		{
			set{ _cfloor=value;}
			get{return _cfloor;}
		}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string FloorTag
        //{
        //    set{ _floortag=value;}
        //    get{return _floortag;}
        //}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string CDirection
		{
			set{ _cdirection=value;}
			get{return _cdirection;}
		}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string DirectionTag
        //{
        //    set{ _directiontag=value;}
        //    get{return _directiontag;}
        //}
		
		/// <summary>
		/// 
		/// </summary>
        public decimal Size
        {
            set;
            get;
        }
		
		/// <summary>
		/// 
		/// </summary>
		public int? SittingRoomCount
		{
			set{ _sittingroomcount=value;}
			get{return _sittingroomcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BedroomCount
		{
			set{ _bedroomcount=value;}
			get{return _bedroomcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ToiletCount
		{
			set{ _toiletcount=value;}
			get{return _toiletcount;}
		}
        ///// <summary>
        ///// 
        ///// </summary>
        //public int? BalconyCount
        //{
        //    set{ _balconycount=value;}
        //    get{return _balconycount;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public int? EnsuiteCount
        //{
        //    set{ _ensuitecount=value;}
        //    get{return _ensuitecount;}
        //}
		/// <summary>
		/// 
		/// </summary>
		public string PostType
		{
			set{ _posttype=value;}
			get{return _posttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Selection
		{
			set{ _selection=value;}
			get{return _selection;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool SoleAgent
		{
			set{ _soleagent=value;}
			get{return _soleagent;}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
        public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public bool? WithLease
		{
			set{ _withlease=value;}
			get{return _withlease;}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
        public decimal? Rental
		{
			set{ _rental=value;}
			get{return _rental;}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string AgentName
		{
			set{ _agentname=value;}
			get{return _agentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentCName
		{
			set{ _agentcname=value;}
			get{return _agentcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AgentNo
		{
			set{ _agentno=value;}
			get{return _agentno;}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string AgentMobile
		{
			set{ _agentmobile=value;}
			get{return _agentmobile;}
		}
       
		/// <summary>
		/// 
		/// </summary>
		public string ThumbPath
		{
			set{ _thumbpath=value;}
			get{return _thumbpath;}
		}
        /// <summary>
        /// 
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
       [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool Follow
        //{
        //    set{ _follow=value;}
        //    get{return _follow;}
        //}
		
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool RotatedIn
        //{
        //    set;
        //    get;
        //}
        public int? post_counter { get; set; }
        public int? unit_price { get; set; }
        public string BranchCostCent { get; set; }

        public string CAddress1 { get; set; }
        public double Agentscore { get; set; }
        public string AgentEmail { get; set; }
    }
}
