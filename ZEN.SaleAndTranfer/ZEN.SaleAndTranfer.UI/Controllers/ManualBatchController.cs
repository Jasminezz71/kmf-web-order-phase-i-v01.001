using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.VM2.ManualBatch;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
    public class ManualBatchController : BaseController
    {

        private readonly IManualBatchService _manualBatchService;
		public ManualBatchController(IManualBatchService svc)
		{
			_manualBatchService = svc;
		}


		public ActionResult Run()
        {
            try
            {
                var vm = new RunVM2();
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Run(RunVM2 vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonResultET<string>
                {
                    SuccessFlag = false,
                    Msg = "กรุณากรอกเลขที่ใบสั่งซื้อ",
                    Data = null
                });
            }

			try
			{
				vm.RunBy = GetCurrentUser.SALE_ADMIN_CODE;
				vm.RunType = "MANUAL";
				//var bc = new ManualBatchBC2();
				//vm = bc.RunInterfaceSTPRToNAV(vm: vm);
				vm = _manualBatchService.RunInterfaceSTPRToNAV(vm);

                vm.PRNo = null;
                return Json(new JsonResultET<string>
                {
                    SuccessFlag = vm.RunBatchResult.SuccessFlag,
                    Msg = vm.RunBatchResult.Msg,
                    Data = null
                });

			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg, Data = null });
			}
		}

    }
}