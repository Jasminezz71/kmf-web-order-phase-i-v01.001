using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT
{
    public class PrWhPickupDC
    {
        public List<PrWhPickupSearchResultET> ExportFile(string username, PrWhPickupET data)
        {
            try
            {
                List<PrWhPickupSearchResultET> result = new List<PrWhPickupSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_ST_Download_PR_WH_Pickup, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_USER_NAME", username);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_FROM", data.PLAN_DELIVERY_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_TO", data.PLAN_DELIVERY_DATE_TO);
                        cm.Parameters.AddWithValue("@P_DISPLAY_HEADER", data.DISPLAY_HEADER);
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_CATEGORY_ID", data.CATEGORY_ID);
                        cm.Parameters.AddWithValue("@P_LOCATION_CODE", data.LOCATION_CODE);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrWhPickupSearchResultET();

                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.ST_PR_CODE_NAV = reader["ST_PR_CODE_NAV"].ToString();
                            newInstance.ORDER_DATE = reader["ORDER_DATE"].ToString();
                            newInstance.POSTING_DATE = reader["DELIVERY_DATE"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_DESC = reader["ITEM_DESC"].ToString();
                            newInstance.REQUEST_QTY = reader["REQUEST_QTY"].ToString();
                            newInstance.REQUEST_UOM = reader["REQUEST_UOM"].ToString();
                            newInstance.SEND_QTY = reader["SEND_QTY"].ToString();
                            newInstance.SEND_UOM = reader["SEND_UOM"].ToString();
                            newInstance.REQUEST_BY_BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.CREATE_BY = reader["CREATE_NAME"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString();
                            newInstance.ST_WH_ITEM_CATEGORY_NAME = reader["CATEGORY"].ToString();
                            newInstance.REQUEST_TO_BRANCH_CODE = reader["LOCATION_CODE"].ToString();

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_ST_Download_PR_WH_Pickup);
                throw ex;
            }
        }
    }
}
