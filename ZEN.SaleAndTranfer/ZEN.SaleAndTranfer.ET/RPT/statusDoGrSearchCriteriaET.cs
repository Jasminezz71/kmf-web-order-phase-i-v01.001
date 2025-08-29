using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.RPT
{
    public class statusDoGrSearchCriteriaET
    {
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? DATE_FROM { get; set; }
        public DateTime? DATE_TO { get; set; }
        public string EXPORT_TYPE { get; set; }
    }
}
