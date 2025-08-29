using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.ADMIN;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.VM.ADMIN
{
    public class BatchHaviVM : BaseVM
    {
        public BatchHaviSearchCriteriaVM batchSearchCriteriaVM { get; set; }
        public BatchHaviSearchResultVM batchResultResultVM { get; set; }
        public BatchHaviET_MA batchVM_MA { get; set; }
    }
    public class BatchHaviSearchCriteriaVM : BatchHaviSearchCriteriaET
    {
    }

    public class BatchHaviET_MA : BatchHaviET
    {
        public List<DDLItemET> companyList { get; set; }
    }

    public class BatchHaviSearchResultVM
    {
        public List<UserBatchHaviInfoET> resultList { get; set; }
        public int CountAll { get; set; }
    }
}
