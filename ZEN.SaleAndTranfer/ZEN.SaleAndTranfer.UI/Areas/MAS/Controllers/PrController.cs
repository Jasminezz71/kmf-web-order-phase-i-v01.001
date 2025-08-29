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
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class PrController : BaseController
    {
        private PrVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_PR_MODEL] != null)
                {
                    return (PrVM)Session[SessionConst.SESSION_PR_MODEL];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_PR_MODEL] = value;
            }
        }
        private PrVM _searchAddItemCartmodel
        {
            get
            {
                if (Session["SearchAddItemCart_VMODEL"] != null)
                {
                    return (PrVM)Session["SearchAddItemCart_VMODEL"];
                }

                return null;
            }

            set
            {
                Session["SearchAddItemCart_VMODEL"] = value;
            }
        }
        private List<PrET> _itemModel
        {
            get
            {
                if (Session[SessionConst.ITEM_CART_PR] != null)
                {
                    return (List<PrET>)Session[SessionConst.ITEM_CART_PR];
                }

                return null;
            }

            set
            {

                Session[SessionConst.ITEM_CART_PR] = this.SetRowItem(value, null, null, null);
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
        private string _MODE
        {
            get
            {
                if (Session[SessionConst.MODE_MODEL] != null)
                {
                    return (string)Session[SessionConst.MODE_MODEL];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (value == null)
                {
                    Response.Cookies[SessionConst.MODE_MODEL].Expires = DateTime.Now.AddDays(-1);
                }
                else
                {
                    HttpCookie modeCookeis = new HttpCookie(SessionConst.MODE_MODEL);
                    modeCookeis.Value = value;
                    modeCookeis.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(modeCookeis);
                    Session[SessionConst.MODE_MODEL] = value;
                }
            }
        }
        private string _updatePRCode
        {
            get
            {
                if (Session[SessionConst.UPDATE_PR_MODEL] != null)
                {
                    return (string)Session[SessionConst.UPDATE_PR_MODEL];
                }

                return null;
            }

            set
            {
                if (value == null)
                {
                    Response.Cookies[SessionConst.UPDATE_PR_MODEL].Expires = DateTime.Now.AddDays(-1);
                }
                else
                {
                    HttpCookie updatePRModelCookies = new HttpCookie(SessionConst.UPDATE_PR_MODEL);
                    updatePRModelCookies.Value = value;
                    updatePRModelCookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(updatePRModelCookies);
                    Session[SessionConst.UPDATE_PR_MODEL] = value;
                }

            }
        }
        private DateTime? _planDeliveryDate
        {
            get
            {
                if (Session[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE] != null)
                {
                    return (DateTime)Session[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE] = value;
            }
        }
        private string _remark
        {
            get
            {
                if (Session[SessionConst.ITEM_CART_ORDER_NAME] != null)
                {
                    return (string)Session[SessionConst.ITEM_CART_ORDER_NAME];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session[SessionConst.ITEM_CART_ORDER_NAME] = value;
            }
        }

        private string _fcLocation
        {
            get
            {
                if (Session[SessionConst.ITEM_CART_FC_LOCATION] != null)
                {
                    return (string)Session[SessionConst.ITEM_CART_FC_LOCATION];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session[SessionConst.ITEM_CART_FC_LOCATION] = value;
            }
        }
        private PrVM createNewModel(PrVM vm)
        {
            if (vm == null) vm = new PrVM();
            if (vm.prVM_MA == null) vm.prVM_MA = new PrET_MA();
            if (vm.prSearchCriteriaVM == null) vm.prSearchCriteriaVM = new PrSearchCriteriaVM();
            if (vm.prSearchResultVM == null) vm.prSearchResultVM = new PrSearchResultVM();
            return vm;
        }

        //
        // GET: /MAS/Pr/
        public ActionResult Index(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = new PrVM();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                PrBC bc = new PrBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                if (vm.prSearchCriteriaVM == null) { vm.prSearchCriteriaVM = new PrSearchCriteriaVM(); }
                PrBC bc = new PrBC();
                vm = bc.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchPr(vm, null, null);
                vm = this.setValuePaginationResult(vm);

                _vmodel = vm;
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
            }
            return View("Index", vm);
        }
        public ActionResult SearchOrderByMenu(string id)
        {
            ModelState.Clear();
            var vm = _vmodel;
            var orderList = _orderprmodel;
            try
            {
                vm.SortingInfoList = orderList;
                PrBC bc = new PrBC();
                vm = bc.OrderByMenu(id, vm);
                _orderprmodel = vm.SortingInfoList;
                ViewBag.searchOrderByMenu = 1;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                ModelState.Clear();
            }
            return View("Index", vm);
        }
        public ActionResult CreatePR(PrVM vm)
        {
            try
            {
                _vmodel = null;
                ModelState.Clear();
                InitMesssageViewBagUI();
                ClearItemCartSesstion();
                vm.SessionLogin = GetCurrentUser;
                this.ItemCart(vm);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                vm.prVM_MA = vm.prVM_MA ?? new PrET_MA();
                vm.prVM_MA.REMARK = vm.SessionLogin.FIRST_NAME_TH + " " + vm.SessionLogin.LAST_NAME_TH;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ItemCart", vm);
        }
        public ActionResult ItemCart(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = _vmodel;
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                //vm = bc.CheckSessionHasBranch(vm);
                if (vm.prVM_MA.itemCartList == null) vm.prVM_MA.itemCartList = new List<PrET>();
                //vm.prVM_MA.PLAN_DELIVERY_DATE = DateTime.Now.AddDays(2); //comment by kodchakorn 10/02/2017 issue meeting request by brand
                if (vm.prVM_MA.REMARK == null) vm.prVM_MA.REMARK = vm.SessionLogin.FIRST_NAME_TH + " " + vm.SessionLogin.LAST_NAME_TH;
                if (_remark != null) vm.prVM_MA.REMARK = _remark;
                if (_planDeliveryDate != null) vm.prVM_MA.PLAN_DELIVERY_DATE = _planDeliveryDate;
                //vm.prVM_MA.itemCartList = _itemModel;
                if (_MODE == "CREATE")
                {
                    if (vm.MessageList != null) vm.MessageList.Clear();
                    vm.prVM_MA.itemCartList = _itemModel;
                    this.PrCalculatorPrice(_itemModel, vm);
                }
                else
                {
                    ClearItemCartSesstion();
                    vm.prVM_MA.itemCartList = _itemModel;
                    this.PrCalculatorPrice(_itemModel, vm);
                }
                //if (vm.MessageList.Count > 0)
                //    ViewBag.checkSessionHasBranch = 1;

                vm.mode = "CREATE";
                _MODE = vm.mode;

                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ItemCart", vm);
        }
        public ActionResult ItemCartSave(PrVM vm)
        {
            LogWsHelper hp = new LogWsHelper();
            vm.SessionLogin = GetCurrentUser;

            try
            {
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.PR__SAVE__BEGIN.ToString(), this);

                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);

                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                setNewValueItemCart(_itemModel, vm.prVM_MA.itemCartList);
                vm.prVM_MA.itemCartList = _itemModel;
                vm = bc.ItemCartSave(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return View("ItemCart", vm);
                    }
                    else if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        _MODE = null;
                        _itemModel = null;
                        _vmodel = null;
                    }
                }
                vm = bc.PrViewDetail(vm);
                if (vm.prVM_MA.PR_STATUS_CODE == 6)
                { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00071)); }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("ItemCart", vm);
            }
            ClearItemCartSesstion();

            hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.PR__SAVE__SUCCESS.ToString(), this);

            return View("PrViewDetail", vm);
        }
        public ActionResult UpdatePR(PrVM vm, string id)
        {
            try
            {
                _vmodel = null;
                ClearItemCartSesstion();
                this.ItemCartUpdate(vm, id);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ItemCartUpdate", vm);
        }
        public ActionResult ItemCartUpdate(PrVM vm, string id)
        {
            try
            {
                vm.mode = "UPDATE";
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                if (id != null) vm.prVM_MA.ST_PR_CODE = id;
                vm = bc.PrViewDetail(vm);
                if (_vmodel != null)
                    if (_vmodel.prVM_MA != null)
                        vm.prVM_MA.REMARK = _vmodel.prVM_MA.REMARK;
                if (_itemModel != null)
                {
                    if (Request.Cookies[SessionConst.UPDATE_PR_MODEL] != null)
                    {
                        if (Request.Cookies[SessionConst.UPDATE_PR_MODEL].Value != null)
                        {
                            if (Request.Cookies[SessionConst.UPDATE_PR_MODEL].Value == id)
                            {
                                vm.prVM_MA.itemCartList = _itemModel;
                                this.PrCalculatorPrice(_itemModel, vm);
                                vm.prVM_MA.PLAN_DELIVERY_DATE = _planDeliveryDate;
                                vm.prVM_MA.REMARK = _remark;
                            }
                            else
                            {
                                _itemModel = vm.prVM_MA.itemCartList;
                                SetItemCartCookie(_itemModel);
                                this.PrCalculatorPrice(_itemModel, vm);
                            }
                        }
                    }
                }
                else
                {
                    _itemModel = vm.prVM_MA.itemCartList;
                    SetItemCartCookie(_itemModel);
                    this.PrCalculatorPrice(_itemModel, vm);
                }
                _MODE = vm.mode;
                _updatePRCode = vm.prVM_MA.ST_PR_CODE;
                _planDeliveryDate = vm.prVM_MA.PLAN_DELIVERY_DATE;
                _remark = vm.prVM_MA.REMARK;

                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ItemCartUpdate", vm);
        }
        public ActionResult ItemCartUpdate_Action(PrVM vm)
        {
            vm.SessionLogin = GetCurrentUser;
            LogWsHelper hp = new LogWsHelper();

            try
            {
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.PR__EDIT__BEGIN.ToString(), this);

                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                setNewValueItemCart(_itemModel, vm.prVM_MA.itemCartList);
                vm.prVM_MA.itemCartList = _itemModel;
                vm = bc.ItemCartUpdate(vm);
                vm = bc.PrViewDetail(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return View("ItemCartUpdate", vm);
                    }
                    else if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                    {
                        _MODE = "";
                        _updatePRCode = "";
                        _itemModel = null;
                    }
                }
                vm = bc.PrViewDetail(vm);
                if (vm.prVM_MA.PR_STATUS_CODE == 6)
                { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00071)); }
                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);

                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.PR__EDIT__SUCCESS.ToString(), this);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("ItemCartUpdate", vm);
            }


            return View("PrViewDetail", vm);
        }
        public ActionResult ItemCartClear(PrVM vm, string id)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                if (id != null) // กดล้างหน้าจอมาจากหน้า ItemCartUpdate
                {
                    vm.prVM_MA.ST_PR_CODE = id;
                    vm = bc.PrViewDetail(vm);
                    _itemModel = vm.prVM_MA.itemCartList;
                    _MODE = "UDPATE";
                    _updatePRCode = vm.prVM_MA.ST_PR_CODE;
                }
                else
                {
                    vm = bc.CheckSessionHasBranch(vm);
                    _itemModel = null;
                    _vmodel = null;
                    vm = new PrVM();
                    vm = createNewModel(vm);
                    vm.SessionLogin = GetCurrentUser;
                    vm.prVM_MA.REMARK = vm.SessionLogin.FIRST_NAME_TH + " " + vm.SessionLogin.LAST_NAME_TH;
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            if (_MODE == "CREATE")
                return View("ItemCart", vm);
            else
                return View("ItemCartUpdate", vm);
        }
        public ActionResult ItemCartRemove(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                if (vm.SessionLogin.BRAND_CODE != null)
                {
                    vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                }
                List<PrET> tempItemList = _itemModel;
                var itemToRemove = tempItemList.Single(
                                        m => m.ITEM_CODE == vm.prVM_MA.ITEM_CODE
                                        && m.REQUEST_UOM_CODE == vm.prVM_MA.REQUEST_UOM_CODE);


                if (itemToRemove.SALE_PRICE.Value != 0 && itemToRemove.SALE_PRICE != null && vm.FRANCHISES_FLAG != false)
                {
                    vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT = vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT - itemToRemove.SALE_PRICE;
                    vm.prVM_MA.itemPrice.VAT = (vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT * 7) / 100;
                    vm.prVM_MA.itemPrice.PRICE_INCLUDE_VAT = vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT + vm.prVM_MA.itemPrice.VAT;
                }

                tempItemList.Remove(itemToRemove);


                _itemModel = tempItemList;
                setNewValueItemCart(_itemModel, vm.prVM_MA.itemCartList);
                vm.prVM_MA.itemCartList = _itemModel;
                this.PrCalculatorPrice(_itemModel, vm);

                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            if (vm.mode == "CREATE")
                return View("ItemCart", vm);
            else
                return View("ItemCartUpdate", vm);
        }
        public ActionResult AddItemCart(PrVM vm, string id)
        {
            try
            {
                if (vm == null)
                {
                    vm = new PrVM();
                    ModelState.Clear();
                    InitMesssageViewBagUI();
                    vm = createNewModel(vm);
                }
                
                //vm = new PrVM();
                //ModelState.Clear();
                //InitMesssageViewBagUI();
                //vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm.prSearchCriteriaVM = new PrSearchCriteriaVM();
                vm = bc.InnitialCriteria(vm);
                setNewValueItemCart(_itemModel, vm.prVM_MA.itemCartList);
                if (id == null) _vmodel = vm; // ถ้ามี id ส่งมาแสดงว่ามีการกดปุ่ม Clear 
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("AddItemCart", vm);
        }
        public ActionResult SearchAddItemCart(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                PrBC bc = new PrBC();
                vm = bc.InnitialCriteria(vm);
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.SearchAddItemCart(vm);
                vm = this.setValuePaginationResult(vm);
                //vm = bc.PrViewDetail(vm);
                _searchAddItemCartmodel = vm;
                if (vm.prSearchResultVM != null)
                    if (vm.prSearchResultVM.resultList != null)
                        if (vm.prSearchResultVM.resultList.Count > 0)
                        {
                            ViewBag.showSessionTimeout = 1;
                            vm = bc.setItemFromCart(vm, _itemModel);
                        }
                //var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                //vm.showImageFlag = Convert.ToBoolean(showFlagValue);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("AddItemCart", vm);
        }
        public ActionResult AddItemCartSave(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                var itemList = _itemModel;
                if (itemList == null) itemList = new List<PrET>();
                else itemList = _itemModel;
                var itemResult = vm.prSearchResultVM.resultList.Where(
                                        m => m.ITEM_CODE == vm.prVM_MA.ITEM_CODE
                                          && m.REQUEST_UOM_CODE == vm.prVM_MA.REQUEST_UOM_CODE
                                          && m.ST_MAP_WH_ITEM_ID == vm.prVM_MA.ST_MAP_WH_ITEM_ID).FirstOrDefault();
                PrBC bc = new PrBC();
                vm = _searchAddItemCartmodel;
                vm = bc.AddItemCartSave(vm, itemResult);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return View("AddItemCart", vm);
                    }
                }
                else
                {
                    itemList = this.SetDataRowItem(itemList, itemResult);
                    _itemModel = this.SetRowItem(itemList, itemResult.ITEM_CODE, itemResult.REQUEST_UOM_CODE, itemResult.ST_MAP_WH_ITEM_ID);
                    vm.MessageList.Clear();
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("AddItemCart", vm);
        }
        public ActionResult AddItemCartSaveAll(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                ViewBag.showSessionTimeout = 1;
                var itemList = _itemModel;
                //this.PrCalculatorPrice(_itemModel,vm);
                if (itemList == null) itemList = new List<PrET>();
                else itemList = _itemModel;
                //set item to add cart
                List<PrSearchResultET> itemResult = new List<PrSearchResultET>();
                if (vm.prSearchResultVM != null) itemResult = vm.prSearchResultVM.resultList.Where(m => m.ITEM_QTY >= 0).ToList();
                PrBC bc = new PrBC();
                vm = _searchAddItemCartmodel;
                vm = bc.AddItemCartSaveAll(vm, itemResult, itemList);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return View("AddItemCart", vm);
                    }
                }
                else
                {
                    foreach (var item in itemResult)
                    {
                        itemList = this.SetDataRowItem(itemList, item);
                        _itemModel = this.SetRowItem(itemList, item.ITEM_CODE, item.REQUEST_UOM_CODE, item.ST_MAP_WH_ITEM_ID);
                    }
                    SetItemCartCookie(_itemModel);

                    vm.MessageList.Clear();
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00070));
                    vm = bc.setItemFromCart(vm, _itemModel);
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("AddItemCart", vm);
        }


        private List<PrET> PrCalculatorPrice(List<PrET> itemList, PrVM vm)
        {
            try
            {
                if (itemList != null)
                {
                    decimal priceBeforeVat = 0;

                    foreach (var item in itemList)
                    {
                        priceBeforeVat += item.ITEM_QTY.Value * item.UNIT_PRICE.Value;

                    }

                    if (vm.prVM_MA.itemCartList.Count != null)
                    {
                        int i = 0;
                        for (i = 0; i < vm.prVM_MA.itemCartList.Count; i++)
                        {
                            vm.prVM_MA.itemCartList[i].SALE_PRICE = vm.prVM_MA.itemCartList[i].ITEM_QTY.Value * vm.prVM_MA.itemCartList[i].UNIT_PRICE.Value;
                        }
                    }


                    decimal vat = priceBeforeVat * 7 / 100;
                    decimal priceIncludeVat = priceBeforeVat + vat;

                    vm.prVM_MA.itemPrice = vm.prVM_MA.itemPrice ?? new USP_R_ST_PR_D_GetByPRCode_200_RET();

                    vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT = priceBeforeVat;
                    vm.prVM_MA.itemPrice.VAT = vat;
                    vm.prVM_MA.itemPrice.PRICE_INCLUDE_VAT = priceIncludeVat;
                    //vm.prVM_MA.itemPrice.UNIT_PRICE = unitPrice;


                }

                return itemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult PrViewDetail(PrVM vm, string id)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                ClearItemCartSesstion();
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                if (id != null) vm.prVM_MA.ST_PR_CODE = id;
                vm = bc.PrViewDetail(vm);
                if (vm.prVM_MA.PR_STATUS_CODE == 6)
                { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00071)); }

                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("PrViewDetail", vm);
        }


        public ActionResult PrDelete(PrVM vm, string id)
        {
            string returnPage = "PR";
            try
            {
                string[] split;
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                if (id != null)
                {
                    split = id.Split('|');
                    vm.prVM_MA.ST_PR_CODE = split[0];
                    if (split[1] == "PR") returnPage = "PR";
                    else returnPage = split[1]; // DASHBOARD
                }
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.PrDelete(vm);
                vm = this.setValuePaginationResult(vm);
                ClearItemCartSesstion();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            if (returnPage == "PR") return View("Index", vm);
            else return Redirect("/MAS/Dashboard/Index");
        }

        private List<PrET> SetDataRowItem(List<PrET> itemList, PrSearchResultET itemResult)
        {
            try
            {
                var itemET = new PrET();
                itemET.ITEM_CODE = itemResult.ITEM_CODE;
                itemET.ITEM_DETAIL = itemResult.ITEM_DETAIL;
                itemET.ITEM_NAME = itemResult.ITEM_NAME_TH;
                itemET.ITEM_QTY = Convert.ToDecimal(itemResult.ITEM_QTY.Value.ToString("#,###,###.00"));
                itemET.ITEM_UNIT = itemResult.REQUEST_UOM_CODE;
                itemET.ST_MAP_WH_ITEM_ID = itemResult.ST_MAP_WH_ITEM_ID;
                itemET.REQUEST_TO_BRAND_CODE = itemResult.REQUEST_TO_BRAND_CODE;
                itemET.REQUEST_TO_BRAND_NAME = itemResult.REQUEST_TO_BRAND_NAME;
                itemET.REQUEST_TO_BRANCH_CODE = itemResult.REQUEST_TO_BRANCH_CODE;
                itemET.REQUEST_TO_BRANCH_NAME = itemResult.REQUEST_TO_BRANCH_NAME;
                itemET.REQUEST_BY_BRAND_CODE = itemResult.REQUEST_BY_BRAND_CODE;
                itemET.REQUEST_BY_BRAND_NAME = itemResult.REQUEST_BY_BRAND_NAME;
                itemET.REQUEST_BY_BRANCH_CODE = itemResult.REQUEST_BY_BRANCH_CODE;
                itemET.REQUEST_BY_BRANCH_NAME = itemResult.REQUEST_BY_BRANCH_NAME;
                itemET.ST_PR_CATEGORY_ID = itemResult.ST_PR_CATEGORY_ID;
                itemET.ST_PR_CATEGORY_NAME = itemResult.ST_PR_CATEGORY_NAME;
                itemET.ITEM_QTY_AVG = itemResult.ITEM_QTY_AVG;
                itemET.WH_UOM_CODE = itemResult.WH_UOM_CODE;

                itemET.FILE_PATH = itemResult.FILE_PATH;
                itemET.FULL_PATH = itemResult.FULL_PATH;
                itemET.FILE_NAME_WITH_EXTENSION = itemResult.FILE_NAME_WITH_EXTENSION;
                itemET.SALE_PRICE = itemResult.SALE_PRICE;
                itemET.REMAIN_QTY = itemResult.REMAIN_QTY;
                itemET.UNIT_PRICE = itemResult.UNIT_PRICE;

                itemList.Add(itemET);
                return itemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<PrET> SetRowItem(List<PrET> itemList, string itemCode, string uomCode, string mapWhID)
        {
            try
            {
                if (itemList != null)
                {
                    int rowNo = 1;
                    var tempItemlist = itemList;
                    if (itemCode != null)
                    {
                        decimal itemQty = 0;
                        var itemGroupList = itemList.Where(
                                                m => m.ITEM_CODE == itemCode
                                                && m.ITEM_UNIT == uomCode
                                                && m.ST_MAP_WH_ITEM_ID == mapWhID).ToList();
                        if (itemGroupList.Count > 1)
                        {
                            //count item qty
                            int removeRow = itemGroupList[0].ROW_NO;
                            foreach (var item in itemGroupList)
                            {
                                itemQty = item.ITEM_QTY.Value;
                            }

                            //Remove duplicate row (last row)
                            itemList.RemoveAt((tempItemlist.Count() - 1));

                            //Update item qty
                            foreach (var item in itemList.Where(
                                                m => m.ITEM_CODE == itemCode
                                                && m.ITEM_UNIT == uomCode
                                                && m.ST_MAP_WH_ITEM_ID == mapWhID))
                            {
                                item.ITEM_QTY = itemQty;
                            }
                        }
                    }
                    var sortedCollection =
                        from item in itemList
                        orderby item.ST_PR_CATEGORY_ID, item.ITEM_CODE
                        select item;

                    var sortItemList = sortedCollection.ToList();

                    itemList = sortItemList;
                    foreach (var item in itemList)
                    {
                        item.ROW_NO = rowNo;
                        rowNo++;
                    }
                    return itemList;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM setValuePaginationCriteria(PrVM vm)
        {
            try
            {
                if (vm.ZenPagination == null) { vm.ZenPagination = new VM.PaginationVM(); }
                if (vm.ZenPagination.pagination == 0) { vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION)); }
                if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
                if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
                vm.prSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.prSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM setValuePaginationResult(PrVM vm)
        {
            try
            {
                if (vm.prSearchResultVM != null)
                {
                    if (vm.prSearchResultVM.countAll != 0)
                    {
                        this.InitPagination
                            (vm,
                             vm.prSearchResultVM.countAll,
                             vm.prSearchResultVM != null ? vm.prSearchResultVM.resultList[0].ROW_NO : 0,
                             vm.prSearchResultVM != null ? (vm.prSearchResultVM.resultList[vm.prSearchResultVM.resultList.Count - 1].ROW_NO) : 0);
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setNewValueItemCart(List<PrET> oldValueitemList, List<PrET> newValueitemList)
        {
            try
            {
                if (oldValueitemList != null && newValueitemList != null)
                {
                    foreach (var item in newValueitemList)
                    {
                        if (oldValueitemList.Where(m => m.ITEM_CODE == item.ITEM_CODE).ToList().Count > 0)
                            oldValueitemList.Where(m => m.ITEM_CODE == item.ITEM_CODE).ToList()[0].ITEM_QTY = item.ITEM_QTY;
                    }

                    _itemModel = oldValueitemList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddValue(PrVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);
                if (vm.prVM_MA != null)
                {
                    if (vm.prVM_MA.PLAN_DELIVERY_DATE != null)
                    {
                        HttpCookie ITEM_CART_PLAN_DELIVERY_DATE = new HttpCookie(SessionConst.ITEM_CART_PLAN_DELIVERY_DATE);
                        ITEM_CART_PLAN_DELIVERY_DATE.Expires = DateTime.Now.AddDays(1);
                        ITEM_CART_PLAN_DELIVERY_DATE.Value = vm.prVM_MA.PLAN_DELIVERY_DATE.ToString();
                        Response.Cookies.Add(ITEM_CART_PLAN_DELIVERY_DATE);
                        _planDeliveryDate = vm.prVM_MA.PLAN_DELIVERY_DATE;
                    }
                    if (vm.prVM_MA.REMARK != null)
                    {
                        HttpCookie ITEM_CART_ORDER_NAME = new HttpCookie(SessionConst.ITEM_CART_ORDER_NAME);
                        ITEM_CART_ORDER_NAME.Expires = DateTime.Now.AddDays(1);
                        ITEM_CART_ORDER_NAME.Value = vm.prVM_MA.REMARK;
                        Response.Cookies.Add(ITEM_CART_ORDER_NAME);
                        _remark = vm.prVM_MA.REMARK;
                    }
                    if (vm.prVM_MA.FC_LOCATION != null)
                    {
                        HttpCookie ITEM_CART_FC_LOCATION = new HttpCookie(SessionConst.ITEM_CART_FC_LOCATION);
                        ITEM_CART_FC_LOCATION.Expires = DateTime.Now.AddDays(1);
                        ITEM_CART_FC_LOCATION.Value = vm.prVM_MA.FC_LOCATION;
                        Response.Cookies.Add(ITEM_CART_FC_LOCATION);
                        _fcLocation = vm.prVM_MA.FC_LOCATION;
                    }
                }
                vm.prVM_MA.itemCartList = _itemModel;

                this.PrCalculatorPrice(_itemModel, vm);

                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);

                //if (vm.prVM_MA.itemPrice != null)
                //{
                //    vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT = vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT - itemToRemove.SALE_PRICE;
                //    vm.prVM_MA.itemPrice.VAT = (vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT * 7) / 100;
                //    vm.prVM_MA.itemPrice.PRICE_INCLUDE_VAT = vm.prVM_MA.itemPrice.PRICE_BEFORE_VAT + vm.prVM_MA.itemPrice.VAT;
                //}


            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }

            if (_MODE == "CREATE")
                return View("ItemCart", vm);
            else
                return View("ItemCartUpdate", vm);
        }

        public JsonResult GetAlertAvgPRQtyMsg(string itemCode, string itemUOM, decimal orderQty)
        {
            try
            {
                var userInfo = GetCurrentUser;

                var bc = new PrBC();
                var pet = new USP_R_ST_PR_GetAlertAvgPRQty__PET();
                pet.BrandCode = userInfo.BRAND_CODE;
                pet.BranchCode = userInfo.BRANCH_CODE;
                pet.ItemCode = itemCode;
                pet.ItemUOM = itemUOM;
                pet.OrderQty = orderQty;
                var msg = bc.GetAlertAvgPRQtyMsg(pet);
                msg = string.IsNullOrWhiteSpace(msg) ? string.Empty : msg;

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CheckPRDupplicate(PrVM vm)
        {
            try
            {
                ////USP_R_ST_CheckPRDupplicate
                //_vmodel = null;
                //ModelState.Clear();
                //InitMesssageViewBagUI();
                //ClearItemCartSesstion();
                //vm.SessionLogin = GetCurrentUser;
                //this.ItemCart(vm);
                //vm.prVM_MA.REMARK = vm.SessionLogin.FIRST_NAME_TH + " " + vm.SessionLogin.LAST_NAME_TH;

                //PrBC bc = new PrBC();
                //vm.prVM_MA.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                //vm.prVM_MA.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                //vm.RESULT_DUPPLICATE = bc.CheckPRDupplicate(vm);
                //vm.CHECK_DUPPLICATE_FLAG = "Checked";

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ItemCart", vm);
        }

        //public ActionResult GetCheckPRDupplicate(PrVM vm)
        //{
        //    try
        //    {
        //        var bc = new PrBC();
        //        //ClearItemCartSesstion();
        //        var stPRCode = bc.CheckPRDupplicate(
        //            GetCurrentUser.BRAND_CODE
        //            , GetCurrentUser.BRANCH_CODE
        //            , vm.prVM_MA.PLAN_DELIVERY_DATE
        //            , vm.prVM_MA.FC_LOCATION);

        //        if (stPRCode != "")
        //        {
        //            ViewBag.CheckPRDupplicate = 1;
        //            vm.prVM_MA.ST_PR_CODE = stPRCode;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelperUI.Write(ex, this);
        //        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
        //    }
        //    return View("ItemCart", vm);
        //}

        public JsonResult GetCheckPRDupplicate(DateTime? planDeliveryDate, string fcLocation)
        {
            try
            {
                var bc = new PrBC();
                //ClearItemCartSesstion();
                var stPRCode = bc.CheckPRDupplicate(
                    GetCurrentUser.BRAND_CODE
                    , GetCurrentUser.BRANCH_CODE
                    , planDeliveryDate
                    , fcLocation);

                if (!string.IsNullOrWhiteSpace(stPRCode))
                {
                    return Json(new CheckPRDupplicateUET { SuccessFlag = true, StPrCode = stPRCode }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new CheckPRDupplicateUET { SuccessFlag = false, ErrorMessage = string.Empty, StPrCode = stPRCode }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new CheckPRDupplicateUET { SuccessFlag = false, ErrorMessage = ex.Message, StPrCode = string.Empty }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PrBackToNew(PrVM vm, string id)
        {
            string returnPage = "PR";
            try
            {
                
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = createNewModel(vm);
                vm.SessionLogin = GetCurrentUser;
                PrBC bc = new PrBC();
                vm = bc.InnitialMA(vm);
                if (id != null)
                {
                    vm.prVM_MA.ST_PR_CODE = id;
                }
                vm = this.setValuePaginationCriteria(vm);
                vm = bc.PrBackToNew(vm);
                vm = this.setValuePaginationResult(vm);
                ClearItemCartSesstion();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            if (returnPage == "PR") return View("Index", vm);
            else return Redirect("/MAS/Dashboard/Index");
        }
    }
}