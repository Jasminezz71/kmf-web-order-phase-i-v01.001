using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT
{
    public class PrWhPickupET
    {
        public string USER_NAME { get; set; }
        public DateTime? PLAN_DELIVERY_DATE_FROM { get; set; }
        public DateTime? PLAN_DELIVERY_DATE_TO { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string CATEGORY_ID { get; set; }
        public string LOCATION_CODE { get; set; }
        public bool? DISPLAY_HEADER { get; set; }
    }
}
