using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT
{
    public class PrWhPickupSearchResultET
    {
        public string ST_PR_CODE { get; set; }
        public string ST_PR_CODE_NAV { get; set; }
        public string ORDER_DATE { get; set; }
        public string POSTING_DATE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_DESC { get; set; }
        public string REQUEST_QTY { get; set; }
        public string REQUEST_UOM { get; set; }
        public string SEND_QTY { get; set; }
        public string SEND_UOM { get; set; }
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string CREATE_BY { get; set; }
        public string CREATE_DATE { get; set; }
        public string ST_WH_ITEM_CATEGORY_NAME { get; set; }
        public string REQUEST_TO_BRANCH_CODE { get; set; }
        public string REMARK { get; set; }
        public string ACTION { get; set; }
    }
}
