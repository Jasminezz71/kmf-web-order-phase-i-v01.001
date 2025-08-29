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
    public class PrVM : BaseVM
    {
        //public bool IsFranchises { get; set; }
        public bool showImageFlag { get; set; }
        public string mode { get; set; }
        public PrSearchCriteriaVM prSearchCriteriaVM { get; set; }
        public PrSearchResultVM prSearchResultVM { get; set; }
        public PrET_MA prVM_MA { get; set; }
        public bool FRANCHISES_FLAG { get; set; }
        public string RESULT_DUPPLICATE { get; set; }
        public string CHECK_DUPPLICATE_FLAG { get; set; }
    }
    public class PrSearchCriteriaVM : PrSearchCriteriaET
    {
        public List<DDLItemET> prCategoryList { get; set; }
    }

    public class PrET_MA : PrET
    {
        public List<DDLItemET> prCategoryList { get; set; }
        public List<PrET> itemCartList { get; set; }
        public int countItemCartAll { get; set; }
        public List<PrET> itemList { get; set; }
        public int countItemAll { get; set; }
        public USP_R_ST_PR_D_GetByPRCode_200_RET itemPrice { get; set; }
        public List<DDLItemET> fcLocation { get; set; }
    }

    public class PrSearchResultVM
    {
        public List<PrSearchResultET> resultList { get; set; }
        public int countAll { get; set; }

        public int ShowItemFlg { get; set; }

        public USP_R_ST_PR_D_GetByPRCode_200_RET itemPrice { get; set; }
    }
}
