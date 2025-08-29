using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;
using System.Collections;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class GrBC
    {
        #region ---- Innitial ----

        public GrVM InnitialCriteria(GrVM vm)
        {
            try
            {
                //var ddlDC = new DDLDC();
                //var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ALL);
                if (vm.grSearchCriteriaVM == null) { vm.grSearchCriteriaVM = new GrSearchCriteriaVM(); }
                //vm.grSearchCriteriaVM.grCategoryList = activeFlagList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GrVM InnitialMA(GrVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                //var activeFlagList = ddlDC.GetStatus(DDLModeEnumET.SELECT_ONE);
                if (vm.grVM_MA == null) { vm.grVM_MA = new GrET_MA(); }

                if (vm.grVM_MA.DO_STATUS_CODE != null) { vm.grVM_MA.doStatusCode = ddlDC.GetDOStatusForGR(DDLModeEnumET.SELECT_ALL); }
                //vm.grVM_MA.grCategoryList = activeFlagList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private GrVM ValidateSearch(GrVM vm)
        {
            try
            {
                if (vm.grSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM.HasValue && vm.grSearchCriteriaVM.PLAN_DELIVERY_DATE_TO.HasValue)
                {
                    if (Convert.ToDateTime(vm.grSearchCriteriaVM.PLAN_DELIVERY_DATE_FROM) > Convert.ToDateTime(vm.grSearchCriteriaVM.PLAN_DELIVERY_DATE_TO))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018, "วันจัดส่งสินค้าจาก", "วันจัดส่งสินค้าถึง"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private GrVM ValidateUpdate(GrVM vm)
        {
            try
            {
                if (!vm.grVM_MA.RECEIVE_DATE.HasValue)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันที่ทำรับสินค้า"));
                }
                if (vm.grVM_MA.REFERANCE_NO == null || vm.grVM_MA.REFERANCE_NO.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "หมายเลขอ้างอิง"));
                }
                if (vm.grVM_MA.RECEIVE_BY == null || vm.grVM_MA.RECEIVE_BY.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ชื่อผู้รับของ"));
                }
                if (vm.grVM_MA.DO_STATUS_CODE == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สถานะ"));
                }
                foreach (var item in vm.grVM_MA.resultList)
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
        public GrVM SearchGr(GrVM vm, string SortField, string Sorttype)
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

                GrDC dc = new GrDC();
                int countAll = 0;
                vm.grSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.grSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.grSearchResultVM == null) { vm.grSearchResultVM = new GrSearchResultVM(); }
                if (vm.grSearchResultVM.resultList == null) { vm.grSearchResultVM.resultList = new List<ET.MAS.GrSearchResultET>(); }
                vm.grSearchResultVM.resultList = dc.SearchGr(vm.grSearchCriteriaVM, SortField, Sorttype, out countAll);
                vm.grSearchResultVM.countAll = countAll;
                if (vm.grSearchResultVM.countAll == 0)
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
        public GrVM OrderByMenu(string menu, GrVM vm)
        {
            try
            {
                if (vm.ZenPagination.currentPage > 1) { vm.grSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage; }
                else { vm.grSearchCriteriaVM.PAGE_INDEX = 1; }
                vm.grSearchCriteriaVM.ROW_PER_PAGE = vm.ZenPagination.rowPerPage;
                vm = this.SearchOrderByMenu(menu, vm);
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private GrVM SearchOrderByMenu(string menu, GrVM vm)
        {
            try
            {
                if (menu == "ST_PR_CODE")
                {
                    if (vm.SortingInfoList[0].SortField == "") { vm.SortingInfoList[0].SortField = "ST_PR_CODE"; }
                    if (vm.SortingInfoList[0].SortType == "DESC") { vm.SortingInfoList[0].SortType = "ASC"; }
                    else { vm.SortingInfoList[0].SortType = "DESC"; }
                    return this.SearchGr(vm, vm.SortingInfoList[0].SortField, vm.SortingInfoList[0].SortType);
                }
                else if (menu == "ST_DO_CODE")
                {
                    if (vm.SortingInfoList[1].SortField == "") { vm.SortingInfoList[1].SortField = "ST_DO_CODE"; }
                    if (vm.SortingInfoList[1].SortType == "DESC") { vm.SortingInfoList[1].SortType = "ASC"; }
                    else { vm.SortingInfoList[1].SortType = "DESC"; }
                    return this.SearchGr(vm, vm.SortingInfoList[1].SortField, vm.SortingInfoList[1].SortType);
                }
                else if (menu == "ST_GR_CODE")
                {
                    if (vm.SortingInfoList[2].SortField == "") { vm.SortingInfoList[2].SortField = "ST_GR_CODE"; }
                    if (vm.SortingInfoList[2].SortType == "DESC") { vm.SortingInfoList[2].SortType = "ASC"; }
                    else { vm.SortingInfoList[2].SortType = "DESC"; }
                    return this.SearchGr(vm, vm.SortingInfoList[2].SortField, vm.SortingInfoList[2].SortType);
                }
                else if (menu == "PLAN_DELIVERY_DATE")
                {
                    if (vm.SortingInfoList[3].SortField == "") { vm.SortingInfoList[3].SortField = "PLAN_DELIVERY_DATE"; }
                    if (vm.SortingInfoList[3].SortType == "DESC") { vm.SortingInfoList[3].SortType = "ASC"; }
                    else { vm.SortingInfoList[3].SortType = "DESC"; }
                    return this.SearchGr(vm, vm.SortingInfoList[3].SortField, vm.SortingInfoList[3].SortType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public GrVM SearchGrByID(GrVM vm)
        {
            try
            {
                GrDC dc = new GrDC();
                var complainDC = new TB_M_COMPLAIN_DC();

                // Get Header
                GrET headerGR = dc.GetGRHeaderByGRCode(vm.grVM_MA.ST_GR_CODE);
                // Set Header to VM
                vm = this.SetGRHeader(vm, headerGR);

                // Get Detail
                vm.grVM_MA.resultList = dc.GetGRDetailByGRCode(vm.grVM_MA.ST_GR_CODE);
                this.SetNoImage2(vm);  //Supaneej, 2019-01-18, FILE_PATH for display no image

                // Get Complain
                vm.complainRETs = complainDC.GetByType("ST_GR", vm.grVM_MA.ST_GR_CODE);

                //var ddlDC = new DDLDC();
                //if (vm.grVM_MA.DO_STATUS_CODE != null) { vm.grVM_MA.doStatusCode = ddlDC.GetDOStatusForGR(DDLModeEnumET.SELECT_ALL); }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNoImage2(GrVM vm)
        {
            try
            {
                if (vm != null
                    && vm.grVM_MA != null
                    && vm.grVM_MA.resultList != null
                    && vm.grVM_MA.resultList.Count > 0)
                {
                    foreach (var r in vm.grVM_MA.resultList)
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

        private GrVM SetGRHeader(GrVM vm, GrET headerGR)
        {
            try
            {
                vm.grVM_MA.ST_PR_CODE = headerGR.ST_PR_CODE;
                vm.grVM_MA.ST_GR_CODE = headerGR.ST_GR_CODE;
                vm.grVM_MA.ST_DO_CODE = headerGR.ST_DO_CODE;
                vm.grVM_MA.RECEIVE_DATE = headerGR.RECEIVE_DATE;
                vm.grVM_MA.RECEIVE_BY = headerGR.RECEIVE_BY;
                vm.grVM_MA.PLAN_DELIVERY_DATE = headerGR.PLAN_DELIVERY_DATE;
                vm.grVM_MA.REMARK = headerGR.REMARK;
                vm.grVM_MA.REFERANCE_NO = headerGR.REFERANCE_NO;
                vm.grVM_MA.DELETE_FLAG = headerGR.DELETE_FLAG;
                vm.grVM_MA.CREATE_BY = headerGR.CREATE_BY;
                vm.grVM_MA.CREATE_DATE = headerGR.CREATE_DATE;
                vm.grVM_MA.UPDATE_BY = headerGR.UPDATE_BY;
                vm.grVM_MA.UPDATE_DATE = headerGR.UPDATE_DATE;
                vm.grVM_MA.DO_STATUS_CODE = headerGR.DO_STATUS_CODE;
                vm.grVM_MA.DO_STATUS_NAME = headerGR.DO_STATUS_NAME;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GrVM UpdateSave(GrVM vm)
        {
            try
            {
                #region ## Validate Update ##
                vm.ClearMessageList();
                vm = this.ValidateUpdate(vm);
                if (!vm.MessageList.Count.Equals(0))
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                GrET dataH = this.SetDataHUpdateGR(vm);
                GrDC dc = new GrDC();
                int result = dc.UpdateGR(dataH, vm.grVM_MA.resultList);

                //Supaneej, 2018-10-19, ST(Enhance) for Complain Save (ทำรับ)
                List<USP_M_COMPLAIN_GetByType_PET> complainPETs = this.SetComplainData(vm);
                if (result == 1)
                {
                    dc.ComplainUpdate(dataH, vm.grVM_MA.resultList, complainPETs);
                }
                //Supaneej, 2018-10-19, ST(Enhance) for Complain Save (ทำรับ)

                if (result == 1) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private GrET SetDataHUpdateGR(GrVM vm)
        {
            try
            {
                GrET dataH = new GrET();
                dataH.ST_GR_CODE = vm.grVM_MA.ST_GR_CODE;
                dataH.RECEIVE_BY = vm.grVM_MA.RECEIVE_BY;
                dataH.RECEIVE_DATE = vm.grVM_MA.RECEIVE_DATE;
                dataH.REFERANCE_NO = vm.grVM_MA.REFERANCE_NO;
                dataH.UPDATE_BY = vm.SessionLogin.USER_NAME;
                dataH.DO_STATUS_CODE = vm.grVM_MA.DO_STATUS_CODE;
                return dataH;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SortingInfoET> SetOrderListGR()
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
        public GrVM GrDelete(GrVM vm)
        {
            try
            {
                vm.MessageList.Clear();

                int result = 0;
                vm.grVM_MA.UPDATE_BY = vm.SessionLogin.USER_NAME;
                vm.grVM_MA.UPDATE_DATE = DateTime.Now;

                GrDC dc = new GrDC();
                result = dc.GrDelete(vm.grVM_MA);
                if (result == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));

                int countAll = 0;
                vm.grSearchCriteriaVM.REQUEST_BY_BRAND_CODE = vm.SessionLogin.BRAND_CODE;
                vm.grSearchCriteriaVM.REQUEST_BY_BRANCH_CODE = vm.SessionLogin.BRANCH_CODE;
                if (vm.grSearchResultVM == null) vm.grSearchResultVM = new GrSearchResultVM();
                vm.grSearchResultVM.resultList = dc.SearchGr(vm.grSearchCriteriaVM, null, null, out countAll);
                vm.grSearchResultVM.countAll = countAll;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ส่วนของการประเมินตอนทำรับ SupaneeJ 20190124
        private List<USP_M_COMPLAIN_GetByType_PET> SetComplainData(GrVM vm)
        {
            List<USP_M_COMPLAIN_GetByType_PET> complainList = new List<USP_M_COMPLAIN_GetByType_PET>();
            try
            {
                USP_M_COMPLAIN_GetByType_PET dataC = new USP_M_COMPLAIN_GetByType_PET();
                for (int i = 0; i < vm.complainRETs.Count; i++)
                {
                    dataC = new USP_M_COMPLAIN_GetByType_PET();
                    //dataC.ST_GR_CODE = vm.GrVM.ST_GR_CODE;
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

    }
}