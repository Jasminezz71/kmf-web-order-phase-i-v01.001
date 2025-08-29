using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class RecTransferSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ST_DO_CODE { get; set; }
        public string ST_GR_CODE { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }
        public string LOCATION { get; set; }
        public string ITEM_CODE { get; set; }
        public decimal? SEND_QTY { get; set; }
        public string SEND_UOM { get; set; }
        public decimal? RECEIVE_QTY { get; set; }
        public string RECEIVE_UOM { get; set; }

        
        public string EXPORT_ROW_NO { get; set; }
        public string EXPORT_BRAND_CODE { get; set; }
        public string EXPORT_BRAND_NAME { get; set; }
        public string EXPORT_BRANCH_CODE { get; set; }
        public string EXPORT_BRANCH_NAME { get; set; }
        public string EXPORT_ST_DO_CODE { get; set; }
        public string EXPORT_ST_GR_CODE { get; set; }
        public string EXPORT_RECEIVE_DATE { get; set; }
        public string EXPORT_LOCATION { get; set; }
        public string EXPORT_ITEM_CODE { get; set; }
        public string EXPORT_SEND_QTY { get; set; }
        public string EXPORT_SEND_UOM { get; set; }
        public string EXPORT_RECEIVE_QTY { get; set; }
        public string EXPORT_RECEIVE_UOM { get; set; }
        public string QUANTITY_DIFF { get; set; }
    }
}
