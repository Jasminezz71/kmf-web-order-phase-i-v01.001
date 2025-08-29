using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.AUT;

namespace ZEN.SaleAndTranfer.DC.AUT
{
    public class UserInfoDC
    {
        public List<UserLoginInfoET> Search(UserLoginInfoET data, string SortField, string Sorttype, out int countAll)
        {
            try
            {
                countAll = 0;
                List<UserLoginInfoET> result = new List<UserLoginInfoET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_MAS_UserSearch, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        cm.Parameters.AddWithValue("@P_FIRST_NAME_TH", data.FIRST_NAME_TH);
                        cm.Parameters.AddWithValue("@P_LAST_NAME_TH", data.LAST_NAME_TH);
                        cm.Parameters.AddWithValue("@P_EMAIL", data.EMAIL);
                        cm.Parameters.AddWithValue("@P_ACTIVE_FLAG", data.ACTIVE_FLAG);
                        cm.Parameters.AddWithValue("@P_START_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_SORT_BY", SortField);
                        cm.Parameters.AddWithValue("@P_SORT_DIRECTION", Sorttype);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new UserLoginInfoET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.USER_NAME = reader["USER_NAME"].ToString();
                            newInstance.PASSWORD_HASH = reader["PASSWORD_HASH"].ToString();
                            newInstance.EMPLOYEE_ID = reader["EMPLOYEE_ID"].ToString();
                            newInstance.FIRST_NAME_TH = reader["FIRST_NAME_TH"].ToString();
                            newInstance.LAST_NAME_TH = reader["LAST_NAME_TH"].ToString();
                            newInstance.FIRST_NAME_EN = reader["FIRST_NAME_EN"].ToString();
                            newInstance.LAST_NAME_EN = reader["LAST_NAME_EN"].ToString();
                            newInstance.EMAIL = reader["EMAIL"].ToString();
                            newInstance.PHONE_NO = reader["PHONE_NO"].ToString();
                            newInstance.PHONE_EXT = reader["PHONE_EXT"].ToString();
                            newInstance.MOBILE_NO = reader["MOBILE_NO"].ToString();
                            newInstance.ACTIVE_FLAG = reader["ACTIVE_FLAG"].ToString();
                            newInstance.ACTIVE_FLAG_DISPLAY = reader["ACTIVE_FLAG_DISPLAY"].ToString();
                            newInstance.USER_USM_TYPE = reader["USER_USM_TYPE"].ToString();
                            newInstance.BRAND_NAME = reader["BRAND_NAME"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.LOCKED_FLAG = reader["LOCKED_FLAG"].ToString();
                            newInstance.ROLE_NAME = reader["ROLE_NAME"].ToString();
                            newInstance.IS_RESET_PWD = reader["IS_RESET_PWD"].ToString();
                            newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;

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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_MAS_UserSearch);
                throw ex;
            }
        }
        public List<UserLoginInfoET> SearchUserByID(string username)
        {
            try
            {
                List<UserLoginInfoET> result = new List<UserLoginInfoET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_MAS_UserSearchByID, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_USER_LOGIN", username);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new UserLoginInfoET();

                            newInstance.USER_NAME = reader["USER_NAME"].ToString();
                            newInstance.PASSWORD_HASH = reader["PASSWORD_HASH"].ToString();
                            newInstance.EMPLOYEE_ID = reader["EMPLOYEE_ID"].ToString();
                            newInstance.FIRST_NAME_TH = reader["FIRST_NAME_TH"].ToString();
                            newInstance.LAST_NAME_TH = reader["LAST_NAME_TH"].ToString();
                            newInstance.FIRST_NAME_EN = reader["FIRST_NAME_EN"].ToString();
                            newInstance.LAST_NAME_EN = reader["LAST_NAME_EN"].ToString();
                            newInstance.EMAIL = reader["EMAIL"].ToString();
                            newInstance.PHONE_NO = reader["PHONE_NO"].ToString();
                            newInstance.PHONE_EXT = reader["PHONE_EXT"].ToString();
                            newInstance.MOBILE_NO = reader["MOBILE_NO"].ToString();
                            newInstance.ACTIVE_FLAG = reader["ACTIVE_FLAG"].ToString();
                            newInstance.USER_USM_TYPE = reader["USER_USM_TYPE"].ToString();
                            //newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRAND_NAME = reader["BRAND_NAME"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.LOCKED_FLAG = reader["LOCKED_FLAG"].ToString();
                            newInstance.ROLE_NAME = reader["ROLE_NAME"].ToString();
                            newInstance.IS_RESET_PWD = reader["IS_RESET_PWD"].ToString();
                            newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;

                            newInstance.FRANCHISES_FLAG = reader["FRANCHISES_FLAG"] != DBNull.Value ? (bool)reader["FRANCHISES_FLAG"] : false;


                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_MAS_UserSearchByID);
                throw ex;
            }
        }

        public int IsExistUsername(string username)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_USER_IsExistUsername, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_USER_NAME", username);

                        #endregion

