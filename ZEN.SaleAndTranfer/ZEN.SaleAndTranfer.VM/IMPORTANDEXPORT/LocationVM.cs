using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class LocationVM : BaseVM
    {
        public LocationSearchCriteriaVM locationSearchCriteriaVM { get; set; }
        public LocationSearchResultVM locationSearchResultVM { get; set; }
        public LocationET_MA locationVM_MA { get; set; }
    }
    public class LocationSearchCriteriaVM : LocationSearchCriteriaET
    {
    }

    public class LocationET_MA : LocationET
    {
    }

    public class LocationSearchResultVM
    {
        public List<LocationSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
