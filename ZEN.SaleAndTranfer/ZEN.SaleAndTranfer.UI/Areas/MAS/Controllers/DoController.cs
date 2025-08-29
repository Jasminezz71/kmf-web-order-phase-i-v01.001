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
    public class DoController : BaseController
    {
        private DoVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_DO_MODEL] != null)
                {
                    return (DoVM)Session[SessionConst.SESSION_DO_MODEL];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_DO_MODEL] = value;
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
        // GET: /MAS/Do/
        public ActionResult Index(DoVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = new DoVM();
                //DoBC bc = new DoBC();
                //vm = bc.InnitialCriteria(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(DoVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DoBC bc = new DoBC();
                vm = bc.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchDo(vm, null, null);
                vm = this.setValuePaginationResult(vm);

                _vmodel = vm;
                ModelState.Clear();
                List<SortingInfoET> orderList = new List<SortingInfoET>();
                if (_orderprmodel == null)
                {
                    orderList = bc.SetOrderListDO();
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
                DoBC bc = new DoBC();
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
        public ActionResult ReceiveItem(DoVM vm, string id)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DoBC bc = new DoBC();
                vm = bc.InnitialMA(vm);
                vm = bc.InnitialCriteria(vm);
                if (id != null)
                {
                    string[] split = id.Split('|');
                    vm.doVM_MA.ST_DO_CODE = split[0];
                    vm.doVM_MA.ST_PR_CODE = split[1];
                    vm.doSearchCriteriaVM.ST_PR_CODE = split[1];
                    vm = bc.InnitialMA(vm);
                    vm = bc.InnitialCriteria(vm);
                }
                else
                {
                    vm.doSearchCriteriaVM.ST_PR_CODE = vm.doVM_MA.ST_PR_CODE;
                    vm.doVM_MA.RECEIVE_DATE = null;
                }
                //vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchDoByID(vm);
                //vm = this.setValuePaginationResult(vm);

                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);

                _vmodel = vm;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
                return View("Index", vm);
            }
            return View("ReceiveItem", vm);
        }
        public ActionResult ReceiveItemSave(DoVM vm)
        {
            vm.SessionLogin = GetCurrentUser;
            LogWsHelper hp = new LogWsHelper();
            try
            {
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.DO__SAVE__BEGIN.ToString(), this);

                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DoBC bc = new DoBC();
                vm = bc.InnitialMA(vm);
                vm = bc.InnitialCriteria(vm);
                vm = bc.ReceiveItemSave(vm);
                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        //vm = bc.SearchDoByID(vm);
                        ModelState.Clear();
                        
                        return View("ReceiveItem", vm);
                    }
                    else if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        if (vm.doVM_MA.ST_GR_CODE != null)
                        {
                            string url = "/MAS/Gr/Detail/" + vm.doVM_MA.ST_GR_CODE;
                            hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.DO__SAVE__SUCCESS.ToString(), this);
                            return Redirect(url);
                        }
                    }
                }
                ModelState.Clear();

                //hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.DO__SAVE__SUCCESS.ToString(), this);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
                return View("ReceiveItem", vm);
            }
            

            return View("ReceiveItem", vm);
        }


        private DoVM setValuePaginationCriteria(DoVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.pagination == 0) { vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
                vm.doSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.doSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DoVM setValuePaginationResult(DoVM vm)
        {
            try
            {
                if (vm.doSearchResultVM != null)
                {
                    if (vm.doSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.doSearchResultVM.countAll,
                             vm.doSearchResultVM != null ? vm.doSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.doSearchResultVM != null ? (vm.doSearchResultVM.resultList[vm.doSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
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