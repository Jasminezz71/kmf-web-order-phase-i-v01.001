using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class RecTransferSearchCriteriaET : BaseET
    {
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public DateTime? RECEIVE_DATE_FROM { get; set; }
        public DateTime? RECEIVE_DATE_TO { get; set; }
        public int PAGE_INDEX { get; set; }
        public int PAGE_SIZE { get; set; }
        public string USER_NAME { get; set; }
    }
}
