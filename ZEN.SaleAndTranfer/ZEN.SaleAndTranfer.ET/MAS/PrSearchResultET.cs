using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class PrSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string ST_PR_CODE { get; set; }
        public string ST_PR_CATEGORY_ID { get; set; }
        public string ST_PR_CATEGORY_NAME { get; set; }
        public string ST_MAP_WH_ITEM_ID { get; set; }
        public string ST_WH_ITEM_CATEGORY_ID { get; set; }
        public string ST_WH_ITEM_CATEGORY_NAME { get; set; }
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRAND_NAME { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public string REQUEST_TO_BRAND_CODE { get; set; }
        public string REQUEST_TO_BRAND_NAME { get; set; }
        public string REQUEST_TO_BRANCH_CODE { get; set; }
        public string REQUEST_TO_BRANCH_NAME { get; set; }
        public DateTime? PLAN_DELIVERY_DATE { get; set; }
        public string ITEM { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME_TH { get; set; }
        public string ITEM_DETAIL { get; set; }
        public decimal? ITEM_QTY { get; set; }
        public decimal ITEM_QTY_AVG { get; set; }
        public string ITEM_UNIT { get; set; }
        public string WH_UOM_CODE { get; set; }
        public string REQUEST_UOM_CODE { get; set; }
        public string DELIVERY_UOM_CODE { get; set; }
        public int? PR_STATUS_CODE { get; set; }
        public string PR_STATUS_NAME { get; set; }
        public string REMARK { get; set; }
        public string DELETE_FLAG { get; set; }
        public string LOCATION_CODE { get; set; }
        public DateTime? USE_START_DATE { get; set; }
        public DateTime? USE_END_DATE { get; set; }
        public string FULL_PATH { get; set; }
        public string FILE_PATH { get; set; }    //Supaneej, 2018-10-11, Add FILE_PATH for display image
        public string FILE_NAME_WITH_EXTENSION { get; set; }    //Supaneej, 2018-10-25, Add FILE_PATH for display image
        public decimal? SALE_PRICE { get; set; }  //Supaneej, 2018-10-11, display SalePrice for Franchise

        public decimal? REMAIN_QTY { get; set; } //Ketsara.k, 2018-12-06 , Add REMAIN_QTY 

        public int? ITEM_COLUMN { get; set; } //ketsara.k 2018-12-07

        public decimal? UNIT_PRICE { get; set; } //ketsra.k 2019-01-27
    }
}
