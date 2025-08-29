using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class DashboardController : BaseController
    {
        private DashboardVM createNewModel(DashboardVM vm)
        {
            if (vm == null) vm = new DashboardVM();
            if (vm.dashboardVM_MA == null) vm.dashboardVM_MA = new DashboardET_MA();
            if (vm.dashboardSearchCriteriaVM == null) vm.dashboardSearchCriteriaVM = new DashboardSearchCriteriaVM();
            if (vm.dashboardSearchResultVM == null) vm.dashboardSearchResultVM = new DashboardSearchResultVM();
            return vm;
        }
        //
        // GET: /MAS/Dashboard/
        public ActionResult Index(DashboardVM vm)
        {
            try
            {
                vm.MessageList.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DashboardBC bc = new DashboardBC();
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchPR(vm, null, null);
                vm = bc.SearchDO(vm, null, null);
                vm = this.setValuePaginationResult(vm);
                vm = bc.CheckAlreadyCount(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }


        private DashboardVM setValuePaginationCriteria(DashboardVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.top_pagination == 0) { vm.ZenPagination.top_pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.top_rowPerPage == 0) { vm.ZenPagination.top_rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.top_currentPage == 0) { vm.ZenPagination.top_currentPage = 1; }

                if (vm.ZenPagination.bottom_pagination == 0) { vm.ZenPagination.bottom_pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.bottom_rowPerPage == 0) { vm.ZenPagination.bottom_rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.bottom_currentPage == 0) { vm.ZenPagination.bottom_currentPage = 1; }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DashboardVM setValuePaginationResult(DashboardVM vm)
        {
            try
            {
                if (vm.dashboardSearchResultVM != null)
                {
                    if (vm.dashboardSearchResultVM.countPRAll != 0)
                    {
                        this.InitPaginationDashBoardTop
                            (vm,
                             vm.dashboardSearchResultVM.countPRAll,
                             vm.dashboardSearchResultVM != null ? vm.dashboardSearchResultVM.resultPRList[0].ROW_NO : 0,
                             vm.dashboardSearchResultVM != null ? (vm.dashboardSearchResultVM.resultPRList[vm.dashboardSearchResultVM.resultPRList.Count - 1].ROW_NO) : 0);
                    }
                    if (vm.dashboardSearchResultVM.countDOAll != 0)
                    {
                        this.InitPaginationDashBoardBottom
                            (vm,
                             vm.dashboardSearchResultVM.countDOAll,
                             vm.dashboardSearchResultVM != null ? vm.dashboardSearchResultVM.resultDOList[0].ROW_NO : 0,
                             vm.dashboardSearchResultVM != null ? (vm.dashboardSearchResultVM.resultDOList[vm.dashboardSearchResultVM.resultDOList.Count - 1].ROW_NO) : 0);
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}