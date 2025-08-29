using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT
{
    public class PrWhPickupBC
    {
        public PrWhPickupVM InnitialMA(PrWhPickupVM vm)
        {
            try
            {
                vm.prWhPickupVM_MA.USER_NAME = vm.SessionLogin.USER_NAME;
                var ddlDC = new DDLDC();
                var brandList = ddlDC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = ddlDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var categoryWHList = ddlDC.GetSTCategoryWH(DDLModeEnumET.SELECT_ALL);
                var locationList = ddlDC.GetSTLocation(DDLModeEnumET.SELECT_ALL);
                vm.prWhPickupVM_MA.brandList = brandList;
                vm.prWhPickupVM_MA.branchList = branchList;
                vm.prWhPickupVM_MA.prWhCategoryList = categoryWHList;
                vm.prWhPickupVM_MA.prWhLocationList = locationList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PrWhPickupVM ValidateExport(PrWhPickupVM vm)
        {
            try
            {
                if (!vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_FROM.HasValue)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันที่ส่งสินค้า จาก"));
                }
                if (!vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO.HasValue)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "วันที่ส่งสินค้า ถึง"));
                }
                if (vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_FROM.HasValue && vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO.HasValue)
                {
                    if (vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_FROM.Value > vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO.Value)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00018, "วันที่ส่งสินค้า ถึง", "วันที่ส่งสินค้า จาก"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrWhPickupVM ExportFile(PrWhPickupVM vm, string fullPath, int batchID)
        {
            try
            {
                #region === Validate Export ===
                vm.MessageList.Clear();
                vm = this.ValidateExport(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }
                #endregion

                BatchBC batchBC = new BatchBC();
                batchBC.InsertBatchLog(batchID, "Export File", "รายงาน PR เพื่อหยิบของ", "Process", null, vm.SessionLogin.USER_NAME);


                PrWhPickupDC dc = new PrWhPickupDC();
                vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO = vm.prWhPickupVM_MA.PLAN_DELIVERY_DATE_TO.Value.AddDays(1).AddSeconds(-1);
                if (vm.prWhPickupVM_MA.CATEGORY_ID == "0") vm.prWhPickupVM_MA.CATEGORY_ID = null;
                if (vm.prWhPickupSearchResultVM == null) vm.prWhPickupSearchResultVM = new PrWhPickupSearchResultVM();
                vm.prWhPickupSearchResultVM.resultList = dc.ExportFile(vm.SessionLogin.USER_NAME, vm.prWhPickupVM_MA); // Get data from database
                this.DataToExcel(vm.prWhPickupSearchResultVM.resultList, fullPath, batchID); // Convert data to excel files

                batchBC.InsertBatchLog(batchID, "Export File", "รายงาน PR เพื่อหยิบของ", "Success", null, vm.SessionLogin.USER_NAME);
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DataToExcel(List<PrWhPickupSearchResultET> dataList, string fullPath, int batchID)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                XSSFSheet sh;

                // create xls if not exists
                if (!File.Exists(fullPath))
                {
                    wb = new XSSFWorkbook();

                    // create sheet
                    sh = (XSSFSheet)wb.CreateSheet("Sheet1");
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        var r = sh.CreateRow(i);
                        var item = dataList[i];
                        if (i == 0)
                        {
                            r.CreateCell(0).SetCellValue("BATCH_ID");
                            r.CreateCell(1).SetCellValue(item.ST_PR_CODE);
                            r.CreateCell(2).SetCellValue(item.ST_PR_CODE_NAV);
                            r.CreateCell(3).SetCellValue(item.ORDER_DATE);
                            r.CreateCell(4).SetCellValue(item.POSTING_DATE);
                            r.CreateCell(5).SetCellValue(item.ITEM_CODE);
                            r.CreateCell(6).SetCellValue(item.ITEM_DESC);
                            r.CreateCell(7).SetCellValue(item.REQUEST_QTY);
                            r.CreateCell(8).SetCellValue(item.REQUEST_UOM);
                            r.CreateCell(9).SetCellValue(item.SEND_QTY);
                            r.CreateCell(10).SetCellValue(item.SEND_UOM);
                            r.CreateCell(11).SetCellValue(item.REQUEST_BY_BRAND_CODE);
                            r.CreateCell(12).SetCellValue(item.REQUEST_BY_BRANCH_CODE);
                            r.CreateCell(13).SetCellValue(item.BRANCH_NAME);
                            r.CreateCell(14).SetCellValue(item.CREATE_BY);
                            r.CreateCell(15).SetCellValue(item.ST_WH_ITEM_CATEGORY_NAME);
                            r.CreateCell(16).SetCellValue(item.REQUEST_TO_BRANCH_CODE);
                        }
                        else
                        {
                            r.CreateCell(0).SetCellValue(batchID);
                            r.CreateCell(1).SetCellValue(item.ST_PR_CODE);
                            r.CreateCell(2).SetCellValue(item.ST_PR_CODE_NAV);
                            r.CreateCell(3).SetCellValue(item.ORDER_DATE);
                            r.CreateCell(4).SetCellValue(item.POSTING_DATE);
                            r.CreateCell(5).SetCellValue(item.ITEM_CODE);
                            r.CreateCell(6).SetCellValue(item.ITEM_DESC);
                            r.CreateCell(7).SetCellValue(item.REQUEST_QTY);
                            r.CreateCell(8).SetCellValue(item.REQUEST_UOM);
                            r.CreateCell(9).SetCellValue(item.SEND_QTY);
                            r.CreateCell(10).SetCellValue(item.SEND_UOM);
                            r.CreateCell(11).SetCellValue(item.REQUEST_BY_BRAND_CODE);
                            r.CreateCell(12).SetCellValue(item.REQUEST_BY_BRANCH_CODE);
                            r.CreateCell(13).SetCellValue(item.BRANCH_NAME);
                            r.CreateCell(14).SetCellValue(item.CREATE_BY);
                            r.CreateCell(15).SetCellValue(item.ST_WH_ITEM_CATEGORY_NAME);
                            r.CreateCell(16).SetCellValue(item.REQUEST_TO_BRANCH_CODE);
                        }
                    }

                    using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        wb.Write(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PrWhPickupVM OnChangeBrand(PrWhPickupVM vm)
        {
            try
            {
                var ddlDC = new DDLDC();
                var branchList = ddlDC.GetBranchbyBrand(vm.prWhPickupVM_MA.BRAND_CODE, DDLModeEnumET.SELECT_ALL);
                vm.prWhPickupVM_MA.branchList = branchList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
