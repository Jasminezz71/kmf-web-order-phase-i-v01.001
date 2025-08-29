using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.DC.DDL
{
    public class DDLDC
    {
        public List<DDLItemET> GetStatus(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetStatus, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetStatus);
                throw ex;
            }
        }
        public List<DDLItemET> GetBrand(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetBrand, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBrand);
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchbyBrand(string brand, DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetBranchbyBrand, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND", brand);
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBrand);
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchbyBrandAndUsername(string brand, string username, DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetBranchbyBrandAndUsername, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", brand);
                        cm.Parameters.AddWithValue("@P_USER_NAME", username);
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBranchbyBrandAndUsername);
                throw ex;
            }
        }
        public List<DDLItemET> GetBrandByUsername(DDLModeEnumET mode, string username)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetBrandByUsername, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());
                        cm.Parameters.AddWithValue("@P_USER_NAME", username);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBrandByUsername);
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchByUsername(DDLModeEnumET mode, string username)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetBranchByUsername, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());
                        cm.Parameters.AddWithValue("@P_USER_NAME", username);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBranchByUsername);
                throw ex;
            }
        }
        public List<DDLItemET> GetCategoryByBrandCode(string brandCode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", brandCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ST_PR_CATEGORY_ID"].ToString();
                            newInstance.ITEM_TEXT = reader["ST_PR_CATEGORY_NAME"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode);
                throw ex;
            }
        }
        public List<DDLItemET> GetDoByPRCode(DDLModeEnumET mode, string prCode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_ST_DO_H_GetByPRCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", prCode);
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_ST_DO_H_GetByPRCode);
                throw ex;
            }
        }
        public List<DDLItemET> GetSTCategoryWH(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetSTCategoryWh, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBrand);
                throw ex;
            }
        }
        public List<DDLItemET> GetSTLocation(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetSTLocation, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetBrand);
                throw ex;
            }
        }
        public List<DDLItemET> GetCompany(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetCompany, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetStatus);
                throw ex;
            }
        }
        public List<DDLItemET> GetAppSystem(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetAppSystem, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetAppSystem);
                throw ex;
            }
        }
        public List<DDLItemET> GetItemStockType(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetItemStockType, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetItemStockType);
                throw ex;
            }
        }

        //Supaneej, 2018-11-08, End Day (ACC)     
        public List<DDLItemET> GetEndDayType(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetEndDayDocType, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetStatus);
                throw ex;
            }
        }

        public List<DDLItemET> GetFCLocation(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetFCLocation, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_GetStatus);
                throw ex;
            }
        }

        public List<DDLItemET> GetDOStatusForGR(DDLModeEnumET mode)
        {
            try
            {
                List<DDLItemET> result = new List<DDLItemET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_DDL_GetDOStatusForGR, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_MODE", mode.ToString());

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DDLItemET();

                            newInstance.ITEM_VALUE = reader["ITEM_VALUE"].ToString();
                            newInstance.ITEM_TEXT = reader["ITEM_TEXT"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_DDL_ST_DO_H_GetByPRCode);
                throw ex;
            }
        }
    }
}
