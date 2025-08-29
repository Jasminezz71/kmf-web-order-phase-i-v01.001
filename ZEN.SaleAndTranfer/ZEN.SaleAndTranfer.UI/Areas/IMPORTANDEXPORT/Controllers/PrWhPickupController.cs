using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.UI.Areas.IMPORTANDEXPORT.Controllers
{
    public class PrWhPickupController : BaseController
    {
        public const string ImportAndExportTitle = "รายงาน PR เพื่อหยิบของ ";
        private string v_fullPath
        {
            get
            {
                if (Session["FULL_PATH_PrWhPickup"] != null)
                {
                    return (string)Session["FULL_PATH_PrWhPickup"];
                }

                return null;
            }

            set
            {
                Session["FULL_PATH_PrWhPickup"] = value;
            }
        }
        private string v_fileName
        {
            get
            {
                if (Session["FULL_NAME_PrWhPickup"] != null)
                {
                    return (string)Session["FULL_NAME_PrWhPickup"];
                }

                return null;
            }

            set
            {
                Session["FULL_NAME_PrWhPickup"] = value;
            }
        }
        //
        // GET: /IMPORTANDEXPORT/PrWhPickup/
        public ActionResult Index(PrWhPickupVM vm)
        {
            try
            {
                ViewBag.ImportAndExportTitle = ImportAndExportTitle;
                vm.prWhPickupVM_MA = new PrWhPickupET_MA();
                PrWhPickupBC bc = new PrWhPickupBC();
                vm.SessionLogin = GetCurrentUser;
                vm = bc.InnitialMA(vm);
                vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_FROM = DateTime.Now.AddDays(2);
                vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO = DateTime.Now.AddDays(2);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ExportFile(PrWhPickupVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                ViewBag.ImportAndExportTitle = ImportAndExportTitle;

                #region == Check return File ==
                if (v_fullPath != null && v_fileName != null)
                {
                    ViewBag.ExportFileFlag = null;
                    var fullPathDownload = v_fullPath;
                    var fileNameDownload = v_fileName;
                    v_fullPath = null;
                    v_fileName = null;
                    if (System.IO.File.Exists(fullPathDownload))
                    {
                        using (FileStream file = new FileStream(fullPathDownload, FileMode.Open, FileAccess.Read))
                        {
                            if (file.Length == 0)
                            {
                                vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00052));
                                return View("Index", vm);
                            }
                        }
                        return File(fullPathDownload, "application/vnd.ms-excel", fileNameDownload);
                    }
                    else
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00052));
                        return View("Index", vm);
                    }
                }
                #endregion

                vm.SessionLogin = GetCurrentUser;
                string fileName = GetFileNameForDownload(ConfigNameEnum.WH_PICKUP);
                string path = Path.GetFullPath(ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.TEMPFILE, ConfigNameEnum.DOWNLOAD));
                string fullPath = path + fileName;

                PrWhPickupBC bc = new PrWhPickupBC();
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Download " + ConfigNameEnum.WH_PICKUP.ToString(), "Download " + ConfigNameEnum.WH_PICKUP.ToString());
                // Processing export files
                vm.prWhPickupVM_MA.DISPLAY_HEADER = true;
                vm.prWhPickupVM_MA.USER_NAME = vm.SessionLogin.USER_NAME;
                vm = bc.ExportFile(vm, fullPath, batchID);
                // End batch
                batchBC.EndBatch(batchID, vm.SessionLogin.USER_NAME);

                vm = bc.InnitialMA(vm);
                ModelState.Clear();

                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return View("Index", vm);
                    }
                }
                v_fullPath = fullPath;
                v_fileName = fileName;
                ViewBag.ExportFileFlag = 1;

                //return File(fullPath, "application/vnd.ms-excel", fileName);
            }
            catch (Exception ex)
            {
                batchBC.InsertBatchLog(batchID, "Error", ex.Message, ex.StackTrace, null, vm.SessionLogin.USER_NAME); // Insert Log Error
                batchBC.ErrorBatch(batchID, ex.Message); // Error batch
                //batchBC.EndBatch(batchID, GetCurrentUser.USER_NAME); // End batch
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        public ActionResult OnChangeBrand(PrWhPickupVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                PrWhPickupBC bc = new PrWhPickupBC();
                vm = bc.InnitialMA(vm);
                vm = bc.OnChangeBrand(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
    }
}