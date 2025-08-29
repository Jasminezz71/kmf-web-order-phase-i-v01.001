using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class ItemStockSearchCriteriaET :BaseET
    {
        public int? PAGE_INDEX { get; set; }
        public int? PAGE_SIZE { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string APP_ID { get; set; }
        public string ITEM_STOCK_TYPE { get; set; }
        public string USER_NAME { get; set; }
    }
}
