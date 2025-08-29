using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
	public class USP_R_ST_PR_Search_V2__Pet
	{
		public string PrCode { get; set; }
		public DateTime? PlanDeliveryDateFrom { get; set; }
		public DateTime? PlanDeliveryDateTo { get; set; }
		public DateTime? CreateDateFrom { get; set; }
		public DateTime? CreateDateTo { get; set; }
		public string CustomerPoCode { get; set; }
		public string Username { get; set; }

        public string isSaleAdmin { get; set; }

        public string saleAdminCode { get; set; }
        public string OrderByList { get; set; }
		public int? PageSize { get; set; }
		public int CurrentPageId { get; set; }

		public string FormID { get; set; }

		public string Name_2 { get; set; }
	}
}