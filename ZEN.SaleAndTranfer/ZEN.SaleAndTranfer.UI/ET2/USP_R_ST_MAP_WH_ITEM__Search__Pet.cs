using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
	public class USP_R_ST_MAP_WH_ITEM__Search__Pet
	{
		public int? StPrCateogryID { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }

        public string BranchCode { get; set; }
        public string BrancnCode { get; set; }
        public string OrderByList { get; set; }
		public int? PageSize { get; set; }
		public int? CurrentPageId { get; set; }
		public string Username { get; set; }

		public string FormMode { get; set; }

		public string FormID { get; set; }
		public string DivSearchResultID { get; set; }

		public bool HideResultCountFlag { get; set; }
	}
}