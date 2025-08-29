using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class PrSearchCriteriaET : BaseET
    {
        public int PAGE_INDEX { get; set; }
        public int ROW_PER_PAGE { get; set; }
        public string ST_PR_CODE { get; set; }
        public string ST_PR_CATEGORY_CODE { get; set; }
        public string ST_PR_CATEGORY_NAME { get; set; }
        public DateTime? CREATE_DATE_FROM { get; set; }
        public DateTime? CREATE_DATE_TO { get; set; }
        public DateTime? PLAN_DELIVERY_DATE_FROM { get; set; }
        public DateTime? PLAN_DELIVERY_DATE_TO { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRAND_NAME { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public string REQUEST_TO_BRAND_CODE { get; set; }
        public string REQUEST_TO_BRAND_NAME { get; set; }
        public string REQUEST_TO_BRANCH_CODE { get; set; }
        public string REQUEST_TO_BRANCH_NAME { get; set; }
        public string FC_LOCATION { get; set; }
    }
}
