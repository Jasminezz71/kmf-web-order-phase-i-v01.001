using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class SignatureBC
    {
        SignatureDC signatureDC = new SignatureDC();

        #region ---- Innitial ----

        public SignatureVM InnitialCriteria(SignatureVM vm)
        {
            try
            {
                if (vm.signatureSearchCriteriaVM == null) { vm.signatureSearchCriteriaVM = new SignatureSearchCriteriaVM(); }
                var ddlBC = new DDLBC();
                var brandList = ddlBC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = ddlBC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var companyList = ddlBC.GetCompany(DDLModeEnumET.SELECT_ALL);

                vm.signatureSearchCriteriaVM.brandList = brandList;
                vm.signatureSearchCriteriaVM.branchList = branchList;
                vm.signatureSearchCriteriaVM.companyList = companyList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SignatureVM InnitialMA(SignatureVM vm)
        {
            try
            {
                if (vm.signatureVM_MA == null) { vm.signatureVM_MA = new SignatureET_MA(); }
                var ddlBC = new DDLBC();
                var brandList = ddlBC.GetBrandByUsername(DDLModeEnumET.SELECT_ONE, vm.SessionLogin.USER_NAME);
                var branchList = ddlBC.GetBranchByUsername(DDLModeEnumET.SELECT_ONE, vm.SessionLogin.USER_NAME);
                var companyList = ddlBC.GetCompany(DDLModeEnumET.SELECT_ONE);

                vm.signatureVM_MA.brandList = brandList;
                vm.signatureVM_MA.branchList = branchList;
                vm.signatureVM_MA.companyList = companyList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public SignatureVM Search(SignatureVM vm)
        {
            try
            {
                if (vm.signatureSearchResultVM == null) vm.signatureSearchResultVM = new SignatureSearchResultVM();
                if (vm.signatureSearchResultVM.resultList == null) vm.signatureSearchResultVM.resultList = new List<SignatureSearchResultET>();
                vm.signatureSearchResultVM.resultList = signatureDC.Search(vm.signatureSearchCriteriaVM);
                if (vm.signatureSearchResultVM.resultList == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                }
                else
                {
                    if (vm.signatureSearchResultVM.resultList.Count == 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                    }
                    else
                    {
                        vm.signatureSearchResultVM.countAll = vm.signatureSearchResultVM.resultList.Count;
                    }
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SignatureVM ValidateInsertSave(SignatureVM vm)
        {
            try
            {
                if (vm.signatureVM_MA.COMPANY_CODE == null || vm.signatureVM_MA.COMPANY_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "บริษัท"));
                }
                if (vm.signatureVM_MA.fileUploadList == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "อัพโหลดไฟล์"));
                }
                else
                {
                    var UploadSize = ConfigBC.GetConfigValue(CategoryConfigEnum.SIGNATURE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.MAX_SIZE);
                    var UploadType = ConfigBC.GetConfigValue(CategoryConfigEnum.SIGNATURE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.TYPE);
                    int errorType = 0;
                    foreach (var item in vm.signatureVM_MA.fileUploadList)
                    {
                        string[] splitType = UploadType.Split(',');
                        foreach (var split in splitType)
                        {
                            if (split == (item.FILE_EXTENSION.Replace(".", "").ToLower()))
                            {
                                errorType++;
                                break;
                            }
                        }

                        if (errorType == 0) // นามสกุลไม่ตรงตามที่กำหนด
                        {
                            var message = MessageBC.GetMessage(MessageCodeConst.M00002);
                            message.MESSAGE_TEXT_FOR_DISPLAY = message.MESSAGE_TEXT_FOR_DISPLAY.Replace("JPG, PNG หรือ GIF", UploadType);
                            vm.AddMessage(message);
                        }

                        if (item.FILE_SIZE > (Convert.ToDecimal(UploadSize) * 1048576))
                        {
                            var message = MessageBC.GetMessage(MessageCodeConst.M00005, UploadSize);
                            message.MESSAGE_TEXT_FOR_DISPLAY = message.MESSAGE_TEXT_FOR_DISPLAY.Replace("ต่อครั้ง", "MB");
                            vm.AddMessage(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public SignatureVM InsertSave(SignatureVM vm)
        {
            try
            {
                #region ---- Validate ----
                vm.ClearMessageList();
                vm = this.ValidateInsertSave(vm);
                if (!vm.MessageList.Count.Equals(0))
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                vm.signatureVM_MA.CREATE_BY = vm.SessionLogin.USER_NAME;
                vm.signatureVM_MA.CREATE_DATE = DateTime.Now;

                int result = 0;
                result = signatureDC.InsertSave(vm.signatureVM_MA, vm.signatureVM_MA.fileUploadList);
                if (result == 0)
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SignatureVM Update(SignatureVM vm, int ImageID)
        {
            try
            {
                vm.signatureVM_MA.IMAGE_ID = ImageID;
                vm.signatureVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.signatureVM_MA.UPDATE_DATE = DateTime.Now;

                int result = 0;
                result = signatureDC.Update(vm.signatureVM_MA);
                if (result == 0)
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                    if (vm.signatureSearchResultVM == null) vm.signatureSearchResultVM = new SignatureSearchResultVM();
                    if (vm.signatureSearchResultVM.resultList == null) vm.signatureSearchResultVM.resultList = new List<SignatureSearchResultET>();
                    vm.signatureSearchResultVM.resultList = signatureDC.Search(vm.signatureSearchCriteriaVM);
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
