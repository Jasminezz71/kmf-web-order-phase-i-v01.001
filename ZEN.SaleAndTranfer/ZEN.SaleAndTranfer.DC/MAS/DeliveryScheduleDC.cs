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
    public class DeliveryScheduleDC
    {
        public List<DeliveryScheduleET> SearchDS(string brandCode, string BranchCode, string LocationCode)
        {
            try
            {
                List<DeliveryScheduleET> result = new List<DeliveryScheduleET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__GetData, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", brandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", BranchCode);
                        cm.Parameters.AddWithValue("@P_LOCATION_CODE", LocationCode);
                        

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DeliveryScheduleET();
                      
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.ZONE = reader["ZONE"].ToString();
                            newInstance.SCHEDULE_TYPE = reader["SCHEDULE_TYPE"].ToString();
                            newInstance.START_TIME = reader["START_TIME"].ToString();
                            newInstance.END_TIME = reader["END_TIME"].ToString();
                            newInstance.SUN_FLAG = (bool)reader["SUN_FLAG"];
                            newInstance.MON_FLAG = (bool)reader["MON_FLAG"];
                            newInstance.TUE_FLAG = (bool)reader["TUE_FLAG"];
                            newInstance.WED_FLAG = (bool)reader["WED_FLAG"];
                            newInstance.THU_FLAG = (bool)reader["THU_FLAG"];
                            newInstance.FRI_FLAG = (bool)reader["FRI_FLAG"];
                            newInstance.SAT_FLAG = (bool)reader["SAT_FLAG"];
                            newInstance.LOCATION_CODE = reader["LOCATION_CODE"].ToString();
                            newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__GetData);
                throw ex;
            }
        }
        public int EditSave(DeliveryScheduleET data)
        {
            try
            {

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__Update, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE",data.BRANCH_CODE );
                        cm.Parameters.AddWithValue("@P_ZONE",data.ZONE );
                        cm.Parameters.AddWithValue("@P_SCHEDULE_TYPE",data.SCHEDULE_TYPE );
                        cm.Parameters.AddWithValue("@P_START_TIME",data.START_TIME );
                        cm.Parameters.AddWithValue("@P_END_TIME",data.END_TIME );
                        cm.Parameters.AddWithValue("@P_SUN_FLAG",data.SUN_FLAG );
                        cm.Parameters.AddWithValue("@P_MON_FLAG",data.MON_FLAG );
                        cm.Parameters.AddWithValue("@P_TUE_FLAG",data.TUE_FLAG );
                        cm.Parameters.AddWithValue("@P_WED_FLAG",data.WED_FLAG );
                        cm.Parameters.AddWithValue("@P_THU_FLAG",data.THU_FLAG );
                        cm.Parameters.AddWithValue("@P_FRI_FLAG",data.FRI_FLAG );
                        cm.Parameters.AddWithValue("@P_SAT_FLAG",data.SAT_FLAG );
                        cm.Parameters.AddWithValue("@P_LOCATION_CODE",data.LOCATION_CODE );
                        cm.Parameters.AddWithValue("@P_UPDATE_BY",data.UPDATE_BY );

                        #endregion

                        var reader = cm.ExecuteNonQuery();
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__Update);
                throw ex;
            }
        }
        public int AddSave(DeliveryScheduleET data)
        {
            try
            {

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__Insert, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE",data.BRANCH_CODE );
                        cm.Parameters.AddWithValue("@P_ZONE",data.ZONE );
                        cm.Parameters.AddWithValue("@P_SCHEDULE_TYPE",data.SCHEDULE_TYPE );
                        cm.Parameters.AddWithValue("@P_START_TIME",data.START_TIME );
                        cm.Parameters.AddWithValue("@P_END_TIME",data.END_TIME );
                        cm.Parameters.AddWithValue("@P_SUN_FLAG",data.SUN_FLAG );
                        cm.Parameters.AddWithValue("@P_MON_FLAG",data.MON_FLAG );
                        cm.Parameters.AddWithValue("@P_TUE_FLAG",data.TUE_FLAG );
                        cm.Parameters.AddWithValue("@P_WED_FLAG",data.WED_FLAG );
                        cm.Parameters.AddWithValue("@P_THU_FLAG",data.THU_FLAG );
                        cm.Parameters.AddWithValue("@P_FRI_FLAG",data.FRI_FLAG );
                        cm.Parameters.AddWithValue("@P_SAT_FLAG",data.SAT_FLAG );
                        cm.Parameters.AddWithValue("@P_LOCATION_CODE",data.LOCATION_CODE );
                        cm.Parameters.AddWithValue("@P_CREATE_BY", data.UPDATE_BY);

                        #endregion

                        var reader = cm.ExecuteNonQuery();
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_DELIVERY_SCHEDULE__Insert);
                throw ex;
            }
        }
    }
}
