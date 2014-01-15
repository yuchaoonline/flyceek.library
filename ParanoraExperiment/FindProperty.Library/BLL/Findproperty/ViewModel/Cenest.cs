
using System;
namespace FindProperty.Lib.BLL.Findproperty.ViewModel
{
	/// <summary>
	/// cenest:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public class Cenest
	{
		public Cenest()
		{}

        #region Model Cenest
        private string _bigest;
		private string _centaest;
		private string _cestcode;
		private string _geo_refno;
		private string _bpolycode;
		private decimal? _lpt_x;
		private decimal? _lpt_y;
		private string _est_type;
		private string _e_estate;
		private string _e_phase;
		private string _pe_addr;
		private string _c_estate;
		private string _c_estate2;
		private string _c_phase;
		private string _pc_addr1;
		private string _pc_addr2;
		private string _scp_id;
		private string _scp_e;
		private string _scp_c;
		private string _scp_mkt;
		private string _gscp_id;
		private string _gscp_e;
		private string _gscp_c;
		private string _gscp_mkt;
		private string _pc_dev;
		private string _lendholdyr;
		private decimal? _est_area;
		private string _framework;
		private int? _tot_unit;
		private string _landgrade;
		private string _landno;
		private string _mgt_com;
		private decimal? _mgt_price;
		private string _bigestcode;
		private int? _x_cnt;
		private int? _y_cnt;
		private DateTime? _opdate;
		private int? _unit_cnt;
		private string _code3;
		private string _name2c;
		private string _name3c;
		private string _name2e;
		private string _name3e;
		private int? _bldgage;
		private string _distname;
		private bool _flagdeleted;
		private DateTime? _lastupdate;
		/// <summary>
		/// 
		/// </summary>
		public string bigest
		{
			set{ _bigest=value;}
			get{return _bigest;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string centaest
		{
			set{ _centaest=value;}
			get{return _centaest;}
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
		public string geo_refno
		{
			set{ _geo_refno=value;}
			get{return _geo_refno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bpolycode
		{
			set{ _bpolycode=value;}
			get{return _bpolycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? lpt_x
		{
			set{ _lpt_x=value;}
			get{return _lpt_x;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? lpt_y
		{
			set{ _lpt_y=value;}
			get{return _lpt_y;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string est_type
		{
			set{ _est_type=value;}
			get{return _est_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string e_estate
		{
			set{ _e_estate=value;}
			get{return _e_estate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string e_phase
		{
			set{ _e_phase=value;}
			get{return _e_phase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pe_addr
		{
			set{ _pe_addr=value;}
			get{return _pe_addr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string c_estate
		{
			set{ _c_estate=value;}
			get{return _c_estate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string c_estate2
		{
			set{ _c_estate2=value;}
			get{return _c_estate2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string c_phase
		{
			set{ _c_phase=value;}
			get{return _c_phase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pc_addr1
		{
			set{ _pc_addr1=value;}
			get{return _pc_addr1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pc_addr2
		{
			set{ _pc_addr2=value;}
			get{return _pc_addr2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_id
		{
			set{ _scp_id=value;}
			get{return _scp_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_e
		{
			set{ _scp_e=value;}
			get{return _scp_e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_c
		{
			set{ _scp_c=value;}
			get{return _scp_c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scp_mkt
		{
			set{ _scp_mkt=value;}
			get{return _scp_mkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gscp_id
		{
			set{ _gscp_id=value;}
			get{return _gscp_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gscp_e
		{
			set{ _gscp_e=value;}
			get{return _gscp_e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gscp_c
		{
			set{ _gscp_c=value;}
			get{return _gscp_c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gscp_mkt
		{
			set{ _gscp_mkt=value;}
			get{return _gscp_mkt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pc_dev
		{
			set{ _pc_dev=value;}
			get{return _pc_dev;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lendholdyr
		{
			set{ _lendholdyr=value;}
			get{return _lendholdyr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? est_area
		{
			set{ _est_area=value;}
			get{return _est_area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string framework
		{
			set{ _framework=value;}
			get{return _framework;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? tot_unit
		{
			set{ _tot_unit=value;}
			get{return _tot_unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string landgrade
		{
			set{ _landgrade=value;}
			get{return _landgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string landno
		{
			set{ _landno=value;}
			get{return _landno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mgt_com
		{
			set{ _mgt_com=value;}
			get{return _mgt_com;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? mgt_price
		{
			set{ _mgt_price=value;}
			get{return _mgt_price;}
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
		public int? x_cnt
		{
			set{ _x_cnt=value;}
			get{return _x_cnt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? y_cnt
		{
			set{ _y_cnt=value;}
			get{return _y_cnt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? opdate
		{
			set{ _opdate=value;}
			get{return _opdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? unit_cnt
		{
			set{ _unit_cnt=value;}
			get{return _unit_cnt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string code3
		{
			set{ _code3=value;}
			get{return _code3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name2c
		{
			set{ _name2c=value;}
			get{return _name2c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name3c
		{
			set{ _name3c=value;}
			get{return _name3c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name2e
		{
			set{ _name2e=value;}
			get{return _name2e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name3e
		{
			set{ _name3e=value;}
			get{return _name3e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? bldgage
		{
			set{ _bldgage=value;}
			get{return _bldgage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string distname
		{
			set{ _distname=value;}
			get{return _distname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Flagdeleted
		{
			set{ _flagdeleted=value;}
			get{return _flagdeleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Lastupdate
		{
			set{ _lastupdate=value;}
			get{return _lastupdate;}
		}
		#endregion Model 
        
        public EstInfo EstInfo { get; set; }
	}
}

