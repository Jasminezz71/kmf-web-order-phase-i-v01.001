using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class UnitVM : BaseVM
    {
        public UnitSearchCriteriaVM unitSearchCriteriaVM { get; set; }
        public UnitSearchResultVM unitSearchResultVM { get; set; }
        public UnitET_MA unitVM_MA { get; set; }
    }
    public class UnitSearchCriteriaVM : UnitSearchCriteriaET
    {
    }

    public class UnitET_MA : UnitET
    {
    }

    public class UnitSearchResultVM
    {
        public List<UnitSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
