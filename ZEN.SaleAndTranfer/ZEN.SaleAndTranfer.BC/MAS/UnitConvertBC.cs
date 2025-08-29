using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class UnitConvertBC
    {
        #region ---- Innitial ----

        public UnitConvertVM InnitialCriteria(UnitConvertVM vm)
        {
            try
            {
                if (vm.unitConvertSearchCriteriaVM == null) { vm.unitConvertSearchCriteriaVM = new UnitConvertSearchCriteriaVM(); }
                var ddlDC = new DDLDC();
                var brandList = ddlDC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = ddlDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);

                if (vm.unitConvertSearchCriteriaVM.BRAND_CODE != null)
                    branchList = ddlDC.GetBranchbyBrandAndUsername(vm.unitConvertSearchCriteriaVM.BRAND_CODE, vm.SessionLogin.USER_NAME, DDLModeEnumET.SELECT_ALL);
                vm.unitConvertSearchCriteriaVM.brandList = brandList;
                vm.unitConvertSearchCriteriaVM.branchList = branchList;

                if (vm.unitConvertSearchCriteriaVM.BRAND_CODE == null && vm.unitConvertSearchCriteriaVM.BRANCH_CODE == null) // null and null
                {
                    vm.unitConvertSearchCriteriaVM.BRAND_CODE_VIEW_FLAG = "1";
                    vm.unitConvertSearchCriteriaVM.BRANCH_CODE_VIEW_FLAG = "0";
                }
                else if (vm.unitConvertSearchCriteriaVM.BRAND_CODE != null && vm.unitConvertSearchCriteriaVM.BRANCH_CODE == null) // ott and null
                {
                    vm.unitConvertSearchCriteriaVM.BRAND_CODE_VIEW_FLAG = "1";
                    vm.unitConvertSearchCriteriaVM.BRANCH_CODE_VIEW_FLAG = "1";
                }
                else if (vm.unitConvertSearchCriteriaVM.BRAND_CODE == null && vm.unitConvertSearchCriteriaVM.BRANCH_CODE != null) // null and ott15
                {
                    vm.unitConvertSearchCriteriaVM.BRAND_CODE_VIEW_FLAG = "1";
                    vm.unitConvertSearchCriteriaVM.BRANCH_CODE_VIEW_FLAG = "0";
                    vm.unitConvertSearchCriteriaVM.BRANCH_CODE = null;
                }
                else // ott and ott05
                {
                    vm.unitConvertSearchCriteriaVM.BRAND_CODE_VIEW_FLAG = "1";
                    vm.unitConvertSearchCriteriaVM.BRANCH_CODE_VIEW_FLAG = "1";
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UnitConvertVM InnitialMA(UnitConvertVM vm)
        {
            try
            {
                //if (vm.unitConvertVM_MA == null) { vm.unitConvertVM_MA = new UnitConvertET_MA(); }
                //var ddlDC = new DDLDC();
                //var appSystemList = ddlDC.GetAppSystem(DDLModeEnumET.SELECT_ALL);

                //vm.unitConvertVM_MA.appSystemList = appSystemList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public UnitConvertVM Search(UnitConvertVM vm)
        {
            try
            {
                int countAll = 0;
                if (vm.unitConvertSearchResultVM == null) vm.unitConvertSearchResultVM = new UnitConvertSearchResultVM();
                if (vm.unitConvertSearchResultVM.resultList == null) vm.unitConvertSearchResultVM.resultList = new List<UnitConvertSearchResultET>();
                UnitConvertDC dc = new UnitConvertDC();
                vm.unitConvertSearchResultVM.resultList = dc.Search(vm.unitConvertSearchCriteriaVM, out countAll);
                vm.unitConvertSearchResultVM.countAll = countAll;
                if (vm.unitConvertSearchResultVM.countAll == 0) vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00007));

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public XSSFWorkbook ExportFile(UnitConvertVM vm, string fullPath, int batchID)
        {
            try
            {
                XSSFWorkbook wb;

                BatchBC batchBC = new BatchBC();
                batchBC.InsertBatchLog(batchID, "Export File", "UnitConvert", "Process", null, vm.SessionLogin.USER_NAME);

                //UnitConvertDC dc = new UnitConvertDC();
                //if (vm.unitConvertSearchResultVM == null) vm.unitConvertSearchResultVM = new UnitConvertSearchResultVM();
                //vm.unitConvertSearchResultVM.resultList = dc.ExportFile(); // Get data from database
                wb = this.DataToExcel(vm.unitConvertSearchResultVM.resultList, fullPath, batchID); // Convert data to excel files

                batchBC.InsertBatchLog(batchID, "Export File", "UnitConvert", "Success", null, vm.SessionLogin.USER_NAME);
                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private XSSFWorkbook DataToExcel(List<UnitConvertSearchResultET> dataList, string fullPath, int batchID)
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
                    var r = sh.CreateRow(0);
                    r.CreateCell(0).SetCellValue("BATCH_ID");
                    r.CreateCell(1).SetCellValue("แบรนด์");
                    r.CreateCell(2).SetCellValue("สาขา");
                    r.CreateCell(3).SetCellValue("รหัสสินค้า");
                    r.CreateCell(4).SetCellValue("ชื่อสินค้า");
                    r.CreateCell(5).SetCellValue("หน่วยในระบบ");
                    r.CreateCell(6).SetCellValue("หน่วยนับ");
                    r.CreateCell(7).SetCellValue("หน่วยนับที่ต้องการเปลี่ยน");
                    r.CreateCell(8).SetCellValue("ตัวคูณ");
                    r.CreateCell(9).SetCellValue("ตัวหาร");
                    r.CreateCell(10).SetCellValue("ค่าคงที่");

                    for (int i = 0; i < dataList.Count; i++)
                    {
                        r = sh.CreateRow(i + 1);
                        var item = dataList[i];
                        r.CreateCell(0).SetCellValue(batchID);
                        r.CreateCell(1).SetCellValue(item.BRAND_CODE);
                        r.CreateCell(2).SetCellValue(item.BRANCH_CODE);
                        r.CreateCell(3).SetCellValue(item.ITEM_CODE);
                        r.CreateCell(4).SetCellValue(item.ITEM_NAME);
                        r.CreateCell(5).SetCellValue(item.UNIT_FROM);
                        r.CreateCell(6).SetCellValue(item.UNIT_TO);
                        r.CreateCell(7).SetCellValue(item.NEW_UNIT_TO);
                        r.CreateCell(8).SetCellValue(item.DIV);
                        r.CreateCell(9).SetCellValue(item.MUL);
                        r.CreateCell(10).SetCellValue(item.CONST);
                    }

                    using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        wb.Write(fs);
                    }
                }

                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public UnitConvertVM ValidateImportFile(HttpPostedFileBase file, UnitConvertVM vm)
        {
            try
            {
                vm.MessageList.Clear();
                if (file.ContentLength == 0 && file.FileName == "") // ไม่ได้เลือกไฟล์ที่ต้องการอัพโหลด
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00004));
                }
                else // ทำการเลือกไฟล์ที่ต้องการอัพโหลด
                {
                    var maxSize = Convert.ToDecimal(ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.MAX_SIZE));
                    var type = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.TYPE);
                    if (file.ContentLength > (maxSize * 1048576)) // ขนาดใหญ่เกินกำหนด
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00005, maxSize.ToString()));
                    }
                    string[] split = file.FileName.Split('.');
                    if (split[1].ToLower() != type.ToLower()) // ไฟล์ไม่ตรงกำหนด
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00002, type.ToUpper()));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UnitConvertVM ImportFile(UnitConvertVM vm, int batchID, string filePath)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                BatchBC batchBC = new BatchBC();
                batchBC.InsertBatchLog(batchID, "Import File", "การเปลี่ยนหน่วยสินค้าของ STOCK", "Process", null, vm.SessionLogin.USER_NAME);

                #region ===== Read Excel =====
                // Read File Excel
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(file);
                }

                // Get data from excel
                DataTable d1 = this.CreateTable();
                ISheet sheet = wb.GetSheetAt(0);
                d1 = this.SetDataTable(d1, sheet, batchID, vm);
                #endregion

                #region ===== Bluk Insert temp table =====
                int resultBlukInsert = 0;
                UnitConvertDC dc = new UnitConvertDC();
                resultBlukInsert = dc.ImportTempTable(d1);
                if (resultBlukInsert > 0)
                    batchBC.InsertBatchLog(batchID, "Bulk Insert", "การเปลี่ยนหน่วยสินค้า", "Success", null, vm.SessionLogin.USER_NAME);
                #endregion

                #region ===== Validate temp table =====
                vm = this.ValidateTempTable(vm, batchID);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        {
                            batchBC.InsertBatchLog(batchID, "Validate temp table", "การเปลี่ยนหน่วยสินค้า", "Error", null, vm.SessionLogin.USER_NAME);
                            return vm;
                        }
                        else batchBC.InsertBatchLog(batchID, "Validate temp table", "การเปลี่ยนหน่วยสินค้า", "Success", null, vm.SessionLogin.USER_NAME);
                    }
                    else batchBC.InsertBatchLog(batchID, "Validate temp table", "การเปลี่ยนหน่วยสินค้า", "Success", null, vm.SessionLogin.USER_NAME);
                }
                else batchBC.InsertBatchLog(batchID, "Validate temp table", "การเปลี่ยนหน่วยสินค้า", "Success", null, vm.SessionLogin.USER_NAME);
                #endregion

                #region ===== ImportFile =====
                //int result = 0;
                string result = string.Empty;
                result = dc.ImportFile(batchID);
                if (result == "1") // Transection Success
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00006));
                    batchBC.InsertBatchLog(batchID, "Import File", "การเปลี่ยนหน่วยสินค้า", "Success", "Insert , Update , Delete ข้อมูลสำเร็จ", vm.SessionLogin.USER_NAME);
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                    batchBC.InsertBatchLog(batchID, "Import File", "การเปลี่ยนหน่วยสินค้า", "Error", result, vm.SessionLogin.USER_NAME);
                    //batchBC.InsertBatchLog(batchID, "Import File", "การเปลี่ยนหน่วยสินค้า", "Error", "Insert , Update , Delete ข้อมูลไม่สำเร็จ", vm.SessionLogin.USER_NAME);
                }
                #endregion

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable CreateTable()
        {
            DataTable d1 = new DataTable("TB_T_STOCK_UNIT_CONVERT");
            d1.Columns.Add(new DataColumn("ROW_NO", typeof(int)));
            d1.Columns.Add(new DataColumn("BATCH_ID", typeof(string)));
            d1.Columns.Add(new DataColumn("BATCH_ID_DOWNLOAD", typeof(string)));
            d1.Columns.Add(new DataColumn("ITEM_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("BRAND_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("BRANCH_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("UNIT_FROM", typeof(string)));
            d1.Columns.Add(new DataColumn("UNIT_TO", typeof(string)));
            d1.Columns.Add(new DataColumn("NEW_UNIT_TO", typeof(string)));
            d1.Columns.Add(new DataColumn("MUL", typeof(string)));
            d1.Columns.Add(new DataColumn("DIV", typeof(string)));
            d1.Columns.Add(new DataColumn("CONST", typeof(string)));
            d1.Columns.Add(new DataColumn("STATUS", typeof(string)));
            d1.Columns.Add(new DataColumn("STATUS_DETAIL", typeof(string)));
            d1.Columns.Add(new DataColumn("REMARK", typeof(string)));
            d1.Columns.Add(new DataColumn("CREATE_BY", typeof(string)));
            d1.Columns.Add(new DataColumn("CREATE_DATE", typeof(DateTime)));
            d1.Columns.Add(new DataColumn("UPDATE_BY", typeof(string)));
            d1.Columns.Add(new DataColumn("UPDATE_DATE", typeof(DateTime)));
            return d1;
        }
        private DataTable SetDataTable(DataTable d1, ISheet sheet, int batchID, UnitConvertVM vm)
        {
            try
            {
                DataRow rowH = null;
                DateTime currentDate = DateTime.Now;

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    // Set data all row
                    rowH = d1.NewRow();
                    rowH["ROW_NO"] = row + 1;
                    rowH["BATCH_ID"] = batchID;
                    rowH["BATCH_ID_DOWNLOAD"] = null;
                    rowH["ITEM_CODE"] = null;
                    rowH["BRAND_CODE"] = null;
                    rowH["BRANCH_CODE"] = null;
                    rowH["UNIT_FROM"] = null;
                    rowH["UNIT_TO"] = null;
                    rowH["NEW_UNIT_TO"] = null;
                    rowH["MUL"] = null;
                    rowH["DIV"] = null;
                    rowH["CONST"] = null;
                    rowH["STATUS"] = null;
                    rowH["STATUS_DETAIL"] = null;
                    rowH["REMARK"] = null;
                    rowH["CREATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["CREATE_DATE"] = currentDate;
                    rowH["UPDATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["UPDATE_DATE"] = currentDate;

                    for (int col = 0; col <= 10; col++)
                    {
                        if (col == 0)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["BATCH_ID_DOWNLOAD"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["BATCH_ID_DOWNLOAD"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        if (col == 1)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["BRAND_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["BRAND_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 2)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["BRANCH_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["BRANCH_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 3)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 4)
                        {
                            continue;
                            //if (sheet.GetRow(row).GetCell(col) != null)
                            //{
                            //    if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                            //        rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                            //    else
                            //        rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            //}
                        }
                        else if (col == 5)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["UNIT_FROM"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["UNIT_FROM"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 6)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["UNIT_TO"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["UNIT_TO"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 7)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["NEW_UNIT_TO"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["NEW_UNIT_TO"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 8)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["MUL"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["MUL"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 9)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["DIV"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["DIV"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 10)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["CONST"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["CONST"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                    }

                    d1.Rows.Add(rowH);
                }

                return d1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private UnitConvertVM ValidateTempTable(UnitConvertVM vm, int batchID)
        {
            try
            {
                vm.MessageList.Clear();
                List<MessageET> messageList = new List<MessageET>();
                UnitConvertDC dc = new UnitConvertDC();
                messageList = dc.ValidateTempTable(batchID, vm.SessionLogin.USER_NAME);

                if (messageList != null)
                {
                    if (messageList.Count > 0)
                    {
                        MessageET messageET = new MessageET();
                        foreach (var item in messageList)
                        {
                            messageET = new MessageET();
                            messageET.MESSAGE_CODE = item.MESSAGE_CODE;
                            messageET.MESSAGE_CODE_DISP = item.MESSAGE_CODE_DISP;
                            messageET.MESSAGE_TYPE = item.MESSAGE_TYPE;
                            messageET.MESSAGE_TEXT_FOR_DISPLAY = item.MESSAGE_TEXT_FOR_DISPLAY;
                            messageET.MESSAGE_TEXT = item.MESSAGE_TEXT;
                            messageET.REMARK = item.REMARK;
                            messageET.ACTIVE_FLAG = item.ACTIVE_FLAG;
                            vm.MessageList.Add(messageET);
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
    }
}
