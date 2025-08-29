using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.ET2
{
    public class BasketItemET
    {
        public int StMapWhItemID { get; set; }
        public decimal? OrderQty { get; set; }


        public string ItemCode { get; set; }
        public string RequestUomCode { get; set; }
        public string ItemName { get; set; }
        public decimal? UnitPrice { get; set; }

        public string Remark { get; set; }
    }
}