using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class PrWhPickupVM : BaseVM
    {
        public PrWhPickupSearchCriteriaVM prWhPickupSearchCriteriaVM { get; set; }
        public PrWhPickupSearchResultVM prWhPickupSearchResultVM { get; set; }
        public PrWhPickupET_MA prWhPickupVM_MA { get; set; }
    }
    public class PrWhPickupSearchCriteriaVM : PrWhPickupSearchCriteriaET
    {
    }

    public class PrWhPickupET_MA : PrWhPickupET
    {
        public List<DDLItemET> prWhCategoryList { get; set; }
        public List<DDLItemET> prWhLocationList { get; set; }
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }

    public class PrWhPickupSearchResultVM
    {
        public List<PrWhPickupSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
