using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;

namespace ZEN.SaleAndTranfer.DC.ADMIN
{
    public class BatchHaviDC
    {
        //============================ Purchase ============================//
        public int RunBatchPO(string sendData, string userName, string processType, string companyAbbr)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_HAVI))
                {
                    conn.Open();

                    #region -- RunBatchPO --

                    using (var cm = new SqlCommand(StoreProcConst.USP_HAVI_CALL_JOB_PO, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ProcessBy", userName);
                        cm.Parameters.AddWithValue("@P_ProcessType", processType);
                        cm.Parameters.AddWithValue("@P_CompanyAbbr", companyAbbr);
                        cm.Parameters.AddWithValue("@P_PONo", sendData);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_HAVI_CALL_JOB_PO);
                throw ex;
            }
        }

        //============================ Warehouse ============================//
        public int RunBatchRN(string createBy, string processType)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_HAVI))
                {
                    conn.Open();

                    #region -- RunBatchRN --
                    using (var cm = new SqlCommand(StoreProcConst.USP_HAVI_CALL_JOB_RN, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ProcessBy", createBy);
                        cm.Parameters.AddWithValue("@P_ProcessType", processType);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_HAVI_CALL_JOB_RN);
                throw ex;
            }
        }
        public int RunBatchSO(string sendData, string userName, string processType)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_HAVI))
                {
                    conn.Open();

                    #region -- RunBatchSO --

                    using (var cm = new SqlCommand(StoreProcConst.USP_HAVI_CALL_JOB_SO, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ProcessBy", userName);
                        cm.Parameters.AddWithValue("@P_ProcessType", processType);
                        cm.Parameters.AddWithValue("@P_WebOrderTRNo", sendData);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_HAVI_CALL_JOB_SO);
                throw ex;
            }
        }
        public int RunBatchDN(string createBy, string processType)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_HAVI))
                {
                    conn.Open();

                    #region -- RunBatchDN --
                    using (var cm = new SqlCommand(StoreProcConst.USP_HAVI_CALL_JOB_DN, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ProcessBy", createBy);
                        cm.Parameters.AddWithValue("@P_ProcessType", processType);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_HAVI_CALL_JOB_DN);
                throw ex;
            }
        }
    }
}
