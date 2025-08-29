using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.DC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Util;
using ZEN.SaleAndTranfer.UI.VM2;
using ZEN.SaleAndTranfer.UI.VM2.Prq;

namespace ZEN.SaleAndTranfer.UI.Controllers
{
	public class PrqController : BaseController
	{
		public ActionResult SearchItem(int? id, string itemCode, string username)
		{
			try
			{
				if (username != null)
				{
					//GetCurrentUser.USER_NAME = username;
					//GetCurrentUser.BRANCH_CODE = username;
					// Edit Add Firstname by M 10/04/2025

					int start = username.IndexOf("-");
					string customer_code = username.Substring(0, start);
					int lentext2 = username.Length - (start + 1);
					string customer_name = username.Substring(start + 1, lentext2);
					if (username.Contains("CDUMMY-TRADE"))
					{
						GetCurrentUser.USER_NAME = "CDUMMY-TRADE";
						GetCurrentUser.BRANCH_CODE = customer_code;
					}
					else
					{
						GetCurrentUser.USER_NAME = customer_code;
						GetCurrentUser.BRANCH_CODE = customer_code;
					}
					
					
					GetCurrentUser.FIRST_NAME_TH = customer_name;
					//
					GetCurrentUser.BRAND_CODE = "KMF";
					GetCurrentUser.ROLE_NAME = "saleadminc";
				}
				var bc = new PrqBC2();
				var vm = bc.SearchItem_OnInit(prCategoryID: id, itemCode: itemCode, userInfo: GetCurrentUser);
				return View(vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult SearchItem(SearchItemVM2 vm)
		{
			try
			{
				//Jasmine : function fearch 
				//if (string.IsNullOrEmpty(vm.Pet.ItemCode.ToString().Trim()) && string.IsNullOrEmpty(vm.Pet.ItemName.ToString().Trim()))
				//{
				//	vm.Pet.CurrentPageId = 1;
				//}

				vm.Pet.Username = GetCurrentUser.USER_NAME;
				vm.Pet.BranchCode = GetCurrentUser.BRANCH_CODE;
				vm.Pet.BrancnCode = GetCurrentUser.BRAND_CODE;
				//vm.Pet.PageSize = 20;
				var bc = new PrqBC2();
				vm = bc.SearchItem(vm: vm);
				vm = BindOrderInBasket(vm: vm);
				return PartialView("_Prq_SearchItemResult_Partial", vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		private SearchItemVM2 BindOrderInBasket(SearchItemVM2 vm)
		{
			try
			{
				if (vm.SearchResult == null)
				{
					return vm;
				}

				if (vm.SearchResult.Count < 1)
				{
					return vm;
				}

				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}

				if (orders.Count > 0)
				{
					foreach (var order in orders)
					{
						var ret = vm.SearchResult.FirstOrDefault(m => m.ItemCode == order.ItemCode);
						if (ret != null)
						{
							ret.OrderQty = order.OrderQty;
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

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult AddItemToBasket(BasketItemET et)
		{
			//#Jasmine : Add1
			if (et == null)
			{
				return Json(new JsonResultET<string> { SuccessFlag = false, Msg = "ไม่พบสินค้าในตะกร้า !" });
			}

			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}


				//#Jasmine : var order = orders.FirstOrDefault(m => m.StMapWhItemID == et.StMapWhItemID);
				var order = orders.FirstOrDefault(m => m.ItemCode == et.ItemCode);
				if (order == null)
				{
					//#Jasmine : ยังไม่มีสินค้าที่ item code ซ้ำกันในตะกร้า
					orders.Add(et);
				}
				else
				{
					//var unitOrder = orders.FirstOrDefault(m => m.RequestUomCode == et.RequestUomCode && m.StMapWhItemID == et.StMapWhItemID);
					var unitOrder = orders.FirstOrDefault(m => m.RequestUomCode == et.RequestUomCode && m.ItemCode == et.ItemCode);
					if (unitOrder == null)
					{
						//#Jasmine : ยังไม่มีสินค้า ที่หน่วยตรงกัน เลยต้องเพิ่มใหม่
						orders.Add(et);
					}
					else
					{
						order.OrderQty = et.OrderQty;
					}
				}
				//#Jasmine : Add2
				Session[SessionNameConst.ItemInBasket] = orders;
				return Json(new JsonResultET<int>() { SuccessFlag = true, Msg = "Success", Data = orders.Count });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg });
			}
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult SaveOrderOnBasket(BasketItemET et)
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}


				//#Jasmine : var order = orders.FirstOrDefault(m => m.StMapWhItemID == et.StMapWhItemID);
				var order = orders.FirstOrDefault(m => m.ItemCode == et.ItemCode && m.RequestUomCode == et.RequestUomCode);
				if (order == null)
				{
					orders.Add(et);
				}
				else
				{
					order.OrderQty = et.OrderQty;
					order.Remark = et.Remark;
				}

				return Json(new JsonResultET<int>() { SuccessFlag = true, Msg = "Success", Data = orders.Count });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg });
			}
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult DeleteOrderOnBasket(BasketItemET et)
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}


				//#Jasmine : var order = orders.FirstOrDefault(m => m.StMapWhItemID == et.StMapWhItemID);
				var order = orders.FirstOrDefault(m => m.ItemCode == et.ItemCode && m.RequestUomCode == et.RequestUomCode);

				orders.Remove(order);

				return Json(new JsonResultET<int>() { SuccessFlag = true, Msg = "Success", Data = orders.Count });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("Error on DeleteOrder On Basket | {0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<string>() { SuccessFlag = false, Msg = msg });
			}
		}

		[HttpPost]
		public ActionResult GetOrderQtyInBasket(int? id)  
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}