                        return Convert.ToInt16(cm.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_M_USER_IsExistUsername);
                throw ex;
            }
        }


        public int ResetPasswordUser(UserLoginInfoET data)
        {
            SqlTransaction transaction = null;
            DateTime currentDate = DateTime.Now;
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        #region -- Reset Password User --
                        using (var cm = new SqlCommand(StoreProcConst.USP_USM_UpdateUser, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                            cm.Parameters.AddWithValue("@P_PASSWORD_HASH", data.PASSWORD_HASH);
                            cm.Parameters.AddWithValue("@P_IS_RESET_PWD", data.IS_RESET_PWD);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_UPDATE_DATE", currentDate);

                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion

                        transaction.Commit();
                        return 1;

                    }
                    catch (SqlException ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                            throw ex;
                        }

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_USM_UpdateUser);
                throw ex;
            }

        }
        public int ChangeStatusUser(UserLoginInfoET data)
        {
            SqlTransaction transaction = null;
            DateTime currentDate = DateTime.Now;
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        #region -- Reset Password User --
                        using (var cm = new SqlCommand(StoreProcConst.USP_USM_UpdateUser, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                            cm.Parameters.AddWithValue("@P_ACTIVE_FLAG", data.ACTIVE_FLAG);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_UPDATE_DATE", currentDate);

                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion

                        transaction.Commit();
                        return 1;

                    }
                    catch (SqlException ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                            throw ex;
                        }

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_USM_UpdateUser);
                throw ex;
            }

        }
        public int CreateUser(UserLoginInfoET data)
        {
            SqlTransaction transaction = null;
            DateTime currentDate = DateTime.Now;
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        #region -- Create User --
                        using (var cm = new SqlCommand(StoreProcConst.USP_ST_USM_InsertUser, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                            cm.Parameters.AddWithValue("@P_PASSWORD_HASH", data.PASSWORD_HASH);
                            cm.Parameters.AddWithValue("@P_EMPLOYEE_ID", data.EMPLOYEE_ID);
                            cm.Parameters.AddWithValue("@P_FIRST_NAME_TH", data.FIRST_NAME_TH);
                            cm.Parameters.AddWithValue("@P_LAST_NAME_TH", data.LAST_NAME_TH);
                            cm.Parameters.AddWithValue("@P_FIRST_NAME_EN", data.FIRST_NAME_EN);
                            cm.Parameters.AddWithValue("@P_LAST_NAME_EN", data.LAST_NAME_EN);
                            cm.Parameters.AddWithValue("@P_EMAIL", data.EMAIL);
                            cm.Parameters.AddWithValue("@P_PHONE_NO", data.PHONE_NO);
                            cm.Parameters.AddWithValue("@P_PHONE_EXT", data.PHONE_EXT);
                            cm.Parameters.AddWithValue("@P_MOBILE_NO", data.MOBILE_NO);
                            cm.Parameters.AddWithValue("@P_ACTIVE_FLAG", data.ACTIVE_FLAG);
                            cm.Parameters.AddWithValue("@P_USER_USM_TYPE", data.USER_USM_TYPE);
                            cm.Parameters.AddWithValue("@P_IS_RESET_PWD", data.IS_RESET_PWD);
                            cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                            cm.Parameters.AddWithValue("@P_CREATE_DATE", data.CREATE_DATE);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_UPDATE_DATE", data.UPDATE_DATE);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion

                        transaction.Commit();
                        return 1;

                    }
                    catch (SqlException ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                            throw ex;
                        }

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_ST_USM_InsertUser);
                throw ex;
            }

        }
        public int UpdateUser(UserLoginInfoET data)
        {
            SqlTransaction transaction = null;
            DateTime currentDate = DateTime.Now;
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction();

                        #region -- Update User --
                        using (var cm = new SqlCommand(StoreProcConst.USP_USM_UpdateUser, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                            cm.Parameters.AddWithValue("@P_PASSWORD_HASH", data.PASSWORD_HASH);
                            cm.Parameters.AddWithValue("@P_EMPLOYEE_ID", data.EMPLOYEE_ID);
                            cm.Parameters.AddWithValue("@P_FIRST_NAME_TH", data.FIRST_NAME_TH);
                            cm.Parameters.AddWithValue("@P_LAST_NAME_TH", data.LAST_NAME_TH);
                            cm.Parameters.AddWithValue("@P_FIRST_NAME_EN", data.FIRST_NAME_EN);
                            cm.Parameters.AddWithValue("@P_LAST_NAME_EN", data.LAST_NAME_EN);
                            cm.Parameters.AddWithValue("@P_EMAIL", data.EMAIL);
                            cm.Parameters.AddWithValue("@P_PHONE_NO", data.PHONE_NO);
                            cm.Parameters.AddWithValue("@P_PHONE_EXT", data.PHONE_EXT);
                            cm.Parameters.AddWithValue("@P_MOBILE_NO", data.MOBILE_NO);
                            cm.Parameters.AddWithValue("@P_ACTIVE_FLAG", data.ACTIVE_FLAG);
                            cm.Parameters.AddWithValue("@P_USER_USM_TYPE", data.USER_USM_TYPE);
                            cm.Parameters.AddWithValue("@P_IS_RESET_PWD", data.IS_RESET_PWD);
                            cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                            cm.Parameters.AddWithValue("@P_CREATE_DATE", data.CREATE_DATE);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_UPDATE_DATE", data.UPDATE_DATE);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion

                        transaction.Commit();
                        return 1;

                    }
                    catch (SqlException ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                            throw ex;
                        }

                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_USM_UpdateUser);
                throw ex;
            }

        }

        public int UpdateActiveFlag(string branchCode, string userName, bool activeFlag, string reson)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- Run Batch PR --
                    using (var cm = new SqlCommand(StoreProcConst.USP_MAS_USER_UpdateActiveFlag, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_USER_NAME", userName);
                        cm.Parameters.AddWithValue("@P_ACTIVE_FLAG", activeFlag);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", branchCode);
                        cm.Parameters.AddWithValue("@P_RESON", reson);
                        #endregion

                        cm.ExecuteNonQuery();
                    }
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_MAS_USER_UpdateActiveFlag);
                throw ex;
            }
        }
    }
}
