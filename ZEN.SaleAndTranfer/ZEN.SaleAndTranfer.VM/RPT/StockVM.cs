using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.RPT;

namespace ZEN.SaleAndTranfer.VM.RPT
{
    public class StockVM : BaseVM
    {
        public StockSearchCriteriaVM stockSearchCriteriaVM { get; set; }
        public StockSearchResultVM stockSearchResultVM { get; set; }
        public StockET_MA stockVM_MA { get; set; }
        public DownloadVM stockDownloadVM { get; set; }

        public StatusDoGrSearchCriteriaVM statusDoGrSearchCriteriaVM { get; set; }
        public StatusDoGrSearchResultVM statusDoGrSearchResultVM { get; set; }
        public StatusDoGrET_MA statusDoGrVM_MA { get; set; }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    public class DownloadVM
    {
        //REPORT
        public byte[] EXPORT_DATA { get; set; }
        public string FILE_NAME { get; set; }
        public string CONTENT_TYPE { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    public class StockSearchCriteriaVM : stockSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }
    public class StockET_MA
    {
        public string REPORT_DATA { get; set; }
    }
    public class StockSearchResultVM
    {
        public List<stockSearchResultET> resultList { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    public class StatusDoGrSearchCriteriaVM : statusDoGrSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }
    public class StatusDoGrET_MA
    {
    }
    public class StatusDoGrSearchResultVM
    {
        public List<statusDoGrSearchResultET> resultList { get; set; }
    }

}
