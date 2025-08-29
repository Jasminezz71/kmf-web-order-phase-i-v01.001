using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS.Controllers
{
    public class DeliveryScheduleController : BaseController
    {
        public ActionResult Index(DeliveryScheduleVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = new DeliveryScheduleVM();
                vm.SessionLogin = GetCurrentUser;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Index", vm);
        }
        public ActionResult Search(DeliveryScheduleVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.SearchDS(vm);

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
            }
            return View("Index", vm);
        }

        public ActionResult Edit(DeliveryScheduleVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm = new DeliveryScheduleVM();
                vm.SessionLogin = GetCurrentUser;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                vm = bc.InnitialCriteria(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
            }
            return View("Edit", vm);
        }
        public ActionResult SearchEdit(DeliveryScheduleVM vm)
        {
            try
            {
                ModelState.Clear();
                InitMesssageViewBagUI();
                vm.SessionLogin = GetCurrentUser;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                vm = bc.InnitialCriteria(vm);
                vm = bc.SearchDS(vm);

            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.ToString() }));
                ModelState.Clear();
            }
            return View("Edit", vm);
        }
        public JsonResult EditSave(string brandCode,
                                    string branchCode,
                                    string zone,
                                    string scheduleType,
                                    string startTime,
                                    string endTime,
                                    bool sunFlag,
                                    bool monFlag,
                                    bool tueFlag,
                                    bool wedFlag,
                                    bool thuFlag,
                                    bool friFlag,
                                    bool satFlag,
                                    string location)
        {
            try
            {
                DeliveryScheduleET data = new DeliveryScheduleET();

                data.BRAND_CODE = brandCode;
                data.BRANCH_CODE = branchCode;
                data.ZONE = zone;
                data.SCHEDULE_TYPE = scheduleType;
                data.START_TIME = startTime;
                data.END_TIME = endTime;
                data.SUN_FLAG = sunFlag;
                data.MON_FLAG = monFlag;
                data.TUE_FLAG = tueFlag;
                data.WED_FLAG = wedFlag;
                data.THU_FLAG = thuFlag;
                data.FRI_FLAG = friFlag;
                data.SAT_FLAG = satFlag;
                data.LOCATION_CODE = location;
                data.UPDATE_BY = GetCurrentUser.USER_NAME;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                int result = bc.EditSave(data);

                if (result.Equals(1))
                {
                    return Json(new DeliveryScheduleET { SuccessFlag = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new DeliveryScheduleET { SuccessFlag = false, ErrorMessage = string.Empty }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new DeliveryScheduleET { SuccessFlag = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddSave(string brandCode,
                                    string branchCode,
                                    string zone,
                                    string scheduleType,
                                    string startTime,
                                    string endTime,
                                    bool sunFlag,
                                    bool monFlag,
                                    bool tueFlag,
                                    bool wedFlag,
                                    bool thuFlag,
                                    bool friFlag,
                                    bool satFlag,
                                    string location)
        {
            try
            {
                DeliveryScheduleET data = new DeliveryScheduleET();

                data.BRAND_CODE = brandCode;
                data.BRANCH_CODE = branchCode;
                data.ZONE = zone;
                data.SCHEDULE_TYPE = scheduleType;
                data.START_TIME = startTime;
                data.END_TIME = endTime;
                data.SUN_FLAG = sunFlag;
                data.MON_FLAG = monFlag;
                data.TUE_FLAG = tueFlag;
                data.WED_FLAG = wedFlag;
                data.THU_FLAG = thuFlag;
                data.FRI_FLAG = friFlag;
                data.SAT_FLAG = satFlag;
                data.LOCATION_CODE = location;
                data.UPDATE_BY = GetCurrentUser.USER_NAME;
                DeliveryScheduleBC bc = new DeliveryScheduleBC();
                int result = bc.AddSave(data);
                //int result = 1;

                if (result.Equals(1))
                {
                    return Json(new DeliveryScheduleET { SuccessFlag = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new DeliveryScheduleET { SuccessFlag = false, ErrorMessage = string.Empty }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new DeliveryScheduleET { SuccessFlag = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}