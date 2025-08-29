using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class ItemStockET : BaseET
    {
        public int? ROW_NO { get; set; }
        public string ITEM_PICK_DATE { get; set; }
        public DateTime ITEM_PICK_DATE_SAVE { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string LOCATION { get; set; }
        public decimal? REMAIN_QTY { get; set; }
        public string REMAIN_UOM { get; set; }
        public string APP_ID { get; set; }
        public string APP_NAME { get; set; }
        public string REMARK { get; set; }
        public bool? DELETE_FLAG { get; set; }
        public bool FIRST_EDIT { get; set; }
        public bool CAN_EDIT { get; set; }
    }
}
