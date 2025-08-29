using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class RecTransferVM : BaseVM
    {
        public RecTransferSearchCriteriaVM recTransferSearchCriteriaVM { get; set; }
        public RecTransferSearchResultVM recTransferSearchResultVM { get; set; }
        public RecTransferET_MA recTransferVM_MA { get; set; }
    }
    public class RecTransferSearchCriteriaVM : RecTransferSearchCriteriaET
    {
        public List<CBXItemET> brandList { get; set; }
        public List<CBXItemET> branchList { get; set; }
    }

    public class RecTransferET_MA : RecTransferET
    {
    }

    public class RecTransferSearchResultVM
    {
        public List<RecTransferSearchResultET> resultList { get; set; }
        public List<RecTransferSearchResultET> exportList { get; set; }
        public int countAll { get; set; }
    }
}
