using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZEN.SaleAndTranfer.DC.CNF;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.ADMIN;
using ZEN.SaleAndTranfer.ET.CNF;

namespace ZEN.SaleAndTranfer.DC.ADMIN
{
    public class BatchDC
    {
        //public int RunBatchPR(List<BatchET> stPrCodeList)
        //{
        //    try
        //    {
        //        DateTime currentDate = DateTime.Now;
        //        using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
        //        {
        //            conn.Open();

        //            #region -- Run Batch PR --
        //            if (stPrCodeList == null)
        //            {
        //                using (var cm = new SqlCommand(StoreProcConst.USP_ST_CALL_JOB_SEND_PR, conn))
        //                {
        //                    cm.CommandTimeout = 7200;
        //                    cm.CommandType = CommandType.StoredProcedure;

        //                    #region -- set param --
        //                    //cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
        //                    #endregion

        //                    cm.ExecuteNonQuery();
        //                    //cm.ExecuteNonQueryAsync();
        //                }
        //            }
        //            else
        //            {
        //                ConfigDC dc = new ConfigDC();
        //                int waitingTime;
        //                var type = dc.GetConfigET(CategoryConfigEnum.BATCH, SubCategoryConfigEnum.TIMER, ConfigNameEnum.WAITING_TIME);
        //                if (type.CONFIG_VALUE != null) { waitingTime = (Convert.ToInt16(type.CONFIG_VALUE) * 1000); }
        //                else { waitingTime = 5000; }
        //                foreach (var item in stPrCodeList)
        //                {
        //                    using (var cm = new SqlCommand("USP_R_BATCH_ST_PR_Sale", conn))
        //                    {
        //                        cm.CommandTimeout = 7200;
        //                        cm.CommandType = CommandType.StoredProcedure;

        //                        #region -- set param --
        //                        cm.Parameters.AddWithValue("@PROCESS_BY", item.PROCESS_BY);
        //                        cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
        //                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", item.ST_PR_CODE);
        //                        #endregion

        //                        cm.ExecuteNonQuery();
        //                    }

        //                    System.Threading.Thread.Sleep(waitingTime);
        //                }
        //            }
        //            #endregion

        //            return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogDC dcLog = new LogDC();
        //        dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_CALL_JOB_SEND_PR);
        //        throw ex;
        //    }
        //}
        public int RunBatchPR(string stPrCode, string userName)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- Run Batch PR --

                    //using (var cm = new SqlCommand(StoreProcConst.USP_T_ST_InsertPRSendNavision, conn))
                    //{
                    //    cm.CommandTimeout = 7200;
                    //    cm.CommandType = CommandType.StoredProcedure;

                    //    #region -- set param --
                    //    cm.Parameters.AddWithValue("@PROCESS_BY", userName);
                    //    //cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
                    //    cm.Parameters.AddWithValue("@P_ST_PR_CODE", stPrCode);
                    //    #endregion

                    //    cm.ExecuteNonQuery();
                    //}

