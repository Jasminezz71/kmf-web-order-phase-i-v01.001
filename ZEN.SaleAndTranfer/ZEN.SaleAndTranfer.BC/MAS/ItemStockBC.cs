using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class ItemStockBC
    {
        ItemStockDC itemStockDC = new ItemStockDC();

        #region ---- Innitial ----

        public ItemStockVM InnitialCriteria(ItemStockVM vm)
        {
            try
            {
                if (vm.itemStockSearchCriteriaVM == null) { vm.itemStockSearchCriteriaVM = new ItemStockSearchCriteriaVM(); }
                var ddlDC = new DDLDC();
                var brandList = ddlDC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = ddlDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                if (vm.itemStockSearchCriteriaVM.BRAND_CODE != null)
                {
                    branchList = ddlDC.GetBranchbyBrandAndUsername(
                                                    vm.itemStockSearchCriteriaVM.BRAND_CODE,
                                                    vm.SessionLogin.USER_NAME
                                                    , DDLModeEnumET.SELECT_ALL);
                }

                var appSystemList = ddlDC.GetAppSystem(DDLModeEnumET.SELECT_ALL);
                var itemStockTypeList = ddlDC.GetItemStockType(DDLModeEnumET.SELECT_ALL);

                vm.itemStockSearchCriteriaVM.brandList = brandList;
                vm.itemStockSearchCriteriaVM.branchList = branchList;
                vm.itemStockSearchCriteriaVM.appSystemList = appSystemList;
                vm.itemStockSearchCriteriaVM.itemStockTypeList = itemStockTypeList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ItemStockVM InnitialMA(ItemStockVM vm)
        {
            try
            {
                if (vm.itemStockVM_MA == null) { vm.itemStockVM_MA = new ItemStockET_MA(); }
                var ddlDC = new DDLDC();
                var appSystemList = ddlDC.GetAppSystem(DDLModeEnumET.SELECT_ALL);

                vm.itemStockVM_MA.appSystemList = appSystemList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private DateTime getLastDateByMonthYear(string data)
        {
            try
            {
                string[] splitDate = data.Split('/');
                int month = Convert.ToInt16(splitDate[0]);
                int year = Convert.ToInt16(splitDate[1]);
                int day = DateTime.DaysInMonth(year, month);
                DateTime lastDate = new DateTime(year, month, day);
                return lastDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ItemStockVM ValidateInventoryStockSearch(ItemStockVM vm)
        {
            try
            {
                if (vm.itemStockSearchCriteriaVM.START_DATE != null && vm.itemStockSearchCriteriaVM.END_DATE != null)
                {
                    if (vm.itemStockSearchCriteriaVM.START_DATE > vm.itemStockSearchCriteriaVM.END_DATE)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018, "วันรับสินค้า ถึง", "วันรับสินค้า จาก"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public ItemStockVM InventoryStockSearch(ItemStockVM vm)
        {
            try
            {
                #region == validate ==
                vm.MessageList.Clear();
                vm = this.ValidateInventoryStockSearch(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        return vm;
                    }
                }
                #endregion

                vm.itemStockSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.itemStockSearchCriteriaVM.PAGE_SIZE = vm.ZenPagination.rowPerPage;
                vm.itemStockSearchCriteriaVM.USER_NAME = vm.SessionLogin.USER_NAME;

                int countAll = 0;
                if (vm.itemStockSearchResultVM == null) vm.itemStockSearchResultVM = new ItemStockSearchResultVM();
                if (vm.itemStockSearchResultVM.resultList == null) vm.itemStockSearchResultVM.resultList = new List<ItemStockSearchResultET>();
                vm.itemStockSearchResultVM.resultList = itemStockDC.InventoryStockSearch(vm.itemStockSearchCriteriaVM, out countAll);
                vm.itemStockSearchResultVM.countAll = countAll;
                if (vm.itemStockSearchResultVM.countAll == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ItemStockVM ValidateInventoryStockGetItem(ItemStockVM vm)
        {
            try
            {
                if (vm.itemStockVM_MA.ITEM_PICK_DATE == null || vm.itemStockVM_MA.ITEM_PICK_DATE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "บันทึกสินค้าคงเหลือ ณ เดือน"));
                }
                else
                {
                    DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    string[] split = vm.itemStockVM_MA.ITEM_PICK_DATE.Split('/');
                    DateTime pickDate = new DateTime(Convert.ToInt16(split[1]), Convert.ToInt16(split[0]), 1);
                    if (pickDate > currentDate)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00073));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public ItemStockVM InventoryStockGetItem(ItemStockVM vm)
        {
            try
            {
                #region == validate ==
                vm.MessageList.Clear();
                vm = this.ValidateInventoryStockGetItem(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        return vm;
                    }
                }
                #endregion

                vm.itemStockVM_MA.ITEM_PICK_DATE_SAVE = getLastDateByMonthYear(vm.itemStockVM_MA.ITEM_PICK_DATE);
                if (vm.itemStockVM_MA.itemStockList == null) vm.itemStockVM_MA.itemStockList = new List<ItemStockET>();
                vm.itemStockVM_MA.itemStockList = itemStockDC.InventoryStockGetItem(vm.itemStockVM_MA);
                if (vm.itemStockVM_MA.itemStockList == null) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                else if (vm.itemStockVM_MA.itemStockList.Count == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region === Module Save ===
        private ItemStockVM ValidateInventoryStockSave(ItemStockVM vm)
        {
            try
            {
                for (int i = 0; i < vm.itemStockVM_MA.itemStockList.Count; i++)
                {
                    if (vm.itemStockVM_MA.itemStockList[i].REMAIN_QTY == null || vm.itemStockVM_MA.itemStockList[i].REMAIN_QTY < 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "จำนวนที่เหลือในร้านรหัสสินค้า : "
                            + vm.itemStockVM_MA.itemStockList[i].ITEM_CODE));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public ItemStockVM InventoryStockSave(ItemStockVM vm, int batchID)
        {
            try
            {
                #region == validate ==
                vm.MessageList.Clear();
                //vm = this.ValidateInventoryStockSave(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        return vm;
                    }
                }
                #endregion

                DataTable dt1 = this.SetDataToDT(vm, batchID);

                #region ===== Bluk Insert temp table =====
                int resultBlukInsert = 0;
                ItemStockDC dc = new ItemStockDC();
                resultBlukInsert = dc.BlukInsertStockCard(dt1);
                #endregion

                //var dataH = SetInventoryStockSaveH(vm.itemStockVM_MA, vm.SessionLogin.USER_NAME);
                //var dataD = SetInventoryStockSaveD(vm.itemStockVM_MA);
                //var result = itemStockDC.InventoryStockSave(dataH, dataD);

                var result = itemStockDC.InventoryStockSave(batchID);
                if (result == null || result == "") vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00010));
                    vm.itemStockVM_MA.ITEM_PICK_DATE_SAVE = getLastDateByMonthYear(vm.itemStockVM_MA.ITEM_PICK_DATE);
                    vm.itemStockVM_MA.itemStockList = itemStockDC.InventoryStockGetItem(vm.itemStockVM_MA);
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ItemStockET SetInventoryStockSaveH(ItemStockET_MA data, string userName)
        {
            try
            {
                ItemStockET dataH = new ItemStockET();
                dataH.BRAND_CODE = data.BRAND_CODE;
                dataH.BRANCH_CODE = data.BRANCH_CODE;
                dataH.ITEM_PICK_DATE_SAVE = getLastDateByMonthYear(data.ITEM_PICK_DATE);
                dataH.UPDATE_BY = userName;
                return dataH;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ItemStockET> SetInventoryStockSaveD(ItemStockET_MA data)
        {
            try
            {
                List<ItemStockET> dataD = new List<ItemStockET>();
                ItemStockET itemStock = new ItemStockET();

                foreach (var item in data.itemStockList)
                {
                    itemStock = new ItemStockET();
                    itemStock.ITEM_CODE = item.ITEM_CODE;
                    itemStock.REMAIN_QTY = item.REMAIN_QTY;
                    itemStock.REMAIN_UOM = item.REMAIN_UOM;
                    dataD.Add(itemStock);
                }
                return dataD;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable SetDataToDT(ItemStockVM vm, int batchID)
        {
            try
            {
                #region === Create new dt ====
                DataTable d1 = new DataTable("TB_T_STOCK_CARD_TRANSACTION");
                d1.Columns.Add(new DataColumn("BATCH_ID", typeof(int)));
                d1.Columns.Add(new DataColumn("BRAND_CODE", typeof(string)));
                d1.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
                d1.Columns.Add(new DataColumn("ITEM_CODE", typeof(string)));
                d1.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
                d1.Columns.Add(new DataColumn("LOCATION", typeof(string)));
                d1.Columns.Add(new DataColumn("REMAIN_QTY", typeof(string)));
                d1.Columns.Add(new DataColumn("REMAIN_UOM", typeof(string)));
                d1.Columns.Add(new DataColumn("ACTION", typeof(string)));
                d1.Columns.Add(new DataColumn("SAVE_DATE", typeof(DateTime)));
                d1.Columns.Add(new DataColumn("REMARK", typeof(string)));
                d1.Columns.Add(new DataColumn("CREATE_BY", typeof(string)));
                d1.Columns.Add(new DataColumn("CREATE_DATE", typeof(DateTime)));
                d1.Columns.Add(new DataColumn("UPDATE_BY", typeof(string)));
                d1.Columns.Add(new DataColumn("UPDATE_DATE", typeof(DateTime)));
                #endregion

                DataRow rowH = null;
                DateTime currentDate = DateTime.Now;

                for (int i = 0; i < vm.itemStockVM_MA.itemStockList.Count; i++)
                {
                    // Set data all row
                    rowH = d1.NewRow();
                    rowH["BATCH_ID"] = batchID;
                    rowH["BRAND_CODE"] = vm.itemStockVM_MA.BRAND_CODE;
                    rowH["BRANCH_CODE"] = vm.itemStockVM_MA.BRANCH_CODE;
                    rowH["ITEM_CODE"] = vm.itemStockVM_MA.itemStockList[i].ITEM_CODE;
                    rowH["ITEM_NAME"] = vm.itemStockVM_MA.itemStockList[i].ITEM_NAME;
                    if (vm.itemStockVM_MA.itemStockList[i].LOCATION == null)
                        rowH["LOCATION"] = vm.itemStockVM_MA.itemStockList[i].LOCATION;
                    else
                        rowH["LOCATION"] = vm.itemStockVM_MA.itemStockList[i].LOCATION.Trim();
                    rowH["REMAIN_QTY"] = vm.itemStockVM_MA.itemStockList[i].REMAIN_QTY;
                    rowH["REMAIN_UOM"] = vm.itemStockVM_MA.itemStockList[i].REMAIN_UOM;
                    rowH["ACTION"] = "REMAIN";
                    rowH["SAVE_DATE"] = getLastDateByMonthYear(vm.itemStockVM_MA.ITEM_PICK_DATE);
                    rowH["REMARK"] = null;
                    rowH["CREATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["CREATE_DATE"] = currentDate;
                    rowH["UPDATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["UPDATE_DATE"] = currentDate;

                    d1.Rows.Add(rowH);
                }
                return d1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
