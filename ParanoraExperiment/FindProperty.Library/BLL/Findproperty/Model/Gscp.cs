using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Model
{
    [Serializable]
    public class Gscp
    {
        public Gscp()
		{}
		#region Model
		private string _gscp_id;
		private string _gscp_c;
		private string _gscp_e;
		private string _gscp_mkt;
		private decimal? _lpt_x;
		private decimal? _lpt_y;
		private int _displayorder=0;
		private bool _flagdeleted;
		private DateTime? _lastupdate;
		/// <summary>
		/// 
		/// </summary>
		public string gscp_id
		{
			set{ _gscp_id=value;}
			get{return _gscp_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gscp_c
		{
			set{ _gscp_c=value;}
			get{return _gscp_c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gscp_e
		{
			set{ _gscp_e=value;}
			get{return _gscp_e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gscp_mkt
		{
			set{ _gscp_mkt=value;}
			get{return _gscp_mkt;}
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
		public int displayOrder
		{
			set{ _displayorder=value;}
			get{return _displayorder;}
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
    }
}
