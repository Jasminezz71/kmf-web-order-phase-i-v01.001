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
    public class BatchHAVIController : BaseController
    {
        //
        // GET: /ADMIN/BatchHAVI/
        public ActionResult Index(BatchHaviVM vm)
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

        //============================ Purchase ============================//
        public ActionResult BatchPO(BatchHaviVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                BatchHaviBC bc = new BatchHaviBC();
                vm = bc.InnitialMA(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("BatchPO", vm);
        }
        public ActionResult RunBatchPO(BatchHaviVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchHaviBC bc = new BatchHaviBC();
                vm = bc.InnitialMA(vm);
                vm = bc.RunBatchPO(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            ViewBag.RunBatchPOFlag = 1;
                            vm.MessageList.Clear();
                            vm.batchVM_MA = new BatchHaviET_MA();
                            vm = bc.InnitialMA(vm);
                        }
                        else
                        {
                            ViewBag.RunBatchPOFlag = 0;
                        }
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("BatchPO", vm);
            }
            return View("BatchPO", vm);
        }


        //============================ Warehouse ============================//
        public ActionResult RunBatchRN(BatchHaviVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchHaviBC bc = new BatchHaviBC();
                vm = bc.RunBatchRN(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR")) ViewBag.RunBatchRNFlag = 0;
                    else ViewBag.RunBatchRNFlag = 1;
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
        public ActionResult BatchSO(BatchHaviVM vm)
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
            return View("BatchSO", vm);
        }
        public ActionResult RunBatchSO(BatchHaviVM vm)
        {
            try
            {

                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchHaviBC bc = new BatchHaviBC();
                vm = bc.InnitialMA(vm);
                vm = bc.RunBatchSO(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("INF"))
                        {
                            ViewBag.RunBatchSOFlag = 1;
                            vm.MessageList.Clear();
                            vm.batchVM_MA = new BatchHaviET_MA();
                            vm = bc.InnitialMA(vm);
                        }
                        else
                        {
                            ViewBag.RunBatchSOFlag = 0;
                        }
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("BatchSO", vm);
            }
            return View("BatchSO", vm);
        }
        public ActionResult RunBatchDN(BatchHaviVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                this.insertLog(vm.SessionLogin.USER_NAME, this);
                BatchHaviBC bc = new BatchHaviBC();
                vm = bc.RunBatchDN(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR")) ViewBag.RunBatchDNFlag = 0;
                    else ViewBag.RunBatchDNFlag = 1;
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
	}
}