				decimal? data = null;
				var order = orders.FirstOrDefault(m => m.StMapWhItemID == id);   
				if (order == null)
				{
					data = null;
				}
				else
				{
					data = order.OrderQty;
				}


				return Json(new JsonResultET<decimal?>() { SuccessFlag = true, Msg = "Success", Data = data });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<decimal?>() { SuccessFlag = false, Msg = msg, Data = null });
			}
		}

		[HttpPost]
		public ActionResult GetItemCountInBasket()
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}
				else
				{
					orders = new List<BasketItemET>();
					Session[SessionNameConst.ItemInBasket] = orders;
				}

				return Json(new JsonResultET<int>() { SuccessFlag = true, Msg = "Success", Data = orders.Count });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<int?>() { SuccessFlag = false, Msg = msg, Data = null });
			}
		}

		[HttpPost]
		public ActionResult Basket()
		{
			try
			{
				var vm = GetBasketData();
				return PartialView("_Basket_Partial", vm);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private BasketVM GetBasketData()
		{
			try
			{
				var vm = new BasketVM();

				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}

				vm.Detail = orders;

				return vm;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpGet]
		public ActionResult Summary()
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}

				VM2.Usr.SearchVM2 sm = new VM2.Usr.SearchVM2();
				var dc = new UserDC2();
				USP_M_USM_USER__Search_V2__Pet pt = new USP_M_USM_USER__Search_V2__Pet();
				pt.Username = GetCurrentUser.USER_NAME;
				sm.Pet = pt;
				//sm.Pet.Username = GetCurrentUser.USER_NAME;
				sm.Result = dc.Search(pet: sm.Pet);

				var vm = new SummaryVM2();

				vm.Detail = orders;
				vm.Het = new PrqHET();
				vm.Het.Address = sm.Result[0].ADDRESS;
				vm.Het.Address2 = sm.Result[0].ADDRESS_2;
				vm.Het.Address3 = sm.Result[0].ADDRESS_3;
				vm.Het.City = sm.Result[0].CITY;
				vm.Het.CustDummy = sm.Result[0].CUST_DUMMY;
				vm.Het.ConfigValue = sm.Result[0].CONFIG_VALUE;
				vm.Het.isSaleAdmin = GetCurrentUser.IS_SALE_ADMIN;
				vm.Het.SaleAdminCode = GetCurrentUser.SALE_ADMIN_CODE;


				return View(vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpPost]
		public ActionResult CreatePr(PrqHET et)
		{
			try
			{
				List<BasketItemET> orders = null;
				if (Session[SessionNameConst.ItemInBasket] is List<BasketItemET>)
				{
					orders = (List<BasketItemET>)Session[SessionNameConst.ItemInBasket];
				}

				et.CreateBy = GetCurrentUser.USER_NAME;
				et.OrderBy = et.OrderBy;
				et.BrandCode = GetCurrentUser.BRAND_CODE;
				et.BranchCode = GetCurrentUser.BRANCH_CODE;
				var bc = new PrqBC2();
				et = bc.CreatePr(et: et, orders: orders);

				//# Jasmine : change to remove session 
				//Session[SessionNameConst.ItemInBasket] = null;  
				Session.Remove(SessionNameConst.ItemInBasket);

				return Json(new JsonResultET<string>() { SuccessFlag = true, Msg = "Success", Data = et.PrCode });
			}
			catch (Exception ex)
			{
				var msg = ex.InnerException == null ? ex.Message : string.Format("{0} | {1}", ex.Message, ex.InnerException.Message);
				return Json(new JsonResultET<int?>() { SuccessFlag = false, Msg = msg, Data = null });
			}
		}

		[HttpPost]
		public ActionResult FindPR(string deliveryDate)
		{
			try
			{
				bool havePr = false;
				SearchVM2 vm = new SearchVM2();

				USP_R_ST_PR_Search_V2__Pet pet = new USP_R_ST_PR_Search_V2__Pet();


				pet.Username = GetCurrentUser.USER_NAME;
				pet.PlanDeliveryDateFrom = Convert.ToDateTime(deliveryDate);
				pet.PlanDeliveryDateTo = Convert.ToDateTime(deliveryDate);
				vm.Pet = pet;

				var bc = new PrqBC2();
				vm = bc.Search(vm: vm);
				if (vm.SearchCountAll > 0)
				{
					havePr = true;
				}
				return Json(new JsonResultET<string>() { SuccessFlag = havePr });
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public ActionResult Search(string id)
		{
			try
			{
				var bc = new PrqBC2();
				if (id != null)
				{
					bool result = bc.CanclePr(prCode: id, updateby: GetCurrentUser.USER_NAME);
				}

				SearchVM2 vm = bc.Search_OnInit();
				return View(vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public ActionResult SearchCus(string username)
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

		public ActionResult getCus(string username)
		{
			try
			{
				GetCurrentUser.USER_NAME = username;
				GetCurrentUser.IS_SALE_ADMIN = "Y";
				GetCurrentUser.SALE_ADMIN_CODE = username;


				return View(new IndexVM());
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
				var bc = new PrqBC2();
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
		public ActionResult Search(SearchVM2 vm)
		{
			try
			{
				vm.Pet.Username = GetCurrentUser.USER_NAME;
				vm.Pet.isSaleAdmin = GetCurrentUser.IS_SALE_ADMIN;
				vm.Pet.saleAdminCode = GetCurrentUser.SALE_ADMIN_CODE;

				var bc = new PrqBC2();
				vm = bc.Search(vm: vm);
				return PartialView("_Prq_SearchResult_Partial", vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public ActionResult Index()
		{
			try
			{
				return View(new IndexVM());
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public ActionResult Detail(string id)
		{
			try
			{
				var bc = new PrqBC2();
				var vm = bc.Detail_OnInit(prCode: id);
				return View(vm);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

	}
}