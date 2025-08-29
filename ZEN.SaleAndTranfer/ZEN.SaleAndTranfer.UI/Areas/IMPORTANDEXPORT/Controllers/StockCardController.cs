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
    public class StockCardController : BaseController
    {
        public const string ImportAndExportTitle = "ประเภทสินค้า ";
        private string v_fullPath
        {
            get
            {
                if (Session["FULL_PATH_PrCategory"] != null)
                {
                    return (string)Session["FULL_PATH_PrCategory"];
                }

                return null;
            }

            set
            {
                Session["FULL_PATH_PrCategory"] = value;
            }
        }
        private string v_fileName
        {
            get
            {
                if (Session["FULL_NAME_PrCategory"] != null)
                {
                    return (string)Session["FULL_NAME_PrCategory"];
                }

                return null;
            }

            set
            {
                Session["FULL_NAME_PrCategory"] = value;
            }
        }
        //
        // GET: /IMPORTANDEXPORT/PrCategory/
        public ActionResult Index(PrCategoryVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ImportAndExportTitle = ImportAndExportTitle;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ExportFile(PrCategoryVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                InitMesssageViewBagUI();
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

                XSSFWorkbook wb;
                vm.SessionLogin = GetCurrentUser;
                string fileName = GetFileNameForDownload(ConfigNameEnum.PR_CATEGORY);
                string path = Path.GetFullPath(ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.TEMPFILE, ConfigNameEnum.DOWNLOAD));
                string fullPath = path + fileName;

                PrCategoryBC bc = new PrCategoryBC();
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Download " + ConfigNameEnum.PR_CATEGORY.ToString(), "Download " + ConfigNameEnum.PR_CATEGORY.ToString());
                // Processing export files
                wb = bc.ExportFile(vm, fullPath, batchID);
                // End batch
                batchBC.EndBatch(batchID, vm.SessionLogin.USER_NAME);

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
        public ActionResult ImportFile(PrCategoryVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ImportAndExportTitle = ImportAndExportTitle;
                vm.SessionLogin = GetCurrentUser;
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Upload " + ConfigNameEnum.STOCK_CARD.ToString(), "Upload " + ConfigNameEnum.STOCK_CARD.ToString());

                PrCategoryBC PrCategoryBC = new PrCategoryBC();
                vm = PrCategoryBC.ValidateImportFile(Request.Files[0], vm);
                if (vm.MessageList.Count > 0)
                {
                    batchBC.InsertBatchLog(batchID, "Import File", "" + ConfigNameEnum.STOCK_CARD.ToString(), "Error"
                                            , MessageBC.GetMessage(MessageCodeConst.M00009, "ไฟล์ที่ต้องการอัพโหลด").MESSAGE_TEXT_FOR_DISPLAY, vm.SessionLogin.USER_NAME);
                    return View("Index", vm);
                }
                else
                {
                    var file = Request.Files[0];
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/TEMPFILEIMPORT/"), fileName);
                    file.SaveAs(path);

                    vm = PrCategoryBC.ImportFile(vm, batchID, path);
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        {
                            // End batch
                            batchBC.ErrorBatch(batchID, vm.MessageList[0].MESSAGE_TEXT_FOR_DISPLAY);
                            return View("Index", vm);
                        }
                    }
                }
                // End batch
                batchBC.EndBatch(batchID, vm.SessionLogin.USER_NAME);

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
	}
}