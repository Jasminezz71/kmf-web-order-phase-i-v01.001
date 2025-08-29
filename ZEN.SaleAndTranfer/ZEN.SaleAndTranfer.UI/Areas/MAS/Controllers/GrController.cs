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
    public class GrController : BaseController
    {
        private GrVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_GR_MODEL] != null)
                {
                    return (GrVM)Session[SessionConst.SESSION_GR_MODEL];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_GR_MODEL] = value;
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
        private GrVM createNewModel(GrVM vm)
        {
            if (vm == null) vm = new GrVM();
            if (vm.grVM_MA == null) vm.grVM_MA = new GrET_MA();
            if (vm.grSearchCriteriaVM == null) vm.grSearchCriteriaVM = new GrSearchCriteriaVM();
            if (vm.grSearchResultVM == null) vm.grSearchResultVM = new GrSearchResultVM();
            return vm;
        }
        //
        // GET: /MAS/Gr/
        public ActionResult Index(GrVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = new GrVM();
                GrBC bc = new GrBC();
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
        public ActionResult Search(GrVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                GrBC bc = new GrBC();
                vm = bc.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchGr(vm, null, null);
                vm = this.setValuePaginationResult(vm);

                _vmodel = vm;
                ModelState.Clear();
                List<SortingInfoET> orderList = new List<SortingInfoET>();
                if (_orderprmodel == null)
                {
                    orderList = bc.SetOrderListGR();
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
                GrBC bc = new GrBC();
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
        public ActionResult Update(GrVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                GrBC bc = new GrBC();
                vm = bc.InnitialMA(vm);
                vm = bc.SearchGrByID(vm);
                vm.FormMode = "PageEditMode";  //FormMode ส่วนของการประเมิน SupaneeJ 20190124
                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Update", vm);
        }
        public ActionResult UpdateSave(GrVM vm)
        {
            vm.SessionLogin = GetCurrentUser;
            LogWsHelper hp = new LogWsHelper();
            try
            {
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.GR__EDIT__BEGIN.ToString(), this);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                GrBC bc = new GrBC();
                vm = bc.InnitialMA(vm);
                vm = bc.UpdateSave(vm);
                vm = bc.SearchGrByID(vm);
                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        vm.FormMode = "PageEditMode";  //FormMode ส่วนของการประเมิน SupaneeJ 20190124
                        return View("Update", vm);
                    }
                    else if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        vm.FormMode = "PageDisplayMode";  //FormMode ส่วนของการประเมิน SupaneeJ 20190124
                        hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.GR__EDIT__SUCCESS.ToString(), this);
                        return View("Detail", vm);
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            
            return View("Update", vm);
        }
        public ActionResult Detail(GrVM vm, string id)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                GrBC bc = new GrBC();
                vm = bc.InnitialMA(vm);
                if (id != null) vm.grVM_MA.ST_GR_CODE = id;
                vm = bc.SearchGrByID(vm);
                vm.FormMode = "PageDisplayMode"; //FormMode ส่วนของการประเมิน SupaneeJ 20190124
                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Detail", vm);
        }
        public ActionResult GrDelete(GrVM vm, string id)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                GrBC bc = new GrBC();
                vm = bc.InnitialMA(vm);
                if (id != null) vm.grVM_MA.ST_GR_CODE = id;
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.GrDelete(vm);
                vm = this.setValuePaginationResult(vm);
                ClearItemCartSesstion();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        private GrVM setValuePaginationCriteria(GrVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.pagination == 0) { vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
                vm.grSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.grSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private GrVM setValuePaginationResult(GrVM vm)
        {
            try
            {
                if (vm.grSearchResultVM != null)
                {
                    if (vm.grSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.grSearchResultVM.countAll,
                             vm.grSearchResultVM != null ? vm.grSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.grSearchResultVM != null ? (vm.grSearchResultVM.resultList[vm.grSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
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