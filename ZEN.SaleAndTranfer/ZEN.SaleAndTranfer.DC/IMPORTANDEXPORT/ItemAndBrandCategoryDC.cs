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
    public class ItemAndBrandCategoryDC : BaseDC
    {
        public List<ItemAndBrandCategorySearchResultET> ExportFile()
        {
            try
            {
                List<ItemAndBrandCategorySearchResultET> result = new List<ItemAndBrandCategorySearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_Download_BRAND_MAP_BRAND_CATEGORY, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = getSqlTimeout();

                        #region -- set param --
                        //cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new ItemAndBrandCategorySearchResultET();

                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.ACTION = reader["ACTION"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_Download_BRAND_MAP_BRAND_CATEGORY);
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

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_validate_Upload_MAP_PR_CATEGORY_BRAND, conn))
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_validate_Upload_MAP_PR_CATEGORY_BRAND);
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

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_Upload_MAP_PR_CATEGORY_BRAND, conn))
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_Upload_MAP_PR_CATEGORY_BRAND);
                throw ex;
            }
        }
    }
}
