using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.DC.MAS
{
    public class RecTransferDC
    {
        public List<RecTransferSearchResultET> Search(RecTransferSearchCriteriaET data, out int countAll)
        {
            try
            {
                countAll = 0;
                List<RecTransferSearchResultET> result = new List<RecTransferSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_RECEIVE_TRANSFER, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_RECEIVE_DATE_FROM", data.RECEIVE_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_RECEIVE_DATE_TO", data.RECEIVE_DATE_TO);
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.PAGE_SIZE);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new RecTransferSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRAND_NAME = reader["BRAND_NAME"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            newInstance.ST_GR_CODE = reader["ST_GR_CODE"].ToString();
                            newInstance.RECEIVE_DATE = reader["RECEIVE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["RECEIVE_DATE"]) : null;
                            newInstance.LOCATION = reader["LOCATION"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.SEND_QTY = reader["SEND_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["SEND_QTY"]) : null;
                            newInstance.SEND_UOM = reader["SEND_UOM"].ToString();
                            newInstance.RECEIVE_QTY = reader["RECEIVE_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["RECEIVE_QTY"]) : null;
                            newInstance.RECEIVE_UOM = reader["RECEIVE_UOM"].ToString();
                            newInstance.QUANTITY_DIFF = reader["QUANTITY_DIFF"].ToString();

                            result.Add(newInstance);
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                countAll = Convert.ToInt32(reader["COUNT_ALL"]);
                            }
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_RECEIVE_TRANSFER);
                throw ex;
            }
        }
        public List<RecTransferSearchResultET> ExportFile(RecTransferSearchCriteriaET data)
        {
            try
            {
                List<RecTransferSearchResultET> result = new List<RecTransferSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_RPT_ST_RECEIVE_TRANSFER, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_RECEIVE_DATE_FROM", data.RECEIVE_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_RECEIVE_DATE_TO", data.RECEIVE_DATE_TO);
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new RecTransferSearchResultET();

                            newInstance.EXPORT_ROW_NO = reader["ROW_NO"].ToString();
                            newInstance.EXPORT_BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.EXPORT_BRAND_NAME = reader["BRAND_NAME"].ToString();
                            newInstance.EXPORT_BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.EXPORT_BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.EXPORT_ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            newInstance.EXPORT_ST_GR_CODE = reader["ST_GR_CODE"].ToString();
                            newInstance.EXPORT_RECEIVE_DATE = reader["RECEIVE_DATE"].ToString();
                            newInstance.EXPORT_LOCATION = reader["LOCATION"].ToString();
                            newInstance.EXPORT_ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.EXPORT_SEND_QTY = reader["SEND_QTY"].ToString();
                            newInstance.EXPORT_SEND_UOM = reader["SEND_UOM"].ToString();
                            newInstance.EXPORT_RECEIVE_QTY = reader["RECEIVE_QTY"].ToString();
                            newInstance.EXPORT_RECEIVE_UOM = reader["RECEIVE_UOM"].ToString();
                            newInstance.QUANTITY_DIFF = reader["QUANTITY_DIFF"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_RPT_ST_RECEIVE_TRANSFER);
                throw ex;
            }
        }
    }
}
