
using System;
using System.Collections.Generic;
namespace FindProperty.Lib.BLL.Findproperty.Model
{
	/// <summary>
	/// Post:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Post
	{
		public Post()
		{}
		#region Model
        private Guid _id;
		private string _cuntcode;
		private string _cblgcode;
		private string _cblgname;
		private string _cblgcname;
		private DateTime? _cblgopdate;
		private string _cestcode;
		private string _cestname;
		private string _cestcname;
		private string _cestdev;
		private string _cestcdev;
		private string _bigestcode;
		private string _bigestname;
		private string _bigestcname;
		private string _scpid;
		private string _scpname;
		private string _scpcname;
		private string _scpmkt;
		private string _gscpid;
		private string _gscpname;
		private string _gscpcname;
		private string _gscpmkt;
		private string _refno;
		private bool _luxury;
		private int _displaystyle;
		private bool _showmap;
		private string _estate;
		private string _address1;
		private string _address2;
		private string _cestate;
		private string _caddress1;
		private string _caddress2;
		private string _cnestate;
		private string _cnaddress;
		private string _display;
		private string _cdisplay;
		private string _propertytype;
		private string _cpropertytype;
		private string _propertytypetag;
		private decimal? _propertytypesearchkey;
		private string _floor;
		private string _cfloor;
		private string _floortag;
		private decimal? _floorsearchkey;
		private string _flat;
		private string _cflat;
		private bool _apartment;
		private string _basicinfo1;
		private string _cbasicinfo1;
		private string _basicinfo1tag;
		private decimal? _basicinfo1searchkey;
		private string _basicinfo2;
		private string _cbasicinfo2;
		private string _basicinfo2tag;
		private decimal? _basicinfo2searchkey;
		private string _basicinfo3;
		private string _cbasicinfo3;
		private string _basicinfo3tag;
		private decimal? _basicinfo3searchkey;
		private string _basicinfo4;
		private string _cbasicinfo4;
		private string _basicinfo4tag;
		private decimal? _basicinfo4searchkey;
		private string _direction;
		private string _cdirection;
		private string _directiontag;
		private decimal? _directionsearchkey;
		private string _characteristic;
		private string _ccharacteristic;
		private decimal _size;
		private bool _sizeapprox;
		private int? _sittingroomcount;
		private int _bedroomcount;
		private int? _toiletcount;
		private int? _balconycount;
		private int? _ensuitecount;
		private string _posttype;
		private bool _selection;
		private bool _soleagent;
		private string _selltype;
		private string _cselltype;
		private string _selltypetag;
		private decimal? _selltypesearchkey;
		private decimal? _price;
		private bool _mortgagee;
		private bool _withlease;
		private decimal? _withleaserental;
		private string _homeownership;
		private decimal? _homeownershipprice;
		private decimal? _rental;
		private bool _rentalinclu;
		private decimal? _management;
		private bool _managementinclu;
		private decimal? _govrate;
		private bool _govrateinclu;
		private decimal? _govrent;
		private bool _govrentinclu;
		private string _title;
		private string _environment;
		private string _transportation;
		private string _education;
		private string _business;
		private string _entertainment;
		private string _agentname;
		private string _agentcname;
		private string _agentno;
		private string _agentlicense;
		private string _agentmobile;
		private string _agentemail;
		private bool _agentportrait;
		private string _managername;
		private string _managercname;
		private string _managerno;
		private string _managerlicense;
		private string _managermobile;
		private string _manageremail;
		private bool _managerportrait;
		private string _branchcostcent;
		private string _branchname;
		private string _branchcname;
		private string _branchphone;
		private string _nightphone;
		private string _thumbpath;
		private DateTime _createdate;
		private DateTime _updatedate;
		private bool _follow;
		private decimal _hitscore;
		private int _hitcountseq;
		private bool _rotatedin;
		private int? _unit_price;
		private int? _unit_rental;
		private int? _max_price;
		private int? _min_price;
		private int? _max_rental;
		private int? _min_rental;
		private int? _max_size;
		private int? _min_size;
		private int? _monthly_payment;
		private decimal? _score=0M;
		private decimal? _score1=0M;
		private decimal? _score2=0M;
		private decimal? _score3=0M;
		private decimal? _score4=0M;
		private decimal? _score5=0M;
		private decimal? _score6=0M;
		private decimal? _score7=0M;
		private decimal? _score8=0M;
		private decimal? _score9=0M;
		private decimal? _score10=0M;
		private decimal? _agentscore=0M;
		private decimal? _agentscore1=0M;
		private decimal? _agentscore2=0M;
		private decimal? _agentscore3=0M;
		private decimal? _agentscore4=0M;
		private decimal? _agentscore5=0M;
		private decimal? _agentscore6=0M;
		private decimal? _agentscore7=0M;
		private decimal? _agentscore8=0M;
		private decimal? _agentscore9=0M;
		private decimal? _agentscore10=0M;
		private string _centamail;
		private int? _post_counter;
		private decimal? _yield;
		private int? _last_update_hour;
		private bool _interior_photo_flag;
		private string _reftype;
		private DateTime? _opdate;
		private string _carpark;
		private string _schoolkid;
		private string _schoolpri;
		private string _schoolsec;
		private string _schooloth;
		private string _railway;
		private string _bus;
		private string _shop;
		private string _food;
		private string _hospital;
		private string _bank;
		private string _park;
		private string _knot;
		private string _clubhouse;
		private string _highway;
		private string _spec;
		private decimal? _netarea;
		/// <summary>
		/// 
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
		}
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
		public string cblgcode
		{
			set{ _cblgcode=value;}
			get{return _cblgcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cblgName
		{
			set{ _cblgname=value;}
			get{return _cblgname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cblgCName
		{
			set{ _cblgcname=value;}
			get{return _cblgcname;}
		}
		/// <summary>
		/// 
		/// </summary>
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
		/// <summary>
		/// 
		/// </summary>
		public string cestName
		{
			set{ _cestname=value;}
			get{return _cestname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cestCName
		{
			set{ _cestcname=value;}
			get{return _cestcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cestDev
		{
			set{ _cestdev=value;}
			get{return _cestdev;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cestCDev
		{
			set{ _cestcdev=value;}
			get{return _cestcdev;}
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
		public string bigestName
		{
			set{ _bigestname=value;}
			get{return _bigestname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bigestCName
		{
			set{ _bigestcname=value;}
			get{return _bigestcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpID
		{
			set{ _scpid=value;}
			get{return _scpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpName
		{
			set{ _scpname=value;}
			get{return _scpname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpCName
		{
			set{ _scpcname=value;}
			get{return _scpcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scpMkt
		{
			set{ _scpmkt=value;}
			get{return _scpmkt;}
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
		public string gscpName
		{
			set{ _gscpname=value;}
			get{return _gscpname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gscpCName
		{
			set{ _gscpcname=value;}
			get{return _gscpcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gscpMkt
		{
			set{ _gscpmkt=value;}
			get{return _gscpmkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RefNo
		{
			set{ _refno=value;}
			get{return _refno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Luxury
		{
			set{ _luxury=value;}
			get{return _luxury;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisplayStyle
		{
			set{ _displaystyle=value;}
			get{return _displaystyle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ShowMap
		{
			set{ _showmap=value;}
			get{return _showmap;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Estate
		{
			set{ _estate=value;}
			get{return _estate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address1
		{
			set{ _address1=value;}
			get{return _address1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address2
		{
			set{ _address2=value;}
			get{return _address2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CEstate
		{
			set{ _cestate=value;}
			get{return _cestate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CAddress1
		{
			set{ _caddress1=value;}
			get{return _caddress1;}
		}
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
		/// <summary>
		/// 
		/// </summary>
		public string CnAddress
		{
			set{ _cnaddress=value;}
			get{return _cnaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Display
		{
			set{ _display=value;}
			get{return _display;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CDisplay
		{
			set{ _cdisplay=value;}
			get{return _cdisplay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyType
		{
			set{ _propertytype=value;}
			get{return _propertytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CPropertyType
		{
			set{ _cpropertytype=value;}
			get{return _cpropertytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyTypeTag
		{
			set{ _propertytypetag=value;}
			get{return _propertytypetag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PropertyTypeSearchKey
		{
			set{ _propertytypesearchkey=value;}
			get{return _propertytypesearchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Floor
		{
			set{ _floor=value;}
			get{return _floor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CFloor
		{
			set{ _cfloor=value;}
			get{return _cfloor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FloorTag
		{
			set{ _floortag=value;}
			get{return _floortag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FloorSearchKey
		{
			set{ _floorsearchkey=value;}
			get{return _floorsearchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flat
		{
			set{ _flat=value;}
			get{return _flat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CFlat
		{
			set{ _cflat=value;}
			get{return _cflat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Apartment
		{
			set{ _apartment=value;}
			get{return _apartment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo1
		{
			set{ _basicinfo1=value;}
			get{return _basicinfo1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CBasicInfo1
		{
			set{ _cbasicinfo1=value;}
			get{return _cbasicinfo1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo1Tag
		{
			set{ _basicinfo1tag=value;}
			get{return _basicinfo1tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BasicInfo1SearchKey
		{
			set{ _basicinfo1searchkey=value;}
			get{return _basicinfo1searchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo2
		{
			set{ _basicinfo2=value;}
			get{return _basicinfo2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CBasicInfo2
		{
			set{ _cbasicinfo2=value;}
			get{return _cbasicinfo2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo2Tag
		{
			set{ _basicinfo2tag=value;}
			get{return _basicinfo2tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BasicInfo2SearchKey
		{
			set{ _basicinfo2searchkey=value;}
			get{return _basicinfo2searchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo3
		{
			set{ _basicinfo3=value;}
			get{return _basicinfo3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CBasicInfo3
		{
			set{ _cbasicinfo3=value;}
			get{return _cbasicinfo3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo3Tag
		{
			set{ _basicinfo3tag=value;}
			get{return _basicinfo3tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BasicInfo3SearchKey
		{
			set{ _basicinfo3searchkey=value;}
			get{return _basicinfo3searchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo4
		{
			set{ _basicinfo4=value;}
			get{return _basicinfo4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CBasicInfo4
		{
			set{ _cbasicinfo4=value;}
			get{return _cbasicinfo4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BasicInfo4Tag
		{
			set{ _basicinfo4tag=value;}
			get{return _basicinfo4tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BasicInfo4SearchKey
		{
			set{ _basicinfo4searchkey=value;}
			get{return _basicinfo4searchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Direction
		{
			set{ _direction=value;}
			get{return _direction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CDirection
		{
			set{ _cdirection=value;}
			get{return _cdirection;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DirectionTag
		{
			set{ _directiontag=value;}
			get{return _directiontag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DirectionSearchKey
		{
			set{ _directionsearchkey=value;}
			get{return _directionsearchkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Characteristic
		{
			set{ _characteristic=value;}
			get{return _characteristic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CCharacteristic
		{
			set{ _ccharacteristic=value;}
			get{return _ccharacteristic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool SizeApprox
		{
			set{ _sizeapprox=value;}
			get{return _sizeapprox;}
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
		/// <summary>
		/// 
		/// </summary>
		public int? BalconyCount
		{
			set{ _balconycount=value;}
			get{return _balconycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnsuiteCount
		{
			set{ _ensuitecount=value;}
			get{return _ensuitecount;}
		}
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
		public string SellType
		{
			set{ _selltype=value;}
			get{return _selltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CSellType
		{
			set{ _cselltype=value;}
			get{return _cselltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SellTypeTag
		{
			set{ _selltypetag=value;}
			get{return _selltypetag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SellTypeSearchKey
		{
			set{ _selltypesearchkey=value;}
			get{return _selltypesearchkey;}
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
		public bool Mortgagee
		{
			set{ _mortgagee=value;}
			get{return _mortgagee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool WithLease
		{
			set{ _withlease=value;}
			get{return _withlease;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WithLeaseRental
		{
			set{ _withleaserental=value;}
			get{return _withleaserental;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HomeOwnership
		{
			set{ _homeownership=value;}
			get{return _homeownership;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? HomeOwnershipPrice
		{
			set{ _homeownershipprice=value;}
			get{return _homeownershipprice;}
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
		public bool RentalInclu
		{
			set{ _rentalinclu=value;}
			get{return _rentalinclu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Management
		{
			set{ _management=value;}
			get{return _management;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ManagementInclu
		{
			set{ _managementinclu=value;}
			get{return _managementinclu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GovRate
		{
			set{ _govrate=value;}
			get{return _govrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool GovRateInclu
		{
			set{ _govrateinclu=value;}
			get{return _govrateinclu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GovRent
		{
			set{ _govrent=value;}
			get{return _govrent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool GovRentInclu
		{
			set{ _govrentinclu=value;}
			get{return _govrentinclu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Environment
		{
			set{ _environment=value;}
			get{return _environment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transportation
		{
			set{ _transportation=value;}
			get{return _transportation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Education
		{
			set{ _education=value;}
			get{return _education;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Business
		{
			set{ _business=value;}
			get{return _business;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Entertainment
		{
			set{ _entertainment=value;}
			get{return _entertainment;}
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
		public string AgentLicense
		{
			set{ _agentlicense=value;}
			get{return _agentlicense;}
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
		public string AgentEmail
		{
			set{ _agentemail=value;}
			get{return _agentemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool AgentPortrait
		{
			set{ _agentportrait=value;}
			get{return _agentportrait;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerName
		{
			set{ _managername=value;}
			get{return _managername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerCName
		{
			set{ _managercname=value;}
			get{return _managercname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerNo
		{
			set{ _managerno=value;}
			get{return _managerno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerLicense
		{
			set{ _managerlicense=value;}
			get{return _managerlicense;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerMobile
		{
			set{ _managermobile=value;}
			get{return _managermobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManagerEmail
		{
			set{ _manageremail=value;}
			get{return _manageremail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ManagerPortrait
		{
			set{ _managerportrait=value;}
			get{return _managerportrait;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BranchCostCent
		{
			set{ _branchcostcent=value;}
			get{return _branchcostcent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BranchName
		{
			set{ _branchname=value;}
			get{return _branchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BranchCName
		{
			set{ _branchcname=value;}
			get{return _branchcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BranchPhone
		{
			set{ _branchphone=value;}
			get{return _branchphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NightPhone
		{
			set{ _nightphone=value;}
			get{return _nightphone;}
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
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Follow
		{
			set{ _follow=value;}
			get{return _follow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal HitScore
		{
			set{ _hitscore=value;}
			get{return _hitscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HitCountSeq
		{
			set{ _hitcountseq=value;}
			get{return _hitcountseq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool RotatedIn
		{
			set{ _rotatedin=value;}
			get{return _rotatedin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? unit_price
		{
			set{ _unit_price=value;}
			get{return _unit_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? unit_rental
		{
			set{ _unit_rental=value;}
			get{return _unit_rental;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? max_price
		{
			set{ _max_price=value;}
			get{return _max_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? min_price
		{
			set{ _min_price=value;}
			get{return _min_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? max_rental
		{
			set{ _max_rental=value;}
			get{return _max_rental;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? min_rental
		{
			set{ _min_rental=value;}
			get{return _min_rental;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? max_size
		{
			set{ _max_size=value;}
			get{return _max_size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? min_size
		{
			set{ _min_size=value;}
			get{return _min_size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? monthly_payment
		{
			set{ _monthly_payment=value;}
			get{return _monthly_payment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score1
		{
			set{ _score1=value;}
			get{return _score1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score2
		{
			set{ _score2=value;}
			get{return _score2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score3
		{
			set{ _score3=value;}
			get{return _score3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score4
		{
			set{ _score4=value;}
			get{return _score4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score5
		{
			set{ _score5=value;}
			get{return _score5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score6
		{
			set{ _score6=value;}
			get{return _score6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score7
		{
			set{ _score7=value;}
			get{return _score7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score8
		{
			set{ _score8=value;}
			get{return _score8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score9
		{
			set{ _score9=value;}
			get{return _score9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? score10
		{
			set{ _score10=value;}
			get{return _score10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore
		{
			set{ _agentscore=value;}
			get{return _agentscore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore1
		{
			set{ _agentscore1=value;}
			get{return _agentscore1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore2
		{
			set{ _agentscore2=value;}
			get{return _agentscore2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore3
		{
			set{ _agentscore3=value;}
			get{return _agentscore3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore4
		{
			set{ _agentscore4=value;}
			get{return _agentscore4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore5
		{
			set{ _agentscore5=value;}
			get{return _agentscore5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore6
		{
			set{ _agentscore6=value;}
			get{return _agentscore6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore7
		{
			set{ _agentscore7=value;}
			get{return _agentscore7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore8
		{
			set{ _agentscore8=value;}
			get{return _agentscore8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore9
		{
			set{ _agentscore9=value;}
			get{return _agentscore9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agentscore10
		{
			set{ _agentscore10=value;}
			get{return _agentscore10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string centamail
		{
			set{ _centamail=value;}
			get{return _centamail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? post_counter
		{
			set{ _post_counter=value;}
			get{return _post_counter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? yield
		{
			set{ _yield=value;}
			get{return _yield;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? last_update_hour
		{
			set{ _last_update_hour=value;}
			get{return _last_update_hour;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool interior_photo_flag
		{
			set{ _interior_photo_flag=value;}
			get{return _interior_photo_flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RefType
		{
			set{ _reftype=value;}
			get{return _reftype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Opdate
		{
			set{ _opdate=value;}
			get{return _opdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string carpark
		{
			set{ _carpark=value;}
			get{return _carpark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string schoolkid
		{
			set{ _schoolkid=value;}
			get{return _schoolkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string schoolpri
		{
			set{ _schoolpri=value;}
			get{return _schoolpri;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string schoolsec
		{
			set{ _schoolsec=value;}
			get{return _schoolsec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string schooloth
		{
			set{ _schooloth=value;}
			get{return _schooloth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string railway
		{
			set{ _railway=value;}
			get{return _railway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bus
		{
			set{ _bus=value;}
			get{return _bus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shop
		{
			set{ _shop=value;}
			get{return _shop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string food
		{
			set{ _food=value;}
			get{return _food;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hospital
		{
			set{ _hospital=value;}
			get{return _hospital;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bank
		{
			set{ _bank=value;}
			get{return _bank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string park
		{
			set{ _park=value;}
			get{return _park;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string knot
		{
			set{ _knot=value;}
			get{return _knot;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string clubhouse
		{
			set{ _clubhouse=value;}
			get{return _clubhouse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string highway
		{
			set{ _highway=value;}
			get{return _highway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string spec
		{
			set{ _spec=value;}
			get{return _spec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? NetArea
		{
			set{ _netarea=value;}
			get{return _netarea;}
		}
		#endregion Model

        public List<PostImage> Images { get; set; }
        public List<PostItem> Items { get; set; }
	}
}

