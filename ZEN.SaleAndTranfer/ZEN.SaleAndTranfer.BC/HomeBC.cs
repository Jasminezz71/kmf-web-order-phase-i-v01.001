using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.BC
{
    public class HomeBC
    {
        public UserVM InnitialMA(UserVM vm)
        {
            try
            {
                DDLBC bc = new DDLBC();
                if (vm.userVM_MA == null) vm.userVM_MA = new UserET_MA();
                vm.userVM_MA.brandList = bc.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                vm.userVM_MA.branchList = bc.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = vm.userVM_MA.branchList;

                if (vm.userVM_MA.BRAND_CODE != null)
                {
                    vm.userVM_MA.branchList = bc.GetBranchbyBrandAndUsername(
                                                    DDLModeEnumET.SELECT_ALL, 
                                                    vm.userVM_MA.BRAND_CODE,
                                                    vm.SessionLogin.USER_NAME);
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private UserVM ValidateSave(UserVM vm)
        {
            try
            {
                if (vm.userVM_MA.BRAND_CODE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ร้าน"));
                }
                if (vm.userVM_MA.BRANCH_CODE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM ChangeBranchSave(UserVM vm)
        {
            try
            {
                #region ===== Validate Save =====

                //vm.MessageList.Clear();
                //vm = this.ValidateSave(vm);
                //if (vm.MessageList.Count > 0)
                //{
                //    return vm;
                //}

                #endregion

                foreach (var item in vm.userVM_MA.brandList)
                {
                    if (item.ITEM_VALUE == vm.userVM_MA.BRAND_CODE)
                    {
                        vm.SessionLogin.BRAND_NAME = item.ITEM_TEXT;
                        break;
                    }
                }

                foreach (var item in vm.userVM_MA.branchList)
                {
                    if (item.ITEM_VALUE == vm.userVM_MA.BRANCH_CODE)
                    {
                        vm.SessionLogin.BRANCH_NAME = item.ITEM_TEXT;
                        break;
                    }
                }

                vm.SessionLogin.BRAND_CODE = vm.userVM_MA.BRAND_CODE;
                vm.SessionLogin.BRANCH_CODE = vm.userVM_MA.BRANCH_CODE;
                if (vm.SessionLogin.BRANCH_CODE == null) vm.SessionLogin.BRANCH_NAME = "-- ทั้งหมด --";

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
