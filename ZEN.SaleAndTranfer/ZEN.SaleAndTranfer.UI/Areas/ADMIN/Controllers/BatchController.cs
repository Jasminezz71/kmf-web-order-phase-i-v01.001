using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.ADMIN;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.ADMIN;

namespace ZEN.SaleAndTranfer.UI.Areas.ADMIN.Controllers
{
    public class BatchController : BaseController
    {
        //
        // GET: /ADMIN/Batch/
        public ActionResult Index(BatchVM vm)
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
            return View("Index", vm);
        }
        public ActionResult BatchPR(BatchVM vm)
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
            return View("BatchPR", vm);
        }
        public ActionResult RunBatchPR(BatchVM vm)
        {
            try
            {

                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchBC bc = new BatchBC();
                vm = bc.RunBatchPR(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            ViewBag.RunBatchPRFlag = 1;
                            vm.MessageList.Clear();
                            vm.batchVM_MA = new BatchET_MA();
                        }
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("BatchPR", vm);
            }
            return View("BatchPR", vm);
        }
        public ActionResult RunBatchDO(BatchVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchBC bc = new BatchBC();
                vm = bc.RunBatchDO(vm);
                ViewBag.RunBatchDOFlag = 1;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult BatchGR(BatchVM vm)
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
            return View("BatchGR", vm);
        }
        public ActionResult RunBatchGR(BatchVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchBC bc = new BatchBC();
                vm = bc.RunBatchGR(vm);
                ViewBag.runBatchGRFlag = 1;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.runBatchGRFlag = 0;
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("BatchGR", vm);
        }
        public ActionResult RunBatchMaster(BatchVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchBC bc = new BatchBC();
                vm = bc.RunBatchMaster(vm);
                ViewBag.RunBatchMasterFlag = 1;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        private void insertLog(string userName, Controller controller)
        {
            try
            {
                LogWsHelper hp = new LogWsHelper();
                hp.InsertLog(userName, AccressTypeConst.WEB.ToString(), this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SaesTranTOERP(BatchVM vm)
        {
            try
            {
                
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                ModelState.Clear();
                var batchBC = new BatchBC();
                vm = batchBC.InnitialCriteria(vm);

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("SaesTranTOERP", vm);
        }

        public ActionResult ExportSaesTranTOERP(BatchVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchBC bc = new BatchBC();
                vm = bc.ExportSaesTranTOERP(vm);
                ViewBag.ExportSaesTranTOERPFlag = 1;
                vm = bc.InnitialCriteria(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("SaesTranTOERP", vm);
        }

        public ActionResult ChangeBrand(BatchVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                var batchBC = new BatchBC();
                vm = batchBC.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("SaesTranTOERP", vm);
        }

    }
}