using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class RecTransferBC
    {
        RecTransferDC recTransferDC = new RecTransferDC();

        #region ---- Innitial ----

        public RecTransferVM InnitialCriteria(RecTransferVM vm)
        {
            try
            {
                if (vm.recTransferSearchCriteriaVM == null) { vm.recTransferSearchCriteriaVM = new RecTransferSearchCriteriaVM(); }
                var cbxDC = new CBXDC();
                var brandList = cbxDC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = cbxDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                if (vm.recTransferSearchCriteriaVM.BRAND_CODE != null)
                {
                    branchList = cbxDC.GetBranchbyBrandAndUsername(
                                                    vm.recTransferSearchCriteriaVM.BRAND_CODE,
                                                    vm.SessionLogin.USER_NAME
                                                    , DDLModeEnumET.SELECT_ALL);
                }

                vm.recTransferSearchCriteriaVM.brandList = brandList;
                vm.recTransferSearchCriteriaVM.branchList = branchList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecTransferVM InnitialMA(RecTransferVM vm)
        {
            try
            {
                if (vm.recTransferVM_MA == null) { vm.recTransferVM_MA = new RecTransferET_MA(); }
                var ddlDC = new DDLDC();

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public RecTransferVM SetBrandByCheckbox(RecTransferVM vm, List<CBXItemET> brandList)
        {
            try
            {
                var brandCheckList = brandList.Where(m => m.CHECKED).ToList();
                string brandCode = string.Empty;
                string brandName = string.Empty;

                if (brandCheckList.Count == 0)
                {
                    var item = brandList[0];
                    brandCode = brandCode + item.ITEM_VALUE;
                    brandName = brandName + item.ITEM_TEXT;
                }
                else
                {
                    foreach (var item in brandCheckList)
                    {
                        brandCode = brandCode + item.ITEM_VALUE + ",";
                        brandName = brandName + item.ITEM_TEXT + ",";
                    }
                    brandCode = brandCode.TrimEnd(brandCode[brandCode.Length - 1]);
                    brandName = brandName.TrimEnd(brandName[brandName.Length - 1]);
                }

                vm.recTransferSearchCriteriaVM.BRAND_CODE = brandCode;
                vm.recTransferSearchCriteriaVM.BRAND_NAME = brandName;

                vm.recTransferSearchCriteriaVM.BRANCH_CODE = null;
                vm.recTransferSearchCriteriaVM.BRANCH_NAME = null;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecTransferVM SetBranchByCheckbox(RecTransferVM vm, List<CBXItemET> branchList)
        {
            try
            {
                var branchCheckList = branchList.Where(m => m.CHECKED).ToList();
                string branchCode = string.Empty;
                string branchName = string.Empty;
                if (branchCheckList.Count == 0)
                {
                    var item = branchList[0];
                    branchCode = branchCode + item.ITEM_VALUE;
                    branchName = branchName + item.ITEM_TEXT;
                }
                else
                {
                    foreach (var item in branchCheckList)
                    {
                        branchCode = branchCode + item.ITEM_VALUE + ",";
                        branchName = branchName + item.ITEM_TEXT + ",";
                    }
                    branchCode = branchCode.TrimEnd(branchCode[branchCode.Length - 1]);
                    branchName = branchName.TrimEnd(branchName[branchName.Length - 1]);
                }


                vm.recTransferSearchCriteriaVM.BRANCH_CODE = branchCode;
                vm.recTransferSearchCriteriaVM.BRANCH_NAME = branchName;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecTransferVM Search(RecTransferVM vm)
        {
            try
            {
                int countAll = 0;
                vm.recTransferSearchCriteriaVM.USER_NAME = vm.SessionLogin.USER_NAME;
                vm.recTransferSearchCriteriaVM.PAGE_INDEX = vm.ZenPagination.currentPage;
                vm.recTransferSearchCriteriaVM.PAGE_SIZE = vm.ZenPagination.rowPerPage;
                if (vm.recTransferSearchCriteriaVM == null) vm.recTransferSearchCriteriaVM = new RecTransferSearchCriteriaVM();
                vm.recTransferSearchResultVM.resultList = recTransferDC.Search(vm.recTransferSearchCriteriaVM, out countAll);
                vm.recTransferSearchResultVM.countAll = countAll;
                if (countAll == 0)
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RecTransferVM ExportFile(RecTransferVM vm)
        {
            try
            {
                if (vm.recTransferSearchCriteriaVM == null) vm.recTransferSearchCriteriaVM = new RecTransferSearchCriteriaVM();
                vm.recTransferSearchCriteriaVM.USER_NAME = vm.SessionLogin.USER_NAME;
                RecTransferDC dc = new RecTransferDC();
                if (vm.recTransferSearchResultVM == null) vm.recTransferSearchResultVM = new RecTransferSearchResultVM();
                vm.recTransferSearchResultVM.resultList = dc.ExportFile(vm.recTransferSearchCriteriaVM); // Get data from database
                vm.recTransferVM_MA = this.DataToExcel(vm.recTransferSearchCriteriaVM, vm.recTransferSearchResultVM.resultList); // Convert data to excel files

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private RecTransferET_MA DataToExcel(RecTransferSearchCriteriaVM data, List<RecTransferSearchResultET> dataList)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                XSSFSheet sh;

                wb = new XSSFWorkbook();

                // create sheet
                sh = (XSSFSheet)wb.CreateSheet("Sheet1");
                for (int i = 0; i < dataList.Count; i++)
                {
                    var r = sh.CreateRow(i);
                    var item = dataList[i];
                    if (i == 0)
                    {
                        r.CreateCell(0).SetCellValue(item.EXPORT_ROW_NO);
                        r.CreateCell(1).SetCellValue(item.EXPORT_BRAND_CODE);
                        r.CreateCell(2).SetCellValue(item.EXPORT_BRAND_NAME);
                        r.CreateCell(3).SetCellValue(item.EXPORT_BRANCH_CODE);
                        r.CreateCell(4).SetCellValue(item.EXPORT_BRANCH_NAME);
                        r.CreateCell(5).SetCellValue(item.EXPORT_ST_DO_CODE);
                        r.CreateCell(6).SetCellValue(item.EXPORT_ST_GR_CODE);
                        r.CreateCell(7).SetCellValue(item.EXPORT_RECEIVE_DATE);
                        r.CreateCell(8).SetCellValue(item.EXPORT_LOCATION);
                        r.CreateCell(9).SetCellValue(item.EXPORT_ITEM_CODE);
                        r.CreateCell(10).SetCellValue(item.EXPORT_SEND_QTY);
                        r.CreateCell(11).SetCellValue(item.EXPORT_SEND_UOM);
                        r.CreateCell(12).SetCellValue(item.EXPORT_RECEIVE_QTY);
                        r.CreateCell(13).SetCellValue(item.EXPORT_RECEIVE_UOM);
                        r.CreateCell(14).SetCellValue(item.QUANTITY_DIFF);

                    }
                    else
                    {
                        r.CreateCell(0).SetCellValue(item.EXPORT_ROW_NO);
                        r.CreateCell(1).SetCellValue(item.EXPORT_BRAND_CODE);
                        r.CreateCell(2).SetCellValue(item.EXPORT_BRAND_NAME);
                        r.CreateCell(3).SetCellValue(item.EXPORT_BRANCH_CODE);
                        r.CreateCell(4).SetCellValue(item.EXPORT_BRANCH_NAME);
                        r.CreateCell(5).SetCellValue(item.EXPORT_ST_DO_CODE);
                        r.CreateCell(6).SetCellValue(item.EXPORT_ST_GR_CODE);
                        r.CreateCell(7).SetCellValue(item.EXPORT_RECEIVE_DATE);
                        r.CreateCell(8).SetCellValue(item.EXPORT_LOCATION);
                        r.CreateCell(9).SetCellValue(item.EXPORT_ITEM_CODE);
                        r.CreateCell(10).SetCellValue(item.EXPORT_SEND_QTY);
                        r.CreateCell(11).SetCellValue(item.EXPORT_SEND_UOM);
                        r.CreateCell(12).SetCellValue(item.EXPORT_RECEIVE_QTY);
                        r.CreateCell(13).SetCellValue(item.EXPORT_RECEIVE_UOM);
                        r.CreateCell(14).SetCellValue(item.QUANTITY_DIFF);
                    }
                }

                RecTransferET_MA recTransferMA = new RecTransferET_MA();
                using (var exportData = new MemoryStream())
                {
                    string fotmatFile = ConfigBC.GetConfigValue(ET.CNF.CategoryConfigEnum.FILE,
                                            ET.CNF.SubCategoryConfigEnum.EXPORT, ET.CNF.ConfigNameEnum.FORMAT_NAME);
                    string contentType = ConfigBC.GetConfigValue(ET.CNF.CategoryConfigEnum.FILE,
                                            ET.CNF.SubCategoryConfigEnum.EXPORT, ET.CNF.ConfigNameEnum.CONTENT_TYPE);
                    string filename = ConfigBC.GetConfigValue(ET.CNF.CategoryConfigEnum.FILE,
                                            ET.CNF.SubCategoryConfigEnum.EXPORT, ET.CNF.ConfigNameEnum.FILENAME_REC_TRANSFER);
                    wb.Write(exportData);
                    recTransferMA.EXPORT_DATA = exportData.ToArray();
                    fotmatFile = fotmatFile.Replace("{0}", filename);
                    fotmatFile = fotmatFile.Replace("{1}", DateTime.Now.ToString("ddMMyyyy_hhmmss"));
                    recTransferMA.CONTENT_TYPE = contentType;
                    recTransferMA.FILE_NAME = fotmatFile;
                }

                return recTransferMA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
