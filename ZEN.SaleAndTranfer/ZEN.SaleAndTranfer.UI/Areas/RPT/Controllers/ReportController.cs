using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.RPT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.RPT;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.RPT;

namespace ZEN.SaleAndTranfer.UI.Areas.RPT.Controllers
{
    public class ReportController : BaseController
    {
        private DownloadVM _downloadFileModel
        {
            get
            {
                if (Session["DownloadFileModel"] != null)
                {
                    return (DownloadVM)Session["DownloadFileModel"];
                }

                return null;
            }
            set
            {
                Session["DownloadFileModel"] = value;
            }
        }

        //
        // GET: /RPT/Report/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RPT_ST_REPORT__20181031(string id)
        {
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ST_PARAMETER_CODE = id;
                if (id.Contains("STPR"))
                {
                    return View("RPT_ST_REPORT");
                }
                else if (id.Contains("STGR"))
                {
                    return View("RPT_ST_GR_REPORT");
                }
                else
                {
                    return View("RPT_ST_DO_REPORT");
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                //throw ex;
                return View();
            }
        }

        /// <summary>
        /// nutty version 2018-11-01
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RPT_ST_REPORT_EQ(string id)
        {
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ST_PARAMETER_CODE = id;
                //vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                //vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);

                if (id.Contains("STPR") || id.Contains("SPR"))
                {
                    return View("RPT_ST_REPORT_EQ");
                }
                else if (id.Contains("STGR") || id.Contains("SGR"))
                {
                    return View("RPT_ST_GR_REPORT");
                }
                else
                {
                    return View("RPT_ST_DO_REPORT");
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                //throw ex;
                return View();
            }
        }



        public ActionResult RPT_ST_REPORT_FC(string id)
        {
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ST_PARAMETER_CODE = id;
                //vm.SessionLogin = GetCurrentUser;
                TB_M_BRANCH_BC bbc = new TB_M_BRANCH_BC();
                //vm.FRANCHISES_FLAG = bbc.IsFranchises(vm.SessionLogin.BRAND_CODE, vm.SessionLogin.BRANCH_CODE);

                if (id.Contains("STPR") || id.Contains("SPR"))
                {
                    return View("RPT_ST_REPORT_FC");
                }
                else if (id.Contains("STGR") || id.Contains("SGR"))
                {
                    return View("RPT_ST_GR_REPORT");
                }
                else
                {
                    return View("RPT_ST_DO_REPORT");
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                //throw ex;
                return View();
            }
        }
        public ActionResult Invoice(InvoiceVM vm)
        {
            try
            {
                vm = new InvoiceVM();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                InvoiceBC bc = new InvoiceBC();
                vm = bc.InnitialCriteria(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Invoice", vm);
        }
        public ActionResult ViewInvoice(InvoiceVM vm)
        {
            try
            {
                vm.SessionLogin = GetCurrentUser;
                InvoiceBC bc = new InvoiceBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.ViewInvoice(vm);
                if (vm.MessageList.Count == 0)
                {
                    ViewBag.ShowReportFlag = 1;
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Invoice", vm);
        }
        public ActionResult RPT_INVOICE(string id)
        {
            try
            {
                ViewBag.ReportData = id;
                return View("RPT_INVOICE");
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                return View("RPT_INVOICE");
            }
        }
        public ActionResult CreditNote(InvoiceVM vm)
        {
            try
            {
                vm = new InvoiceVM();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                InvoiceBC bc = new InvoiceBC();
                vm = bc.InnitialCriteria(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("CreditNote", vm);
        }
        public ActionResult ViewCreditNote(InvoiceVM vm)
        {
            try
            {
                vm.SessionLogin = GetCurrentUser;
                InvoiceBC bc = new InvoiceBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.ViewInvoice(vm);
                if (vm.MessageList.Count == 0)
                {
                    ViewBag.ShowReportFlag = 1;
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("CreditNote", vm);
        }
        public ActionResult RPT_CREDITNOTE(string id)
        {
            try
            {
                ViewBag.ReportData = id;
                return View("RPT_CREDITNOTE");
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                return View("RPT_CREDITNOTE");
            }
        }
        public ActionResult RPT_STOCKCARD(string id)
        {
            try
            {
                if (id.Contains("RPT_STOCKCARD_SEARCH"))
                {
                    id = id.Replace("RPT_STOCKCARD_SEARCH", "");
                    ViewBag.OpenReportType = 1;
                }
                else
                {
                    id = id.Replace("RPT_STOCKCARD_GETITEM", "");
                    ViewBag.OpenReportType = 2;
                }

                ViewBag.ReportData = id;
                return View("RPT_STOCKCARD");
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                return View("RPT_STOCKCARD");
            }
        }
        public ActionResult StockTotal(StockVM vm)
        {
            try
            {
                vm = new StockVM();
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("StockTotal", vm);
        }
        public ActionResult StockTotalSearch(StockVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.StockTotalSearch(vm);
                if (vm.stockDownloadVM != null)
                {
                    if (vm.stockDownloadVM.EXPORT_DATA.Length > 0)
                    {
                        _downloadFileModel = vm.stockDownloadVM;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("StockTotal", vm);
        }
        public ActionResult StockTotalExport(StockVM vm)
        {
            DownloadVM downloadFile = _downloadFileModel;
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return File(downloadFile.EXPORT_DATA, downloadFile.CONTENT_TYPE, downloadFile.FILE_NAME);
        }
        public ActionResult StatusReceiveByDoAndGr(StockVM vm)
        {
            try
            {
                vm = new StockVM();
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("StatusReceiveByDoAndGr", vm);
        }
        public ActionResult StatusReceiveByDoAndGrSearch(StockVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.StatusReceiveByDoAndGrSearch(vm);
                if (vm.stockDownloadVM != null)
                {
                    if (vm.stockDownloadVM.EXPORT_DATA.Length > 0)
                    {
                        _downloadFileModel = vm.stockDownloadVM;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("StatusReceiveByDoAndGr", vm);
        }
        public ActionResult StatusReceiveByDoAndGrExport(StockVM vm)
        {
            DownloadVM downloadFile = _downloadFileModel;
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                StockBC bc = new StockBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return File(downloadFile.EXPORT_DATA, downloadFile.CONTENT_TYPE, downloadFile.FILE_NAME);
        }
    }
}