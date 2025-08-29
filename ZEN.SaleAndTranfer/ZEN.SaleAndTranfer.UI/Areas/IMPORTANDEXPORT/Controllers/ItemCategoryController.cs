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
    public class ItemCategoryController : BaseController
    {
        public const string ImportAndExportTitle = "ประเภทใบตลาด ";
        private string v_fullPath
        {
            get
            {
                if (Session["FULL_PATH_ItemCategory"] != null)
                {
                    return (string)Session["FULL_PATH_ItemCategory"];
                }

                return null;
            }

            set
            {
                Session["FULL_PATH_ItemCategory"] = value;
            }
        }
        private string v_fileName
        {
            get
            {
                if (Session["FULL_NAME_ItemCategory"] != null)
                {
                    return (string)Session["FULL_NAME_ItemCategory"];
                }

                return null;
            }

            set
            {
                Session["FULL_NAME_ItemCategory"] = value;
            }
        }
        //
        // GET: /IMPORTANDEXPORT/ItemCategory/
        public ActionResult Index(ItemCategoryVM vm)
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
        public ActionResult ExportFile(ItemCategoryVM vm)
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
                string fileName = GetFileNameForDownload(ConfigNameEnum.WH_CATEGORY);
                string path = Path.GetFullPath(ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.TEMPFILE, ConfigNameEnum.DOWNLOAD));
                string fullPath = path + fileName;

                ItemCategoryBC bc = new ItemCategoryBC();
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Download " + ConfigNameEnum.WH_CATEGORY.ToString(), "Download " + ConfigNameEnum.WH_CATEGORY.ToString());
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
        public ActionResult ImportFile(ItemCategoryVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ImportAndExportTitle = ImportAndExportTitle;
                vm.SessionLogin = GetCurrentUser;
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Upload" + ConfigNameEnum.WH_CATEGORY.ToString(), "Upload" + ConfigNameEnum.WH_CATEGORY.ToString());

                ItemCategoryBC itemCategoryBC = new ItemCategoryBC();
                vm = itemCategoryBC.ValidateImportFile(Request.Files[0], vm);
                if (vm.MessageList.Count > 0)
                {
                    batchBC.InsertBatchLog(batchID, "Import File", "ประเภทใบตลาด", "Error"
                                            , MessageBC.GetMessage(MessageCodeConst.M00009, "ไฟล์ที่ต้องการอัพโหลด").MESSAGE_TEXT_FOR_DISPLAY, vm.SessionLogin.USER_NAME);
                    return View("Index", vm);
                }
                else
                {
                    var file = Request.Files[0];
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/TEMPFILEIMPORT/"), fileName);
                    file.SaveAs(path);

                    vm = itemCategoryBC.ImportFile(vm, batchID, path);
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