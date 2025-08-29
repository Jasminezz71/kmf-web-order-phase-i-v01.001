using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT
{
    public class ImportFileBC
    {
        private DataTable CreateTempTable()
        {
            DataTable dtTemp = new DataTable("TB_T_ST_MAP_WH_ITEM");
            #region -- dtTemp
            dtTemp.Columns.Add(new DataColumn("ST_MAP_WH_ITEM_ID", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("BATCH_NO", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REQEUST_BY_BRAND_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REQEUST_BY_BRANCH_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REQUEST_TO_BRAND_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REQUEST_TO_LOCATION_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("ST_WH_ITEM_CATEGORY_ID", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("ITEM_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REQUEST_UOM_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("DELIVERY_UOM_CODE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("USE_START_DATE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("USE_END_DATE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("ACTIVE_FLAG", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("REMARK", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("CREATE_BY", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("CREATE_DATE", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("UPDATE_BY", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("UPDATE_DATE", typeof(string)));
            #endregion

            return dtTemp;
        }

        public void ImportFile(int batchID, string IMPORT_BY, string IMPORT_DATE, string BATCH_NAME, string APP_NAME, string BRAND_CODE)
        {
            try
            {
                //int counter = 0;
                //string line;

                //DataRow rowH = null;
                //int index = 1;
                //int H_ID = 0;
                //int CREDIT_TYPE_ID = 0;

                //Get file from upload files
                //var filePaths = Directory.GetFiles("");
                //var filePaths = Directory.GetFiles(IMPORT_KTC_INPUT_FOLDER);

                //foreach (var filePath in filePaths)
                //{
                //    var d1 = this.CreateTempTable();

                //    // Read the file and display it line by line.
                //    StreamReader file = new StreamReader(filePath);
                //    while ((line = file.ReadLine()) != null)
                //    {
                //        if (line.Substring(0, 13) == "REPORT DATE :")
                //        {
                //            CREDIT_TYPE_ID = 0;
                //            H_ID++;
                //            rowH = d1.NewRow();
                //            try
                //            {
                //                d1.Rows.Add(rowH);
                //            }
                //            catch (Exception ex)
                //            {
                //                throw ex;
                //            }
                //            rowH["H_ID"] = H_ID;
                //            rowH["BATCH_ID"] = batchID;
                //            rowH["BRAND_CODE"] = BRAND_CODE;
                //            rowH["REF_BATCH_ID"] = -99;
                //            rowH["CREATE_BY"] = IMPORT_BY;
                //            rowH["UPDATE_BY"] = IMPORT_BY;
                //            rowH["REPORT_DATE"] = line.Substring(14, 8).Trim();
                //        }
                //        else if (line.Substring(131, 6) == "PAGE :")
                //        {

                //            rowH["PAGE_NO"] = line.Substring(137, 9).Trim();

                //        }
                //        else if (line.Substring(0, 13) == "MERCHANT-NO :")
                //        {
                //            rowH["MERCHANT_NO"] = line.Substring(15, 9).Trim();
                //            rowH["BRANCH_NAME"] = line.Substring(25, 49).Trim();
                //        }
                //        else if (line.Substring(0, 4) == "9043")
                //        {
                //            var rowD = d2.NewRow();
                //            rowD["D_ID"] = Convert.ToInt32(line.Substring(20, 8).Trim());
                //            rowD["H_ID"] = H_ID;
                //            rowD["BATCH_ID"] = batchID;
                //            rowD["TERMINAL"] = line.Substring(0, 8).Trim();
                //            rowD["SEQ_NO"] = line.Substring(20, 8).Trim();
                //            rowD["CREDIT_NO"] = line.Substring(29, 19).Trim();
                //            rowD["TRANS_DATE"] = line.Substring(94, 10).Trim();
                //            rowD["TRANS_TIME"] = line.Substring(106, 8).Trim();
                //            rowD["AMOUNT"] = line.Substring(122, 22).Trim();
                //            rowD["CREATE_BY"] = IMPORT_BY;
                //            rowD["UPDATE_BY"] = IMPORT_BY;

                //            d2.Rows.Add(rowD);
                //        }
                //        else if (line.Substring(0, 15) == "      KTC ON-US")
                //        {
                //            CREDIT_TYPE_ID++;
                //            var rowE = d3.NewRow();

                //            rowE["H_ID"] = H_ID;
                //            rowE["BATCH_ID"] = batchID;
                //            rowE["CREDIT_TYPE_ID"] = CREDIT_TYPE_ID;
                //            rowE["CREDIT_TYPE_NAME"] = line.Substring(0, 44).Trim();
                //            rowE["ITEMS"] = line.Substring(45, 5).Trim();
                //            rowE["AMOUNT"] = line.Substring(50, 25).Trim();
                //            rowE["CREATE_BY"] = IMPORT_BY;
                //            rowE["UPDATE_BY"] = IMPORT_BY;

                //            d3.Rows.Add(rowE);
                //        }

                //        index++;
                //        counter++;
                //    }

                //    file.Close();

                //    var dc = new ImportFileDC();
                //    dc.ImportToDB(d1);
                //} // foreach
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
