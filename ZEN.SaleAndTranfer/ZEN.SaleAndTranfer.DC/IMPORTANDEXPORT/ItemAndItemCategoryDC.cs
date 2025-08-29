using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.CNF;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT
{
    public class ItemAndItemCategoryDC : BaseDC
    {
        public List<ItemAndItemCategorySearchResultET> ExportFile()
        {
            try
            {
                var result = new List<ItemAndItemCategorySearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_Download_BRAND_MAP_ITEM_CATEGORY, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = getSqlTimeout();

                        #region -- set param --
                        //cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new ItemAndItemCategorySearchResultET();

                            newInstance.ITEM_CODE = reader["ITEM_CODE"] != null && reader["ITEM_CODE"] != DBNull.Value ? (string)reader["ITEM_CODE"] : null;
                            newInstance.ITEM_NAME_1 = reader["ITEM_NAME_1"] != null && reader["ITEM_NAME_1"] != DBNull.Value ? (string)reader["ITEM_NAME_1"] : null;
                            newInstance.ITEM_NAME_2 = reader["ITEM_NAME_2"] != null && reader["ITEM_NAME_2"] != DBNull.Value ? (string)reader["ITEM_NAME_2"] : null;
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"] != null && reader["ST_PR_CATEGORY_NAME"] != DBNull.Value ? (string)reader["ST_PR_CATEGORY_NAME"] : null;
                            newInstance.REMARK = reader["REMARK"] != null && reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            newInstance.ACTION = reader["ACTION"] != null && reader["ACTION"] != DBNull.Value ? (string)reader["ACTION"] : null;
                            newInstance.DISP_ORDER = reader["DISP_ORDER"] != null && reader["DISP_ORDER"] != DBNull.Value ? (string)reader["DISP_ORDER"] : null;
                            
                            
                            result.Add(newInstance);

                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_Download_BRAND_MAP_ITEM_CATEGORY);
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

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_validate_Upload_MAP_PR_CATEGORY_ITEM, conn))
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_validate_Upload_MAP_PR_CATEGORY_ITEM);
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

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_Upload_MAP_PR_CATEGORY_ITEM, conn))
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_Upload_MAP_PR_CATEGORY_ITEM);
                throw ex;
            }
        }
    }
}
