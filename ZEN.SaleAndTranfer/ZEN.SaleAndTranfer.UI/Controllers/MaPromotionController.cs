using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.VM2.MaPromotion;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class MaPromotionController : BaseController
    {
        public ActionResult Do()
        {
            try
            {
                var bc = new PromotionBC2();
                DoVM vm = bc.Do_OnInit();
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Do(DoVM vm)
        {
            try
            {
                vm.Pet.UpdateBy = User.Identity.Name;
                var bc = new PromotionBC2();
                bc.SavePopupPromotionItem(pet: vm.Pet);
                return Json(new JsonResultET<string>() { SuccessFlag = true, Msg = "Success", Data = null });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        }
    }
}