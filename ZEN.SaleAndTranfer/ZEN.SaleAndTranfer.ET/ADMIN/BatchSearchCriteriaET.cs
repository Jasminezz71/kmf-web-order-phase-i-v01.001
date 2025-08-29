using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.ADMIN
{
    public class BatchSearchCriteriaET : BaseET
    {
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        
    }
}
