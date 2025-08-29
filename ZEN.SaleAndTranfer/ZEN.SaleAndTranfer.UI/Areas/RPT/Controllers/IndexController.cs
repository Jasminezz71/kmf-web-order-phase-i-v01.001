using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;

namespace ZEN.SaleAndTranfer.UI.Areas.RPT.Controllers
{
    public class IndexController : BaseController
    {
        //
        // GET: /RPT/Index/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RPT_ST_REPORT(string id)
        {
            try
            {
                InitMesssageViewBagUI();
                ViewBag.ST_PARAMETER_CODE = id;
                return View();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                //throw ex;
                return View();
            }
        }
	}
}