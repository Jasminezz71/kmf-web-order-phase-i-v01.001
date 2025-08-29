using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DashboardSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRAND_NAME { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public string REQUEST_TO_BRAND_CODE { get; set; }
        public string REQUEST_TO_BRAND_NAME { get; set; }
        public string REQUEST_TO_BRANCH_CODE { get; set; }
        public string REQUEST_TO_BRANCH_NAME { get; set; }
        public string ST_DO_CODE { get; set; }
        public string ST_PR_CODE { get; set; }
        public string ST_PR_CATEGORY_CODE { get; set; }
        public string ST_PR_CATEGORY_NAME { get; set; }
        public DateTime? REQUEST_DATE { get; set; }
        public DateTime? PLAN_DELIVERY_DATE { get; set; }
        public int ITEM_QTY { get; set; }
        public int? PR_STATUS_CODE { get; set; }
        public string PR_STATUS_NAME { get; set; }
        public int? DO_STATUS_CODE { get; set; }
        public string DO_STATUS_NAME { get; set; }
        public string REMARK { get; set; }
        public bool? DELETE_FLAG { get; set; }
        public string LOCATION_CODE { get; set; }
    }
}
