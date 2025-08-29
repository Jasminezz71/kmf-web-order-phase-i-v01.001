using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.VM;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public void SetCurrentUser(UserLoginInfoET vm)
        {
            Session[SessionConst.CURRENT_USER_LOGIN] = vm;
        }
        public UserLoginInfoET GetCurrentUser
        {
            get
            {
                UserLoginInfoET userSession;
                if (Session[SessionConst.CURRENT_USER_LOGIN] != null)
                {
                    userSession = (UserLoginInfoET)Session[SessionConst.CURRENT_USER_LOGIN];
                }
                else
                {
                    // Get link to login page to log out
                    // Or Insert link logout 
                    //string REDIRECTTOWEBORDER = ConfigurationManager.AppSettings["REDIRECTTOWEBORDER"];
                    userSession = new UserLoginInfoET();
                    //Session[SessionConst.EXPIRE_SESSION_FLAG] = "1";
                    //Session[SessionConst.EXPIRE_SESSION_MESSAGE] = "session expired.";
                    //ViewBag.sessionExpireFlag = "1";
                    //ViewBag.sessionExpireMessage = "Session is expired (ViewBag.sessionExpireMessage)";
                    //HttpContext.Response.Redirect("/Home/LogOut/expireSession");
                    //Response.Redirect("http://localhost:21799/Home/LogOut/");
                    //Response.Redirect(REDIRECTTOWEBORDER);
                }
                return userSession;
            }
        }
        public void ClearAllSession()
        {
            Session[SessionConst.CURRENT_USER_LOGIN] = null;
            Session[SessionConst.ITEM_CART_PR] = null;
            Session[SessionConst.USER_COOKIES] = null;
            Session[SessionConst.PASSWORD_COOKIES] = null;
            Session[SessionConst.MODE_MODEL] = null;
            Session[SessionConst.UPDATE_PR_MODEL] = null;
        }
        public void ClearItemCartSesstion()
        {
            Session[SessionConst.MODE_MODEL] = null;
            Session[SessionConst.ITEM_CART_PR] = null;
            Session[SessionConst.UPDATE_PR_MODEL] = null;
            Response.Cookies[SessionConst.ITEM_CART_COOKIES].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[SessionConst.ITEM_CART_TOTAL_COOKIES].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[SessionConst.ITEM_CART_PLAN_DELIVERY_DATE].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[SessionConst.ITEM_CART_ORDER_NAME].Expires = DateTime.Now.AddDays(-1);
        }
        [NonAction]
        protected virtual void InitMesssageViewBagUI()
        {
            ViewBag.uploadFileMaxSize = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.MAX_SIZE);
            ViewBag.uploadFileType = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.TYPE);
            ViewBag.signatureUploadFileMaxSize = ConfigBC.GetConfigValue(CategoryConfigEnum.SIGNATURE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.MAX_SIZE);
            ViewBag.signatureUploadFileType = ConfigBC.GetConfigValue(CategoryConfigEnum.SIGNATURE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.TYPE);
            ViewBag.msgAlertNobranchInSession = MessageBC.GetMessage(MessageCodeConst.M00039).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgAlertNobranchStockInSession = MessageBC.GetMessage(MessageCodeConst.M00074).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmGrUpdate = MessageBC.GetMessage(MessageCodeConst.M00040).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmGrDelete = MessageBC.GetMessage(MessageCodeConst.M00041).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmGrDetail = MessageBC.GetMessage(MessageCodeConst.M00042).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmDoSave = MessageBC.GetMessage(MessageCodeConst.M00043).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchPR = MessageBC.GetMessage(MessageCodeConst.M00044).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchDO = MessageBC.GetMessage(MessageCodeConst.M00045).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchGR = MessageBC.GetMessage(MessageCodeConst.M00066).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchHAVIPO = MessageBC.GetMessage(MessageCodeConst.M00076).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchHAVISO = MessageBC.GetMessage(MessageCodeConst.M00077).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchHAVIRN = MessageBC.GetMessage(MessageCodeConst.M00078).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchHAVIDN = MessageBC.GetMessage(MessageCodeConst.M00079).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRunBatchMaster = MessageBC.GetMessage(MessageCodeConst.M00046).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmReceiveItem = MessageBC.GetMessage(MessageCodeConst.M00047).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmSave = MessageBC.GetMessage(MessageCodeConst.M00035).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmChooseItem = MessageBC.GetMessage(MessageCodeConst.M00032).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmSaveCart = MessageBC.GetMessage(MessageCodeConst.M00033).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmClearCart = MessageBC.GetMessage(MessageCodeConst.M00034).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmDelete = MessageBC.GetMessage(MessageCodeConst.M00036).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmRenewGR = MessageBC.GetMessage(MessageCodeConst.M00069).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmEdit = MessageBC.GetMessage(MessageCodeConst.M00037).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmResetPassword = MessageBC.GetMessage(MessageCodeConst.M00038).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmClear = MessageBC.GetMessage(MessageCodeConst.M00034).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmReturn = MessageBC.GetMessage(MessageCodeConst.M00048).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmActive = MessageBC.GetMessage(MessageCodeConst.M00049).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmInActive = MessageBC.GetMessage(MessageCodeConst.M00050).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.currentWebVersion = ConfigBC.GetConfigValue(CategoryConfigEnum.COMMON, SubCategoryConfigEnum.VERSION, ConfigNameEnum.CURRENT);
            ViewBag.sessionTimeout = ConfigBC.GetConfigValue(CategoryConfigEnum.WEB, SubCategoryConfigEnum.SESSION, ConfigNameEnum.TIMEOUT);
            ViewBag.sessionTimeoutAlert = ConfigBC.GetConfigValue(CategoryConfigEnum.WEB, SubCategoryConfigEnum.SESSION, ConfigNameEnum.TIMEOUT_ALERT);
            ViewBag.sessionTimeoutMessage = ConfigBC.GetConfigValue(CategoryConfigEnum.WEB, SubCategoryConfigEnum.SESSION, ConfigNameEnum.TIMEOUT_MESSAGE);
            ViewBag.msgAlertChangePage = MessageBC.GetMessage(MessageCodeConst.M00065).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgAlertTotalItemAvg = MessageBC.GetMessage(MessageCodeConst.M00067).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgAlertItemRecieveNotEqualDefault = MessageBC.GetMessage(MessageCodeConst.M00068).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmBackToNew = MessageBC.GetMessage(MessageCodeConst.M00101).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmBlockUser = MessageBC.GetMessage(MessageCodeConst.M00102).MESSAGE_TEXT_FOR_DISPLAY;
            ViewBag.msgConfirmUnblockUser = MessageBC.GetMessage(MessageCodeConst.M00103).MESSAGE_TEXT_FOR_DISPLAY;

        }
        protected void InitPagination(BaseVM vm, int countAll, int pagesFrom, int pagesTo)
        {
            BaseBC bc = new BaseBC();
            if (vm.ZenPagination == null) { vm.ZenPagination = new PaginationVM(); }
            vm.ZenPagination.perPageList = bc.GetPerPage();
            vm.ZenPagination.pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION));
            if (vm.ZenPagination.rowPerPage == 0) { vm.ZenPagination.rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
            if (vm.ZenPagination.currentPage == 0) { vm.ZenPagination.currentPage = 1; }
            vm.ZenPagination.countAll = countAll;
            vm.ZenPagination.pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(vm.ZenPagination.countAll) / Convert.ToDouble(vm.ZenPagination.rowPerPage)));
            vm.ZenPagination.pagesFrom = pagesFrom;
            vm.ZenPagination.pagesTo = pagesTo;

        }
        protected void InitPaginationDashBoardTop(BaseVM vm, int countAll, int pagesFrom, int pagesTo)
        {
            BaseBC bc = new BaseBC();
            vm.ZenPagination.top_perPageList = bc.GetPerPage();
            vm.ZenPagination.top_pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION));
            if (vm.ZenPagination.top_rowPerPage == 0) { vm.ZenPagination.top_rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
            if (vm.ZenPagination.top_currentPage == 0) { vm.ZenPagination.top_currentPage = 1; }
            vm.ZenPagination.top_countAll = countAll;
            vm.ZenPagination.top_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(vm.ZenPagination.top_countAll) / Convert.ToDouble(vm.ZenPagination.top_rowPerPage)));
            vm.ZenPagination.top_pagesFrom = pagesFrom;
            vm.ZenPagination.top_pagesTo = pagesTo;

        }
        protected void InitPaginationDashBoardBottom(BaseVM vm, int countAll, int pagesFrom, int pagesTo)
        {
            BaseBC bc = new BaseBC();
            vm.ZenPagination.bottom_perPageList = bc.GetPerPage();
            vm.ZenPagination.bottom_pagination = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.PAGINATION));
            if (vm.ZenPagination.bottom_rowPerPage == 0) { vm.ZenPagination.bottom_rowPerPage = Convert.ToInt16(ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.GRID, ConfigNameEnum.ROW_PER_PAGE)); }
            if (vm.ZenPagination.bottom_currentPage == 0) { vm.ZenPagination.bottom_currentPage = 1; }
            vm.ZenPagination.bottom_countAll = countAll;
            vm.ZenPagination.bottom_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(vm.ZenPagination.bottom_countAll) / Convert.ToDouble(vm.ZenPagination.bottom_rowPerPage)));
            vm.ZenPagination.bottom_pagesFrom = pagesFrom;
            vm.ZenPagination.bottom_pagesTo = pagesTo;

        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var needToLogin = this.CheckHasSession();
            if (!needToLogin) // Not have a session
            {
                if (CheckUserCookie()) // Have a user cookies
                {
                    bool prItem = Request.RawUrl.ToString().ToLower().Contains("/MAS/Pr/Item".ToLower());
                    bool prAddItem = Request.RawUrl.ToString().ToLower().Contains("/MAS/Pr/AddItem".ToLower());
                    bool prSearchAddItem = Request.RawUrl.ToString().ToLower().Contains("/MAS/Pr/SearchAddItem".ToLower());
                    if (prItem || prAddItem || prSearchAddItem)
                    {
                        requestContext.HttpContext.Response.Redirect("/Home/LogOut/expireSession");
                    }
                    else
                    {
                        requestContext.HttpContext.Response.Redirect("/AUT/Login/Index");
                    }

                }
                else
                {
                    requestContext.HttpContext.Response.Clear();
                    requestContext.HttpContext.Response.Redirect("/Home/LogOut/expireSession");
                    requestContext.HttpContext.Response.End();
                }
            }

            else // Have a session
            {
                #region === Check ว่ามีการเลือก "สาขา" หรือยังสำหรับเวลา Login เข้ามาเป็น Admin , Brand Admin เป็นต้น ===
                bool needChooseBrand = this.CheckHasBrandBranch();
                if (needChooseBrand)
                {
                    ViewBag.needChooseBrand = 1;
                }
                #endregion
            }

            //Clear Session Item Cart When Change Brand
            this.ClearItemCartPR();

            string cultureThread = ConfigurationManager.AppSettings["CULTER_INFO_Culture_Thread"];
            Thread.CurrentThread.CurrentCulture = CreateCulterInfoInstance(cultureThread);
            Thread.CurrentThread.CurrentUICulture = CreateCulterInfoInstance(cultureThread);
        }
        private static CultureInfo CreateCulterInfoInstance(string culture)
        {
            var cul = new System.Globalization.CultureInfo(culture);

            cul.DateTimeFormat.DateSeparator = ConfigurationManager.AppSettings["CULTER_INFO_DateSeparator"];
            cul.DateTimeFormat.ShortDatePattern = ConfigurationManager.AppSettings["CULTER_INFO_ShortDatePattern"];
            cul.DateTimeFormat.FullDateTimePattern = ConfigurationManager.AppSettings["CULTER_INFO_FullDateTimePattern"];

            //cul.DateTimeFormat.DateSeparator = "/"; 
            //cul.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //cul.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";

            return cul;
        }

        private bool CheckHasSession()
        {
            var sessionUser = (UserLoginInfoET)Session[SessionConst.CURRENT_USER_LOGIN];
            if (sessionUser == null)
            {
                bool isLinkLogin = Request.RawUrl.ToString().ToLower().Contains("/AUT/Login".ToLower());
                bool isLinkLogout = Request.RawUrl.ToString().ToLower().Contains("/Home/Logout".ToLower());
                bool isLink = Request.RawUrl.ToString().Equals("/"); // เข้าผ่านลิงค์ตรงๆเช่น saleandtransfer.zengroup.co.th

                if (isLinkLogin || isLinkLogout || isLink)
                //if (isLinkLogin || isLinkLogout || (Request.RawUrl.ToString() == "/"))
                {
                    // เข้าหน้า Login 
                    // หรือ Logout 
                    // หรือ เข้ามาแต่ลิงค์แบบไม่มี Controller
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool CheckHasBrandBranch()
        {
            var sessionUser = (UserLoginInfoET)Session[SessionConst.CURRENT_USER_LOGIN];
            bool createPR = Request.RawUrl.ToString().ToLower().Contains("/MAS/Pr/CreatePR".ToLower());
            bool createPrItemCart = Request.RawUrl.ToString().ToLower().Contains("/MAS/pr/ItemCart".ToLower());
            bool CreateDO = Request.RawUrl.ToString().ToLower().Contains("/MAS/Do/ReceiveItem".ToLower());
            bool InventoryStock = Request.RawUrl.ToString().ToLower().Contains("/MAS/ItemStock/InventoryStock".ToLower());
            if (sessionUser != null)
            {
                if (sessionUser.BRANCH_CODE == null)
                {
                    if (createPR)
                    {
                        return true;
                    }
                    else if (createPrItemCart)
                    {
                        return true;
                    }
                    else if (CreateDO)
                    {
                        return true;
                    }
                    else if (InventoryStock)
                    {
                        ViewBag.InventoryStock = 1;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void ClearItemCartPR()
        {
            bool changeBrand = Request.RawUrl.ToString().ToLower().Contains("/Home/ChangeBranch".ToLower());
            if (changeBrand) { Session[SessionConst.ITEM_CART_PR] = null; }
            //bool pr = Request.RawUrl.ToString().ToLower().Contains("/MAS/pr".ToLower());
            //bool prIndex = Request.RawUrl.ToString().ToLower().Contains("/MAS/pr/index".ToLower());

            //if (!pr) { Session[SessionConst.ITEM_CART_PR] = null; }
            //else
            //{
            //    if (prIndex) { Session[SessionConst.ITEM_CART_PR] = null; }
            //}
        }
        public string GetFileNameForDownload(ConfigNameEnum downloadType)
        {
            string dateFormat = ConfigBC.GetConfigValue(CategoryConfigEnum.DOWNLOAD, SubCategoryConfigEnum.ST_FILENAME_DATETIME, downloadType);
            string titleFormat = ConfigBC.GetConfigValue(CategoryConfigEnum.DOWNLOAD, SubCategoryConfigEnum.ST_FILENAME_TITLE, downloadType);
            string datetime = DateTime.Now.ToString(dateFormat);
            string fileName = titleFormat + "__" + datetime + ".xlsx";
            return fileName;
        }
        public void ClearCookies()
        {
            if (Request.Cookies[SessionConst.USER_COOKIES] != null)
                Response.Cookies[SessionConst.USER_COOKIES].Expires = DateTime.Now.AddDays(-1);
            if (Request.Cookies[SessionConst.ITEM_CART_COOKIES] != null)
                Response.Cookies[SessionConst.ITEM_CART_COOKIES].Expires = DateTime.Now.AddDays(-1);
            if (Request.Cookies[SessionConst.ITEM_CART_TOTAL_COOKIES] != null)
                Response.Cookies[SessionConst.ITEM_CART_TOTAL_COOKIES].Expires = DateTime.Now.AddDays(-1);
            if (Request.Cookies[SessionConst.MODE_MODEL] != null)
                Response.Cookies[SessionConst.MODE_MODEL].Expires = DateTime.Now.AddDays(-1);
            if (Request.Cookies[SessionConst.UPDATE_PR_MODEL] != null)
                Response.Cookies[SessionConst.UPDATE_PR_MODEL].Expires = DateTime.Now.AddDays(-1);
        }
        public bool CheckUserCookie()
        {
            try
            {
                if (Request.Cookies[SessionConst.USER_COOKIES] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SetItemCartCookie(List<ZEN.SaleAndTranfer.ET.MAS.PrET> itemResult)
        {
            try
            {
                int row = 0;
                HttpCookie itemCartCookeis = new HttpCookie(SessionConst.ITEM_CART_COOKIES);
                HttpCookie itemCartTotalCookeis = new HttpCookie(SessionConst.ITEM_CART_TOTAL_COOKIES);
                foreach (var item in itemResult)
                {
                    itemCartCookeis[row.ToString()] = item.ITEM_CODE + "|" + item.ITEM_QTY + "|" + item.UNIT_PRICE;
                    itemCartTotalCookeis.Value = row.ToString();

                    row++;
                }
                itemCartCookeis.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(itemCartCookeis);
                itemCartTotalCookeis.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(itemCartTotalCookeis);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}