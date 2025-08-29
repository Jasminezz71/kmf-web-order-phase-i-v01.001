using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class UnitConvertVM : BaseVM
    {
        public UnitConvertSearchCriteriaVM unitConvertSearchCriteriaVM { get; set; }
        public UnitConvertSearchResultVM unitConvertSearchResultVM { get; set; }
        public UnitConvertET_MA unitConvertVM_MA { get; set; }
    }
    public class UnitConvertSearchCriteriaVM : UnitConvertSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }

    public class UnitConvertET_MA : UnitConvertET
    {
    }

    public class UnitConvertSearchResultVM
    {
        public List<UnitConvertSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
