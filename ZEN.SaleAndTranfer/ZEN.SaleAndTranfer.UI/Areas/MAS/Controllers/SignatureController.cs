using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class SignatureController : BaseController
    {
        private SignatureVM _vmodel
        {
            get
            {
                if (Session[SessionConst.SESSION_SIGNATURE] != null)
                {
                    return (SignatureVM)Session[SessionConst.SESSION_SIGNATURE];
                }

                return null;
            }

            set
            {
                Session[SessionConst.SESSION_SIGNATURE] = value;
            }
        }
        SignatureBC signatureBC = new SignatureBC();
        private SignatureVM createNewModel(SignatureVM vm)
        {
            if (vm == null) vm = new SignatureVM();
            if (vm.signatureVM_MA == null) vm.signatureVM_MA = new SignatureET_MA();
            if (vm.signatureSearchCriteriaVM == null) vm.signatureSearchCriteriaVM = new SignatureSearchCriteriaVM();
            if (vm.signatureSearchResultVM == null) vm.signatureSearchResultVM = new SignatureSearchResultVM();
            return vm;
        }

        //
        // GET: /MAS/Signature/
        public ActionResult Index(SignatureVM vm)
        {
            try
            {
                vm = new SignatureVM();
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                _vmodel = null;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(SignatureVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                vm = signatureBC.Search(vm);
                _vmodel = vm;

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        public ActionResult Insert(SignatureVM vm)
        {
            try
            {
                vm = new SignatureVM();
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialMA(vm);
                _vmodel = null;
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Insert", vm);
        }
        public ActionResult InsertSave(SignatureVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialMA(vm);
                vm = this.GetFileFromUI(vm);
                vm = signatureBC.InsertSave(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Insert", vm);
        }

        public ActionResult UpdateActiveDeactive(SignatureVM vm, int id)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
                vm = signatureBC.Update(vm, id);
                _vmodel = vm;

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        public ActionResult ChangeBrand(SignatureVM vm)
        {
            try
            {
                ModelState.Clear();
                createNewModel(vm);
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                vm = this.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }

        #region === Innitial ===
        private SignatureVM InnitialCriteria(SignatureVM vm)
        {
            try
            {
                vm = signatureBC.InnitialCriteria(vm);
                if (vm.SessionLogin.ROLE_NAME.ToLower() != "admin"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "accounting"
                    && vm.SessionLogin.ROLE_NAME.ToLower() != "audit")
                {
                    if (vm.SessionLogin.BRAND_CODE == null || vm.SessionLogin.BRAND_CODE == "")
                    {
                        ViewBag.CanEditBrand = 1;
                        ViewBag.CanEditBranch = 0;
                    }
                    else
                    {
                        ViewBag.CanEditBrand = 1;
                        ViewBag.CanEditBranch = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        private SignatureVM InnitialMA(SignatureVM vm)
        {
            try
            {
                vm = signatureBC.InnitialMA(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        private SignatureVM GetFileFromUI(SignatureVM vm)
        {
            try
            {
                SignatureET fileUploadET = new SignatureET();
                if (vm.signatureVM_MA == null) vm.signatureVM_MA = new SignatureET_MA();
                if (vm.signatureVM_MA.fileUploadList == null) vm.signatureVM_MA.fileUploadList = new List<SignatureET>();

                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        fileUploadET = new SignatureET();

                        if (Request.Files[i].ContentLength > 0)
                        {
                            fileUploadET.FILE = Request.Files[i];
                            fileUploadET.FILE_PATH = Path.Combine(fileUploadET.FILE.FileName);
                            fileUploadET.FILE_NAME = Path.GetFileName(fileUploadET.FILE.FileName);
                            fileUploadET.FILE_SIZE = fileUploadET.FILE.ContentLength;
                            fileUploadET.CONTENT_TYPE = fileUploadET.FILE.ContentType;
                            fileUploadET.FILE_EXTENSION = Path.GetExtension(fileUploadET.FILE.FileName);

                            using (Stream fs = Request.Files[i].InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    fileUploadET.ATTACHMENT = bytes;
                                }
                            }
                        }
                        vm.signatureVM_MA.fileUploadList.Add(fileUploadET);
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}