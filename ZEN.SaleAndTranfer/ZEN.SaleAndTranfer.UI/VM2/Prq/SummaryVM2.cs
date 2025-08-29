using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.VM2.Prq
{
    public class SummaryVM2
    {
        public List<BasketItemET> Detail { get; set; }

        public PrqHET Het { get; set; }

        public USP_M_USM_USER__Search_V2__Pet Pet { get; set; }
    }
}