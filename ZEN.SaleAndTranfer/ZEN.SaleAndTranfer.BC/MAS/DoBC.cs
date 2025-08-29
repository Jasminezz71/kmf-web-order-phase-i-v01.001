using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class DoBC
    {
        #region ---- Innitial ----

        public DoVM InnitialCriteria(DoVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                //var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ALL);
                if (vm.doSearchCriteriaVM == null) { vm.doSearchCriteriaVM = new DoSearchCriteriaVM(); }
                if (vm.doSearchCriteriaVM.doList == null) { vm.doSearchCriteriaVM.doList = new List<DDLItemET>(); }
                if (vm.doVM_MA.ST_PR_CODE != null) { vm.doSearchCriteriaVM.doList = ddlDC.GetDoByPRCode(DDLModeEnumET.SELECT_ONE, vm.doVM_MA.ST_PR_CODE); }

                //vm.doSearchCriteriaVM.doCategoryList = activeFlagList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DoVM InnitialMA(DoVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                //var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ONE);
                if (vm.doVM_MA == null) { vm.doVM_MA = new DoET_MA(); }
                if (vm.doVM_MA.doList == null) { vm.doVM_MA.doList = new List<DDLItemET>(); }
                if (vm.doVM_MA.ST_PR_CODE != null) { vm.doVM_MA.doList = ddlDC.GetDoByPRCode(DDLModeEnumET.SELECT_ONE, vm.doVM_MA.ST_PR_CODE); }
                if (vm.doVM_MA.DO_STATUS_CODE == null) { vm.doVM_MA.doStatusCode = ddlDC.GetDOStatusForGR(DDLModeEnumET.SELECT_ALL); }
                if (vm.doVM_MA.DO_STATUS_CODE != null) { vm.doVM_MA.doStatusCode = ddlDC.GetDOStatusForGR(DDLModeEnumET.SELECT_ALL); }
                //if (vm.doVM_MA.DO_STATUS_CODE != 3 || vm.doVM_MA.DO_STATUS_CODE != 4)
                //{
                //    vm.doVM_MA.doStatusCode = 0;
                //}
                //vm.doVM_MA.doCategoryList = activeFlagList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private DoVM ValidateSearch(DoVM vm)
        {
            try
            {
                if (vm.doSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM != null && vm.doSearchCriteriaVM.PLAN_DELIVERY_DATE_TO != null)
                {
                    if (Convert.ToDateTime(vm.doSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM) > Convert.ToDateTime(vm.doSearchCriteriaVM.PLAN_DELIVERY_DATE_TO))
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
        private DoVM ValidateReceiveItemSave(DoVM vm)
        {
            try
            {
                if (vm.doVM_MA.ST_DO_CODE == null || vm.doVM_MA.ST_DO_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "เลขที่ใบเตรียมรับสินค้า"));
                }
                if (vm.doVM_MA.RECEIVE_DATE == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันที่ทำรับสินค้า"));
                }
                if (vm.doVM_MA.REFERANCE_NO == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "หมายเลขอ้างอิง"));
                }
                if (vm.doVM_MA.RECEIVE_BY == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อผู้รับของ"));
                }
                if (vm.doVM_MA.DO_STATUS_CODE == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สถานะ"));
                }
                foreach (var item in vm.doVM_MA.resultList)
                {
                    if (item.QTY_RECEIVE == null)
                    {
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, "จำนวนรับของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH));
                    }
                    else
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(item.QTY_RECEIVE, out n);
                        if (!isNumeric) // ไม่เป็นตัวเลข
                        {
                            item.QTY_RECEIVE = null;
                            vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00018, "จำนวนของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH, "หรือเท่ากับ 0 เท่านั้น"));
                        }
                        else // เป็นตัวเลข
                        {
                            if (Convert.ToDecimal(item.QTY_RECEIVE) < 0) // ตัวเลขที่น้อยกว่า 0
                            {
                                item.QTY_RECEIVE = null;
                                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00018, "จำนวนของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH, "หรือเท่ากับ 0 เท่านั้น"));
                            }
                            //else if (Convert.ToDecimal(item.QTY_RECEIVE) > Convert.ToDecimal(item.QTY_SEND))
                            //{
                            //    item.QTY_RECEIVE = null;
                            //    vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00075, "จำนวนรับของ " + item.ITEM_CODE + " : " + item.ITEM_NAME_TH, " จำนวนที่ส่ง"));
                            //}
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
        public DoVM SearchDo(DoVM vm, string SortField, string Sorttype)
        {
            try
            {

                #region ## Validate ##
                vm.ClearMessageList();
                vm = this.ValidateSearch(vm);
                if (!vm.MessageList.Count.Equals(0))
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                DoDC dc = new DoDC();

                int countAll = 0;
                vm.doSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.doSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.doSearchResultVM == null) { vm.doSearchResultVM = new DoSearchResultVM(); }
                if (vm.doSearchResultVM.resultList == null) { vm.doSearchResultVM.resultList = new List<ET.MAS.DoSearchResultET>(); }
                vm.doSearchResultVM.resultList = dc.SearchDo(vm.doSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.doSearchResultVM.countAll = countAll;
                if (vm.doSearchResultVM.countAll == 0)
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
        public DoVM OrderByMenu(string menu, DoVM vm)
        {
            try
            {
                if (vm.ZenPagination.currentPage > 1) { vm.doSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage; }
                else { vm.doSearchCriteriaVM.PAGE_INDEX = 1; }
                vm.doSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                vm = this.SearchOrderByMenu(menu, vm);
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DoVM SearchOrderByMenu(string menu, DoVM vm)
        {
            try
            {
                if (menu == "ST_PR_CODE")
                {
                    if (vm.SortingInfoList[0].SortField == "") { vm.SortingInfoList[0].SortField = "ST_PR_CODE"; }
                    if (vm.SortingInfoList[0].SortType == "DESC") { vm.SortingInfoList[0].SortType = "ASC"; }
                    else { vm.SortingInfoList[0].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[0].SortField, vm.SortingInfoList[0].SortType);
                }
                else if (menu == "ST_DO_CODE")
                {
                    if (vm.SortingInfoList[1].SortField == "") { vm.SortingInfoList[1].SortField = "ST_DO_CODE"; }
                    if (vm.SortingInfoList[1].SortType == "DESC") { vm.SortingInfoList[1].SortType = "ASC"; }
                    else { vm.SortingInfoList[1].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[1].SortField, vm.SortingInfoList[1].SortType);
                }
                else if (menu == "CREATE_DATE")
                {
                    if (vm.SortingInfoList[2].SortField == "") { vm.SortingInfoList[2].SortField = "CREATE_DATE"; }
                    if (vm.SortingInfoList[2].SortType == "DESC") { vm.SortingInfoList[2].SortType = "ASC"; }
                    else { vm.SortingInfoList[2].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[2].SortField, vm.SortingInfoList[2].SortType);
                }
                else if (menu == "PLAN_DELIVERY_DATE")
                {
                    if (vm.SortingInfoList[3].SortField == "") { vm.SortingInfoList[3].SortField = "PLAN_DELIVERY_DATE"; }
                    if (vm.SortingInfoList[3].SortType == "DESC") { vm.SortingInfoList[3].SortType = "ASC"; }
                    else { vm.SortingInfoList[3].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[3].SortField, vm.SortingInfoList[3].SortType);
                }
                else if (menu == "DO_STATUS_CODE")
                {
                    if (vm.SortingInfoList[4].SortField == "") { vm.SortingInfoList[4].SortField = "DO_STATUS_CODE"; }
                    if (vm.SortingInfoList[4].SortType == "DESC") { vm.SortingInfoList[4].SortType = "ASC"; }
                    else { vm.SortingInfoList[4].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[4].SortField, vm.SortingInfoList[4].SortType);
                }
                else if (menu == "DO_STATUS_NAME")
                {
                    if (vm.SortingInfoList[4].SortField == "") { vm.SortingInfoList[4].SortField = "DO_STATUS_NAME"; }
                    if (vm.SortingInfoList[4].SortType == "DESC") { vm.SortingInfoList[4].SortType = "ASC"; }
                    else { vm.SortingInfoList[4].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[4].SortField, vm.SortingInfoList[4].SortType);
                }
                else if (menu == "ST_GR_CODE")
                {
                    if (vm.SortingInfoList[4].SortField == "") { vm.SortingInfoList[4].SortField = "ST_GR_CODE"; }
                    if (vm.SortingInfoList[4].SortType == "DESC") { vm.SortingInfoList[4].SortType = "ASC"; }
                    else { vm.SortingInfoList[4].SortType = "DESC"; }
                    return this.SearchDo(vm, vm.SortingInfoList[4].SortField, vm.SortingInfoList[4].SortType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public DoVM SearchDoByID(DoVM vm)
        {
            try
            {
                DoDC dc = new DoDC();
                var complainDC = new TB_M_COMPLAIN_DC();

                if (vm.doVM_MA.ST_DO_CODE != null && vm.doVM_MA.ST_DO_CODE != "")
                {
                    // Get Header
                    DoET headerDO = dc.GetDOHeaderByDOCode(vm.doVM_MA.ST_DO_CODE);
                    // Set Header to VM
                    vm = this.SetDOHeader(vm, headerDO);

                    // Get Detail
                    vm.doVM_MA.resultList = dc.GetDODetailByDOCode(vm.doVM_MA.ST_DO_CODE);

                    // Set NoImage 
                    this.SetNoImage(vm);  //ketsara.k 20181209

                    #region == แก้จาก จำนวนส่งเป็นจำนวนรับ ==
                    //if (vm.doVM_MA.resultList != null)
                    //{
                    //    foreach (var item in vm.doVM_MA.resultList)
                    //    {
                    //        item.QTY_RECEIVE = item.QTY_SEND;
                    //    }
                    //}
                    #endregion

                    // Get Complain
                    vm.complainRETs = complainDC.GetByType("ST_GR", null);
                }
                else
                {
                    PrDC prDC = new PrDC();
                    var headerPR = prDC.GetPRHeaderByPRCode(vm.doVM_MA.ST_PR_CODE);
                    vm.doVM_MA.CREATE_DATE = headerPR.CREATE_DATE;
                    vm.doVM_MA.PLAN_DELIVERY_DATE = headerPR.PLAN_DELIVERY_DATE;
                    vm.doVM_MA.RECEIVE_DATE = null;
                    vm.doVM_MA.resultList = new List<DoET>();
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNoImage(DoVM vm)
        {
            try
            {
                if (vm != null
                    && vm.doVM_MA != null
                    && vm.doVM_MA.resultList != null
                    && vm.doVM_MA.resultList.Count > 0)
                {
                    foreach (var r in vm.doVM_MA.resultList)
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
        private DoVM SetDOHeader(DoVM vm, DoET headerDO)
        {
            try
            {
                vm.doVM_MA.ST_DO_CODE = headerDO.ST_DO_CODE;
                vm.doVM_MA.ST_PR_CODE = headerDO.ST_PR_CODE;
                vm.doVM_MA.REQUEST_DATE = headerDO.REQUEST_DATE;
                vm.doVM_MA.PLAN_DELIVERY_DATE = headerDO.PLAN_DELIVERY_DATE;
                vm.doVM_MA.DO_STATUS_CODE = headerDO.DO_STATUS_CODE;
                vm.doVM_MA.REMARK = headerDO.REMARK;
                vm.doVM_MA.DELETE_FLAG = headerDO.DELETE_FLAG;
                vm.doVM_MA.ST_DO_CODE_NAV = headerDO.ST_DO_CODE_NAV;
                vm.doVM_MA.REFERANCE_NO = headerDO.ST_DO_CODE_NAV;
                vm.doVM_MA.RECEIVE_DATE = DateTime.Now;
                vm.doVM_MA.CREATE_BY = headerDO.CREATE_BY;
                vm.doVM_MA.CREATE_DATE = headerDO.CREATE_DATE;
                vm.doVM_MA.UPDATE_BY = headerDO.UPDATE_BY;
                vm.doVM_MA.UPDATE_DATE = headerDO.UPDATE_DATE;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoVM ReceiveItemSave(DoVM vm)
        {
            try
            {
                #region === Validate ReceiveItem ===
                vm.MessageList.Clear();
                vm = this.ValidateReceiveItemSave(vm);
                vm = this.ValidateComplainBeforeSave(vm);
                if (vm.MessageList.Count() > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                string grCode = string.Empty;
                DoET dataH = this.SetDataDoH(vm);
                DoDC dc = new DoDC();

                int result = dc.ReceiveItemSave(dataH, vm.doVM_MA.resultList, out grCode);
                dataH.ST_GR_CODE = grCode;

                //Supaneej, 2018-10-19, Complain and Save ทำรับ
                var complainPETs = this.SetComplainData(vm);
                if (result == 1)
                {
                    dc.ComplainSave(dataH, vm.doVM_MA.resultList, complainPETs);
                }
                //Supaneej, 2018-10-19, Complain and Save ทำรับ

                vm.doVM_MA.ST_GR_CODE = grCode;
                if (result == 1) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DoVM ValidateComplainBeforeSave(DoVM vm)
        {
            try
            {
                if (vm.complainRETs == null)
                {
                    // case 1 | User ไม่ได้เลือกเลยซักข้อ (กรณีไม่เลือกซักข้อยังตรวจสอบเงื่อนไขไม่ถูก)
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00084));
                }

                if (vm.complainRETs != null && vm.complainRETs.Count == 0)
                {
                    // case 1 | User ไม่ได้เลือกเลยซักข้อ 
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00084));
                }

                // case 2 | User ไม่ได้เลือกข้อใดข้อหนึ่ง
                foreach (var c in vm.complainRETs)
                {
                    if (c.SELECTED_RESULT_ID == 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00085, c.COMPLAIN_DESCRIPTION));
                    }
                }

                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public DoVM ReceiveItemSave(DoVM vm)
        //{
        //    try
        //    {
        //        #region === Validate ReceiveItem ===
        //        vm.MessageList.Clear();
        //        vm = this.ValidateReceiveItemSave(vm);
        //        if (vm.MessageList.Count() > 0)
        //        {
        //            if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
        //            {
        //                return vm;
        //            }
        //        }
        //        #endregion

        //        string grCode = string.Empty;
        //        DoET dataH = this.SetDataDoH(vm);
        //        DoDC dc = new DoDC();
        //        int result = dc.ReceiveItemSave(dataH, vm.doVM_MA.resultList, out grCode);
        //        vm.doVM_MA.ST_GR_CODE = grCode;
        //        if (result == 1) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
        //        else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
        //        return vm;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //Supaneej, 2018-10-19, ST(Enhance) for Complain
        private List<USP_M_COMPLAIN_GetByType_PET> SetComplainData(DoVM vm)
        {
            List<USP_M_COMPLAIN_GetByType_PET> complainList = new List<USP_M_COMPLAIN_GetByType_PET>();
            try
            {
                USP_M_COMPLAIN_GetByType_PET dataC = new USP_M_COMPLAIN_GetByType_PET();
                for (int i = 0; i < vm.complainRETs.Count; i++)
                {
                    dataC = new USP_M_COMPLAIN_GetByType_PET();
                    dataC.COMPLAIN_ID = vm.complainRETs[i].COMPLAIN_ID;
                    dataC.RESULT_ID = vm.complainRETs[i].SELECTED_RESULT_ID;
                    dataC.CREATE_BY = vm.SessionLogin.USER_NAME;
                    complainList.Add(dataC);
                }
                return complainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Supaneej, 2018-10-25, ST(Enhance) for iMAGE DISPLAY
        private void SetNoImageDetail(DoVM vm)
        {
            try
            {
                if (vm != null
                    && vm.doVM_MA != null
                    && vm.doVM_MA.resultList != null
                    && vm.doVM_MA.resultList.Count > 0)
                {
                    foreach (var r in vm.doVM_MA.resultList)
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
        private DoET SetDataDoH(DoVM vm)
        {
            try
            {
                DoET dataH = new DoET();
                dataH.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                dataH.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                dataH.ST_DO_CODE = vm.doVM_MA.ST_DO_CODE;
                dataH.RECEIVE_BY = vm.doVM_MA.RECEIVE_BY;
                dataH.RECEIVE_DATE = vm.doVM_MA.RECEIVE_DATE;
                dataH.REFERANCE_NO = vm.doVM_MA.REFERANCE_NO;
                dataH.CREATE_BY = vm.SessionLogin.USER_NAME;
                dataH.DO_STATUS_CODE = vm.doVM_MA.DO_STATUS_CODE;
                return dataH;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SortingInfoET> SetOrderListDO()
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
    }
}
