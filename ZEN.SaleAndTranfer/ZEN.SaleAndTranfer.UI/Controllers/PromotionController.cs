using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.ET2;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class PromotionController : BaseController
    {
        public ActionResult GetPopupPromotionItem(string mode)
        {
            try
            {
                var bc = new PromotionBC2();
                var result = bc.GetPopupPromotionItem(username: GetCurrentUser.USER_NAME);
                ViewBag.Mode = mode;
                return PartialView("_Promotion_Popup_Detail_Partial", result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult HasPopupPromotionItem()
        {
            try
            {
                var bc = new PromotionBC2();
                var result = bc.HasPopupPromotionItem();
                return Json(new JsonResultET<bool?>()
                {
                    SuccessFlag = true,
                    Msg = null,
                    Data = result != null && result.HasValue ? result.Value : false
                }
                );
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
                return Json(new JsonResultET<bool?>() { SuccessFlag = false, Msg = msg, Data = null });
            }
        }
    }
}