using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class SignatureVM : BaseVM
    {
        public SignatureSearchCriteriaVM signatureSearchCriteriaVM { get; set; }
        public SignatureSearchResultVM signatureSearchResultVM { get; set; }
        public SignatureET_MA signatureVM_MA { get; set; }
    }
    public class SignatureSearchCriteriaVM : SignatureSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
        public List<DDLItemET> companyList { get; set; }
    }

    public class SignatureET_MA : SignatureET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
        public List<DDLItemET> companyList { get; set; }
        public List<SignatureET> fileUploadList { get; set; }
    }

    public class SignatureSearchResultVM
    {
        public List<SignatureSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
