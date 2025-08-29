using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.RPT;

namespace ZEN.SaleAndTranfer.DC.RPT
{
    public class StockDC
    {
        public List<stockSearchResultET> InventoryStockSearch(stockSearchCriteriaET data)
        {
            try
            {
                List<stockSearchResultET> result = new List<stockSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_RPT_ST_REPORT_SEND_AND_RECEIVE, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_DATE_TO", data.DATE_TO_SEARCH);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new stockSearchResultET();

                            newInstance.ST_PR_CATEGORY_ID = reader["ST_PR_CATEGORY_ID"].ToString() != string.Empty ? new int?((int)reader["ST_PR_CATEGORY_ID"]) : null;
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
                            newInstance.BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME_TH = reader["ITEM_NAME_TH"].ToString();

                            newInstance.S_01 = reader["01_S"].ToString();
                            newInstance.S_02 = reader["02_S"].ToString();
                            newInstance.S_03 = reader["03_S"].ToString();
                            newInstance.S_04 = reader["04_S"].ToString();
                            newInstance.S_05 = reader["05_S"].ToString();
                            newInstance.S_06 = reader["06_S"].ToString();
                            newInstance.S_07 = reader["07_S"].ToString();
                            newInstance.S_08 = reader["08_S"].ToString();
                            newInstance.S_09 = reader["09_S"].ToString();
                            newInstance.S_10 = reader["10_S"].ToString();
                            newInstance.S_11 = reader["11_S"].ToString();
                            newInstance.S_12 = reader["12_S"].ToString();
                            newInstance.S_13 = reader["13_S"].ToString();
                            newInstance.S_14 = reader["14_S"].ToString();
                            newInstance.S_15 = reader["15_S"].ToString();
                            newInstance.S_16 = reader["16_S"].ToString();
                            newInstance.S_17 = reader["17_S"].ToString();
                            newInstance.S_18 = reader["18_S"].ToString();
                            newInstance.S_19 = reader["19_S"].ToString();
                            newInstance.S_20 = reader["20_S"].ToString();
                            newInstance.S_21 = reader["21_S"].ToString();
                            newInstance.S_22 = reader["22_S"].ToString();
                            newInstance.S_23 = reader["23_S"].ToString();
                            newInstance.S_24 = reader["24_S"].ToString();
                            newInstance.S_25 = reader["25_S"].ToString();
                            newInstance.S_26 = reader["26_S"].ToString();
                            newInstance.S_27 = reader["27_S"].ToString();
                            newInstance.S_28 = reader["28_S"].ToString();
                            newInstance.S_29 = reader["29_S"].ToString();
                            newInstance.S_30 = reader["30_S"].ToString();
                            newInstance.S_31 = reader["31_S"].ToString();

                            newInstance.R_01 = reader["01_R"].ToString();
                            newInstance.R_02 = reader["02_R"].ToString();
                            newInstance.R_03 = reader["03_R"].ToString();
                            newInstance.R_04 = reader["04_R"].ToString();
                            newInstance.R_05 = reader["05_R"].ToString();
                            newInstance.R_06 = reader["06_R"].ToString();
                            newInstance.R_07 = reader["07_R"].ToString();
                            newInstance.R_08 = reader["08_R"].ToString();
                            newInstance.R_09 = reader["09_R"].ToString();
                            newInstance.R_10 = reader["10_R"].ToString();
                            newInstance.R_11 = reader["11_R"].ToString();
                            newInstance.R_12 = reader["12_R"].ToString();
                            newInstance.R_13 = reader["13_R"].ToString();
                            newInstance.R_14 = reader["14_R"].ToString();
                            newInstance.R_15 = reader["15_R"].ToString();
                            newInstance.R_16 = reader["16_R"].ToString();
                            newInstance.R_17 = reader["17_R"].ToString();
                            newInstance.R_18 = reader["18_R"].ToString();
                            newInstance.R_19 = reader["19_R"].ToString();
                            newInstance.R_20 = reader["20_R"].ToString();
                            newInstance.R_21 = reader["21_R"].ToString();
                            newInstance.R_22 = reader["22_R"].ToString();
                            newInstance.R_23 = reader["23_R"].ToString();
                            newInstance.R_24 = reader["24_R"].ToString();
                            newInstance.R_25 = reader["25_R"].ToString();
                            newInstance.R_26 = reader["26_R"].ToString();
                            newInstance.R_27 = reader["27_R"].ToString();
                            newInstance.R_28 = reader["28_R"].ToString();
                            newInstance.R_29 = reader["29_R"].ToString();
                            newInstance.R_30 = reader["30_R"].ToString();
                            newInstance.R_31 = reader["31_R"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_RPT_ST_REPORT_SEND_AND_RECEIVE);
                throw ex;
            }
        }
        public List<statusDoGrSearchResultET> StatusReceiveByDoAndGrSearch(statusDoGrSearchCriteriaET data)
        {
            try
            {
                List<statusDoGrSearchResultET> result = new List<statusDoGrSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_RPT_ST_REPORT_WHO_REC_ITEM, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_DATE_FROM", data.DATE_FROM);
                        cm.Parameters.AddWithValue("@P_DATE_TO", data.DATE_TO);
                        cm.Parameters.AddWithValue("@P_EXPORT_TYPE", "EXCEL");
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new statusDoGrSearchResultET();

                            newInstance.ROW_NO = reader["ROW_NO"].ToString();
                            newInstance.DO_DATE = reader["DO_DATE"].ToString();
                            newInstance.CREATE_DATE_DO = reader["CREATE_DATE_DO"].ToString();
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.DO_CODE = reader["DO_CODE"].ToString();
                            newInstance.GR_CODE = reader["GR_CODE"].ToString();
                            newInstance.RECEIVE_BY = reader["RECEIVE_BY"].ToString();
                            newInstance.RECEIVE_DATE = reader["RECEIVE_DATE"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_RPT_ST_REPORT_WHO_REC_ITEM);
                throw ex;
            }
        }
    }
}
