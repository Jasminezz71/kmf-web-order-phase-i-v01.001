using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.RPT
{
    public class InvoiceSearchCriteriaET
    {
        public string COMPANY_CODE { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string P_NO { get; set; }
        public DateTime? POSTING_DATE_FROM { get; set; }
        public DateTime? POSTING_DATE_TO { get; set; }
    }
}
