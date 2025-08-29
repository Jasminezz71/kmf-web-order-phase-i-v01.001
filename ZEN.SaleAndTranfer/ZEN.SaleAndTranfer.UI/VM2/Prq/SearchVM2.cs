using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.VM2.Prq
{
    public class SearchVM2
    {
        public USP_R_ST_PR_Search_V2__Pet Pet { get; set; }
        public List<USP_R_ST_PR_Search_V2_Result> Result { get; set; }

        public int SearchCountAll { get; set; }
    }
}