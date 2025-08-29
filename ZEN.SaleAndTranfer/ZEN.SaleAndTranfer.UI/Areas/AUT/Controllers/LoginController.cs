using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.AUT;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.UI.LogWs;
using ZEN.SaleAndTranfer.UI.Util;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.UI.Areas.AUT.Controllers
{
    public class LoginController : BaseController
    {
        #region ==== Session Value ====
        private string _MODE
        {
            set
            {
                Session[SessionConst.MODE_MODEL] = value;
            }
        }
        private string _updatePRCode
        {
            set
            {
                Session[SessionConst.UPDATE_PR_MODEL] = value;
            }
        }
        private DateTime _planDeliveryDate
        {
            set
            {
                Session[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE] = value;
            }
        }
        private string _remark
        {
            set
            {
                Session[SessionConst.ITEM_CART_ORDER_NAME] = value;
            }
        }
        private LoginVM _loginModel
        {
            get
            {
                if (Session["LOGIN_VMODEL"] != null)
                {
                    return (LoginVM)Session["LOGIN_VMODEL"];
                }

                return null;
            }

            set
            {
                Session["LOGIN_VMODEL"] = value;
            }
        }
        public void SetitemModel(List<PrET> itemlist)
        {
            Session[SessionConst.ITEM_CART_PR] = this.SetRowItem(itemlist, null, null, null);
        }
        #endregion

        //
        // GET: /AUT/Login/
        public ActionResult Index(LoginVM vm)
        {
            try
            {
                ModelState.Clear();
                vm = new LoginVM();
                vm.loginVM_MA = new LoginET_MA();

                ClearItemCartSesstion();

                //if (CheckUserCookie()) // True = have a user cookies , False = Not have a user cookies
                //{
                //    vm.loginVM_MA.USER_NAME = Request.Cookies[SessionConst.USER_COOKIES].Value;
                //    LoginBC bc = new LoginBC();
                //    vm = bc.SearchUserByID(vm);
                //    var sessionData = bc.SetDataToSession(vm);
                //    this.SetUserSession(sessionData);
                //    getItemCartCookies();
                //    return Redirect("/MAS/Dashboard/Index");
                //}
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Login(LoginVM vm)
        {
            try
            {
                bool success = false;
                //vm.loginVM_MA.CAPTCHA_CONFIRM = this.Session["CaptchaImageText"].ToString();
                LoginBC bc = new LoginBC();
                vm = bc.Login(vm, out success);
                //if (success)
                if (true)
                {
                    if (vm.loginResultResultVM.resultList != null)
                    {
                        LogWsHelper hp = new LogWsHelper();
                        //hp.InsertLog(vm.loginVM_MA.USER_NAME, AccressTypeConst.WEB.ToString(), this);
                        var sessionData = bc.SetDataToSession(vm);
                        this.SetUserSession(sessionData);

                        if (Request.Cookies[SessionConst.USER_COOKIES] == null)
                        {
                            HttpCookie StudentCookies = new HttpCookie(SessionConst.USER_COOKIES);
                            StudentCookies.Value = vm.loginResultResultVM.resultList[0].USER_NAME;
                            StudentCookies.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(StudentCookies);
                        }
                        else
                        {
                            string cookieUser = Request.Cookies[SessionConst.USER_COOKIES].Value;
                            if (cookieUser == vm.loginResultResultVM.resultList[0].USER_NAME)
                                getItemCartCookies();
                            else
                            {
                                ClearItemCartSesstion();
                                HttpCookie StudentCookies = new HttpCookie(SessionConst.USER_COOKIES);
                                StudentCookies.Value = vm.loginResultResultVM.resultList[0].USER_NAME;
                                StudentCookies.Expires = DateTime.Now.AddDays(1);
                                Response.Cookies.Add(StudentCookies);
                            }
                        }

                        if (vm.loginResultResultVM.resultList[0].IS_RESET_PWD.ToLower() == "true")
                        {
                            _loginModel = vm;
                            return Redirect("/AUT/Login/ResetPassword");
                        }
                    }


                    if (GetCurrentUser.ROLE_NAME.ToLower().Contains("account")
                        || GetCurrentUser.ROLE_NAME.ToLower().Contains("audit"))
                    {
                        return Redirect("/RPT/Report/Invoice");
                    }
                    else if (GetCurrentUser.ROLE_NAME.ToLower().Contains("purchasing"))
                    {
                        return Redirect("/ADMIN/BatchHAVI/Index");
                    }
                    else if (GetCurrentUser.ROLE_NAME == "ADMIN")
                    {
                        if (vm.loginVM_MA.USER_NAME == vm.loginVM_MA.PASSWORD)
                        {
                            return RedirectToAction("ChangePwd", "Usr", new { Area = "" });
                        }

                        return RedirectToAction("Search", "Prq", new { Area = "" });
                    }
                    else if (GetCurrentUser.ROLE_NAME == "SALEADMIN")
                    {
                        if (vm.loginVM_MA.USER_NAME == vm.loginVM_MA.PASSWORD)
                        {
                            return RedirectToAction("ChangePwd", "Usr", new { Area = "" });
                        }
                        GetCurrentUser.SALE_ADMIN_CODE = GetCurrentUser.USER_NAME;
                        GetCurrentUser.IS_SALE_ADMIN = "Y";
                        return RedirectToAction("SearchCus", "Prq", new { Area = "" });
                    }
                    else
                    {
                        //return Redirect("/MAS/Dashboard/Index");
                        //Session[SessionNameConst.ItemInBasket] = null;

                        if (vm.loginVM_MA.USER_NAME == vm.loginVM_MA.PASSWORD)
                        {
                            return RedirectToAction("ChangePwd", "Usr", new { Area = "" });
                        }

                        return RedirectToAction("Index", "Prq", new { Area = "", id = PrCategoryIDConst.All });
                    }
                }
                else
                {
                    vm.loginVM_MA.PASSWORD = null;
                    vm.loginVM_MA.CAPTCHA = null;
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

        public ActionResult ResetPassword(LoginVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm = _loginModel;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("ResetPassword", vm);
        }
        public ActionResult ResetPasswordSave(LoginVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                LoginBC bc = new LoginBC();
                vm = bc.ResetPasswordSave(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (!vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        ViewBag.UpdateComplete = 1;
                        vm.MessageList.Clear();

                        var sessionData = bc.SetDataToSession(vm);
                        this.SetUserSession(sessionData);
                        return Redirect("/MAS/Dashboard/Index");
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("ResetPassword", vm);
            }
            return View("ResetPassword", vm);
        }
        private void SetUserSession(UserInfoVM vmLogin)
        {
            try
            {
                if (vmLogin.ROLE_NAME.ToLower().Contains("admin"))
                {
                    if (vmLogin.FIRST_NAME_TH.ToLower().Equals("admin brand"))
                    {
                        vmLogin.BRAND_CODE = vmLogin.LAST_NAME_TH;
                        vmLogin.BRANCH_CODE = null;
                    }
                    else if (vmLogin.ROLE_NAME.ToLower().Equals("rest-admin"))
                    {

                    }
                    else
                    {
                        vmLogin.BRAND_CODE = null;
                        vmLogin.BRANCH_CODE = null;
                    }
                }
                SetCurrentUser(vmLogin);
                Session["LOGIN_SESSION"] = vmLogin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getItemCartCookies()
        {
            #region ==== Declare Valiable ====

            var brandCode = GetCurrentUser.BRAND_CODE;
            var branchCode = GetCurrentUser.BRANCH_CODE;
            string totalRows;
            int rows = 0;
            List<string> itemCode = new List<string>();
            string[] itemSplit;
            List<PrET> itemList = new List<PrET>();
            ZEN.SaleAndTranfer.BC.MAS.PrBC prBC = new ZEN.SaleAndTranfer.BC.MAS.PrBC();

            #endregion

            #region ==== Get Item Cart ====

            if (Request.Cookies[SessionConst.ITEM_CART_TOTAL_COOKIES] != null)
            {
                totalRows = Request.Cookies[SessionConst.ITEM_CART_TOTAL_COOKIES].Value;
                rows = Convert.ToInt16(totalRows);
            }
            if (Request.Cookies[SessionConst.ITEM_CART_COOKIES] != null)
            {
                for (int i = 0; i <= rows; i++)
                {
                    itemCode.Add(Request.Cookies[SessionConst.ITEM_CART_COOKIES][i.ToString()]);
                }
            }

            for (int i = 0; i < itemCode.Count; i++)
            {
                itemSplit = itemCode[i].Split('|');
                var itemET = prBC.GetItemByItemCode(itemSplit[0], brandCode, branchCode);
                itemET.ITEM_QTY = Convert.ToDecimal(itemSplit[1]);
                itemET.UNIT_PRICE = Convert.ToDecimal(itemSplit[2]);
                itemList.Add(itemET);
            }
            if (itemList.Count > 0) SetitemModel(itemList);

            #endregion

            #region ==== Get Delivery Date And Remark ====

            if (Request.Cookies[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE] != null)
            {
                _planDeliveryDate = Convert.ToDateTime(Request.Cookies[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE].Value);
            }

            if (Request.Cookies[SessionConst.ITEM_CART_ORDER_NAME] != null)
            {
                _remark = Request.Cookies[SessionConst.ITEM_CART_ORDER_NAME].Value;
            }

            #endregion

            #region ==== Get Mode and Update PR ====

            if (Request.Cookies[SessionConst.MODE_MODEL] != null)
            {
                _MODE = Request.Cookies[SessionConst.MODE_MODEL].Value;
            }
            if (Request.Cookies[SessionConst.UPDATE_PR_MODEL] != null)
            {
                _updatePRCode = Request.Cookies[SessionConst.UPDATE_PR_MODEL].Value;
            }

            #endregion
        }
        private List<PrET> SetRowItem(List<PrET> itemList, string itemCode, string uomCode, string mapWhID)
        {
            try
            {
                int rowNo = 1;
                foreach (var item in itemList)
                {
                    item.ROW_NO = rowNo;
                    rowNo++;
                }
                return itemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}