using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class UnitConversionVM : BaseVM
    {
        public UnitConversionSearchCriteriaVM unitConversionSearchCriteriaVM { get; set; }
        public UnitConversionSearchResultVM unitConversionSearchResultVM { get; set; }
        public UnitConversionET_MA unitConversionVM_MA { get; set; }
    }
    public class UnitConversionSearchCriteriaVM : UnitConversionSearchCriteriaET
    {
    }

    public class UnitConversionET_MA : UnitConversionET
    {
    }

    public class UnitConversionSearchResultVM
    {
        public List<UnitConversionSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
