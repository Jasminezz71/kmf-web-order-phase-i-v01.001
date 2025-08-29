using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.BC.LOG;
using ZEN.SaleAndTranfer.DC.AUT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.BC.AUT
{
    public class LoginBC
    {
        private LoginVM ValidateLogin(LoginVM vm, out Boolean success)
        {
            try
            {
                //bool captchaSuccess = false;
                bool captchaSuccess = true;
                success = true;
                UserInfoDC dc = new UserInfoDC();
                var useInfo = dc.SearchUserByID(vm.loginVM_MA.USER_NAME);
                if (vm.loginVM_MA.USER_NAME == null || vm.loginVM_MA.USER_NAME.Trim() == "")
                {
                    success = false;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "ชื่อผู้ใช้งาน" }));
                }
                if (vm.loginVM_MA.PASSWORD == null || vm.loginVM_MA.PASSWORD.Trim() == "")
                {
                    success = false;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "รหัสผ่าน" }));
                }
                if (vm.loginVM_MA.USER_NAME != null && captchaSuccess)
                {
                    int result = dc.IsExistUsername(vm.loginVM_MA.USER_NAME);
                    if (result == 0) // ไม่มี User ในระบบ
                    {
                        success = false;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "ชื่อผู้ใช้และรหัสผ่าน" }));
                    }
                    else // มี User ในระบบ
                    {
                        bool checkUserAndPass = PasswordHashBC.ValidatePassword(vm.loginVM_MA.PASSWORD, useInfo[0].PASSWORD_HASH);
                        if (!checkUserAndPass) // Password ไม่ตรงกัน
                        {
                            success = false;
                            vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "ชื่อผู้ใช้หรือรหัสผ่าน" }));
                        }
                        else // Password ตรงกัน
                        {
                            if (useInfo[0].ACTIVE_FLAG.ToLower() == "false") // User ไม่ใช้งาน 
                            {
                                success = false;
                                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, "ชื่อผู้ใช้หรือรหัสผ่าน"));
                            }
                        }
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LoginVM Login(LoginVM vm, out bool success)
        {
            try
            {
                success = false;

                #region ---- Validate ----
                vm.ClearMessageList();
                vm = this.ValidateLogin(vm, out success);
                if (!success) { return vm; }
                #endregion

                vm = SearchUserByID(vm);

                return vm;
            }
            catch (Exception ex)
            {
                LogBC.WriteLogBC(ex, vm.SessionLogin.USER_NAME, this.GetType().Name);
                throw ex;
            }
        }
        public LoginVM SearchUserByID(LoginVM vm)
        {
            try
            {
                UserInfoDC dc = new UserInfoDC();
                if (vm.loginResultResultVM == null) { vm.loginResultResultVM = new LoginSearchResultVM(); }
                if (vm.loginResultResultVM.resultList == null) { vm.loginResultResultVM.resultList = new List<UserLoginInfoET>(); }
                vm.loginResultResultVM.resultList = dc.SearchUserByID(vm.loginVM_MA.USER_NAME);

                return vm;
            }
            catch (Exception ex)
            {
                LogBC.WriteLogBC(ex, vm.SessionLogin.USER_NAME, this.GetType().Name);
                throw ex;
            }
        }
        public UserInfoVM SetDataToSession(LoginVM vm)
        {
            UserInfoVM vmLogin = new UserInfoVM();
            try
            {
                if (vm.loginResultResultVM == null)
                {
                    UserInfoDC userLoginInfoDC = new UserInfoDC();
                    vm.loginResultResultVM = new LoginSearchResultVM();
                    vm.loginResultResultVM.resultList = new List<UserLoginInfoET>();
                    vm.loginResultResultVM.resultList = userLoginInfoDC.SearchUserByID(vm.loginVM_MA.USER_NAME);
                }

                vmLogin.USER_NAME = vm.loginResultResultVM.resultList[0].USER_NAME;
                vmLogin.PASSWORD_HASH = vm.loginResultResultVM.resultList[0].PASSWORD_HASH;
                vmLogin.EMPLOYEE_ID = vm.loginResultResultVM.resultList[0].EMPLOYEE_ID;
                vmLogin.FIRST_NAME_TH = vm.loginResultResultVM.resultList[0].FIRST_NAME_TH;
                vmLogin.LAST_NAME_TH = vm.loginResultResultVM.resultList[0].LAST_NAME_TH;
                vmLogin.FIRST_NAME_EN = vm.loginResultResultVM.resultList[0].FIRST_NAME_EN;
                vmLogin.LAST_NAME_EN = vm.loginResultResultVM.resultList[0].LAST_NAME_EN;
                vmLogin.EMAIL = vm.loginResultResultVM.resultList[0].EMAIL;
                vmLogin.PHONE_NO = vm.loginResultResultVM.resultList[0].PHONE_NO;
                vmLogin.PHONE_EXT = vm.loginResultResultVM.resultList[0].PHONE_EXT;
                vmLogin.MOBILE_NO = vm.loginResultResultVM.resultList[0].MOBILE_NO;
                vmLogin.ACTIVE_FLAG = vm.loginResultResultVM.resultList[0].ACTIVE_FLAG;
                vmLogin.IS_RESET_PWD = vm.loginResultResultVM.resultList[0].IS_RESET_PWD;
                vmLogin.USER_USM_TYPE = vm.loginResultResultVM.resultList[0].USER_USM_TYPE;
                vmLogin.BRAND_CODE = vm.loginResultResultVM.resultList[0].BRAND_CODE;
                vmLogin.BRAND_NAME = vm.loginResultResultVM.resultList[0].BRAND_NAME;
                vmLogin.BRANCH_NAME = vm.loginResultResultVM.resultList[0].BRANCH_NAME;
                vmLogin.BRANCH_CODE = vm.loginResultResultVM.resultList[0].LAST_NAME_TH;
                vmLogin.LOCKED_FLAG = vm.loginResultResultVM.resultList[0].LOCKED_FLAG;
                vmLogin.ROLE_NAME = vm.loginResultResultVM.resultList[0].ROLE_NAME;

                #region ===== Set Brand and Branch =====
                
                DDLBC ddlbc = new DDLBC();
                var branchlist = ddlbc.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vmLogin.USER_NAME);
                var brandlist = ddlbc.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vmLogin.USER_NAME);

                if (branchlist != null && brandlist != null)
                {
                    if (branchlist.Count == 2 && brandlist.Count == 2)
                    {
                        vmLogin.BRANCH_COUNT = branchlist.Count - 1;
                        vmLogin.BRAND_CODE = brandlist[1].ITEM_VALUE;
                        vmLogin.BRAND_NAME = brandlist[1].ITEM_TEXT;
                        vmLogin.BRANCH_NAME = branchlist[1].ITEM_TEXT;
                        vmLogin.BRANCH_CODE = branchlist[1].ITEM_VALUE;
                    }
                    else
                    {
                        vmLogin.BRANCH_COUNT = branchlist.Count - 1;
                        vmLogin.BRAND_NAME = brandlist[0].ITEM_TEXT;
                        vmLogin.BRAND_CODE = null;
                        vmLogin.BRANCH_NAME = branchlist[0].ITEM_TEXT;
                        vmLogin.BRANCH_CODE = null;
                    }
                }
                else
                {
                    vmLogin.BRANCH_COUNT = branchlist.Count - 1;
                    vmLogin.BRAND_CODE = null;
                    vmLogin.BRAND_NAME = null;
                    vmLogin.BRANCH_NAME = null;
                    vmLogin.BRANCH_CODE = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogBC.WriteLogBC(ex, vm.SessionLogin.USER_NAME, this.GetType().Name);
                throw ex;
            }
            return vmLogin;
        }

        private LoginVM ValidateResetPassword(LoginVM vm)
        {
            try
            {
                UserInfoDC userLoginInfoDC = new UserInfoDC();
                bool result = false;
                if (vm.loginVM_MA.USER_NAME == null || vm.loginVM_MA.USER_NAME.Trim() == "")
                {
                    result = true;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "User name" }));
                }
                if (vm.loginVM_MA.EMAIL == null || vm.loginVM_MA.EMAIL.Trim() == "")
                {
                    result = true;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "Email" }));
                }
                else
                {
                    var _userET = userLoginInfoDC.SearchUserByID(vm.loginVM_MA.USER_NAME);
                    if (_userET[0].EMAIL.ToLower() == "") _userET[0].EMAIL = null;
                    if (_userET[0].EMAIL == null)
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00030, "Email", "Email ในระบบ"));
                    }
                    else
                    {
                        if (vm.loginVM_MA.EMAIL.ToLower() != _userET[0].EMAIL.ToLower())
                        {
                            result = true;
                            vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00030, "Email", "Email ในระบบ"));
                        }
                    }
                }
                if (vm.loginVM_MA.NEW_PASSWORD == null || vm.loginVM_MA.NEW_PASSWORD.Trim() == "")
                {
                    result = true;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "New Password" }));
                }
                else
                {
                    if (vm.loginVM_MA.NEW_PASSWORD.Length < 8 || vm.loginVM_MA.NEW_PASSWORD.Length > 13)
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "New Password" }));
                    }
                    else if (!Regex.IsMatch(vm.loginVM_MA.NEW_PASSWORD, @"^[0-9a-zA-Z_-]+$"))
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "New Password" }));
                    }
                }
                if (vm.loginVM_MA.NEW_CONFIRM_PASSWORD == null || vm.loginVM_MA.NEW_CONFIRM_PASSWORD.Trim() == "")
                {
                    result = true;
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "Confirm New Password" }));
                }
                else
                {
                    if (vm.loginVM_MA.NEW_CONFIRM_PASSWORD.Length < 8 || vm.loginVM_MA.NEW_CONFIRM_PASSWORD.Length > 13)
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "Confirm New Password" }));
                    }
                    else if (!Regex.IsMatch(vm.loginVM_MA.NEW_CONFIRM_PASSWORD, @"^[0-9a-zA-Z_-]+$"))
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00012, new string[] { "Confirm New Password" }));
                    }
                }
                if (vm.loginVM_MA.NEW_CONFIRM_PASSWORD != null && vm.loginVM_MA.NEW_PASSWORD != null)
                {
                    if (vm.loginVM_MA.NEW_CONFIRM_PASSWORD != vm.loginVM_MA.NEW_PASSWORD)
                    {
                        result = true;
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00030, "Password", "New Password"));
                    }
                    else
                    {
                        var _userET = userLoginInfoDC.SearchUserByID(vm.loginVM_MA.USER_NAME);
                        string newPasswordHasH = PasswordHashBC.CreateHash(vm.loginVM_MA.NEW_PASSWORD);
                        if (PasswordHashBC.ValidatePassword(newPasswordHasH, _userET[0].PASSWORD_HASH))
                        {
                            result = true;
                            vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00030, "Password", "Password ในระบบ"));
                        }
                    }
                }
                if (result)
                {
                    vm.loginVM_MA.NEW_PASSWORD = null;
                    vm.loginVM_MA.NEW_CONFIRM_PASSWORD = null;
                }
            }
            catch (Exception ex)
            {
                LogBC.WriteLogBC(ex, vm.SessionLogin.USER_NAME, this.GetType().Name);
                throw ex;
            }
            return vm;
        }
        public LoginVM ResetPasswordSave(LoginVM vm)
        {
            try
            {
                #region ---- Validate ----
                vm.ClearMessageList();
                vm = this.ValidateResetPassword(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                vm.loginVM_MA.PASSWORD_HASH = PasswordHashBC.CreateHash(vm.loginVM_MA.NEW_PASSWORD);
                vm.loginVM_MA.UPDATE_BY = vm.loginVM_MA.USER_NAME;
                UserInfoDC dc = new UserInfoDC();
                int result = dc.ResetPasswordUser(this.SetValueToResetPassword(vm));
                if (result == 1) { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010)); }
                else { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011)); }
            }
            catch (Exception ex)
            {
                LogBC.WriteLogBC(ex, vm.SessionLogin.USER_NAME, this.GetType().Name);
                throw ex;
            }
            return vm;
        }

        private UserLoginInfoET SetValueToResetPassword(LoginVM vm)
        {
            try
            {
                UserLoginInfoET resetPasswordET = new UserLoginInfoET();
                resetPasswordET.USER_NAME = vm.loginVM_MA.USER_NAME;
                resetPasswordET.PASSWORD_HASH = vm.loginVM_MA.PASSWORD_HASH;
                resetPasswordET.IS_RESET_PWD = "0";
                resetPasswordET.UPDATE_BY = vm.loginVM_MA.UPDATE_BY;
                return resetPasswordET;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
