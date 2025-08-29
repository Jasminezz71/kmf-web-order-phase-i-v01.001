using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class DashboardBC
    {
        public DashboardVM SearchPR(DashboardVM vm, string SortField, string Sorttype)
        {
            try
            {
                if (vm.dashboardSearchCriteriaVM == null) vm.dashboardSearchCriteriaVM = new DashboardSearchCriteriaVM();
                vm.dashboardSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.top_currentPage;
                vm.dashboardSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.top_rowPerPage;
                vm.dashboardSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.dashboardSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.dashboardSearchCriteriaVM.USER_NAME = vm.SessionLogin.USER_NAME;
                vm.dashboardSearchCriteriaVM.ROLE_NAME = vm.SessionLogin.ROLE_NAME;

                int countAll = 0;
                DashBoardDC dc = new DashBoardDC();
                if (vm.dashboardSearchResultVM == null) vm.dashboardSearchResultVM = new DashboardSearchResultVM();
                vm.dashboardSearchResultVM.resultPRList = dc.SearchDashboardPR(vm.dashboardSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.dashboardSearchResultVM.countPRAll = countAll;
                if (vm.dashboardSearchResultVM.countPRAll == 0)
                {
                    vm.dashboardSearchResultVM.messagePRList = new List<MessageET>();
                    vm.dashboardSearchResultVM.messagePRList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DashboardVM SearchDO(DashboardVM vm, string SortField, string Sorttype)
        {
            try
            {
                if (vm.dashboardSearchCriteriaVM == null) vm.dashboardSearchCriteriaVM = new DashboardSearchCriteriaVM();
                vm.dashboardSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.bottom_currentPage;
                vm.dashboardSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.bottom_rowPerPage;
                vm.dashboardSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.dashboardSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.dashboardSearchCriteriaVM.USER_NAME = vm.SessionLogin.USER_NAME;
                vm.dashboardSearchCriteriaVM.ROLE_NAME = vm.SessionLogin.ROLE_NAME;

                int countAll = 0;
                DashBoardDC dc = new DashBoardDC();
                if (vm.dashboardSearchResultVM == null) vm.dashboardSearchResultVM = new DashboardSearchResultVM();
                vm.dashboardSearchResultVM.resultDOList = dc.SearchDashboardDO(vm.dashboardSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.dashboardSearchResultVM.countDOAll = countAll;
                if (vm.dashboardSearchResultVM.countDOAll == 0)
                {
                    vm.dashboardSearchResultVM.messageDOList = new List<MessageET>();
                    vm.dashboardSearchResultVM.messageDOList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DashboardVM CheckAlreadyCount(DashboardVM vm)
        {
            try
            {
                DashboardET result = new DashboardET();
                vm.dashboardVM_MA = new DashboardET_MA();
                ItemStockDC dc = new ItemStockDC();
                result = dc.CheckAlreadyCount(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                vm.dashboardVM_MA.AlreadyCount = result.AlreadyCount;
                vm.dashboardVM_MA.MsgAlert = result.MsgAlert;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
