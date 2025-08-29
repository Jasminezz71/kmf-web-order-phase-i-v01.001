using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class GrET : BaseET
    {
        //Header
        public string ST_PR_CODE { get; set; }
        public string ST_DO_CODE { get; set; }
        public string ST_GR_CODE { get; set; }
        public DateTime? PLAN_DELIVERY_DATE { get; set; }
        public DateTime? RECEIVE_DATE { get; set; }
        public string RECEIVE_BY { get; set; }
        public string REFERANCE_NO { get; set; }
        public string REMARK { get; set; }
        public bool? DELETE_FLAG { get; set; }
        public string LOCATION_CODE { get; set; }


        //Detail
        public int ROW_NO { get; set; }
        public int LINE_NO { get; set; }
        public string ST_PR_CATEGORY_ID { get; set; }
        public string ST_PR_CATEGORY_NAME { get; set; }
        public string ST_MAP_WH_ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME_TH { get; set; }
        public string ITEM_NAME_TH2 { get; set; }
        public string QTY_REQUEST { get; set; }
        public string REQUEST_UOM_CODE { get; set; }
        public string QTY_SEND { get; set; }
        public string SEND_UOM_CODE { get; set; }
        public string QTY_RECEIVE { get; set; }
        public string RECEIVE_UOM_CODE { get; set; }

        //Other
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRAND_NAME { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public string REQUEST_TO_BRAND_CODE { get; set; }
        public string REQUEST_TO_BRAND_NAME { get; set; }
        public string REQUEST_TO_BRANCH_CODE { get; set; }
        public string REQUEST_TO_BRANCH_NAME { get; set; }


        public string FULL_PATH { get; set; }
        public string FILE_PATH { get; set; }    //Supaneej, 2018-10-11, Add FILE_PATH for display image
        public string FILE_NAME_WITH_EXTENSION { get; set; }    //Supaneej, 2018-10-25, Add FILE_PATH for display image
        public decimal? SALE_PRICE { get; set; }  //Supaneej, 2018-10-11, display SalePrice for Franchise

        public decimal? REMAIN_QTY { get; set; } //Ketsara.k, 2018-12-06 , Add REMAIN_QTY 
        public decimal? QTY_RECEIVED { get; set; } //Ketsara.k, 2020-07-15 , Add QTY_RECEIVED 
        public int DO_STATUS_CODE { get; set; } //Ketsara.k, 2020-07-15 , Add DO_STATUS_CODE 
        public string DO_STATUS_NAME { get; set; } //Ketsara.k, 2020-07-15 , Add DO_STATUS_NAME 
        
        
        


    }
}
