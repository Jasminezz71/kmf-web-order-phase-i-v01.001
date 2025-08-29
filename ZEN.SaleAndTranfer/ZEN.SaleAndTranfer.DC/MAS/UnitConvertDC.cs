using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.DC.MAS
{
    public class UnitConvertDC : BaseDC
    {
        public List<UnitConvertSearchResultET> Search(UnitConvertSearchCriteriaET data, out int countAll)
        {
            try
            {
                countAll = 0;
                List<UnitConvertSearchResultET> result = new List<UnitConvertSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_STOCK_UNIT_CONVERT_Search, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new UnitConvertSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt16(reader["ROW_NO"]);
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME = reader["ITEM_NAME"].ToString();
                            newInstance.UNIT_FROM = reader["UNIT_FROM"].ToString();
                            newInstance.UNIT_TO = reader["UNIT_TO"].ToString();
                            newInstance.NEW_UNIT_TO = reader["NEW_UNIT_TO"].ToString();
                            newInstance.MUL = reader["MUL"].ToString();
                            newInstance.DIV = reader["DIV"].ToString();
                            newInstance.CONST = reader["CONST"].ToString();
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_STOCK_UNIT_CONVERT_Search);
                throw ex;
            }
        }


        public int ImportTempTable(DataTable d1) // Bluk Insert Not use Store
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = d1.TableName;
                        bulkCopy.BulkCopyTimeout = getSqlTimeout();

                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(d1);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", "Bluk Insert " + d1.TableName);
                throw ex;
            }
        }
        public List<MessageET> ValidateTempTable(int batchID, string userName) // Use Store to connect DB
        {
            try
            {
                List<MessageET> result = new List<MessageET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_STOCK_UNIT_CONVERT_validate_Upload, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = getSqlTimeout();

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BATCH_ID", batchID);
                        cm.Parameters.AddWithValue("@P_USER_NAME", userName);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new MessageET();

                            newInstance.MESSAGE_CODE = reader["MESSAGE_CODE"].ToString();
                            newInstance.MESSAGE_CODE_DISP = reader["MESSAGE_CODE_DISP"].ToString();
                            newInstance.MESSAGE_TYPE = reader["MESSAGE_TYPE"].ToString();
                            newInstance.MESSAGE_TEXT_FOR_DISPLAY = reader["MESSAGE_TEXT_FOR_DISPLAY"].ToString();
                            newInstance.MESSAGE_TEXT = reader["MESSAGE_TEXT"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_STOCK_UNIT_CONVERT_validate_Upload);
                throw ex;
            }
        }
        public string ImportFile(int batchID) // Use Store to connect DB
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_STOCK_UNIT_CONVERT_Upload, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = getSqlTimeout();

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BATCH_ID", batchID);
                        #endregion

                        var result = cm.ExecuteNonQuery();
                        return "1";
                        //return Convert.ToInt16(cm.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_STOCK_UNIT_CONVERT_Upload);
                throw ex;
            }
        }
    }
}
