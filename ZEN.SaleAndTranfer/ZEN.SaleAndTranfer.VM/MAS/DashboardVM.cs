using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class DashboardVM : BaseVM
    {
        public DashboardSearchCriteriaVM dashboardSearchCriteriaVM { get; set; }
        public DashboardSearchResultVM dashboardSearchResultVM { get; set; }
        public DashboardET_MA dashboardVM_MA { get; set; }
    }
    public class DashboardSearchCriteriaVM : DashboardSearchCriteriaET
    {
    }

    public class DashboardET_MA : DashboardET
    {
    }

    public class DashboardSearchResultVM
    {
        public List<DashboardSearchResultET> resultPRList { get; set; }
        public List<DashboardSearchResultET> resultDOList { get; set; }
        public List<MessageET> messagePRList { get; set; }
        public List<MessageET> messageDOList { get; set; }
        public int countPRAll { get; set; }
        public int countDOAll { get; set; }
    }
}
