using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DoSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string ST_PR_CODE { get; set; }
        public string ST_DO_CODE { get; set; }
        public DateTime? PLAN_DELIVERY_DATE { get; set; }
        public string DO_STATUS_CODE { get; set; }
        public string DO_STATUS_NAME { get; set; }
        public string ST_GR_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
    }
}
