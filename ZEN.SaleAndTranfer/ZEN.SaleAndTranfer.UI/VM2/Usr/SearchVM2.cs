using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.VM2.Usr
{
    public class SearchVM2
    {
        public USP_M_USM_USER__Search_V2__Pet Pet { get; set; }
        public List<USP_M_USM_USER__Search_V2_Result> Result { get; set; }

        public int SearchCountAll { get; set; }
    }
}