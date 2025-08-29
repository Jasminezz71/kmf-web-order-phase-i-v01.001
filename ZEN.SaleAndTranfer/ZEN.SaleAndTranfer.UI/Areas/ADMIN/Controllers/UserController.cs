using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.ADMIN;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.UI.Areas.ADMIN.Controllers
{
    public class UserController : BaseController
    {
        private UserVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_USER_MODEL] != null)
                {
                    return (UserVM)Session[SessionConst.SESSION_USER_MODEL];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_USER_MODEL] = value;
            }
        }
        private List<SortingInfoET> _orderprmodel
        {
            get
            {
                if (Session["ORDERBY_VMODEL"] != null)
                {
                    return (List<SortingInfoET>)Session["ORDERBY_VMODEL"];
                }

                return null;
            }

            set
            {
                Session["ORDERBY_VMODEL"] = value;
            }
        }
        //
        // GET: /ADMIN/User/
        public ActionResult Index(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = new UserVM();
                UserBC bc = new UserBC();
                vm = bc.InnitialCriteria(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(UserVM vm)
        {
            try
            {
                ViewBag.PageTitle = "จัดการผู้ใช้งาน";
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                if (vm.userSearchCriteriaVM == null) { vm.userSearchCriteriaVM = new UserSearchCriteriaVM(); }
                UserBC bc = new UserBC();
                vm = bc.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchUser(vm, null, null);
                vm = this.setValuePaginationResult(vm);

                _vmodel = vm;
                ModelState.Clear();
                List<SortingInfoET> orderList = new List<SortingInfoET>();
                if (_orderprmodel == null)
                {
                    orderList = bc.SetOrderListPR();
                    _orderprmodel = orderList;
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
                return View("Index", vm);
            }
            return View("Index", vm);
        }
        public ActionResult SearchOrderByMenu(string id)
        {
            var vm = _vmodel;
            var orderList = _orderprmodel;
            try
            {
                vm.SortingInfoList = orderList;
                UserBC bc = new UserBC();
                vm = bc.OrderByMenu(id, vm);
                _orderprmodel = vm.SortingInfoList;
                ViewBag.searchOrderByMenu = 1;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                ModelState.Clear();
            }
            return View("Index", vm);
        }
        private UserVM setValuePaginationCriteria(UserVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.pagination == 0) { vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
                vm.userSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.userSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private UserVM setValuePaginationResult(UserVM vm)
        {
            try
            {
                if (vm.userSearchResultVM != null)
                {
                    if (vm.userSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.userSearchResultVM.countAll,
                             vm.userSearchResultVM != null ? vm.userSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.userSearchResultVM != null ? (vm.userSearchResultVM.resultList[vm.userSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Create(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = new UserVM();
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm.userVM_MA.ACTIVE_FLAG = "1";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Create", vm);
        }
        public ActionResult CreateSave(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.CreateSave(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Create", vm);
        }
        public ActionResult Update(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.SearchUserByID(vm);
                vm = bc.InnitialMA(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Update", vm);
        }
        public ActionResult UpdateSave(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.UpdateSave(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Update", vm);
        }
        public ActionResult OnChangeBrand(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.OnChangeBrand(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            if (vm.userVM_MA.PAGE_MODE == "CREATE")
            {
                return View("Create", vm);
            }
            else
            {
                return View("Update", vm);
            }
        }
        public ActionResult ResetPassword(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.InnitialCriteria(vm);
                vm = bc.ResetPassword(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        vm = bc.InnitialCriteria(vm);
                        vm = this.setValuePaginationCriteria(vm);
                        vm = bc.SearchUser(vm, null, null);
                        vm = this.setValuePaginationResult(vm);
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ActiveUser(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.ChangeStatusUser(vm, "1");
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        vm = bc.InnitialCriteria(vm);
                        vm = this.setValuePaginationCriteria(vm);
                        vm = bc.SearchUser(vm, null, null);
                        vm = this.setValuePaginationResult(vm);
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult InActiveUser(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UserBC bc = new UserBC();
                vm = bc.InnitialMA(vm);
                vm = bc.ChangeStatusUser(vm, "0");
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        vm = bc.InnitialCriteria(vm);
                        vm = this.setValuePaginationCriteria(vm);
                        vm = bc.SearchUser(vm, null, null);
                        vm = this.setValuePaginationResult(vm);
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        [HttpGet]
        public ActionResult BlockUser(UserVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("BlockUser", vm);
        }

        [HttpPost]
        public ActionResult BlockUserSave(UserVM vm)
        {
            try
            {

                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                // this.insertLog(vm.SessionLogin.USER_NAME, this);
                UserBC bc = new UserBC();
                vm = bc.BlockUser(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            ViewBag.BlockUserFlag = 1;
                            vm.MessageList.Clear();
                            vm.userVM_MA = new UserET_MA();
                        }
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("BlockUser", vm);
            }
            return View("BlockUser", vm);
        }

        public ActionResult UnblockUserSave(UserVM vm)
        {
            try
            {

                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                // this.insertLog(vm.SessionLogin.USER_NAME, this);
                UserBC bc = new UserBC();
                vm = bc.UnblockUser(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            ViewBag.BlockUserFlag = 2;
                            vm.MessageList.Clear();
                            vm.userVM_MA = new UserET_MA();
                        }
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("BlockUser", vm);
            }
            return View("BlockUser", vm);
        }

    }
}