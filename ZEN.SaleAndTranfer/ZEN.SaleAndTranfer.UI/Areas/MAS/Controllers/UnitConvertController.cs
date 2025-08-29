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
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class UnitConvertController : BaseController
    {
        private UnitConvertVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_ITEM_STOCK] != null)
                {
                    return (UnitConvertVM)Session[SessionConst.SESSION_ITEM_STOCK];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_ITEM_STOCK] = value;
            }
        }
        
        private UnitConvertVM createNewModel(UnitConvertVM vm)
        {
            if (vm == null) vm = new UnitConvertVM();
            if (vm.unitConvertVM_MA == null) vm.unitConvertVM_MA = new UnitConvertET_MA();
            if (vm.unitConvertSearchCriteriaVM == null) vm.unitConvertSearchCriteriaVM = new UnitConvertSearchCriteriaVM();
            if (vm.unitConvertSearchResultVM == null) vm.unitConvertSearchResultVM = new UnitConvertSearchResultVM();
            return vm;
        }
        private string v_fullPath
        {
            get
            {
                if (Session[ExportFileConst.FULL_PATH_UnitConvert] != null)
                {
                    return (string)Session[ExportFileConst.FULL_PATH_UnitConvert];
                }

                return null;
            }

            set
            {
                Session[ExportFileConst.FULL_PATH_UnitConvert] = value;
            }
        }
        private string v_fileName
        {
            get
            {
                if (Session[ExportFileConst.FULL_NAME_UnitConvert] != null)
                {
                    return (string)Session[ExportFileConst.FULL_NAME_UnitConvert];
                }

                return null;
            }

            set
            {
                Session[ExportFileConst.FULL_NAME_UnitConvert] = value;
            }
        }
        //
        // GET: /MAS/UnitConvert/
        public ActionResult Index(UnitConvertVM vm)
        {
            try
            {
                vm = new UnitConvertVM();
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UnitConvertBC bc = new UnitConvertBC();
                vm = bc.InnitialCriteria(vm);
                _vmodel = null;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(UnitConvertVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UnitConvertBC bc = new UnitConvertBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.Search(vm);
                _vmodel = vm;

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult OnChangeBrand(UnitConvertVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UnitConvertBC bc = new UnitConvertBC();
                vm = bc.InnitialCriteria(vm);
                _vmodel = vm;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }


        public ActionResult ExportFile(UnitConvertVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                vm = _vmodel;
                vm.MessageList.Clear();
                InitMesssageViewBagUI();
                //ViewBag.ImportAndExportTitle = ImportAndExportTitle;

                if (v_fullPath != null && v_fileName != null)
                {
                    ViewBag.ExportFileFlag = null;
                    var fullPathDownload = v_fullPath;
                    var fileNameDownload = v_fileName;
                    v_fullPath = null;
                    v_fileName = null;
                    return File(fullPathDownload, "application/vnd.ms-excel", fileNameDownload);
                }
                XSSFWorkbook wb;
                vm.SessionLogin = GetCurrentUser;
                string fileName = GetFileNameForDownload(ConfigNameEnum.UNITCONVERT);
                string path = Path.GetFullPath(ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.TEMPFILE, ConfigNameEnum.DOWNLOAD));
                string fullPath = path + fileName;

                UnitConvertBC bc = new UnitConvertBC();
                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Download " + ConfigNameEnum.UNITCONVERT.ToString(), "Download " + ConfigNameEnum.UNITCONVERT.ToString());
                // Processing export files
                wb = bc.ExportFile(vm, fullPath, batchID);
                // End batch
                batchBC.EndBatch(batchID, vm.SessionLogin.USER_NAME);

                v_fullPath = fullPath;
                v_fileName = fileName;
                ViewBag.ExportFileFlag = 1;
            }
            catch (Exception ex)
            {
                batchBC.InsertBatchLog(batchID, "Error", ex.Message, ex.StackTrace, null, vm.SessionLogin.USER_NAME); // Insert Log Error
                batchBC.ErrorBatch(batchID, ex.Message); // Error batch
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult ImportFile(UnitConvertVM vm)
        {
            int batchID = -1;
            BatchBC batchBC = new BatchBC();
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                UnitConvertBC bc = new UnitConvertBC();
                vm = bc.InnitialCriteria(vm);

                // Start batch get batchID
                batchID = batchBC.StartBatch(vm.SessionLogin.USER_NAME, "Upload " + ConfigNameEnum.UNITCONVERT.ToString(), "Upload " + ConfigNameEnum.UNITCONVERT.ToString());

                UnitConvertBC unitConversionBC = new UnitConvertBC();
                vm = unitConversionBC.ValidateImportFile(Request.Files[0], vm);
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

                    vm = unitConversionBC.ImportFile(vm, batchID, path);
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