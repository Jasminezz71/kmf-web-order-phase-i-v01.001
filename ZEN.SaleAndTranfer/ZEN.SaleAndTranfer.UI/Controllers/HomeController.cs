using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class HomeController : BaseController
    {
        private string _linkRedirectmodel
        {
            get
            {
                if (Session["LINK_REDIRECT"] != null)
                {
                    return (string)Session["LINK_REDIRECT"];
                }

                return null;
            }

            set
            {
                Session["LINK_REDIRECT"] = value;
            }
        }

        public ActionResult Index()
        {
            return Redirect("/Home/LogOut");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChangeBrand(UserVM vm)
        {
            try
            {
                HomeBC bc = new HomeBC();
                vm.SessionLogin = GetCurrentUser;
                vm = bc.InnitialMA(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("ChangeBranch", vm);
        }
        public ActionResult ChangeBranch(UserVM vm)
        {
            try
            {
                vm = new UserVM();
                HomeBC bc = new HomeBC();
                vm.SessionLogin = GetCurrentUser;
                vm = bc.InnitialMA(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("ChangeBranch", vm);
        }
        public ActionResult ChangeBranchSave(UserVM vm)
        {
            try
            {
                HomeBC bc = new HomeBC();
                vm.SessionLogin = GetCurrentUser;
                vm = bc.InnitialMA(vm);
                vm = bc.ChangeBranchSave(vm);
                if (vm.MessageList.Count > 0)
                {
                    return View("ChangeBranch", vm);
                }
                else
                {
                    #region ===== Set Cookies Brand Branch Admin =====
                    HttpCookie brandCookeis = new HttpCookie(SessionConst.BRAND_COOKIES);
                    brandCookeis.Value = vm.SessionLogin.BRAND_CODE;
                    brandCookeis.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(brandCookeis);

                    HttpCookie branchCookeis = new HttpCookie(SessionConst.BRANCH_COOKIES);
                    branchCookeis.Value = vm.SessionLogin.BRANCH_CODE;
                    branchCookeis.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(branchCookeis);
                    #endregion

                    SetCurrentUser(vm.SessionLogin);

                    if (GetCurrentUser.ROLE_NAME.ToLower().Contains("account") || GetCurrentUser.ROLE_NAME.ToLower().Contains("audit")) 
                        return Redirect("/RPT/Report/Invoice");
                    else return Redirect("/MAS/Dashboard/Index");
                    //if (_linkRedirectmodel != null)
                    //{
                    //    return Redirect(_linkRedirectmodel);
                    //}
                    //else
                    //{
                    //    return Redirect("/MAS/Dashboard/Index");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult LogOut(string id)
        {

            FormsAuthentication.SignOut();
            ClearAllSession();
            Session.Clear();
            Session.Abandon(); // it will clear the session at the end of request
            if (id != null)
            {
                //ClearCookies(); // Clear Cookies when session is expired
                if (id.ToLower() != "logout") // True = Click Logout Link , False = Session is expired
                {
                    ViewBag.sessionExpireFlag = "1";
                    ViewBag.sessionExpireMessage = MessageBC.GetMessage(ZEN.SaleAndTranfer.ET.MessageCodeConst.M00064).MESSAGE_TEXT_FOR_DISPLAY;
                }
            }

            return View();
            //return Redirect("/AUT/Login/Index");
        }
    }
}