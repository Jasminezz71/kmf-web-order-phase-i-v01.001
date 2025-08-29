using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT
{
    public class ItemAndLocationSearchResultET
    {
        public string ST_MAP_WH_ITEM_ID { get; set; }
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public string REQUEST_TO_BRAND_CODE { get; set; }
        public string REQUEST_TO_LOCATION_CODE { get; set; }
        public string ST_WH_ITEM_CATEGORY_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME_1 { get; set; }
        public string ITEM_NAME_2 { get; set; }
        public string REQUEST_UOM_CODE { get; set; }
        public string DELIVERY_UOM_CODE { get; set; }
        public string REMARK { get; set; }
        public string ACTION { get; set; }
    }
}
