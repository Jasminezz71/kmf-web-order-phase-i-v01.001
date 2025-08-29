using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;
using System.IO;
using System.Collections;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.DC;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class PrBC : BaseBC
    {
        #region ---- Innitial ----

        public PrVM InnitialCriteria(PrVM vm)
        {
            try
            {
                if (vm.prSearchCriteriaVM == null) { vm.prSearchCriteriaVM = new PrSearchCriteriaVM(); }
                var ddlDC = new DDLDC();
                List<DDLItemET> prCategoryList = new List<DDLItemET>();
                if (vm.SessionLogin.BRAND_CODE != null && vm.SessionLogin.BRAND_CODE != "-- ทั้งหมด --") { prCategoryList = ddlDC.GetCategoryByBrandCode(vm.SessionLogin.BRAND_CODE); }
                else { prCategoryList = ddlDC.GetCategoryByBrandCode(null); }
                vm.prSearchCriteriaVM.prCategoryList = prCategoryList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM InnitialMA(PrVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ONE);
                var locationFC = ddlDC.GetFCLocation(DDLModeEnumET.SELECT_ONE);
                if (vm.prVM_MA == null) { vm.prVM_MA = new PrET_MA(); }
                vm.prVM_MA.prCategoryList = activeFlagList;
                vm.prVM_MA.fcLocation = locationFC;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private PrVM ValidateItemCartSave(PrVM vm)
        {
            try
            {
                if (vm.prVM_MA.PLAN_DELIVERY_DATE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันกำหนดส่งสินค้า"));
                }
                else
                {
                    if (vm.prVM_MA.PLAN_DELIVERY_DATE.Value < DateTime.Now)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018, "วันกำหนดส่งสินค้า", DateTime.Now.ToShortDateString()));
                    }
                }
                if (vm.prVM_MA.itemCartList == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สินค้าในตะกร้า"));
                }
                else
                {
                    if (vm.prVM_MA.itemCartList.Count == 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สินค้าในตะกร้า"));
                    }
                }
                if (vm.prVM_MA.REMARK == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อผู้สั่งสินค้า"));
                }

                if (vm.prVM_MA.itemCartList != null)
                {
                    if (vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == 0).Count() > 0)
                    {
                        foreach (var item in vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == 0).ToList())
                        {
                            vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018,
                                "จำนวนสินค้าของ " + item.ITEM_CODE + " : " + item.ITEM_NAME, "0"));
                        }
                    }

                    if (vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == null).Count() > 0)
                    {
                        foreach (var item in vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == null).ToList())
                        {
                            vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018,
                                "จำนวนสินค้าของ " + item.ITEM_CODE + " : " + item.ITEM_NAME, "0"));
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
        private PrVM ValidateItemCartUpdate(PrVM vm)
        {
            try
            {
                if (vm.prVM_MA.PLAN_DELIVERY_DATE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันกำหนดส่งสินค้า"));
                }
                else
                {
                    if (vm.prVM_MA.PLAN_DELIVERY_DATE.Value < DateTime.Now)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018, "วันกำหนดส่งสินค้า", DateTime.Now.ToShortDateString()));
                    }
                }
                if (vm.prVM_MA.itemCartList == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สินค้าในตะกร้า"));
                }
                else
                {
                    if (vm.prVM_MA.itemCartList.Count == 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สินค้าในตะกร้า"));
                    }
                }
                if (vm.prVM_MA.REMARK == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อผู้ติดต่อ"));
                }
                if (vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == 0).Count() > 0)
                {
                    foreach (var item in vm.prVM_MA.itemCartList.Where(m => m.ITEM_QTY == 0).ToList())
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018,
                            "จำนวนสินค้าของ " + item.ITEM_CODE + " : " + item.ITEM_NAME, "0"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM ValidateAddItemCartSave(PrVM vm, PrSearchResultET itemResult)
        {
            try
            {
                if (itemResult.ITEM_QTY == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009,
                        "จำนวนสินค้าของ " + itemResult.ITEM_CODE + " : " + itemResult.ITEM_NAME_TH));
                }
                else if (itemResult.ITEM_QTY <= 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018,
                        "จำนวนสินค้าของ " + itemResult.ITEM_CODE + " : " + itemResult.ITEM_NAME_TH, "0"));
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM ValidateAddItemCartSaveAll(PrVM vm, List<PrSearchResultET> itemResult, List<PrET> prETs)
        {
            try
            {
                if (itemResult.Count == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009,
                        "สินค้าที่ต้องการสั่ง"));
                }
                else
                {
                    #region -- validate | suntichon | เพื่อไม่ให้ใบ Stationery ปนกับ item ใบตลาดของ warehouse --
                    // step 1: check ใน item จากที่เพิ่มมาใหม่
                    if (itemResult != null && itemResult.Count > 0)
                    {
                        var countStationery = itemResult.Count(m => m.ST_PR_CATEGORY_ID == "14" || m.ST_PR_CATEGORY_ID == "15");
                        if (countStationery > 0 && countStationery < itemResult.Count)
                        {
                            vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00086));
                            return vm;
                        }
                    }

                    // step 2: check ใน item ในตะกร้า
                    if (itemResult != null && itemResult.Count > 0
                        && prETs != null && prETs.Count > 0)
                    {
                        var countStationery1 = prETs.Count(m => m.ST_PR_CATEGORY_ID == "14" || m.ST_PR_CATEGORY_ID == "15");

                        if (countStationery1 > 0) // step 2.1: ถ้าในตะกร้าเป็น stationery ล้วน
                        {
                            var countStationery2 = itemResult.Count(m => !(m.ST_PR_CATEGORY_ID == "14" || m.ST_PR_CATEGORY_ID == "15"));
                            if (countStationery2 > 0)
                            {
                                vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00087));
                                return vm;
                            }
                        }
                        else // step 2.2: ถ้าในตะกร้าเป็น item ของ warehouse ล้วน
                        {
                            var countStationery2 = itemResult.Count(m => m.ST_PR_CATEGORY_ID == "14" || m.ST_PR_CATEGORY_ID == "15");
                            if (countStationery2 > 0)
                            {
                                vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00088));
                                return vm;
                            }
                        }
                    }
                    #endregion

                    foreach (var item in itemResult)
                    {
                        if (item.ITEM_QTY == null)
                        {
                            vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009,
                                "จำนวนสินค้าของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH));
                        }
                        else if (item.ITEM_QTY <= 0)
                        {
                            vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018,
                                "จำนวนสินค้าของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH, "0"));
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
        private PrVM ValidateSearch(PrVM vm)
        {
            try
            {
                if (vm.prSearchCriteriaVM.CREATE_DATE_FROM != null && vm.prSearchCriteriaVM.CREATE_DATE_TO != null)
                {
                    if (Convert.ToDateTime(vm.prSearchCriteriaVM.CREATE_DATE_FROM) > Convert.ToDateTime(vm.prSearchCriteriaVM.CREATE_DATE_TO))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00019, "วันที่สร้างใบตลาด จาก", "วันที่สร้างใบตลาด ถึง"));
                    }
                }
                if (vm.prSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM != null && vm.prSearchCriteriaVM.PLAN_DELIVERY_DATE_TO != null)
                {
                    if (Convert.ToDateTime(vm.prSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM) > Convert.ToDateTime(vm.prSearchCriteriaVM.PLAN_DELIVERY_DATE_TO))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00019, "วันกำหนดส่งสินค้า จาก", "วันกำหนดส่งสินค้า ถึง"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrVM SearchPr(PrVM vm, string SortField, string Sorttype)
        {
            try
            {
                #region == validate search ==
                vm.MessageList.Clear();
                vm = this.ValidateSearch(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                int countAll = 0;
                PrDC dc = new PrDC();
                vm.prSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.prSearchResultVM == null) vm.prSearchResultVM = new PrSearchResultVM();
                vm.prSearchResultVM.resultList = dc.SearchPr(vm.prSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.prSearchResultVM.countAll = countAll;
                if (countAll == 0)
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM OrderByMenu(string menu, PrVM vm)
        {
            try
            {
                if (vm.ZenPagination.currentPage > 1) { vm.prSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage; }
                else { vm.prSearchCriteriaVM.PAGE_INDEX = 1; }
                vm.prSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                vm = this.SearchOrderByMenu(menu, vm);
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM SearchOrderByMenu(string menu, PrVM vm)
        {
            try
            {
                if (menu == "REQUEST_BY_BRAND_CODE")
                {
                    if (vm.SortingInfoList[0].SortField == "") { vm.SortingInfoList[0].SortField = "REQUEST_BY_BRAND_CODE"; }
                    if (vm.SortingInfoList[0].SortType == "DESC") { vm.SortingInfoList[0].SortType = "ASC"; }
                    else { vm.SortingInfoList[0].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[0].SortField, vm.SortingInfoList[0].SortType);
                }
                else if (menu == "REQUEST_BY_BRAND_NAME")
                {
                    if (vm.SortingInfoList[1].SortField == "") { vm.SortingInfoList[1].SortField = "REQUEST_BY_BRAND_NAME"; }
                    if (vm.SortingInfoList[1].SortType == "DESC") { vm.SortingInfoList[1].SortType = "ASC"; }
                    else { vm.SortingInfoList[1].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[1].SortField, vm.SortingInfoList[1].SortType);
                }
                else if (menu == "ST_PR_CODE")
                {
                    if (vm.SortingInfoList[2].SortField == "") { vm.SortingInfoList[2].SortField = "ST_PR_CODE"; }
                    if (vm.SortingInfoList[2].SortType == "DESC") { vm.SortingInfoList[2].SortType = "ASC"; }
                    else { vm.SortingInfoList[2].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[2].SortField, vm.SortingInfoList[2].SortType);
                }
                else if (menu == "PR_STATUS_NAME")
                {
                    if (vm.SortingInfoList[3].SortField == "") { vm.SortingInfoList[3].SortField = "PR_STATUS_NAME"; }
                    if (vm.SortingInfoList[3].SortType == "DESC") { vm.SortingInfoList[3].SortType = "ASC"; }
                    else { vm.SortingInfoList[3].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[3].SortField, vm.SortingInfoList[3].SortType);
                }
                else if (menu == "PLAN_DELIVERY_DATE")
                {
                    if (vm.SortingInfoList[4].SortField == "") { vm.SortingInfoList[4].SortField = "PLAN_DELIVERY_DATE"; }
                    if (vm.SortingInfoList[4].SortType == "DESC") { vm.SortingInfoList[4].SortType = "ASC"; }
                    else { vm.SortingInfoList[4].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[4].SortField, vm.SortingInfoList[4].SortType);
                }
                else if (menu == "CREATE_DATE")
                {
                    if (vm.SortingInfoList[5].SortField == "") { vm.SortingInfoList[5].SortField = "CREATE_DATE"; }
                    if (vm.SortingInfoList[5].SortType == "DESC") { vm.SortingInfoList[5].SortType = "ASC"; }
                    else { vm.SortingInfoList[5].SortType = "DESC"; }
                    return this.SearchPr(vm, vm.SortingInfoList[5].SortField, vm.SortingInfoList[5].SortType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }

        public PrVM SearchAddItemCart(PrVM vm)
        {
            try
            {
                vm.prSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.prSearchCriteriaVM.FC_LOCATION = vm.prVM_MA.FC_LOCATION;
                PrDC dc = new PrDC();
                int countAll = 0;
                if (vm.prSearchResultVM == null) { vm.prSearchResultVM = new PrSearchResultVM(); }
                if (vm.prSearchResultVM.resultList == null) { vm.prSearchResultVM.resultList = new List<PrSearchResultET>(); }
                vm.prSearchResultVM.resultList = dc.SearchAddItemCart(vm.prSearchCriteriaVM, out countAll);
                vm.prSearchResultVM.ShowItemFlg = 1;

                this.SetNoImage(vm);  ////Supaneej, 2018-10-25, FILE_PATH for display image

                vm.prSearchResultVM.countAll = countAll;
                if (vm.prSearchResultVM.countAll == 0)
                {
                    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00007));
                }

                var showFlagValue = ConfigBC.GetConfigValue(CategoryConfigEnum.UI, SubCategoryConfigEnum.PR, ConfigNameEnum.SHOW_IMAGE_FLAG);
                vm.showImageFlag = Convert.ToBoolean(showFlagValue);

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public PrVM SearchAddItemCart(PrVM vm)
        //{
        //    try
        //    {
        //        vm.prSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
        //        vm.prSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
        //        PrDC dc = new PrDC();
        //        int countAll = 0;
        //        if (vm.prSearchResultVM == null) { vm.prSearchResultVM = new PrSearchResultVM(); }
        //        if (vm.prSearchResultVM.resultList == null) { vm.prSearchResultVM.resultList = new List<PrSearchResultET>(); }
        //        vm.prSearchResultVM.resultList = dc.SearchAddItemCart(vm.prSearchCriteriaVM, out countAll);
        //        vm.prSearchResultVM.countAll = countAll;
        //        if (vm.prSearchResultVM.countAll == 0)
        //        {
        //            vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00007));
        //        }
        //        return vm;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void SetNoImage(PrVM vm)
        {
            try
            {
                if (vm != null
                    && vm.prSearchResultVM != null
                    && vm.prSearchResultVM.resultList != null
                    && vm.prSearchResultVM.resultList.Count > 0)
                {
                    foreach (var r in vm.prSearchResultVM.resultList)
                    {
                        if (!File.Exists(r.FULL_PATH))
                        {
                            r.FILE_PATH = r.FILE_PATH.Replace(r.FILE_NAME_WITH_EXTENSION, "NOIMG.png");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public PrVM CheckSessionHasBranch(PrVM vm)
        {
            try
            {
                vm.MessageList.Clear();
                if (vm.SessionLogin.BRANCH_CODE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM ItemCartSave(PrVM vm)
        {
            try
            {
                #region ==== Validate Item Cart Save ====
                vm.MessageList.Clear();
                vm = this.ValidateItemCartSave(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                PrDC dc = new PrDC();
                //vm.prVM_MA.ST_PR_CODE = dc.GetPRCode(vm.SessionLogin.BRANCH_CODE);
                vm.prVM_MA.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prVM_MA.REQUEST_BY_BRAND_NAME = vm.SessionLogin.BRAND_NAME;
                vm.prVM_MA.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.prVM_MA.REQUEST_BY_BRANCH_NAME = vm.SessionLogin.BRANCH_NAME;
                vm.prVM_MA.CREATE_BY = vm.SessionLogin.USER_NAME;

                var result = dc.ItemCartSave(vm.prVM_MA, vm.prVM_MA.itemCartList);
                if (result == 1) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM ItemCartUpdate(PrVM vm)
        {
            try
            {
                #region ==== Validate Item Cart Save ====
                vm.MessageList.Clear();
                vm = this.ValidateItemCartUpdate(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                PrDC dc = new PrDC();
                //vm.prVM_MA.ST_PR_CODE = dc.GetPRCode(vm.SessionLogin.BRANCH_CODE);
                vm.prVM_MA.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prVM_MA.REQUEST_BY_BRAND_NAME = vm.SessionLogin.BRAND_NAME;
                vm.prVM_MA.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                vm.prVM_MA.REQUEST_BY_BRANCH_NAME = vm.SessionLogin.BRANCH_NAME;
                vm.prVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;

                var result = dc.ItemCartUpdate(vm.prVM_MA, vm.prVM_MA.itemCartList);
                if (result == 1) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM AddItemCartSave(PrVM vm, PrSearchResultET itemResult)
        {
            try
            {
                #region ==== Validate Add Item Cart Save ====
                vm.MessageList.Clear();
                vm = this.ValidateAddItemCartSave(vm, itemResult);
                #endregion
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM AddItemCartSaveAll(PrVM vm, List<PrSearchResultET> itemResult, List<PrET> prETs)
        {
            try
            {
                #region ==== Validate Add Item Cart Save ====
                vm.MessageList.Clear();
                vm = this.ValidateAddItemCartSaveAll(vm, itemResult, prETs);
                #endregion
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        int cntUpdate = 0;
                        int cntItemResult = itemResult.Count();
                        foreach (var item in vm.prSearchResultVM.resultList) // set item_qty for current item result
                        {
                            if (cntUpdate == cntItemResult)
                            {
                                break;
                            }
                            else
                            {
                                foreach (var item2 in itemResult)
                                {
                                    if (item.ST_MAP_WH_ITEM_ID == item2.ST_MAP_WH_ITEM_ID)
                                    {
                                        item.ITEM_QTY = item2.ITEM_QTY;
                                        cntUpdate++;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else // Clear input item_qty
                {
                    foreach (var item in vm.prSearchResultVM.resultList) // set item_qty for current item result
                    {
                        item.ITEM_QTY = null;
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM PrViewDetail(PrVM vm)
        {
            try
            {
                PrDC dc = new PrDC();
                // Get Header
                PrET headerPR = dc.GetPRHeaderByPRCode(vm.prVM_MA.ST_PR_CODE);
                // Set Header to VM
                vm = this.SetPRHeader(vm, headerPR);

                // Get Detail
                var priceET = new USP_R_ST_PR_D_GetByPRCode_200_RET();
                vm.prVM_MA.itemCartList = dc.GetPRDetailByPRCode(vm.prVM_MA.ST_PR_CODE, out priceET);
                vm.prVM_MA.itemPrice = priceET;
                this.SetNoImage2(vm);  //Supaneej, 2019-01-18, FILE_PATH for display no image

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNoImage2(PrVM vm)
        {
            try
            {
                if (vm != null
                    && vm.prVM_MA != null
                    && vm.prVM_MA.itemCartList != null
                    && vm.prVM_MA.itemCartList.Count > 0)
                {
                    foreach (var r in vm.prVM_MA.itemCartList)
                    {
                        if (!File.Exists(r.FULL_PATH))
                        {
                            r.FILE_PATH = r.FILE_PATH.Replace(r.FILE_NAME_WITH_EXTENSION, "NOIMG.png");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public PrVM PrDelete(PrVM vm)
        {
            try
            {
                vm.MessageList.Clear();

                int result = 0;
                vm.prVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.prVM_MA.UPDATE_DATE = DateTime.Now;

                PrDC dc = new PrDC();
                result = dc.PrDelete(vm.prVM_MA);
                if (result == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));

                int countAll = 0;
                vm.prSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.prSearchResultVM == null) vm.prSearchResultVM = new PrSearchResultVM();
                vm.prSearchResultVM.resultList = dc.SearchPr(vm.prSearchCriteriaVM, null, null, out countAll);
                vm.prSearchResultVM.countAll = countAll;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrVM SetPRHeader(PrVM vm, PrET headerPR)
        {
            try
            {
                vm.prVM_MA.PLAN_DELIVERY_DATE = headerPR.PLAN_DELIVERY_DATE;
                vm.prVM_MA.PR_STATUS_CODE = headerPR.PR_STATUS_CODE;
                vm.prVM_MA.REMARK = headerPR.REMARK;
                vm.prVM_MA.DELETE_FLAG = headerPR.DELETE_FLAG;
                vm.prVM_MA.CREATE_BY = headerPR.CREATE_BY;
                vm.prVM_MA.CREATE_DATE = headerPR.CREATE_DATE;
                vm.prVM_MA.UPDATE_BY = headerPR.UPDATE_BY;
                vm.prVM_MA.UPDATE_DATE = headerPR.UPDATE_DATE;
                vm.prVM_MA.FC_LOCATION = headerPR.REQUEST_TO_BRANCH_CODE;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SortingInfoET> SetOrderListPR()
        {
            List<SortingInfoET> orderList = new List<SortingInfoET>();
            try
            {
                SortingInfoET result = new SortingInfoET();
                for (int i = 0; i < 10; i++)
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
        public PrET GetItemByItemCode(string itemCode, string brandCode, string branchCode)
        {
            try
            {
                PrDC dc = new PrDC();
                PrET itemET = new PrET();
                PrSearchCriteriaET searchET = new PrSearchCriteriaET();
                searchET.ITEM_CODE = itemCode;
                searchET.REQUEST_BY_BRAND_CODE = brandCode;
                searchET.REQUEST_BY_BRANCH_CODE = branchCode;
                int countAll = 0;
                var result = dc.SearchAddItemCart(searchET, out countAll);
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            itemET.ITEM_CODE = item.ITEM_CODE;
                            itemET.ITEM_DETAIL = item.ITEM_DETAIL;
                            itemET.ITEM_NAME = item.ITEM_NAME_TH;
                            itemET.ITEM_QTY = item.ITEM_QTY;
                            itemET.ITEM_UNIT = item.REQUEST_UOM_CODE;
                            itemET.ST_MAP_WH_ITEM_ID = item.ST_MAP_WH_ITEM_ID;
                            itemET.REQUEST_TO_BRAND_CODE = item.REQUEST_TO_BRAND_CODE;
                            itemET.REQUEST_TO_BRAND_NAME = item.REQUEST_TO_BRAND_NAME;
                            itemET.REQUEST_TO_BRANCH_CODE = item.REQUEST_TO_BRANCH_CODE;
                            itemET.REQUEST_TO_BRANCH_NAME = item.REQUEST_TO_BRANCH_NAME;
                            itemET.REQUEST_BY_BRAND_CODE = item.REQUEST_BY_BRAND_CODE;
                            itemET.REQUEST_BY_BRAND_NAME = item.REQUEST_BY_BRAND_NAME;
                            itemET.REQUEST_BY_BRANCH_CODE = item.REQUEST_BY_BRANCH_CODE;
                            itemET.REQUEST_BY_BRANCH_NAME = item.REQUEST_BY_BRANCH_NAME;
                            itemET.ST_PR_CATEGORY_ID = item.ST_PR_CATEGORY_ID;
                            itemET.ST_PR_CATEGORY_NAME = item.ST_PR_CATEGORY_NAME;
                            break;
                        }
                    }
                }
                return itemET;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrVM setItemFromCart(PrVM vm, List<PrET> itemFromCart)
        {
            try
            {
                if (itemFromCart != null)
                {
                    foreach (var item in itemFromCart.ToList())
                    {
                        //vm.prSearchResultVM.resultList.Where(m => m.ITEM_CODE == item.ITEM_CODE).ToList();
                        vm.prSearchResultVM.resultList.Where(w => w.ITEM_CODE == item.ITEM_CODE).ToList().ForEach(s => s.ITEM_QTY = item.ITEM_QTY);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }

        public string GetAlertAvgPRQtyMsg(USP_R_ST_PR_GetAlertAvgPRQty__PET pet)
        {
            try
            {
                var needAlertFlag = false;
                var dc = new TB_R_ST_PR_D_DC();
                var msg = dc.GetAlertAvgPRQty(pet, out needAlertFlag);

                if (needAlertFlag)
                {
                    return msg.MessageDispText;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CheckPRDupplicate(string brandCode, string branchCode, DateTime? planDeliveryDate, string fcLocation)
        {
            try
            {
                string result = string.Empty;

                PrDC dc = new PrDC();
                result = dc.CheckPRDupplicate(brandCode, branchCode, planDeliveryDate, fcLocation);

                

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrVM PrBackToNew(PrVM vm)
        {
            try
            {
                vm.MessageList.Clear();

                int result = 0;
                vm.prVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                //vm.prVM_MA.UPDATE_DATE = DateTime.Now;

                PrDC dc = new PrDC();
                result = dc.PrBackToNew(vm.prVM_MA);
                if (result == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                   
                
                int countAll = 0;
                vm.prSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.prSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.prSearchResultVM == null) vm.prSearchResultVM = new PrSearchResultVM();
                vm.prSearchResultVM.resultList = dc.SearchPr(vm.prSearchCriteriaVM, null, null, out countAll);
                vm.prSearchResultVM.countAll = countAll;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
