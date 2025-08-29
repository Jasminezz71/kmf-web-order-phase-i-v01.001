using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.ACC;
using ZEN.SaleAndTranfer.DC;
using ZEN.SaleAndTranfer.BC;

namespace ZEN.SaleAndTranfer.BC.Acc
{
    public class EndDayDocBC : BaseBC
    {
        public bool SaveFileDetail(UploadEndDayDocVM vm)
        {
            try
            {    
                // step 1: จัดรูป parameter จาก vm ให้อยู่ในรูปของ pet
                var pet = new USP_R_END_DAY_DOC_Insert_PET();

                pet.BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                pet.BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                pet.END_DAY_DATE = vm.EndDayDate;
                pet.END_DAY_DOC_TYPE_ID = vm.EndDayDocTypeID;
                pet.FILE_NAME_ORI = vm.SelectedFile.FileName;
                pet.FILE_NAME_DEST = vm.FileNameDest;
                pet.FILE_SIZE = vm.SelectedFile.ContentLength;
                pet.FILE_CONTENT_TYPE = vm.SelectedFile.ContentType;
                pet.FILE_PATH = vm.FilePath;
                pet.CREATE_BY = vm.SessionLogin.USER_NAME;
                
                // step 2: call dc to save
                var dc = new TB_R_END_DAY_DOC_DC();
                bool saveDBSuccess = dc.SaveEndDayDocData(pet);

                return saveDBSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteFileDetail(IndexEndDayDocVM vm)
        {
            try
            {
                // step 1: จัดรูป parameter จาก vm ให้อยู่ในรูปของ pet
                var pet = new USP_R_END_DAY_DOC_Delete_PET();
                pet.END_DAY_DOC_ID = vm.RET.END_DAY_DOC_ID;
                pet.UPDATE_BY = vm.SessionLogin.USER_NAME;

                // step 2: call dc to delete
                var dc = new TB_R_END_DAY_DOC_DC();
                bool deleteDBSuccess = dc.DeleteEndDayDocData(pet);

                return deleteDBSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UnDeleteFileDetail(IndexEndDayDocVM vm)
        {
            try
            {
                // step 1: จัดรูป parameter จาก vm ให้อยู่ในรูปของ pet
                var pet = new USP_R_END_DAY_DOC_Delete_PET();
                pet.END_DAY_DOC_ID = vm.RET.END_DAY_DOC_ID;
                pet.UPDATE_BY = vm.SessionLogin.USER_NAME;

                // step 2: call dc to delete
                var dc = new TB_R_END_DAY_DOC_DC();
                bool deleteDBSuccess = dc.UnDeleteEndDayDocData(pet);

                return deleteDBSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetDataByPK(IndexEndDayDocVM vm)
        {
            try
            {
                // step 1: validate EndDayDate ต้องห้ามเป็น Null
                bool validateSuccess = this.ValidateBeforGetDataByPK(vm.END_DAY_DOC_ID);
                if (!validateSuccess)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "เอกสารที่ต้องการลบ"));
                }

                var dc = new TB_R_END_DAY_DOC_DC();
                vm.RET = dc.GetDataByPK(vm.END_DAY_DOC_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetByEndDayDate(IndexEndDayDocVM vm)
        {
            try
            {
                // step 1: validate EndDayDate ต้องห้ามเป็น Null
                bool validateSuccess = this.ValidateBeforGetByEndDayDate(vm);
                if (!validateSuccess)
                {
                    return;
                }

                var dc = new TB_R_END_DAY_DOC_DC();
                vm.RETs = dc.GetByEndDayDate(vm.EndDayDate.Value, vm.SessionLogin.BRANCH_CODE);

                if (vm.RETs != null && vm.RETs.Count > 0)
                {
                    return;
                }
                else
                {
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00094));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateBeforGetByEndDayDate(IndexEndDayDocVM vm)
        {
            try
            {
                if (!vm.EndDayDate.HasValue)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันที่ระบุในหัวเอกสารปิดสิ้นวัน"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private bool ValidateBeforGetDataByPK(int id)
        {
            try
            {
                if (id == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void OnInitUpload(UploadEndDayDocVM vm)
        {
            try
            {
                var dc = new TB_M_END_DAY_DOC_TYPE_DC();
                var ret = dc.GetByPK(vm.EndDayDocTypeID);
                vm.EndDayDocTypeDisplay = ret.END_DAY_DOC_TYPE_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
