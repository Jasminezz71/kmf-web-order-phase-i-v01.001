using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class DeliveryScheduleVM : BaseVM
    {
        public DeliveryScheduleSearchCriteriaVM DSSearchCriteriaVM { get; set; }
        public DeliveryScheduleResultVM DSResultVM { get; set; }
        public DeliveryScheduleET DS_MA { get; set; }
    }
    public class DeliveryScheduleSearchCriteriaVM : DeliveryScheduleET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
        public List<DDLItemET> locationList { get; set; }
        public List<DDLItemET> typeList { get; set; }
        public List<DDLItemET> zoneList { get; set; }
    }

    public class DeliveryScheduleResultVM
    {
        public List<DeliveryScheduleET> resultList { get; set; }
    }
}
