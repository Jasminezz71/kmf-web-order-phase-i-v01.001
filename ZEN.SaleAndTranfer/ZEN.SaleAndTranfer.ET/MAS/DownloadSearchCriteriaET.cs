using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DownloadSearchCriteriaET : BaseET
    {
        public string ROLE_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? REQUEST_DATE_FROM { get; set; }
        public DateTime? REQUEST_DATE_TO { get; set; }
        public string APPROVE_STATUS_FLAG { get; set; }
        public string APPLICATION_TYPE { get; set; }
    }
}
