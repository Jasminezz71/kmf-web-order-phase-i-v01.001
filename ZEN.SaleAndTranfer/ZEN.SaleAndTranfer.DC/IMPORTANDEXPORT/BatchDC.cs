using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;

namespace ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT
{
    public class BatchDC
    {
        public int StartBatch(string batchName, string appName, string startBy)
        {
            try
            {
                object batchID = 0;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@P_BATCH_NAME", batchName);
                    cmd.Parameters.AddWithValue("@P_BATCH_APP_NAME", appName);
                    cmd.Parameters.AddWithValue("@P_START_BY", startBy);

                    var outputParam1 = new SqlParameter();
                    outputParam1.ParameterName = "@P_BATCH_ID";
                    outputParam1.Direction = ParameterDirection.Output;
                    outputParam1.DbType = DbType.Int32;
                    cmd.Parameters.Add(outputParam1);

                    cmd.CommandText = StoreProcConst.USP_R_BATCH_PROCESS_Start;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                    batchID = outputParam1.Value;
                }

                if (batchID != null)
                {
                    return Convert.ToInt32(batchID);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_R_BATCH_PROCESS_Start);
                throw ex;
            }
        }
        public void ErrorBatch(int batchID, string errorMassage)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@P_BATCH_ID", batchID);
                    cmd.Parameters.AddWithValue("@P_ERROR_MESSAGE", errorMassage);

                    cmd.CommandText = StoreProcConst.USP_R_BATCH_PROCESS_Error;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_R_BATCH_PROCESS_Error);
                throw ex;
            }
        }
        public void EndBatch(int batchID)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@P_BATCH_ID", batchID);
                    cmd.CommandText = StoreProcConst.USP_R_BATCH_PROCESS_End;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_R_BATCH_PROCESS_End);
                throw ex;
            }
        }
        public void InsertBatchLog(int batchID, string logType, string msg1, string msg2, string remark, string log_by)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@P_BATCH_ID", batchID);
                    cmd.Parameters.AddWithValue("@P_LOG_TYPE", logType.ToString());
                    cmd.Parameters.AddWithValue("@P_LOG_MSG_1", msg1);
                    cmd.Parameters.AddWithValue("@P_LOG_MSG_2", msg2);
                    cmd.Parameters.AddWithValue("@P_REMARK", remark);
                    cmd.Parameters.AddWithValue("@P_CREATE_BY", log_by);

                    cmd.CommandText = StoreProcConst.USP_L_BATCH_PROCESS__Insert;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
