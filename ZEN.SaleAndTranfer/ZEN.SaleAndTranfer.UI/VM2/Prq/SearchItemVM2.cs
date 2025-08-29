using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.VM2.Prq
{
    public class SearchItemVM2
    {
        public List<USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode_Result> CategoryDdl { get; set; }
        public USP_R_ST_MAP_WH_ITEM__Search__Pet Pet { get; set; }

        public List<USP_R_ST_MAP_WH_ITEM__Search_Result> SearchResult { get; set; }

        public int SearchCountAll { get; set; }
    }
}