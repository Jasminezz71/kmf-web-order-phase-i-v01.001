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
    public class GrVM : BaseVM
    {
        public bool showImageFlag { get; set; }
        public GrSearchCriteriaVM grSearchCriteriaVM { get; set; }
        public GrSearchResultVM grSearchResultVM { get; set; }
        public GrET_MA grVM_MA { get; set; }

        public List<USP_M_COMPLAIN_GetByType_RET> complainRETs { get; set; }
        public List<USP_M_COMPLAIN_GetByType_PET> complainPETs { get; set; }
    }
    public class GrSearchCriteriaVM : GrSearchCriteriaET
    {
    }

    public class GrET_MA : GrET
    {
        public List<GrET> resultList { get; set; }
        public int countAll { get; set; }
        public List<DDLItemET> doStatusCode { get; set; }
        
    }

    public class GrSearchResultVM
    {
        public List<GrSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
