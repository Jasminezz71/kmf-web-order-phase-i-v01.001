using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.VM2.Usr;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class UsrController : BaseController
    {
        public ActionResult Search(string username)
        {
            try
            {
                var bc = new UserBC2();
                var vm = bc.Search_OnInit(username: username);
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Search(SearchVM2 vm)
        {
            try
            {
                var bc = new UserBC2();
                vm = bc.Search(vm: vm);
                return PartialView("_Usr_SearchResult_Partial", vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SearchCus(SearchVM2 vm)
        {
            try
            {
                var bc = new UserBC2();
                vm = bc.SearchCus(vm: vm);
                return PartialView("_Usr_CustomerResult_Partial", vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Insert, Update, Delete
        /// </summary>
        /// <param name="id">id = username</param>
        /// <returns>Do View</returns>
        public ActionResult Do(string un)
        {
            try
            {
                var bc = new UserBC2();
                var vm = bc.Do_OnInit(username: un);
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Do(DoVM2 vm)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = "Validate Fail.", Data = null });
                //}

                vm.Pet.SaveBy = GetCurrentUser.USER_NAME;
                var bc = new UserBC2();
                bc.Save(vm: vm);

                return Json(new JsonResultET<UserET2>() { SuccessFlag = vm.SaveResult.SuccessFlag.Value, Msg = vm.SaveResult.Msg, Data = vm.Pet });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<UserET2>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                var vm = new DoVM2();
                vm.Pet = new UserET2();
                vm.Pet.Username = id;
                vm.Pet.Mode = "Delete";
                vm.Pet.SaveBy = GetCurrentUser.USER_NAME;
                var bc = new UserBC2();
                bc.Save(vm: vm);

                return Json(new JsonResultET<string>() { SuccessFlag = vm.SaveResult.SuccessFlag.Value, Msg = vm.SaveResult.Msg, Data = vm.Pet.Username });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ResetPwd(ResetPwdVM2 vm)
        {
            try
            {
                vm.Pet.UpdateBy = GetCurrentUser.USER_NAME;
                var bc = new UserBC2();
                bool result = bc.ResetPwd(vm: vm);

                return Json(new JsonResultET<string>() { SuccessFlag = result, Msg = null, Data = vm.Pet.Password });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        } 

        public ActionResult ChangePwd()
        {
            try
            {               
                var vm = new ChangePwdVM2();
                vm.Pet = new ChangePwdET() { Username = GetCurrentUser.USER_NAME };
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePwd(ChangePwdVM2 vm)
        {
            try
            {
                vm.Pet.ChanageBy = GetCurrentUser.USER_NAME;
                var bc = new UserBC2();
                vm = bc.ChangePwd(vm: vm);

                return Json(new JsonResultET<string>() { SuccessFlag = vm.SuccessFlag, Msg = vm.Msg, Data = GetCurrentUser.ROLE_NAME });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        }
    }
}