                    using (var cm = new SqlCommand(StoreProcConst.USP_BATCH_ST_PR_Run, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@PROCESS_BY", userName);
                        cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", stPrCode);
                        #endregion

                        cm.ExecuteNonQuery();
                    }

                    //if (stPrCode == null)
                    //{
                    //    using (var cm = new SqlCommand(StoreProcConst.USP_ST_CALL_JOB_SEND_PR, conn))
                    //    {
                    //        cm.CommandTimeout = 7200;
                    //        cm.CommandType = CommandType.StoredProcedure;
                    //        cm.ExecuteNonQuery();
                    //    }
                    //}
                    //else
                    //{
                    //    using (var cm = new SqlCommand(StoreProcConst.USP_R_BATCH_ST_PR_Sale, conn))
                    //    {
                    //        cm.CommandTimeout = 7200;
                    //        cm.CommandType = CommandType.StoredProcedure;

                    //        #region -- set param --
                    //        cm.Parameters.AddWithValue("@PROCESS_BY", userName);
                    //        cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
                    //        cm.Parameters.AddWithValue("@P_ST_PR_CODE", stPrCode);
                    //        #endregion

                    //        cm.ExecuteNonQuery();
                    //    }
                    //}
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_CALL_JOB_SEND_PR);
                throw ex;
            }
        }
        public int RunBatchDO()
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- Run Batch DO --
                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_CALL_JOB_GET_DO, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        //cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                        #endregion

                        cm.ExecuteNonQuery();
                        //cm.ExecuteNonQueryAsync();
                    }
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_CALL_JOB_GET_DO);
                throw ex;
            }
        }
        public int RunBatchGR(string stGrCode, string userName)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- Run Batch GR --
                    using (var cm = new SqlCommand(StoreProcConst.USP_BATCH_ST_GR_Run, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@PROCESS_BY", userName);
                        cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", stGrCode);
                        #endregion

                        cm.ExecuteNonQuery();
                    }
                    //if (stGrCode == null || stGrCode == "")
                    //{
                    //    using (var cm = new SqlCommand(StoreProcConst.USP_BATCH_ST_GR_Run, conn))
                    //    {
                    //        cm.CommandTimeout = 7200;
                    //        cm.CommandType = CommandType.StoredProcedure;
                    //        cm.ExecuteNonQuery();
                    //    }
                    //}
                    //else
                    //{
                    //    using (var cm = new SqlCommand(StoreProcConst.USP_R_BATCH_ST_GR_Sale, conn))
                    //    {
                    //        cm.CommandTimeout = 7200;
                    //        cm.CommandType = CommandType.StoredProcedure;

                    //        #region -- set param --
                    //        cm.Parameters.AddWithValue("@PROCESS_BY", userName);
                    //        cm.Parameters.AddWithValue("@PROCESS_TYPE", "MANUAL");
                    //        cm.Parameters.AddWithValue("@P_ST_GR_CODE", stGrCode);
                    //        #endregion

                    //        cm.ExecuteNonQuery();
                    //    }
                    //}
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_BATCH_ST_DO_Run);
                throw ex;
            }
        }
        public int RunBatchMaster(string createBy, string processType)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- Run Batch Master --
                    using (var cm = new SqlCommand(StoreProcConst.USP_R_BATCH_MASTER, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@CREATE_BY ", createBy);
                        cm.Parameters.AddWithValue("@PROCESS_TYPE ", processType);
                        #endregion

                        //cm.ExecuteNonQuery();
                        cm.BeginExecuteNonQuery();
                    }
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_BATCH_MASTER);
                throw ex;
            }
        }

        public int ExportSaesTranTOERP(string brandCode, string branchCode, string processBy, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_STAGING))
                {
                    conn.Open();

                    #region -- Run Batch Master --
                    using (var cm = new SqlCommand(StoreProcConst.Usp_R_ExportSaesTranTOERP, conn))
                    {
                        cm.CommandTimeout = 7200;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE ", brandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE ", branchCode);
                        cm.Parameters.AddWithValue("@P_ProcessBy", processBy);
                        cm.Parameters.AddWithValue("@From_Date", fromDate);
                        cm.Parameters.AddWithValue("@To_Date", toDate);
                        #endregion

                        cm.ExecuteNonQuery();
                        //cm.BeginExecuteNonQuery();
                    }
                    #endregion

                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_BATCH_MASTER);
                throw ex;
            }
        }


        public int ExportSaesTranTOERPxxx(string brandCode, string branchCode, string processBy, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string result = string.Empty;

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF_STAGING))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.Usp_R_ExportSaesTranTOERP, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 600;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE ", brandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE ", branchCode);
                        cm.Parameters.AddWithValue("@P_ProcessBy", processBy);
                        cm.Parameters.AddWithValue("@From_Date", fromDate);
                        cm.Parameters.AddWithValue("@To_Date", toDate);
                        #endregion
                        var resultExec = cm.ExecuteScalar();
                        if (resultExec != null)
                        {
                            result = cm.ExecuteScalar().ToString();
                        }


                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_STOCK_CARD_SaveTransaction);
                throw ex;
            }
        }


    }
}
