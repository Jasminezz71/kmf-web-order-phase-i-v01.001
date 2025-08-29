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
    public class DoVM : BaseVM
    {
        public bool showImageFlag { get; set; }
        public DoSearchCriteriaVM doSearchCriteriaVM { get; set; }
        public DoSearchResultVM doSearchResultVM { get; set; }
        public DoET_MA doVM_MA { get; set; }
        //Supaneej, 2018-10-19, ST(Enhance) for Complain
        public List<USP_M_COMPLAIN_GetByType_RET> complainRETs { get; set; }
    }
    public class DoSearchCriteriaVM : DoSearchCriteriaET
    {
        public List<DDLItemET> doList { get; set; }
    }

    public class DoET_MA : DoET
    {
        public List<DDLItemET> doList { get; set; }
        public List<DoET> resultList { get; set; }
        public int countAll { get; set; }
        public List<DDLItemET> doStatusCode { get; set; }
    }

    public class DoSearchResultVM
    {
        public List<DoSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
