using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class ItemAndLocationVM : BaseVM
    {
        public ItemAndLocationSearchCriteriaVM itemAndLocationSearchCriteriaVM { get; set; }
        public ItemAndLocationSearchResultVM itemAndLocationSearchResultVM { get; set; }
        public ItemAndLocationET_MA itemAndLocationVM_MA { get; set; }
    }
    public class ItemAndLocationSearchCriteriaVM : ItemAndLocationSearchCriteriaET
    {
    }

    public class ItemAndLocationET_MA : ItemAndLocationET
    {
    }

    public class ItemAndLocationSearchResultVM
    {
        public List<ItemAndLocationSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
