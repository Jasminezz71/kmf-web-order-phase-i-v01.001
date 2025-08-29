using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class DownloadBC
    {
        public DownloadVM Search(DownloadVM vm)
        {
            try
            {
                DownloadDC dc = new DownloadDC();
                if (vm.downloadSearchCriteriaVM == null) vm.downloadSearchCriteriaVM = new DownloadSearchCriteriaET_Criteria();
                vm.downloadSearchCriteriaVM.ROLE_NAME = vm.SessionLogin.ROLE_NAME;
                vm.downloadSearchCriteriaVM.APPLICATION_TYPE = "ST";
                if (vm.downloadSearchResultVM == null) vm.downloadSearchResultVM = new DownloadSearchResultET_Result();
                if (vm.downloadSearchResultVM.resultList == null) vm.downloadSearchResultVM.resultList = new List<ET.MAS.DownloadSearchResultET>();
                vm.downloadSearchResultVM.resultList = dc.Search(vm.downloadSearchCriteriaVM);
                vm.downloadSearchResultVM.COUNT_ALL = vm.downloadSearchResultVM.resultList.Count;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
