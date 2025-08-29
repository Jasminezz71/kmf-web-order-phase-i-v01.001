using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;

namespace ZEN.SaleAndTranfer.UI.VM2
{
    public class BasketVM
    {
        public string Mode { get; set; }
        public List<BasketItemET> Detail { get; set; }
    }
}