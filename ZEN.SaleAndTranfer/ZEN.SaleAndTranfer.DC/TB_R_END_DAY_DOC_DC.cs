using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.DC.LOG;

namespace ZEN.SaleAndTranfer.DC
{
    public class TB_R_END_DAY_DOC_DC : BaseDC
    {
        /// <summary>
        /// call store procedure : USP_R_END_DAY_DOC_InsertData
        /// </summary>
        /// <param name="COMPLAIN_TYPE">COMPLAIN_TYPE</param>
        /// <returns>List<USP_R_END_DAY_DOC_InsertData_PET></returns>
        public bool SaveEndDayDocData(USP_R_END_DAY_DOC_Insert_PET pet)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new List<USP_R_END_DAY_DOC_Insert_PET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    if (pet != null)
                    {
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_Insert, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_BRAND_CODE", pet.BRAND_CODE);
                            cm.Parameters.AddWithValue("@P_BRANCH_CODE", pet.BRANCH_CODE);
                            cm.Parameters.AddWithValue("@P_END_DAY_DATE", pet.END_DAY_DATE);
                            cm.Parameters.AddWithValue("@P_END_DAY_DOC_TYPE_ID", pet.END_DAY_DOC_TYPE_ID);
                            cm.Parameters.AddWithValue("@P_FILE_NAME_ORI", pet.FILE_NAME_ORI);
                            cm.Parameters.AddWithValue("@P_FILE_NAME_DEST", pet.FILE_NAME_DEST);
                            cm.Parameters.AddWithValue("@P_FILE_PATH", pet.FILE_PATH);
                            cm.Parameters.AddWithValue("@P_FILE_SIZE", pet.FILE_SIZE);
                            cm.Parameters.AddWithValue("@P_FILE_CONTENT_TYPE", pet.FILE_CONTENT_TYPE);
                            cm.Parameters.AddWithValue("@P_CREATE_BY", pet.CREATE_BY);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveEndDayDocPath(List<USP_R_END_DAY_DOC_Insert_PET> endDayDocData)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new List<USP_R_END_DAY_DOC_Insert_PET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    
                    if (endDayDocData != null && endDayDocData.Count > 0)
                    {
                        foreach (var data in endDayDocData)
                        {
                            //using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_InsertData, conn))
                            //{
                            //    cm.Transaction = transaction;
                            //    cm.CommandType = CommandType.StoredProcedure;

                            //    #region -- set param --
                            //    cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                            //    cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                            //    cm.Parameters.AddWithValue("@P_REPORT_DATE", data.END_DAY_DATE);
                            //    cm.Parameters.AddWithValue("@P_REPORT_TYPE", data.END_DAY_DOC_TYPE_ID);
                            //    cm.Parameters.AddWithValue("@P_FILE_NAME_ORIGIN", data.FILE_NAME_ORI);
                            //    cm.Parameters.AddWithValue("@P_FILE_NAME_DESTINATION", data.FILE_NAME_DEST);
                            //    cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                            //    #endregion

                            //    cm.ExecuteNonQuery();
                            //}
                        }
                    }
                    transaction.Commit();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEndDayDocData(USP_R_END_DAY_DOC_Delete_PET pet)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new List<USP_R_END_DAY_DOC_Delete_PET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    if (pet != null)
                    {
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_Delete, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_END_DAY_DOC_ID", pet.END_DAY_DOC_ID);
                            cm.Parameters.AddWithValue("@P_DELETE_BY", pet.UPDATE_BY);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_END_DAY_DOC_Delete);
                throw ex;
            }
        }
        public bool UnDeleteEndDayDocData(USP_R_END_DAY_DOC_Delete_PET pet)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new List<USP_R_END_DAY_DOC_Delete_PET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    if (pet != null)
                    {
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_UnDelete, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_END_DAY_DOC_ID", pet.END_DAY_DOC_ID);
                            cm.Parameters.AddWithValue("@P_DELETE_BY", pet.UPDATE_BY);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_END_DAY_DOC_UnDelete);
                throw ex;
            }
        }

        public USP_R_END_DAY_DOC_GetDataByPK_RET GetDataByPK(int id)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new USP_R_END_DAY_DOC_GetDataByPK_RET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                   
                    using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_GetByPK, conn))
                    {
                        cm.Transaction = transaction;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_END_DAY_DOC_ID", id);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            result.END_DAY_DOC_ID = Convert.ToInt32(reader["END_DAY_DOC_ID"]);
                            result.BRAND_CODE = reader["BRAND_CODE"] != DBNull.Value ? (string)reader["BRAND_CODE"] : null;
                            result.BRANCH_CODE = reader["BRANCH_CODE"] != DBNull.Value ? (string)reader["BRANCH_CODE"] : null;
                            result.END_DAY_DATE = reader["END_DAY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["END_DAY_DATE"]) : null;
                            result.END_DAY_DOC_TYPE_ID = Convert.ToInt32(reader["END_DAY_DOC_TYPE_ID"]);
                            result.END_DAY_DOC_TYPE_NAME = reader["END_DAY_DOC_TYPE_NAME"] != DBNull.Value ? (string)reader["END_DAY_DOC_TYPE_NAME"] : null;
                            result.FILE_NAME_ORI = reader["FILE_NAME_ORI"] != DBNull.Value ? (string)reader["FILE_NAME_ORI"] : null;
                            result.FILE_NAME_DEST = reader["FILE_NAME_DEST"] != DBNull.Value ? (string)reader["FILE_NAME_DEST"] : null;
                            result.FILE_PATH = reader["FILE_PATH"] != DBNull.Value ? (string)reader["FILE_PATH"] : null;
                            result.FILE_SIZE = Convert.ToInt32(reader["FILE_SIZE"]);
                            result.FILE_CONTENT_TYPE = reader["FILE_CONTENT_TYPE"] != DBNull.Value ? (string)reader["FILE_CONTENT_TYPE"] : null;
                            result.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            result.REMARK = reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            result.CREATE_BY = reader["CREATE_BY"] != DBNull.Value ? (string)reader["CREATE_BY"] : null;
                            result.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            result.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            result.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_END_DAY_DOC_GetByPK);
                throw ex;
            }
        }
        public List<USP_R_END_DAY_DOC_GetByEndDayDate_RET> GetByEndDayDate(DateTime dateTime, string branchCode)
        {
            try
            {
                List<USP_R_END_DAY_DOC_GetByEndDayDate_RET> ret = new List<USP_R_END_DAY_DOC_GetByEndDayDate_RET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_END_DAY_DOC_GetByEndDayDate, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_END_DAY_DATE", dateTime);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", branchCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var result = new USP_R_END_DAY_DOC_GetByEndDayDate_RET();
                            result.END_DAY_DOC_ID = Convert.ToInt32(reader["END_DAY_DOC_ID"]);
                            result.BRAND_CODE = reader["BRAND_CODE"] != DBNull.Value ? (string)reader["BRAND_CODE"] : null;
                            result.BRANCH_CODE = reader["BRANCH_CODE"] != DBNull.Value ? (string)reader["BRANCH_CODE"] : null;
                            result.END_DAY_DATE = reader["END_DAY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["END_DAY_DATE"]) : null;
                            result.END_DAY_DOC_TYPE_ID = Convert.ToInt32(reader["END_DAY_DOC_TYPE_ID"]);
                            result.END_DAY_DOC_TYPE_NAME = reader["END_DAY_DOC_TYPE_NAME"] != DBNull.Value ? (string)reader["END_DAY_DOC_TYPE_NAME"] : null;
                            result.FILE_NAME_ORI = reader["FILE_NAME_ORI"].ToString();
                            result.FILE_NAME_DEST = reader["FILE_NAME_DEST"].ToString();
                            result.FILE_PATH = reader["FILE_PATH"].ToString();
                            result.FILE_SIZE = Convert.ToInt32(reader["FILE_SIZE"]);
                            result.FILE_CONTENT_TYPE = reader["FILE_CONTENT_TYPE"].ToString();
                            result.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            result.REMARK = reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            result.CREATE_BY = reader["CREATE_BY"].ToString();
                            result.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            result.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            result.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
                            ret.Add(result);
                        }
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_END_DAY_DOC_GetByEndDayDate);
                throw ex;
            }
        }
    }
}
