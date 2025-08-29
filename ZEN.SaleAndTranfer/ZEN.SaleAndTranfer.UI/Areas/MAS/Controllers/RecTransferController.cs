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
    public class RecTransferController : BaseController
    {
        //
        // GET: /MAS/RecTransfer/
        private RecTransferVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_REC_TRANSFER] != null)
                {
                    return (RecTransferVM)Session[SessionConst.SESSION_REC_TRANSFER];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_REC_TRANSFER] = value;
            }
        }
        RecTransferBC recTransferBC = new RecTransferBC();
        private RecTransferVM createNewModel(RecTransferVM vm)
        {
            if (vm == null) vm = new RecTransferVM();
            if (vm.recTransferVM_MA == null) vm.recTransferVM_MA = new RecTransferET_MA();
            if (vm.recTransferSearchCriteriaVM == null) vm.recTransferSearchCriteriaVM = new RecTransferSearchCriteriaVM();
            if (vm.recTransferSearchResultVM == null) vm.recTransferSearchResultVM = new RecTransferSearchResultVM();
            return vm;
        }

        public ActionResult Index(RecTransferVM vm)
        {
            try
            {
                vm = new RecTransferVM();
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                _vmodel = null;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(RecTransferVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                PrBC bc = new PrBC();
                vm = this.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = recTransferBC.Search(vm);
                vm = this.setValuePaginationResult(vm);

                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
            }
            return View("Index", vm);
        }
        public ActionResult ExportFile(RecTransferVM vm)
        {
            var currentVM = _vmodel;
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                vm = recTransferBC.ExportFile(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                currentVM.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("Index", currentVM);
            }
            return File(vm.recTransferVM_MA.EXPORT_DATA, vm.recTransferVM_MA.CONTENT_TYPE, vm.recTransferVM_MA.FILE_NAME);
        }

        #region === Modal ===
        public ActionResult ModalBrand(RecTransferVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                ViewBag.ShowBrandModal = 1;
                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ModalBrandSave(RecTransferVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = recTransferBC.SetBrandByCheckbox(_vmodel, vm.recTransferSearchCriteriaVM.brandList);
                vm = this.InnitialCriteria(vm);
                ViewBag.ShowBrandModal = 0;
                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ModalBranch(RecTransferVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                ViewBag.ShowBranchModal = 1;
                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ModalBranchSave(RecTransferVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = recTransferBC.SetBranchByCheckbox(_vmodel, vm.recTransferSearchCriteriaVM.branchList);
                vm = this.InnitialCriteria(vm);
                ViewBag.ShowBranchModal = 0;
                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        #endregion

        #region === Innitial ===
        private RecTransferVM InnitialCriteria(RecTransferVM vm)
        {
            try
            {
                vm = recTransferBC.InnitialCriteria(vm);
                if (vm.SessionLogin.ROLE_NAME.ToLower() != "admin"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "warehouse"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "accounting"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "audit"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "rest-admin")
                {
                    if (vm.recTransferSearchCriteriaVM.branchList.Count > 2) // More Brand
                    {
                        vm.recTransferSearchCriteriaVM.BRAND_CODE = null;
                        vm.recTransferSearchCriteriaVM.BRANCH_CODE = null;
                    }
                    else // All, Own Brand
                    {
                        vm.recTransferSearchCriteriaVM.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                        vm.recTransferSearchCriteriaVM.BRAND_NAME = vm.SessionLogin.BRAND_CODE;
                        vm.recTransferSearchCriteriaVM.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                        vm.recTransferSearchCriteriaVM.BRANCH_NAME = vm.SessionLogin.BRANCH_CODE;

                        // Cant Choose Brand and Branch
                        ViewBag.chooseBranch = 0;
                        ViewBag.chooseBrand = 0;

                    }
                }
                else
                {
                    if (vm.recTransferSearchCriteriaVM.BRAND_CODE == null)
                    {
                        ViewBag.chooseBrand = 1;
                        ViewBag.chooseBranch = 0;
                    }
                    else
                    {
                        ViewBag.chooseBrand = 1;
                        ViewBag.chooseBranch = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        //private RecTransferVM InnitialCriteria(RecTransferVM vm)
        //{
        //    try
        //    {
        //        vm = recTransferBC.InnitialCriteria(vm);
        //        if (vm.SessionLogin.ROLE_NAME.ToLower() != "admin"
        //            //&& vm.SessionLogin.ROLE_NAME.ToLower() != "warehouse"
        //            && vm.SessionLogin.ROLE_NAME.ToLower() != "accounting"
        //            && vm.SessionLogin.ROLE_NAME.ToLower() != "audit"
        //            && vm.SessionLogin.ROLE_NAME.ToLower() != "rest-admin")
        //        {
        //            vm.recTransferSearchCriteriaVM.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
        //            vm.recTransferSearchCriteriaVM.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
        //            if (vm.recTransferSearchCriteriaVM.branchList.Count == 2) // Rest Admin
        //            {
        //                ViewBag.CanEditBrand = 0;
        //                ViewBag.CanEditBranch = 0;
        //            }
        //            else // Brand Admin
        //            {
        //                ViewBag.CanEditBrand = 0;
        //                ViewBag.CanEditBranch = 1;
        //            }
        //        }
        //        else
        //        {
        //            if (vm.recTransferSearchCriteriaVM.BRAND_CODE == null)
        //            {
        //                ViewBag.CanEditBrand = 1;
        //                ViewBag.CanEditBranch = 0;
        //            }
        //            else
        //            {
        //                ViewBag.CanEditBrand = 1;
        //                ViewBag.CanEditBranch = 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return vm;
        //}
        private RecTransferVM InnitialMA(RecTransferVM vm)
        {
            try
            {
                vm = recTransferBC.InnitialMA(vm);
                vm.recTransferVM_MA.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.recTransferVM_MA.BRAND_NAME = vm.SessionLogin.BRAND_NAME;
                vm.recTransferVM_MA.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.recTransferVM_MA.BRANCH_NAME = vm.SessionLogin.BRANCH_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        #endregion

        #region === Pagination ===
        private RecTransferVM setValuePaginationCriteria(RecTransferVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.pagination == 0) { vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private RecTransferVM setValuePaginationResult(RecTransferVM vm)
        {
            try
            {
                if (vm.recTransferSearchResultVM != null)
                {
                    if (vm.recTransferSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.recTransferSearchResultVM.countAll,
                             vm.recTransferSearchResultVM != null ? vm.recTransferSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.recTransferSearchResultVM != null ? (vm.recTransferSearchResultVM.resultList[vm.recTransferSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}