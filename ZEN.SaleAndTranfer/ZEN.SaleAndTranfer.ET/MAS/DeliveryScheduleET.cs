using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DeliveryScheduleET : BaseET
    {        
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ZONE { get; set; }
        public string SCHEDULE_TYPE { get; set; }
        public string START_TIME { get; set; }
        public string END_TIME { get; set; }
        public bool SUN_FLAG { get; set; }
        public bool MON_FLAG { get; set; }
        public bool TUE_FLAG { get; set; }						
        public bool WED_FLAG { get; set; }
        public bool THU_FLAG { get; set; }
        public bool FRI_FLAG { get; set; }
        public bool SAT_FLAG { get; set; }
        public string LOCATION_CODE { get; set; }
        public bool SuccessFlag { get; set; }
        public string ErrorMessage { get; set; }

    }
}
