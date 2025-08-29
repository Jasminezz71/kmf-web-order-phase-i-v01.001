using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.DC.RPT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.RPT;
using ZEN.SaleAndTranfer.VM.RPT;

namespace ZEN.SaleAndTranfer.BC.RPT
{
    public class StockBC
    {
        public StockVM InnitialCriteria(StockVM vm)
        {
            try
            {
                DDLBC bc = new DDLBC();
                if (vm.stockSearchCriteriaVM == null) vm.stockSearchCriteriaVM = new StockSearchCriteriaVM();
                if (vm.statusDoGrSearchCriteriaVM == null) vm.statusDoGrSearchCriteriaVM = new StatusDoGrSearchCriteriaVM();
                var brandList = bc.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = bc.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                vm.stockSearchCriteriaVM.brandList = brandList;
                vm.stockSearchCriteriaVM.branchList = branchList;
                vm.statusDoGrSearchCriteriaVM.brandList = brandList;
                vm.statusDoGrSearchCriteriaVM.branchList = branchList;

                if (vm.stockSearchCriteriaVM.BRAND_CODE != null)
                {
                    vm.stockSearchCriteriaVM.branchList = bc.GetBranchbyBrandAndUsername(
                                                                                        DDLModeEnumET.SELECT_ALL,
                                                                                        vm.stockSearchCriteriaVM.BRAND_CODE,
                                                                                        vm.SessionLogin.USER_NAME);
                }
                if (vm.statusDoGrSearchCriteriaVM.BRAND_CODE != null)
                {
                    vm.statusDoGrSearchCriteriaVM.branchList = bc.GetBranchbyBrandAndUsername(
                                                                                        DDLModeEnumET.SELECT_ALL,
                                                                                        vm.statusDoGrSearchCriteriaVM.BRAND_CODE,
                                                                                        vm.SessionLogin.USER_NAME);
                }

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StockVM ValidateStockTotalSearch(StockVM vm)
        {
            try
            {
                if (vm.stockSearchCriteriaVM.BRAND_CODE == null || vm.stockSearchCriteriaVM.BRAND_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "บริษัท"));
                }
                if (vm.stockSearchCriteriaVM.BRANCH_CODE == null || vm.stockSearchCriteriaVM.BRANCH_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                }
                if (vm.stockSearchCriteriaVM.DATE_TO == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ตรวจสอบสินค้า ณ เดือน"));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StockVM StockTotalSearch(StockVM vm)
        {
            try
            {
                #region === Validate Search ===
                vm.MessageList.Clear();
                vm = this.ValidateStockTotalSearch(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        {
                            return vm;
                        }
                    }
                }
                #endregion

                if (vm.stockSearchCriteriaVM.DATE_TO.HasValue)
                {
                    var year = vm.stockSearchCriteriaVM.DATE_TO.Value.Year;
                    var month = vm.stockSearchCriteriaVM.DATE_TO.Value.Month;
                    var day = DateTime.DaysInMonth(year, month);
                    vm.stockSearchCriteriaVM.DATE_TO_SEARCH = new DateTime(year, month, day);
                }
                vm.stockSearchResultVM = new StockSearchResultVM();
                vm.stockSearchResultVM.resultList = new List<stockSearchResultET>();
                StockDC dc = new StockDC();
                vm.stockSearchResultVM.resultList = dc.InventoryStockSearch(vm.stockSearchCriteriaVM);
                if (vm.stockSearchResultVM.resultList != null)
                {
                    if (vm.stockSearchResultVM.resultList.Count > 0)
                    {
                        foreach (var item in vm.stockSearchResultVM.resultList)
                        {
                            #region row 01 - 10
                            if (item.S_01 == "0.00") item.S_01 = "N/A";
                            if (item.R_01 == "0.00") item.R_01 = "N/A";
                            if (item.S_02 == "0.00") item.S_02 = "N/A";
                            if (item.R_02 == "0.00") item.R_02 = "N/A";
                            if (item.S_03 == "0.00") item.S_03 = "N/A";
                            if (item.R_03 == "0.00") item.R_03 = "N/A";
                            if (item.S_04 == "0.00") item.S_04 = "N/A";
                            if (item.R_04 == "0.00") item.R_04 = "N/A";
                            if (item.S_05 == "0.00") item.S_05 = "N/A";
                            if (item.R_05 == "0.00") item.R_05 = "N/A";
                            if (item.S_06 == "0.00") item.S_06 = "N/A";
                            if (item.R_06 == "0.00") item.R_06 = "N/A";
                            if (item.S_07 == "0.00") item.S_07 = "N/A";
                            if (item.R_07 == "0.00") item.R_07 = "N/A";
                            if (item.S_08 == "0.00") item.S_08 = "N/A";
                            if (item.R_08 == "0.00") item.R_08 = "N/A";
                            if (item.S_09 == "0.00") item.S_09 = "N/A";
                            if (item.R_09 == "0.00") item.R_09 = "N/A";
                            if (item.S_10 == "0.00") item.S_10 = "N/A";
                            if (item.R_10 == "0.00") item.R_10 = "N/A";
                            #endregion
                            #region row 11 - 20
                            if (item.S_11 == "0.00") item.S_11 = "N/A";
                            if (item.R_11 == "0.00") item.R_11 = "N/A";
                            if (item.S_12 == "0.00") item.S_12 = "N/A";
                            if (item.R_12 == "0.00") item.R_12 = "N/A";
                            if (item.S_13 == "0.00") item.S_13 = "N/A";
                            if (item.R_13 == "0.00") item.R_13 = "N/A";
                            if (item.S_14 == "0.00") item.S_14 = "N/A";
                            if (item.R_14 == "0.00") item.R_14 = "N/A";
                            if (item.S_15 == "0.00") item.S_15 = "N/A";
                            if (item.R_15 == "0.00") item.R_15 = "N/A";
                            if (item.S_16 == "0.00") item.S_16 = "N/A";
                            if (item.R_16 == "0.00") item.R_16 = "N/A";
                            if (item.S_17 == "0.00") item.S_17 = "N/A";
                            if (item.R_17 == "0.00") item.R_17 = "N/A";
                            if (item.S_18 == "0.00") item.S_18 = "N/A";
                            if (item.R_18 == "0.00") item.R_18 = "N/A";
                            if (item.S_19 == "0.00") item.S_19 = "N/A";
                            if (item.R_19 == "0.00") item.R_19 = "N/A";
                            if (item.S_20 == "0.00") item.S_20 = "N/A";
                            if (item.R_20 == "0.00") item.R_20 = "N/A";
                            #endregion
                            #region row 21 - 31
                            if (item.S_21 == "0.00") item.S_21 = "N/A";
                            if (item.R_21 == "0.00") item.R_21 = "N/A";
                            if (item.S_22 == "0.00") item.S_22 = "N/A";
                            if (item.R_22 == "0.00") item.R_22 = "N/A";
                            if (item.S_23 == "0.00") item.S_23 = "N/A";
                            if (item.R_23 == "0.00") item.R_23 = "N/A";
                            if (item.S_24 == "0.00") item.S_24 = "N/A";
                            if (item.R_24 == "0.00") item.R_24 = "N/A";
                            if (item.S_25 == "0.00") item.S_25 = "N/A";
                            if (item.R_25 == "0.00") item.R_25 = "N/A";
                            if (item.S_26 == "0.00") item.S_26 = "N/A";
                            if (item.R_26 == "0.00") item.R_26 = "N/A";
                            if (item.S_27 == "0.00") item.S_27 = "N/A";
                            if (item.R_27 == "0.00") item.R_27 = "N/A";
                            if (item.S_28 == "0.00") item.S_28 = "N/A";
                            if (item.R_28 == "0.00") item.R_28 = "N/A";
                            if (item.S_29 == "0.00") item.S_29 = "N/A";
                            if (item.R_29 == "0.00") item.R_29 = "N/A";
                            if (item.S_30 == "0.00") item.S_30 = "N/A";
                            if (item.R_30 == "0.00") item.R_30 = "N/A";
                            if (item.S_31 == "0.00") item.S_31 = "N/A";
                            if (item.R_31 == "0.00") item.R_31 = "N/A";
                            #endregion
                        }

                        vm = this.DataToExcelStockTotal(vm, vm.stockSearchResultVM.resultList, vm.stockSearchCriteriaVM.DATE_TO_SEARCH);
                    }
                    else
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                    }
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StockVM ValidateStatusReceiveByDoAndGrSearch(StockVM vm)
        {
            try
            {
                if (vm.statusDoGrSearchCriteriaVM.BRAND_CODE == null || vm.statusDoGrSearchCriteriaVM.BRAND_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "บริษัท"));
                }
                if (vm.statusDoGrSearchCriteriaVM.BRANCH_CODE == null || vm.statusDoGrSearchCriteriaVM.BRANCH_CODE.Trim(' ') == "")
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "สาขา"));
                }
                if (vm.statusDoGrSearchCriteriaVM.DATE_FROM == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ตั้งแต่วันที่"));
                }
                if (vm.statusDoGrSearchCriteriaVM.DATE_TO == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "ถึงวันที่"));
                }
                if (vm.statusDoGrSearchCriteriaVM.DATE_TO != null && vm.statusDoGrSearchCriteriaVM.DATE_FROM != null)
                {
                    if (vm.stockSearchCriteriaVM.DATE_FROM > vm.stockSearchCriteriaVM.DATE_TO)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00019, "ตั้งแต่วันที่", "ถึงวันที่"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StockVM StatusReceiveByDoAndGrSearch(StockVM vm)
        {
            try
            {
                #region === Validate Search ===
                vm.MessageList.Clear();
                vm = this.ValidateStatusReceiveByDoAndGrSearch(vm);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        {
                            return vm;
                        }
                    }
                }
                #endregion

                vm.statusDoGrSearchResultVM = new StatusDoGrSearchResultVM();
                vm.statusDoGrSearchResultVM.resultList = new List<statusDoGrSearchResultET>();
                StockDC dc = new StockDC();
                vm.statusDoGrSearchResultVM.resultList = dc.StatusReceiveByDoAndGrSearch(vm.statusDoGrSearchCriteriaVM);
                if (vm.statusDoGrSearchResultVM.resultList != null)
                {
                    if (vm.statusDoGrSearchResultVM.resultList.Count > 0)
                    {
                        vm = this.DataToExcelStatusReceiveByDoAndGr(vm, vm.statusDoGrSearchResultVM.resultList);
                    }
                    else
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                    }
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StockVM DataToExcelStockTotal(StockVM vm, List<stockSearchResultET> dataList, DateTime? dateSelect)
        {
            try
            {
                IWorkbook wb = new XSSFWorkbook();
                ISheet sh = (XSSFSheet)wb.CreateSheet("Sheet1");

                #region === Set Header ===

                var r1 = sh.CreateRow(0);
                var r2 = sh.CreateRow(1);
                var r3 = sh.CreateRow(2);

                r1.CreateCell(0).SetCellValue("แบรนด์");
                CellRangeAddress regionBrand = CellRangeAddress.ValueOf("A1:A3");
                r1.CreateCell(1).SetCellValue("สาขา");
                CellRangeAddress regionBranch = CellRangeAddress.ValueOf("B1:B3");
                r1.CreateCell(2).SetCellValue("รหัสสินค้า");
                CellRangeAddress regionItemCode = CellRangeAddress.ValueOf("C1:C3");
                r1.CreateCell(3).SetCellValue("ชื่อสินค้า");
                CellRangeAddress regionItemName = CellRangeAddress.ValueOf("D1:D3");
                r1.CreateCell(4).SetCellValue("วันที่รับสินค้า");
                CellRangeAddress regionDateRecieve = CellRangeAddress.ValueOf("E1:BN1");
                r2.CreateCell(4).SetCellValue("1");
                r2.CreateCell(6).SetCellValue("2");
                r2.CreateCell(8).SetCellValue("3");
                r2.CreateCell(10).SetCellValue("4");
                r2.CreateCell(12).SetCellValue("5");
                r2.CreateCell(14).SetCellValue("6");
                r2.CreateCell(16).SetCellValue("7");
                r2.CreateCell(18).SetCellValue("8");
                r2.CreateCell(20).SetCellValue("9");
                r2.CreateCell(22).SetCellValue("10");
                r2.CreateCell(24).SetCellValue("11");
                r2.CreateCell(26).SetCellValue("12");
                r2.CreateCell(28).SetCellValue("13");
                r2.CreateCell(30).SetCellValue("14");
                r2.CreateCell(32).SetCellValue("15");
                r2.CreateCell(34).SetCellValue("16");
                r2.CreateCell(36).SetCellValue("17");
                r2.CreateCell(38).SetCellValue("18");
                r2.CreateCell(40).SetCellValue("19");
                r2.CreateCell(42).SetCellValue("20");
                r2.CreateCell(44).SetCellValue("21");
                r2.CreateCell(46).SetCellValue("22");
                r2.CreateCell(48).SetCellValue("23");
                r2.CreateCell(50).SetCellValue("24");
                r2.CreateCell(52).SetCellValue("25");
                r2.CreateCell(54).SetCellValue("26");
                r2.CreateCell(56).SetCellValue("27");
                r2.CreateCell(58).SetCellValue("28");
                r2.CreateCell(60).SetCellValue("29");
                r2.CreateCell(62).SetCellValue("30");
                r2.CreateCell(64).SetCellValue("31");

                CellRangeAddress regionDate1 = CellRangeAddress.ValueOf("E2:F2");
                CellRangeAddress regionDate2 = CellRangeAddress.ValueOf("G2:H2");
                CellRangeAddress regionDate3 = CellRangeAddress.ValueOf("I2:J2");
                CellRangeAddress regionDate4 = CellRangeAddress.ValueOf("K2:L2");
                CellRangeAddress regionDate5 = CellRangeAddress.ValueOf("M2:N2");
                CellRangeAddress regionDate6 = CellRangeAddress.ValueOf("O2:P2");
                CellRangeAddress regionDate7 = CellRangeAddress.ValueOf("Q2:R2");
                CellRangeAddress regionDate8 = CellRangeAddress.ValueOf("S2:T2");
                CellRangeAddress regionDate9 = CellRangeAddress.ValueOf("U2:V2");
                CellRangeAddress regionDate10 = CellRangeAddress.ValueOf("W2:X2");
                CellRangeAddress regionDate11 = CellRangeAddress.ValueOf("Y2:Z2");
                CellRangeAddress regionDate12 = CellRangeAddress.ValueOf("AA2:AB2");
                CellRangeAddress regionDate13 = CellRangeAddress.ValueOf("AC2:AD2");
                CellRangeAddress regionDate14 = CellRangeAddress.ValueOf("AE2:AF2");
                CellRangeAddress regionDate15 = CellRangeAddress.ValueOf("AG2:AH2");
                CellRangeAddress regionDate16 = CellRangeAddress.ValueOf("AI2:AJ2");
                CellRangeAddress regionDate17 = CellRangeAddress.ValueOf("AK2:AL2");
                CellRangeAddress regionDate18 = CellRangeAddress.ValueOf("AM2:AN2");
                CellRangeAddress regionDate19 = CellRangeAddress.ValueOf("AO2:AP2");
                CellRangeAddress regionDate20 = CellRangeAddress.ValueOf("AQ2:AR2");
                CellRangeAddress regionDate21 = CellRangeAddress.ValueOf("AS2:AT2");
                CellRangeAddress regionDate22 = CellRangeAddress.ValueOf("AU2:AV2");
                CellRangeAddress regionDate23 = CellRangeAddress.ValueOf("AW2:AX2");
                CellRangeAddress regionDate24 = CellRangeAddress.ValueOf("AY2:AZ2");
                CellRangeAddress regionDate25 = CellRangeAddress.ValueOf("BA2:BB2");
                CellRangeAddress regionDate26 = CellRangeAddress.ValueOf("BC2:BD2");
                CellRangeAddress regionDate27 = CellRangeAddress.ValueOf("BE2:BF2");
                CellRangeAddress regionDate28 = CellRangeAddress.ValueOf("BG2:BH2");
                CellRangeAddress regionDate29 = CellRangeAddress.ValueOf("BI2:BJ2");
                CellRangeAddress regionDate30 = CellRangeAddress.ValueOf("BK2:BL2");
                CellRangeAddress regionDate31 = CellRangeAddress.ValueOf("BM2:BN2");

                sh.AddMergedRegion(regionDate1);
                sh.AddMergedRegion(regionDate2);
                sh.AddMergedRegion(regionDate3);
                sh.AddMergedRegion(regionDate4);
                sh.AddMergedRegion(regionDate5);
                sh.AddMergedRegion(regionDate6);
                sh.AddMergedRegion(regionDate7);
                sh.AddMergedRegion(regionDate8);
                sh.AddMergedRegion(regionDate9);
                sh.AddMergedRegion(regionDate10);
                sh.AddMergedRegion(regionDate11);
                sh.AddMergedRegion(regionDate12);
                sh.AddMergedRegion(regionDate13);
                sh.AddMergedRegion(regionDate14);
                sh.AddMergedRegion(regionDate15);
                sh.AddMergedRegion(regionDate16);
                sh.AddMergedRegion(regionDate17);
                sh.AddMergedRegion(regionDate18);
                sh.AddMergedRegion(regionDate19);
                sh.AddMergedRegion(regionDate20);
                sh.AddMergedRegion(regionDate21);
                sh.AddMergedRegion(regionDate22);
                sh.AddMergedRegion(regionDate23);
                sh.AddMergedRegion(regionDate24);
                sh.AddMergedRegion(regionDate25);
                sh.AddMergedRegion(regionDate26);
                sh.AddMergedRegion(regionDate27);
                sh.AddMergedRegion(regionDate28);
                sh.AddMergedRegion(regionDate29);
                sh.AddMergedRegion(regionDate30);
                sh.AddMergedRegion(regionDate31);

                r3.CreateCell(4).SetCellValue("รับ");
                r3.CreateCell(5).SetCellValue("ส่ง");
                r3.CreateCell(6).SetCellValue("รับ");
                r3.CreateCell(7).SetCellValue("ส่ง");
                r3.CreateCell(8).SetCellValue("รับ");
                r3.CreateCell(9).SetCellValue("ส่ง");
                r3.CreateCell(10).SetCellValue("รับ");
                r3.CreateCell(11).SetCellValue("ส่ง");
                r3.CreateCell(12).SetCellValue("รับ");
                r3.CreateCell(13).SetCellValue("ส่ง");
                r3.CreateCell(14).SetCellValue("รับ");
                r3.CreateCell(15).SetCellValue("ส่ง");
                r3.CreateCell(16).SetCellValue("รับ");
                r3.CreateCell(17).SetCellValue("ส่ง");
                r3.CreateCell(18).SetCellValue("รับ");
                r3.CreateCell(19).SetCellValue("ส่ง");
                r3.CreateCell(20).SetCellValue("รับ");
                r3.CreateCell(21).SetCellValue("ส่ง");
                r3.CreateCell(22).SetCellValue("รับ");
                r3.CreateCell(23).SetCellValue("ส่ง");
                r3.CreateCell(24).SetCellValue("รับ");
                r3.CreateCell(25).SetCellValue("ส่ง");
                r3.CreateCell(26).SetCellValue("รับ");
                r3.CreateCell(27).SetCellValue("ส่ง");
                r3.CreateCell(28).SetCellValue("รับ");
                r3.CreateCell(29).SetCellValue("ส่ง");
                r3.CreateCell(30).SetCellValue("รับ");
                r3.CreateCell(31).SetCellValue("ส่ง");
                r3.CreateCell(32).SetCellValue("รับ");
                r3.CreateCell(33).SetCellValue("ส่ง");
                r3.CreateCell(34).SetCellValue("รับ");
                r3.CreateCell(35).SetCellValue("ส่ง");
                r3.CreateCell(36).SetCellValue("รับ");
                r3.CreateCell(37).SetCellValue("ส่ง");
                r3.CreateCell(38).SetCellValue("รับ");
                r3.CreateCell(39).SetCellValue("ส่ง");
                r3.CreateCell(40).SetCellValue("รับ");
                r3.CreateCell(41).SetCellValue("ส่ง");
                r3.CreateCell(42).SetCellValue("รับ");
                r3.CreateCell(43).SetCellValue("ส่ง");
                r3.CreateCell(44).SetCellValue("รับ");
                r3.CreateCell(45).SetCellValue("ส่ง");
                r3.CreateCell(46).SetCellValue("รับ");
                r3.CreateCell(47).SetCellValue("ส่ง");
                r3.CreateCell(48).SetCellValue("รับ");
                r3.CreateCell(49).SetCellValue("ส่ง");
                r3.CreateCell(50).SetCellValue("รับ");
                r3.CreateCell(51).SetCellValue("ส่ง");
                r3.CreateCell(52).SetCellValue("รับ");
                r3.CreateCell(53).SetCellValue("ส่ง");
                r3.CreateCell(54).SetCellValue("รับ");
                r3.CreateCell(55).SetCellValue("ส่ง");
                r3.CreateCell(56).SetCellValue("รับ");
                r3.CreateCell(57).SetCellValue("ส่ง");
                r3.CreateCell(58).SetCellValue("รับ");
                r3.CreateCell(59).SetCellValue("ส่ง");
                r3.CreateCell(60).SetCellValue("รับ");
                r3.CreateCell(61).SetCellValue("ส่ง");
                r3.CreateCell(62).SetCellValue("รับ");
                r3.CreateCell(63).SetCellValue("ส่ง");
                r3.CreateCell(64).SetCellValue("รับ");
                r3.CreateCell(65).SetCellValue("ส่ง");


                sh.AddMergedRegion(regionBrand);
                sh.AddMergedRegion(regionBranch);
                sh.AddMergedRegion(regionItemCode);
                sh.AddMergedRegion(regionItemName);
                sh.AddMergedRegion(regionDateRecieve);

                #endregion

                #region === Set Detail ===
                var dataType = dataList.GroupBy(m => m.ST_PR_CATEGORY_ID).ToList().OrderBy(m => m.Key).ToList();

                for (int prTypeRow = 0; prTypeRow < dataType.Count; prTypeRow++)
                {
                    var prType = dataType[prTypeRow].Key;
                    var data = dataList.Where(m => m.ST_PR_CATEGORY_ID == prType).OrderBy(m => m.ITEM_CODE).ToList();

                    #region === Set Pr Type ===
                    var row = sh.CreateRow((sh.LastRowNum) + 1);
                    row.CreateCell(0).SetCellValue("ประเภท " + data[0].ST_PR_CATEGORY_NAME);

                    string cellMerged = string.Empty;
                    cellMerged = "A{0}:D{1}";
                    cellMerged = cellMerged.Replace("{0}", (row.RowNum + 1).ToString()).Replace("{1}", (row.RowNum + 1).ToString());
                    CellRangeAddress regionPrType = CellRangeAddress.ValueOf(cellMerged);
                    sh.AddMergedRegion(regionPrType);
                    #endregion

                    #region === Set Row Data ===
                    for (int i = 0; i < data.Count; i++)
                    {
                        row = sh.CreateRow((sh.LastRowNum) + 1);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].BRAND_CODE);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].BRANCH_CODE);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].ITEM_CODE);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].ITEM_NAME_TH);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_01);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_01);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_02);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_02);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_03);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_03);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_04);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_04);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_05);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_05);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_06);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_06);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_07);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_07);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_08);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_08);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_09);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_09);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_10);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_10);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_11);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_11);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_12);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_12);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_13);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_13);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_14);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_14);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_15);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_15);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_16);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_16);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_17);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_17);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_18);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_18);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_19);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_19);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_20);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_20);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_21);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_21);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_22);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_22);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_23);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_23);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_24);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_24);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_25);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_25);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_26);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_26);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_27);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_27);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_28);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_28);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_29);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_29);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_30);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_30);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].S_31);
                        row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data[i].R_31);
                    }
                    #endregion
                }
                #endregion

                #region === Set Download File ===
                vm.stockDownloadVM = new DownloadVM();
                using (var exportData = new MemoryStream())
                {
                    string date = dateSelect.Value.ToShortDateString().Substring(3, (dateSelect.Value.ToShortDateString().Length - 3)).Replace("/", "-");
                    string filename = "รายงานการส่งและรับสินค้า_{0}";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    wb.Write(exportData);
                    vm.stockDownloadVM.EXPORT_DATA = exportData.ToArray();
                    filename = filename.Replace("{0}", date);

                    vm.stockDownloadVM.FILE_NAME = filename + ".xlsx";
                    vm.stockDownloadVM.CONTENT_TYPE = contentType;
                }
                #endregion

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private StockVM DataToExcelStatusReceiveByDoAndGr(StockVM vm, List<statusDoGrSearchResultET> dataList)
        {
            try
            {
                IWorkbook wb = new XSSFWorkbook();
                ISheet sh = (XSSFSheet)wb.CreateSheet("Sheet1");

                for (int i = 0; i < dataList.Count; i++)
                {
                    var data = dataList[i];
                    var row = sh.CreateRow(i);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.ROW_NO);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.DO_DATE);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.CREATE_DATE_DO);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.BRAND_CODE);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.BRANCH_NAME);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.DO_CODE);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.GR_CODE);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.RECEIVE_BY);
                    row.CreateCell(row.PhysicalNumberOfCells).SetCellValue(data.RECEIVE_DATE);
                }

                #region === Set Download File ===
                vm.stockDownloadVM = new DownloadVM();
                using (var exportData = new MemoryStream())
                {
                    string dateFrom = vm.statusDoGrSearchCriteriaVM.DATE_FROM.Value.ToShortDateString().Replace("/", "-");
                    string dateTo = vm.statusDoGrSearchCriteriaVM.DATE_TO.Value.ToShortDateString().Replace("/", "-");
                    string filename = "รายงานสถานะการทำรับสินค้าเปรียบเทียบระหว่าง DO และ GR วันที่ {0} ถึง {1}";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    wb.Write(exportData);
                    vm.stockDownloadVM.EXPORT_DATA = exportData.ToArray();
                    filename = filename.Replace("{0}", dateFrom);
                    filename = filename.Replace("{1}", dateTo);

                    vm.stockDownloadVM.FILE_NAME = filename + ".xlsx";
                    vm.stockDownloadVM.CONTENT_TYPE = contentType;
                }
                #endregion

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
