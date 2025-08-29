using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.RPT;

namespace ZEN.SaleAndTranfer.VM.RPT
{
    public class InvoiceVM : BaseVM
    {
        public InvoiceSearchCriteriaVM invoiceSearchCriteriaVM { get; set; }
        public InvoiceSearchResultVM invoiceSearchResultVM { get; set; }
        public InvoiceET_MA invoiceVM_MA { get; set; }
    }
    public class InvoiceSearchCriteriaVM : InvoiceSearchCriteriaET
    {
        public List<DDLItemET> companyList { get; set; }
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }

    public class InvoiceET_MA
    {
        public string REPORT_DATA { get; set; }
    }

    public class InvoiceSearchResultVM
    {
    }
}
