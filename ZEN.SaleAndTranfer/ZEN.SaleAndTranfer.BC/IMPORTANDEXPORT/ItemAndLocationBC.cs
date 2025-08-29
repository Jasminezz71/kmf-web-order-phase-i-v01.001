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
using ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;
using ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT
{
    public class ItemAndLocationBC
    {
        public XSSFWorkbook ExportFile(ItemAndLocationVM vm, string fullPath, int batchID)
        {
            try
            {
                XSSFWorkbook wb;

                BatchBC batchBC = new BatchBC();
                batchBC.InsertBatchLog(batchID, "Export File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Process", null, vm.SessionLogin.USER_NAME);

                ItemAndLocationDC dc = new ItemAndLocationDC();
                if (vm.itemAndLocationSearchResultVM == null) vm.itemAndLocationSearchResultVM = new ItemAndLocationSearchResultVM();
                vm.itemAndLocationSearchResultVM.resultList = dc.ExportFile(vm.SessionLogin.USER_NAME); // Get data from database
                wb = this.DataToExcel(vm.itemAndLocationSearchResultVM.resultList, fullPath, batchID); // Convert data to excel files

                batchBC.InsertBatchLog(batchID, "Export File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Success", null, vm.SessionLogin.USER_NAME);
                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private XSSFWorkbook DataToExcel(List<ItemAndLocationSearchResultET> dataList, string fullPath, int batchID)
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
                            r.CreateCell(1).SetCellValue(item.ST_MAP_WH_ITEM_ID);
                            r.CreateCell(2).SetCellValue(item.REQUEST_BY_BRAND_CODE);
                            r.CreateCell(3).SetCellValue(item.REQUEST_BY_BRANCH_CODE);
                            r.CreateCell(4).SetCellValue(item.REQUEST_BY_BRANCH_NAME);
                            r.CreateCell(5).SetCellValue(item.REQUEST_TO_BRAND_CODE);
                            r.CreateCell(6).SetCellValue(item.REQUEST_TO_LOCATION_CODE);
                            r.CreateCell(7).SetCellValue(item.ST_WH_ITEM_CATEGORY_NAME);
                            r.CreateCell(8).SetCellValue(item.ITEM_CODE);
                            r.CreateCell(9).SetCellValue(item.ITEM_NAME_1);
                            r.CreateCell(10).SetCellValue(item.ITEM_NAME_2);
                            r.CreateCell(11).SetCellValue(item.REQUEST_UOM_CODE);
                            r.CreateCell(12).SetCellValue(item.DELIVERY_UOM_CODE);
                            r.CreateCell(13).SetCellValue(item.REMARK);
                            r.CreateCell(14).SetCellValue(item.ACTION);
                        }
                        else
                        {
                            r.CreateCell(0).SetCellValue(batchID);
                            r.CreateCell(1).SetCellValue(item.ST_MAP_WH_ITEM_ID);
                            r.CreateCell(2).SetCellValue(item.REQUEST_BY_BRAND_CODE);
                            r.CreateCell(3).SetCellValue(item.REQUEST_BY_BRANCH_CODE);
                            r.CreateCell(4).SetCellValue(item.REQUEST_BY_BRANCH_NAME);
                            r.CreateCell(5).SetCellValue(item.REQUEST_TO_BRAND_CODE);
                            r.CreateCell(6).SetCellValue(item.REQUEST_TO_LOCATION_CODE);
                            r.CreateCell(7).SetCellValue(item.ST_WH_ITEM_CATEGORY_NAME);
                            r.CreateCell(8).SetCellValue(item.ITEM_CODE);
                            r.CreateCell(9).SetCellValue(item.ITEM_NAME_1);
                            r.CreateCell(10).SetCellValue(item.ITEM_NAME_2);
                            r.CreateCell(11).SetCellValue(item.REQUEST_UOM_CODE);
                            r.CreateCell(12).SetCellValue(item.DELIVERY_UOM_CODE);
                            r.CreateCell(13).SetCellValue(item.REMARK);
                            r.CreateCell(14).SetCellValue(item.ACTION);
                        }
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

        public ItemAndLocationVM ValidateImportFile(HttpPostedFileBase file, ItemAndLocationVM vm)
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
        public ItemAndLocationVM ImportFile(ItemAndLocationVM vm, int batchID, string filePath)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                BatchBC batchBC = new BatchBC();
                batchBC.InsertBatchLog(batchID, "Import File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Process", null, vm.SessionLogin.USER_NAME);

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
                ItemAndLocationDC dc = new ItemAndLocationDC();
                resultBlukInsert = dc.ImportTempTable(d1);
                if (resultBlukInsert > 0)
                    batchBC.InsertBatchLog(batchID, "Bulk Insert", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Success", null, vm.SessionLogin.USER_NAME);
                #endregion

                #region ===== Validate temp table =====
                vm = this.ValidateTempTable(vm, batchID);
                if (vm.MessageList != null)
                {
                    if (vm.MessageList.Count > 0)
                    {
                        if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        {
                            batchBC.InsertBatchLog(batchID, "Validate temp table", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Success", null, vm.SessionLogin.USER_NAME);
                            return vm;
                        }
                    }
                }
                #endregion

                #region ===== ImportFile =====
                //int result = 0;
                string result = string.Empty;
                result = dc.ImportFile(batchID);
                if (result == "1") // Transection Success
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00006));
                    batchBC.InsertBatchLog(batchID, "Import File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Success", "Insert , Update , Delete ข้อมูลสำเร็จ", vm.SessionLogin.USER_NAME);
                }
                else
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00011));
                    batchBC.InsertBatchLog(batchID, "Import File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Error", result, vm.SessionLogin.USER_NAME);
                    //batchBC.InsertBatchLog(batchID, "Import File", ConfigNameEnum.WH_MAP_ITEM.ToString(), "Error", "Insert , Update , Delete ข้อมูลไม่สำเร็จ", vm.SessionLogin.USER_NAME);
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
            DataTable d1 = new DataTable("TB_T_ST_MAP_WH_ITEM");
            d1.Columns.Add(new DataColumn("ROW_NO", typeof(int)));
            d1.Columns.Add(new DataColumn("BATCH_ID", typeof(string)));
            d1.Columns.Add(new DataColumn("BATCH_ID_DOWNLOAD", typeof(string)));
            d1.Columns.Add(new DataColumn("ST_MAP_WH_ITEM_ID", typeof(string)));
            d1.Columns.Add(new DataColumn("REQUEST_BY_BRAND_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("REQUEST_BY_BRANCH_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("REQUEST_TO_BRAND_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("REQUEST_TO_LOCATION_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("ST_WH_ITEM_CATEGORY_NAME", typeof(string)));
            d1.Columns.Add(new DataColumn("ITEM_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("ITEM_NAME_1", typeof(string)));
            d1.Columns.Add(new DataColumn("ITEM_NAME_2", typeof(string)));
            d1.Columns.Add(new DataColumn("REQUEST_UOM_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("DELIVERY_UOM_CODE", typeof(string)));
            d1.Columns.Add(new DataColumn("REMARK", typeof(string)));
            d1.Columns.Add(new DataColumn("ACTION", typeof(string)));
            d1.Columns.Add(new DataColumn("STATUS", typeof(string)));
            d1.Columns.Add(new DataColumn("STATUS_DETAIL", typeof(string)));
            d1.Columns.Add(new DataColumn("CREATE_BY", typeof(string)));
            //d1.Columns.Add(new DataColumn("CREATE_DATE", typeof(DateTime)));
            d1.Columns.Add(new DataColumn("CREATE_DATE", typeof(string)));
            d1.Columns.Add(new DataColumn("UPDATE_BY", typeof(string)));
            //d1.Columns.Add(new DataColumn("UPDATE_DATE", typeof(DateTime)));
            d1.Columns.Add(new DataColumn("UPDATE_DATE", typeof(string)));
            return d1;
        }
        private DataTable SetDataTable(DataTable d1, ISheet sheet, int batchID, ItemAndLocationVM vm)
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
                    rowH["ST_MAP_WH_ITEM_ID"] = null;
                    rowH["REQUEST_BY_BRAND_CODE"] = null;
                    rowH["REQUEST_BY_BRANCH_CODE"] = null;
                    rowH["REQUEST_TO_BRAND_CODE"] = null;
                    rowH["REQUEST_TO_LOCATION_CODE"] = null;
                    rowH["ST_WH_ITEM_CATEGORY_NAME"] = null;
                    rowH["ITEM_CODE"] = null;
                    rowH["ITEM_NAME_1"] = null;
                    rowH["ITEM_NAME_2"] = null;
                    rowH["REQUEST_UOM_CODE"] = null;
                    rowH["DELIVERY_UOM_CODE"] = null;
                    rowH["REMARK"] = null;
                    rowH["ACTION"] = null;
                    rowH["STATUS"] = null;
                    rowH["STATUS_DETAIL"] = null;
                    rowH["CREATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["CREATE_DATE"] = currentDate;
                    rowH["UPDATE_BY"] = vm.SessionLogin.USER_NAME;
                    rowH["UPDATE_DATE"] = currentDate;

                    for (int col = 0; col <= 14; col++)
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
                        else if (col == 1)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ST_MAP_WH_ITEM_ID"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ST_MAP_WH_ITEM_ID"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 2)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REQUEST_BY_BRAND_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REQUEST_BY_BRAND_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 3)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REQUEST_BY_BRANCH_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REQUEST_BY_BRANCH_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 5)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REQUEST_TO_BRAND_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REQUEST_TO_BRAND_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 6)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REQUEST_TO_LOCATION_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REQUEST_TO_LOCATION_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 7)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ST_WH_ITEM_CATEGORY_NAME"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ST_WH_ITEM_CATEGORY_NAME"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 8)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ITEM_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 9)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ITEM_NAME_1"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ITEM_NAME_1"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 10)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ITEM_NAME_2"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ITEM_NAME_2"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 11)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REQUEST_UOM_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REQUEST_UOM_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 12)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["DELIVERY_UOM_CODE"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["DELIVERY_UOM_CODE"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 13)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["REMARK"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["REMARK"] = sheet.GetRow(row).GetCell(col).StringCellValue;
                            }
                        }
                        else if (col == 14)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(row).GetCell(col).CellType.ToString() == "Numeric")
                                    rowH["ACTION"] = sheet.GetRow(row).GetCell(col).NumericCellValue;
                                else
                                    rowH["ACTION"] = sheet.GetRow(row).GetCell(col).StringCellValue;
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
        private ItemAndLocationVM ValidateTempTable(ItemAndLocationVM vm, int batchID)
        {
            try
            {
                vm.MessageList.Clear();
                List<MessageET> messageList = new List<MessageET>();
                ItemAndLocationDC dc = new ItemAndLocationDC();
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
