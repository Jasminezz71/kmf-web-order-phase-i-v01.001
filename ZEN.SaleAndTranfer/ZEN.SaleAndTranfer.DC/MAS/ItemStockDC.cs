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
    public class ItemStockDC
    {
        public List<ItemStockSearchResultET> InventoryStockSearch(ItemStockSearchCriteriaET data, out int countAll)
        {
            try
            {
                int rowNo = 1;
                countAll = 0;
                List<ItemStockSearchResultET> result = new List<ItemStockSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_STOCK_CARD_Search, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_START_DATE", data.START_DATE);
                        cm.Parameters.AddWithValue("@P_END_DATE", data.END_DATE);
                        cm.Parameters.AddWithValue("@P_APPLICATION", data.APP_ID);
                        cm.Parameters.AddWithValue("@P_HAVE_ITEM_FLAG", data.ITEM_STOCK_TYPE);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.PAGE_SIZE);
                        cm.Parameters.AddWithValue("@P_USER_NAME", data.USER_NAME);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new ItemStockSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt16(reader["ROW_NO"]);
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.LOCATION = reader["LOCATION"].ToString();
                            newInstance.ITEM_DESCRIPTION = reader["ITEM_DESCRIPTION"].ToString();
                            newInstance.INCOMING_BALANCE_QTY = reader["INCOMING_BALANCE_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["INCOMING_BALANCE_QTY"]) : null;
                            newInstance.INCOMING_BALANCE_UOM = reader["INCOMING_BALANCE_UOM"].ToString();
                            newInstance.RECEIVE_QTY = reader["RECEIVE_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["RECEIVE_QTY"]) : null;
                            newInstance.RECEIVE_UOM = reader["RECEIVE_UOM"].ToString();
                            newInstance.REMAIN_QTY = reader["REMAIN_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["REMAIN_QTY"]) : null;
                            newInstance.REMAIN_UOM = reader["REMAIN_UOM"].ToString();
                            newInstance.USAGE_QTY = reader["USAGE_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["USAGE_QTY"]) : null;
                            newInstance.USAGE_UOM = reader["USAGE_UOM"].ToString();
                            newInstance.APP_NAME = reader["APPLICATION"].ToString();
                            result.Add(newInstance);
                            rowNo++;
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_STOCK_CARD_Search);
                throw ex;
            }
        }
        public List<ItemStockET> InventoryStockGetItem(ItemStockET data)
        {
            try
            {
                int rowNo = 1;
                List<ItemStockET> result = new List<ItemStockET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_STOCK_CARD_GetItem, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_SAVE_DATE", data.ITEM_PICK_DATE_SAVE);
                        cm.Parameters.AddWithValue("@P_APPLICATION", data.APP_NAME);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new ItemStockET();

                            newInstance.ROW_NO = rowNo;
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME = reader["ITEM_DESCRIPTION"].ToString();
                            newInstance.LOCATION = reader["LOCATION"].ToString();
                            newInstance.REMAIN_QTY = reader["REMAIN_QTY"].ToString() != string.Empty ? new decimal?((decimal)reader["REMAIN_QTY"]) : null;
                            newInstance.REMAIN_UOM = reader["REMAIN_UOM"].ToString();
                            newInstance.APP_NAME = reader["APPLICATION"].ToString();
                            newInstance.CAN_EDIT = reader["CAN_EDIT"].ToString() != string.Empty ? Convert.ToBoolean(reader["CAN_EDIT"]) : false;
                            newInstance.FIRST_EDIT = reader["FIRST_EDIT"].ToString() != string.Empty ? Convert.ToBoolean(reader["FIRST_EDIT"]) : false;

                            result.Add(newInstance);
                            rowNo++;
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_STOCK_CARD_GetItem);
                throw ex;
            }
        }
        public string InventoryStockSave(int batchID)
        {
            try
            {
                string result = string.Empty;

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_STOCK_CARD_SaveTransaction, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 600;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BATCH_NO", batchID);
                        #endregion
                        var resultExec = cm.ExecuteScalar();
                        if (resultExec != null)
                        {
                            result = cm.ExecuteScalar().ToString();
                        }


                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_STOCK_CARD_SaveTransaction);
                throw ex;
            }
        }

        //// Old Code InventoryStockSave
        //public string InventoryStockSave(ItemStockET dataH, List<ItemStockET> dataD)
        //{
        //    try
        //    {
        //        string result = string.Empty;

        //        using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
        //        {
        //            conn.Open();

        //            for (int i = 0; i < dataD.Count; i++)
        //            {
        //                using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_STOCK_CARD_SaveTransaction, conn))
        //                {
        //                    cm.CommandType = CommandType.StoredProcedure;
        //                    cm.CommandTimeout = 600;

        //                    #region -- set param --
        //                    cm.Parameters.AddWithValue("@P_BRAND_CODE", dataH.BRAND_CODE);
        //                    cm.Parameters.AddWithValue("@P_BRANCH_CODE", dataH.BRANCH_CODE);
        //                    cm.Parameters.AddWithValue("@P_ITEM_CODE", dataD[i].ITEM_CODE);
        //                    cm.Parameters.AddWithValue("@P_SAVE_DATE", dataH.ITEM_PICK_DATE_SAVE);
        //                    cm.Parameters.AddWithValue("@P_REMAIN_QTY", dataD[i].REMAIN_QTY);
        //                    cm.Parameters.AddWithValue("@P_REMAIN_UOM", dataD[i].REMAIN_UOM);
        //                    cm.Parameters.AddWithValue("@P_UPDATE_BY", dataH.UPDATE_BY);
        //                    cm.Parameters.AddWithValue("@P_ACTION", "USAGE");

        //                    #endregion

        //                    result = cm.ExecuteScalar().ToString();

        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogDC dcLog = new LogDC();
        //        dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_STOCK_CARD_SaveTransaction);
        //        throw ex;
        //    }
        //}

        public int BlukInsertStockCard(DataTable d1) // Bluk Insert Not use Store
        {
            try
            {
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = d1.TableName;

                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(d1);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", "Bluk Insert " + d1.TableName);
                throw ex;
            }
        }

        public DashboardET CheckAlreadyCount(string brandCode, string branchCode)
        {
            try
            {
                DashboardET result = new DashboardET();
                //string result = "";
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_STOCK_CARD_CheckAlreadyCount, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", brandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", branchCode);

                        cm.ExecuteNonQuery();

                        #endregion
                        var reader = cm.ExecuteReader();

                        while (reader.Read())
                        {
                            result.AlreadyCount = Convert.ToInt16(reader["AlreadyCount"]);
                            result.MsgAlert = reader["MsgAlert"] != DBNull.Value ? (string)reader["MsgAlert"] : null;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.UFN_GEN_ST_PR_NO);
                throw ex;
            }
        }
    }
}
