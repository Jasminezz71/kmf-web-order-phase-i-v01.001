using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class DownloadVM : BaseVM
    {
        public Download_MA downloadMAVM { get; set; }
        public DownloadSearchCriteriaET_Criteria downloadSearchCriteriaVM { get; set; }
        public DownloadSearchResultET_Result downloadSearchResultVM { get; set; }
    }
    public class DownloadSearchResultET_Result
    {
        public int COUNT_ALL { get; set; }
        public List<DownloadSearchResultET> resultList { get; set; }
    }
    public class DownloadSearchCriteriaET_Criteria : DownloadSearchCriteriaET
    {
    }
    public class Download_MA : DownloadET
    {
    }
}
