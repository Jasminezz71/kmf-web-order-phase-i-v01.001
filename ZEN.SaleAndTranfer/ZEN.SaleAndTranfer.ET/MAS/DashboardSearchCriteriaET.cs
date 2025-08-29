using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DashboardSearchCriteriaET : BaseET
    {
        public string REQUEST_BY_BRAND_CODE { get; set; }
        public string REQUEST_BY_BRAND_NAME { get; set; }
        public string REQUEST_BY_BRANCH_CODE { get; set; }
        public string REQUEST_BY_BRANCH_NAME { get; set; }
        public int PAGE_INDEX { get; set; }
        public int ROW_PER_PAGE { get; set; }
        public string USER_NAME { get; set; }
        public string ROLE_NAME { get; set; }
    }
}
