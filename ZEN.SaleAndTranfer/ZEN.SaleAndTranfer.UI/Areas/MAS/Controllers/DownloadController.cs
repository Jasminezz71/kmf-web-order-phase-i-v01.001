using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class DownloadController : BaseController
    {
        //
        // GET: /MAS/Download/
        public ActionResult Index(DownloadVM vm)
        {
            try
            {
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DownloadBC bc = new DownloadBC();
                vm = bc.Search(vm);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View("Index", vm);
            }
            return View("Index", vm);
        }
	}
}