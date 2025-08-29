using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.DC.MAS
{
    public class DashBoardDC
    {
        public List<DashboardSearchResultET> SearchDashboardPR(DashboardSearchCriteriaET data, string SortField, string Sorttype, out int countAll)
        {
            try
            {
                countAll = 0;
                List<DashboardSearchResultET> result = new List<DashboardSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_GetDashboard, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 120;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_SORT_BY", SortField);
                        cm.Parameters.AddWithValue("@P_SORT_DIRECTION", Sorttype);
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        cm.Parameters.AddWithValue("@P_USER_ROLE", data.ROLE_NAME);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DashboardSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_NAME = reader["REQUEST_BY_BRAND_NAME"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_NAME = reader["REQUEST_BY_BRANCH_NAME"].ToString();
                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            newInstance.PR_STATUS_CODE = Convert.ToInt16(reader["PR_STATUS_CODE"]);
                            newInstance.PR_STATUS_NAME = reader["PR_STATUS_NAME"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.DELETE_FLAG = reader["DELETE_FLAG"].ToString() != string.Empty ? new Boolean?((Boolean)reader["DELETE_FLAG"]) : null;
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_GetDashboard);
                throw ex;
            }
        }
        public List<DashboardSearchResultET> SearchDashboardDO(DashboardSearchCriteriaET data, string SortField, string Sorttype, out int countAll)
        {
            try
            {
                countAll = 0;
                List<DashboardSearchResultET> result = new List<DashboardSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_DO_GetDashboard, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_SORT_BY", SortField);
                        cm.Parameters.AddWithValue("@P_SORT_DIRECTION", Sorttype);
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        cm.Parameters.AddWithValue("@P_USER_ROLE", data.ROLE_NAME);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new DashboardSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_NAME = reader["REQUEST_BY_BRAND_NAME"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_NAME = reader["REQUEST_BY_BRANCH_NAME"].ToString();
                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            newInstance.REQUEST_DATE = reader["REQUEST_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["REQUEST_DATE"]) : null;
                            newInstance.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            newInstance.DO_STATUS_CODE = Convert.ToInt16(reader["DO_STATUS_CODE"]);
                            newInstance.DO_STATUS_NAME = reader["DO_STATUS_NAME"].ToString();
                            newInstance.LOCATION_CODE = reader["LOCATION_CODE"].ToString();

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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_DO_GetDashboard);
                throw ex;
            }
        }
    }
}
