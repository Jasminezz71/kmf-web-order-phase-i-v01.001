using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.ADMIN;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.VM.ADMIN
{
    public class BatchVM : BaseVM
    {
        public BatchSearchCriteriaVM batchSearchCriteriaVM { get; set; }
        public BatchSearchResultVM batchResultResultVM { get; set; }
        public BatchET_MA batchVM_MA { get; set; }
    }
    public class BatchSearchCriteriaVM : BatchSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }

    public class BatchET_MA : BatchET
    {
        public string brandCode { get; set; }
        public string branchCode { get; set; }
        public string processBy { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }

    public class BatchSearchResultVM
    {
        public List<UserBatchInfoET> resultList { get; set; }
        public int CountAll { get; set; }
    }
}
