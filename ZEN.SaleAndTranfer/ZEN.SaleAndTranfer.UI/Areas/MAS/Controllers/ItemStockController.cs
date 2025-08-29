using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class ItemStockController : BaseController
    {
        private ItemStockVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_ITEM_STOCK] != null)
                {
                    return (ItemStockVM)Session[SessionConst.SESSION_ITEM_STOCK];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_ITEM_STOCK] = value;
            }
        }
        ItemStockBC itemStockBC = new ItemStockBC();
        private ItemStockVM createNewModel(ItemStockVM vm)
        {
            if (vm == null) vm = new ItemStockVM();
            if (vm.itemStockVM_MA == null) vm.itemStockVM_MA = new ItemStockET_MA();
            if (vm.itemStockSearchCriteriaVM == null) vm.itemStockSearchCriteriaVM = new ItemStockSearchCriteriaVM();
            if (vm.itemStockSearchResultVM == null) vm.itemStockSearchResultVM = new ItemStockSearchResultVM();
            return vm;
        }
        //
        // GET: /MAS/ItemStock/
        public ActionResult Index(ItemStockVM vm)
        {
            try
            {
                vm = new ItemStockVM();
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
        public ActionResult Search(ItemStockVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = itemStockBC.InventoryStockSearch(vm);
                vm = this.setValuePaginationResult(vm);
                _vmodel = vm;

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult InventoryStock(ItemStockVM vm)
        {
            try
            {
                vm = new ItemStockVM();
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialMA(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("InventoryStock", vm);
        }
        public ActionResult InventoryStockGetItem(ItemStockVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialMA(vm);
                vm.itemStockVM_MA.itemStockList = null;
                vm = itemStockBC.InventoryStockGetItem(vm);
                _vmodel = vm;
                ViewBag.showSessionTimeout = 1;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("InventoryStock", vm);
        }
        public ActionResult InventoryStockSave(ItemStockVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialMA(vm);
                ViewBag.showSessionTimeout = 1;
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Save " + ConfigNameEnum.STOCK_CARD.ToString(), "Save " + ConfigNameEnum.STOCK_CARD.ToString());
                vm = itemStockBC.InventoryStockSave(vm, batchID);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            LogWsHelper hp = new LogWsHelper();
                            hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.SAVE_STOCK_CARD.ToString(), this);
                            ViewBag.saveSuccessFlag = 1;
                        }
                    }
                }
                // End batch
                batchBC.EndBatch(batchID, vm.SessionLogin.USER_NAME);
            }
            catch (Exception ex)
            {
                batchBC.InsertBatchLog(batchID, "Error", ex.Message, ex.StackTrace, null, vm.SessionLogin.USER_NAME); // Insert Log Error
                batchBC.ErrorBatch(batchID, ex.Message); // Error batch
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("InventoryStock", vm);
        }

        public ActionResult ChangeBrand(ItemStockVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        #region === Innitial ===
        private ItemStockVM InnitialCriteria(ItemStockVM vm)
        {
            try
            {
                vm = itemStockBC.InnitialCriteria(vm);
                if (vm.SessionLogin.ROLE_NAME.ToLower() != "admin"
                    //&& vm.SessionLogin.ROLE_NAME.ToLower() != "warehouse"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "accounting"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "audit"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "rest-admin")
                {
                    vm.itemStockSearchCriteriaVM.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                        vm.itemStockSearchCriteriaVM.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                    if (vm.itemStockSearchCriteriaVM.branchList.Count == 2) // Rest Admin
                    {
                        ViewBag.CanEditBrand = 0;
                        ViewBag.CanEditBranch = 0;
                    }
                    else // Brand Admin
                    {
                        ViewBag.CanEditBrand = 0;
                        ViewBag.CanEditBranch = 1;
                    }
                }
                else
                {
                    if (vm.itemStockSearchCriteriaVM.BRAND_CODE == null)
                    {
                        ViewBag.CanEditBrand = 1;
                        ViewBag.CanEditBranch = 0;
                    }
                    else
                    {
                        ViewBag.CanEditBrand = 1;
                        ViewBag.CanEditBranch = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        private ItemStockVM InnitialMA(ItemStockVM vm)
        {
            try
            {
                vm = itemStockBC.InnitialMA(vm);
                vm.itemStockVM_MA.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.itemStockVM_MA.BRAND_NAME = vm.SessionLogin.BRAND_NAME;
                vm.itemStockVM_MA.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.itemStockVM_MA.BRANCH_NAME = vm.SessionLogin.BRANCH_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        #endregion

        #region === Pagination ===
        private ItemStockVM setValuePaginationCriteria(ItemStockVM vm)
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
        private ItemStockVM setValuePaginationResult(ItemStockVM vm)
        {
            try
            {
                if (vm.itemStockSearchResultVM != null)
                {
                    if (vm.itemStockSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.itemStockSearchResultVM.countAll,
                             vm.itemStockSearchResultVM != null ? vm.itemStockSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.itemStockSearchResultVM != null ? (vm.itemStockSearchResultVM.resultList[vm.itemStockSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
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