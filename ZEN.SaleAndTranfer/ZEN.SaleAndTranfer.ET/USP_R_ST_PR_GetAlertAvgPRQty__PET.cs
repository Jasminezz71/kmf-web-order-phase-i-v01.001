using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class USP_R_ST_PR_GetAlertAvgPRQty__PET : BaseET
    {
        public string BrandCode { get; set; }
        public string BranchCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemUOM { get; set; }
        public decimal OrderQty { get; set; }

    }
}
