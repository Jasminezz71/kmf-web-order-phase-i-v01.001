using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class ItemStockSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string LOCATION { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public decimal? INCOMING_BALANCE_QTY { get; set; }
        public string INCOMING_BALANCE_UOM { get; set; }
        public decimal? RECEIVE_QTY { get; set; }
        public string RECEIVE_UOM { get; set; }
        public decimal? REMAIN_QTY { get; set; }
        public string REMAIN_UOM { get; set; }
        public decimal? USAGE_QTY { get; set; }
        public string USAGE_UOM { get; set; }
        public string APP_ID { get; set; }
        public string APP_NAME { get; set; }
    }
}
