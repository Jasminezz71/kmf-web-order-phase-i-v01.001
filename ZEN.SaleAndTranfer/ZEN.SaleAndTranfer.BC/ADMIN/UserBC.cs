using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.DC.AUT;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.BC.ADMIN
{
    public class UserBC
    {
        #region ---- Innitial ----

        public UserVM InnitialCriteria(UserVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ALL);
                if (vm.userSearchCriteriaVM == null) { vm.userSearchCriteriaVM = new UserSearchCriteriaVM(); }
                vm.userSearchCriteriaVM.activeFlagList = activeFlagList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM InnitialMA(UserVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ONE);
                //var brandList = ddlDC.GetBrand(DDLModeEnumET.SELECT_ONE);
                //var branchList = ddlDC.GetBranchbyBrand(null, DDLModeEnumET.SELECT_ONE);
                if (vm.userVM_MA == null) { vm.userVM_MA = new UserET_MA(); }
                vm.userVM_MA.activeFlagList = activeFlagList;
                //vm.userVM_MA.brandList = brandList;
                //vm.userVM_MA.branchList = branchList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public List<SortingInfoET> SetOrderListPR()
        {
            List<SortingInfoET> orderList = new List<SortingInfoET>();
            try
            {
                SortingInfoET result = new SortingInfoET();
                for (int i = 0; i < 7; i++)
                {
                    result = new SortingInfoET();
                    result.SortField = "";
                    result.SortType = "ASC";
                    orderList.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderList;
        }

        private UserVM ValidateCreate(UserVM vm)
        {
            try
            {
                if (vm.userVM_MA.USER_NAME == null || vm.userVM_MA.USER_NAME.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อผู้ใช้งาน"));
                }
                if (vm.userVM_MA.PASSWORD == null || vm.userVM_MA.PASSWORD.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "รหัสผ่าน"));
                }
                if (vm.userVM_MA.FIRST_NAME_TH == null || vm.userVM_MA.FIRST_NAME_TH.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อ (ภาษาไทย)"));
                }
                if (vm.userVM_MA.LAST_NAME_TH == null || vm.userVM_MA.LAST_NAME_TH.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "นามสกุล (ภาษาไทย)"));
                }
                if (vm.userVM_MA.EMAIL == null || vm.userVM_MA.EMAIL.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "อีเมล"));
                }
                else
                {
                    if (!IsMailFormat(vm.userVM_MA.EMAIL))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00031, vm.userVM_MA.EMAIL, "example@zengroup.co.th"));
                    }
                }
                if (vm.userVM_MA.ACTIVE_FLAG == null || vm.userVM_MA.ACTIVE_FLAG.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สถานะการใช้งาน"));
                }
                //if (vm.userVM_MA.BRAND_CODE == null || vm.userVM_MA.BRAND_CODE.Trim() == "")
                //{
                //    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ร้าน"));
                //}
                //else
                //{
                //    if (vm.userVM_MA.BRANCH_CODE == null || vm.userVM_MA.BRANCH_CODE.Trim() == "")
                //    {
                //        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                //    }
                //}
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        private UserVM ValidateUpdate(UserVM vm)
        {
            try
            {
                if (vm.userVM_MA.FIRST_NAME_TH == null || vm.userVM_MA.FIRST_NAME_TH.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อ (ภาษาไทย)"));
                }
                if (vm.userVM_MA.LAST_NAME_TH == null || vm.userVM_MA.LAST_NAME_TH.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "นามสกุล (ภาษาไทย)"));
                }
                if (vm.userVM_MA.EMAIL == null || vm.userVM_MA.EMAIL.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "อีเมล"));
                }
                else
                {
                    if (!IsMailFormat(vm.userVM_MA.EMAIL))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00031, vm.userVM_MA.EMAIL, "example@zengroup.co.th"));
                    }
                }
                if (vm.userVM_MA.ACTIVE_FLAG == null || vm.userVM_MA.ACTIVE_FLAG.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สถานะการใช้งาน"));
                }
                //if (vm.userVM_MA.BRAND_CODE == null || vm.userVM_MA.BRAND_CODE.Trim() == "")
                //{
                //    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ร้าน"));
                //}
                //else
                //{
                //    if (vm.userVM_MA.BRANCH_CODE == null || vm.userVM_MA.BRANCH_CODE.Trim() == "")
                //    {
                //        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                //    }
                //}
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsMailFormat(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                string[] split = emailaddress.Split('@');
                if (split[split.Length - 1].Trim(' ') != "zengroup.co.th")
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public UserVM SearchUser(UserVM vm, string SortField, string Sorttype)
        {
            try
            {

                #region ## Validate ##
                //vm.ClearMessageList();
                //vm = this.ValidateSearch(vm);
                //if (!vm.MessageList.Count.Equals(0))
                //{
                //    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                //    {
                //        return vm;
                //    }
                //}
                #endregion

                UserInfoDC dc = new UserInfoDC();
                int countAll = 0;
                if (vm.userSearchResultVM == null) { vm.userSearchResultVM = new UserSearchResultVM(); }
                vm.userSearchResultVM.resultList = dc.Search(vm.userSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.userSearchResultVM.countAll = countAll;
                if (vm.userSearchResultVM.countAll == 0)
                {
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00007));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM SearchUserByID(UserVM vm)
        {
            try
            {

                UserInfoDC dc = new UserInfoDC();
                if (vm.userSearchResultVM == null) vm.userSearchResultVM = new UserSearchResultVM();
                vm.userSearchResultVM.resultList = dc.SearchUserByID(vm.userVM_MA.USER_NAME);
                if (vm.userSearchResultVM.resultList != null)
                {
                    vm.userVM_MA = this.SetUserMA(vm.userSearchResultVM.resultList.FirstOrDefault());
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public UserVM CreateSave(UserVM vm)
        {
            try
            {
                #region ==== Validate Create ====

                vm.MessageList.Clear();
                vm = this.ValidateCreate(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                #region == Set values to Create User ==
                vm.userVM_MA.PASSWORD_HASH = PasswordHashBC.CreateHash(vm.userVM_MA.USER_NAME);
                vm.userVM_MA.IS_RESET_PWD = "1";
                vm.userVM_MA.USER_USM_TYPE = "USM";
                vm.userVM_MA.CREATE_BY = vm.SessionLogin.USER_NAME;
                vm.userVM_MA.CREATE_DATE = DateTime.Now;
                vm.userVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.userVM_MA.UPDATE_DATE = DateTime.Now;
                #endregion

                int result = 0;
                UserInfoDC dc = new UserInfoDC();
                result = dc.CreateUser(vm.userVM_MA);
                if (result == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM UpdateSave(UserVM vm)
        {
            try
            {
                #region ==== Validate Update ====

                vm.MessageList.Clear();
                vm = this.ValidateUpdate(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                #region == Set values to Update User ==
                vm.userVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.userVM_MA.UPDATE_DATE = DateTime.Now;
                #endregion

                int result = 0;
                UserInfoDC dc = new UserInfoDC();
                result = dc.UpdateUser(vm.userVM_MA);
                if (result == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM ResetPassword(UserVM vm)
        {
            try
            {
                vm.userVM_MA.PASSWORD_HASH = PasswordHashBC.CreateHash(vm.userVM_MA.USER_NAME);
                vm.userVM_MA.IS_RESET_PWD = "1";
                vm.userVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.userVM_MA.UPDATE_DATE = DateTime.Now;
                UserInfoDC dc = new UserInfoDC();
                int result = dc.ResetPasswordUser(vm.userVM_MA);
                if (result == 1) { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010)); }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM ChangeStatusUser(UserVM vm, string statusFlag)
        {
            try
            {
                vm.userVM_MA.ACTIVE_FLAG = statusFlag;
                vm.userVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.userVM_MA.UPDATE_DATE = DateTime.Now;
                UserInfoDC dc = new UserInfoDC();
                int result = dc.ChangeStatusUser(vm.userVM_MA);
                if (result == 1) { vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010)); }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM OnChangeBrand(UserVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                var branchList = ddlDC.GetBranchbyBrand(vm.userVM_MA.BRAND_CODE, DDLModeEnumET.SELECT_ONE);
                vm.userVM_MA.branchList = branchList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserET_MA SetUserMA(UserLoginInfoET vm)
        {
            try
            {
                UserET_MA vmLogin = new UserET_MA();

                vmLogin.USER_NAME = vm.USER_NAME;
                vmLogin.PASSWORD_HASH = vm.PASSWORD_HASH;
                vmLogin.EMPLOYEE_ID = vm.EMPLOYEE_ID;
                vmLogin.FIRST_NAME_TH = vm.FIRST_NAME_TH;
                vmLogin.LAST_NAME_TH = vm.LAST_NAME_TH;
                vmLogin.FIRST_NAME_EN = vm.FIRST_NAME_EN;
                vmLogin.LAST_NAME_EN = vm.LAST_NAME_EN;
                vmLogin.EMAIL = vm.EMAIL;
                vmLogin.PHONE_NO = vm.PHONE_NO;
                vmLogin.PHONE_EXT = vm.PHONE_EXT;
                vmLogin.MOBILE_NO = vm.MOBILE_NO;
                if (vm.ACTIVE_FLAG == "True") vmLogin.ACTIVE_FLAG = "1";
                else vmLogin.ACTIVE_FLAG = "0"; vmLogin.USER_USM_TYPE = vm.USER_USM_TYPE;
                if (vm.IS_RESET_PWD == "True") vmLogin.IS_RESET_PWD = "1";
                else vmLogin.IS_RESET_PWD = "0";
                vmLogin.BRAND_NAME = vm.BRAND_NAME;
                vmLogin.BRANCH_NAME = vm.BRANCH_NAME;
                vmLogin.BRANCH_CODE = vm.LAST_NAME_TH;
                vmLogin.LOCKED_FLAG = vm.LOCKED_FLAG;
                vmLogin.ROLE_NAME = vm.ROLE_NAME;

                return vmLogin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UserVM OrderByMenu(string menu, UserVM vm)
        {
            try
            {
                if (vm.ZenPagination.currentPage > 1) { vm.userSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage; }
                else { vm.userSearchCriteriaVM.PAGE_INDEX = 1; }
                vm.userSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                vm = this.SearchOrderByMenu(menu, vm);
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private UserVM SearchOrderByMenu(string menu, UserVM vm)
        {
            try
            {
                if (menu == "USER_NAME")
                {
                    if (vm.SortingInfoList[0].SortField == "") { vm.SortingInfoList[0].SortField = "USER_NAME"; }
                    if (vm.SortingInfoList[0].SortType == "DESC") { vm.SortingInfoList[0].SortType = "ASC"; }
                    else { vm.SortingInfoList[0].SortType = "DESC"; }
                    return this.SearchUser(vm, vm.SortingInfoList[0].SortField, vm.SortingInfoList[0].SortType);
                }
                else if (menu == "FIRST_NAME_TH")
                {
                    if (vm.SortingInfoList[1].SortField == "") { vm.SortingInfoList[1].SortField = "FIRST_NAME_TH"; }
                    if (vm.SortingInfoList[1].SortType == "DESC") { vm.SortingInfoList[1].SortType = "ASC"; }
                    else { vm.SortingInfoList[1].SortType = "DESC"; }
                    return this.SearchUser(vm, vm.SortingInfoList[1].SortField, vm.SortingInfoList[1].SortType);
                }
                else if (menu == "LAST_NAME_TH")
                {
                    if (vm.SortingInfoList[2].SortField == "") { vm.SortingInfoList[2].SortField = "LAST_NAME_TH"; }
                    if (vm.SortingInfoList[2].SortType == "DESC") { vm.SortingInfoList[2].SortType = "ASC"; }
                    else { vm.SortingInfoList[2].SortType = "DESC"; }
                    return this.SearchUser(vm, vm.SortingInfoList[2].SortField, vm.SortingInfoList[2].SortType);
                }
                else if (menu == "EMAIL")
                {
                    if (vm.SortingInfoList[3].SortField == "") { vm.SortingInfoList[3].SortField = "EMAIL"; }
                    if (vm.SortingInfoList[3].SortType == "DESC") { vm.SortingInfoList[3].SortType = "ASC"; }
                    else { vm.SortingInfoList[3].SortType = "DESC"; }
                    return this.SearchUser(vm, vm.SortingInfoList[3].SortField, vm.SortingInfoList[3].SortType);
                }
                else if (menu == "ACTIVE_FLAG")
                {
                    if (vm.SortingInfoList[4].SortField == "") { vm.SortingInfoList[4].SortField = "ACTIVE_FLAG"; }
                    if (vm.SortingInfoList[4].SortType == "DESC") { vm.SortingInfoList[4].SortType = "ASC"; }
                    else { vm.SortingInfoList[4].SortType = "DESC"; }
                    return this.SearchUser(vm, vm.SortingInfoList[4].SortField, vm.SortingInfoList[4].SortType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }

        public UserVM BlockUser(UserVM vm)
        {
            try
            {
                #region ==== Validate Update ====

                vm.MessageList.Clear();
                vm = this.ValidateBlockUser(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                vm = SplitBranchCodeFromData(vm);

                int result = 0;
                UserInfoDC dc = new UserInfoDC();
                result = dc.UpdateActiveFlag(vm.userVM_MA.BRANCH_CODE, vm.SessionLogin.USER_NAME,false,vm.userVM_MA.RESON);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserVM UnblockUser(UserVM vm)
        {
            try
            {
                #region ==== Validate Update ====

                vm.MessageList.Clear();
                vm = this.ValidateBlockUser(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                vm = SplitBranchCodeFromData(vm);

                int result = 0;
                UserInfoDC dc = new UserInfoDC();
                result = dc.UpdateActiveFlag(vm.userVM_MA.BRANCH_CODE, vm.SessionLogin.USER_NAME, true, vm.userVM_MA.RESON);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserVM SplitBranchCodeFromData(UserVM vm)
        {
            try
            {
                if (vm.userVM_MA.BRANCH_CODE != null)
                {
                    vm.userVM_MA.StBranchCodeList = new List<UserLoginInfoET>();
                    string stBranchCode = vm.userVM_MA.BRANCH_CODE.Replace("\r\n", "|");
                    vm.userVM_MA.BRANCH_CODE = stBranchCode;
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserVM ValidateBlockUser(UserVM vm)
        {
            try
            {
                if (vm.userVM_MA.BRANCH_CODE == null || vm.userVM_MA.BRANCH_CODE.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "หมายเลขสาขา"));
                }
                if (vm.userVM_MA.RESON == null || vm.userVM_MA.RESON.Trim() == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "หมายเหตุ"));
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
