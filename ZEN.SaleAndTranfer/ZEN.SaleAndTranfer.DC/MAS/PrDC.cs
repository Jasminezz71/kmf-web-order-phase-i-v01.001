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
    public class PrDC
    {
        public List<PrSearchResultET> SearchPr(PrSearchCriteriaET data, string SortField, string Sorttype, out int countAll)
        {
            try
            {
                countAll = 0;
                List<PrSearchResultET> result = new List<PrSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_Search, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_FROM", data.PLAN_DELIVERY_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_TO", data.PLAN_DELIVERY_DATE_TO);
                        cm.Parameters.AddWithValue("@P_CREATE_DATE_FROM", data.CREATE_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_CREATE_DATE_TO", data.CREATE_DATE_TO);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_SORT_BY", SortField);
                        cm.Parameters.AddWithValue("@P_SORT_DIRECTION", Sorttype);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_NAME = reader["REQUEST_BY_BRAND_NAME"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_NAME = reader["REQUEST_BY_BRANCH_NAME"].ToString();
                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            newInstance.PR_STATUS_CODE = Convert.ToInt16(reader["PR_STATUS_CODE"].ToString());
                            newInstance.PR_STATUS_NAME = reader["PR_STATUS_NAME"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.DELETE_FLAG = reader["DELETE_FLAG"].ToString();
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_Search);
                throw ex;
            }
        }
        public string GetPRCode(string brandCode)
        {
            try
            {
                PrET result = new PrET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.UFN_GEN_ST_PR_NO, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --

                        SqlParameter p1 = new SqlParameter("@BRANCH_CODE", SqlDbType.VarChar);

                        // You can call the return value parameter anything, .e.g. "@Result".
                        SqlParameter p3 = new SqlParameter("@TMP_STR", SqlDbType.VarChar);

                        p1.Direction = ParameterDirection.Input;
                        p3.Direction = ParameterDirection.ReturnValue;

                        p1.Value = brandCode;

                        cm.Parameters.Add(p1);
                        cm.Parameters.Add(p3);

                        cm.ExecuteNonQuery();

                        #endregion

                        return p3.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.UFN_GEN_ST_PR_NO);
                throw ex;
            }
        }

        public List<PrSearchResultET> SearchAddItemCart(PrSearchCriteriaET data, out int countAll)
        {
            try
            {
                int rowNo = 1;
                countAll = 0;
                List<PrSearchResultET> result = new List<PrSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_SearchShoppingItem, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_REQEUST_BY_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_REQEUST_BY_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_REQUEST_TO_BRAND_CODE", data.REQUEST_TO_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_ST_PR_CATEGORY_ID", data.ST_PR_CATEGORY_CODE);
                        cm.Parameters.AddWithValue("@P_ITEM_CODE", data.ITEM_CODE);
                        cm.Parameters.AddWithValue("@P_ITEM_NAME_TH", data.ITEM_NAME);
                        cm.Parameters.AddWithValue("@P_START_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_REQUEST_TO_BRANCH_CODE", data.FC_LOCATION);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrSearchResultET();

                            //newInstance.ROW_NO = rowNo;
                            newInstance.ROW_NO = Convert.ToInt16(reader["ROW_NO"]);
                            newInstance.ST_MAP_WH_ITEM_ID = reader["ST_MAP_WH_ITEM_ID"].ToString();
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_TO_BRAND_CODE = reader["REQUEST_TO_BRAND_CODE"].ToString();
                            newInstance.REQUEST_TO_BRANCH_CODE = reader["REQUEST_TO_BRANCH_CODE"].ToString();
                            newInstance.ST_WH_ITEM_CATEGORY_ID = reader["ST_WH_ITEM_CATEGORY_ID"].ToString();
                            newInstance.ST_WH_ITEM_CATEGORY_NAME = reader["ST_WH_ITEM_CATEGORY_NAME"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME_TH = reader["ITEM_NAME_TH"].ToString();
                            newInstance.ITEM_DETAIL = reader["ITEM_NAME_TH2"].ToString();
                            newInstance.WH_UOM_CODE = reader["WH_UOM_CODE"].ToString();
                            newInstance.REQUEST_UOM_CODE = reader["REQUEST_UOM_CODE"].ToString();
                            newInstance.DELIVERY_UOM_CODE = reader["DELIVERY_UOM_CODE"].ToString();
                            newInstance.USE_START_DATE = reader["USE_START_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["USE_START_DATE"]) : null;
                            newInstance.USE_END_DATE = reader["USE_END_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["USE_END_DATE"]) : null;
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.ST_PR_CATEGORY_ID = reader["ST_PR_CATEGORY_ID"].ToString();
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
                            newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
                            newInstance.ITEM_QTY_AVG = Convert.ToDecimal(reader["ITEM_QTY_AVG"]);

                            newInstance.FILE_PATH = reader["FILE_PATH"] != DBNull.Value ? (string)reader["FILE_PATH"] : null;   //Supaneej, 2018-10-11, Add FILE_PATH for Image
                            newInstance.FULL_PATH = reader["FULL_PATH"] != DBNull.Value ? (string)reader["FULL_PATH"] : null;
                            //newInstance.FILE_NAME_WITH_EXTENSION = reader["FILE_NAME_WITH_EXTENSION"].ToString();  //Supaneej, 2018-10-25, Add FILE_PATH for Image
                            newInstance.FILE_NAME_WITH_EXTENSION = reader["FILE_NAME_WITH_EXTENSION"] != DBNull.Value ? (string)reader["FILE_NAME_WITH_EXTENSION"] : null;
                            //newInstance.SalePrice = Convert.ToDecimal(reader["SalePrice"]); //Supaneej, 2018-10-11, display SalePrice for Franchise
                            //newInstance.SALE_PRICE = reader["SALE_PRICE"] != DBNull.Value ? (decimal?)reader["SALE_PRICE"] : 0;
                            newInstance.REMAIN_QTY = reader["REMAIN_QTY"] != DBNull.Value ? (decimal?)reader["REMAIN_QTY"] : 0; //Ketsara, 2018-12-06  
                            //newInstance.ITEM_COLUMN = reader["ITEM_COLUMN"] != DBNull.Value ? (int?)reader["ITEM_COLUMN"] : 0; //Ketsara, 2018-12-07  
                            newInstance.UNIT_PRICE = reader["UNIT_PRICE"] != DBNull.Value ? (decimal?)reader["UNIT_PRICE"] : 0; //ketsara.k,2019-01-27


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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_SearchShoppingItem);
                throw ex;
            }
        }
        //public List<PrSearchResultET> SearchAddItemCart(PrSearchCriteriaET data, out int countAll)
        //{
        //    try
        //    {
        //        int rowNo = 1;
        //        countAll = 0;
        //        List<PrSearchResultET> result = new List<PrSearchResultET>();

        //        using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
        //        {
        //            conn.Open();

        //            using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_SearchShoppingItem, conn))
        //            {
        //                cm.CommandType = CommandType.StoredProcedure;
        //                cm.CommandTimeout = 60;

        //                #region -- set param --
        //                cm.Parameters.AddWithValue("@P_REQEUST_BY_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
        //                cm.Parameters.AddWithValue("@P_REQEUST_BY_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
        //                cm.Parameters.AddWithValue("@P_REQUEST_TO_BRAND_CODE", data.REQUEST_TO_BRAND_CODE);
        //                cm.Parameters.AddWithValue("@P_ST_PR_CATEGORY_ID", data.ST_PR_CATEGORY_CODE);
        //                cm.Parameters.AddWithValue("@P_ITEM_CODE", data.ITEM_CODE);
        //                cm.Parameters.AddWithValue("@P_ITEM_NAME_TH", data.ITEM_NAME);
        //                cm.Parameters.AddWithValue("@P_START_INDEX", data.PAGE_INDEX);
        //                cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);

        //                #endregion

        //                var reader = cm.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    var newInstance = new PrSearchResultET();

        //                    //newInstance.ROW_NO = rowNo;
        //                    newInstance.ROW_NO =  Convert.ToInt16(reader["ROW_NO"]);
        //                    newInstance.ST_MAP_WH_ITEM_ID = reader["ST_MAP_WH_ITEM_ID"].ToString();
        //                    newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
        //                    newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
        //                    newInstance.REQUEST_TO_BRAND_CODE = reader["REQUEST_TO_BRAND_CODE"].ToString();
        //                    newInstance.REQUEST_TO_BRANCH_CODE = reader["REQUEST_TO_BRANCH_CODE"].ToString();
        //                    newInstance.ST_WH_ITEM_CATEGORY_ID = reader["ST_WH_ITEM_CATEGORY_ID"].ToString();
        //                    newInstance.ST_WH_ITEM_CATEGORY_NAME = reader["ST_WH_ITEM_CATEGORY_NAME"].ToString();
        //                    newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
        //                    newInstance.ITEM_NAME_TH = reader["ITEM_NAME_TH"].ToString();
        //                    newInstance.ITEM_DETAIL = reader["ITEM_NAME_TH2"].ToString();
        //                    newInstance.WH_UOM_CODE = reader["WH_UOM_CODE"].ToString();
        //                    newInstance.REQUEST_UOM_CODE = reader["REQUEST_UOM_CODE"].ToString();
        //                    newInstance.DELIVERY_UOM_CODE = reader["DELIVERY_UOM_CODE"].ToString();
        //                    newInstance.USE_START_DATE = reader["USE_START_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["USE_START_DATE"]) : null;
        //                    newInstance.USE_END_DATE = reader["USE_END_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["USE_END_DATE"]) : null;
        //                    newInstance.REMARK = reader["REMARK"].ToString();
        //                    newInstance.ST_PR_CATEGORY_ID = reader["ST_PR_CATEGORY_ID"].ToString();
        //                    newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
        //                    newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
        //                    newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
        //                    newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
        //                    newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
        //                    newInstance.ITEM_QTY_AVG = Convert.ToDecimal(reader["ITEM_QTY_AVG"]);

        //                    result.Add(newInstance);
        //                    rowNo++;
        //                }
        //                if (reader.NextResult())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        countAll = Convert.ToInt32(reader["COUNT_ALL"]);
        //                    }
        //                }
        //            }
        //        }

        //        return result.Count > 0 ? result : null;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogDC dcLog = new LogDC();
        //        dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_SearchShoppingItem);
        //        throw ex;
        //    }
        //}
        public int ItemCartSave(PrET data, List<PrET> dataD)
        {
            SqlTransaction transaction = null;
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();

                        transaction = conn.BeginTransaction();
                        #region -- header --
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_H_Insert, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            //cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                            cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                            cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE", Convert.ToDateTime(data.PLAN_DELIVERY_DATE));
                            //cm.Parameters.AddWithValue("@P_SEND_FLAG", 0);
                            cm.Parameters.AddWithValue("@P_REMARK", data.REMARK);
                            cm.Parameters.AddWithValue("@P_DELETE_FLAG", 0);
                            cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                            cm.Parameters.AddWithValue("@P_CREATE_DATE", currentDate);

                            data.ST_PR_CODE = cm.ExecuteScalar().ToString();
                            #endregion
                        }
                        #endregion
                        #region -- detail --

                        if (dataD != null && dataD.Count > 0)
                        {
                            foreach (var detail in dataD)
                            {
                                using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_D_Insert, conn))
                                {
                                    cm.Transaction = transaction;
                                    cm.CommandType = CommandType.StoredProcedure;

                                    #region -- set param --
                                    cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                                    cm.Parameters.AddWithValue("@P_LINE_NO", detail.ROW_NO);
                                    cm.Parameters.AddWithValue("@P_ST_PR_CATEGORY_ID", detail.ST_PR_CATEGORY_ID);
                                    cm.Parameters.AddWithValue("@P_ST_MAP_WH_ITEM_ID", detail.ST_MAP_WH_ITEM_ID);
                                    cm.Parameters.AddWithValue("@P_REQUEST_BY_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_BY_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_TO_BRAND_CODE", detail.REQUEST_TO_BRAND_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_TO_BRANCH_CODE", detail.REQUEST_TO_BRANCH_CODE);
                                    cm.Parameters.AddWithValue("@P_ITEM_CODE", detail.ITEM_CODE);
                                    cm.Parameters.AddWithValue("@P_ITEM_QTY", detail.ITEM_QTY);
                                    cm.Parameters.AddWithValue("@P_ITEM_UNIT", detail.ITEM_UNIT);
                                    cm.Parameters.AddWithValue("@P_REMARK", detail.REMARK);
                                    cm.Parameters.AddWithValue("@P_DELETE_FLAG", 0);
                                    cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                                    cm.Parameters.AddWithValue("@P_CRAETE_DATE", currentDate);
                                    #endregion

                                    cm.ExecuteNonQuery();
                                }
                            }
                        }
                        #endregion
                        transaction.Commit();

                        #region -- Send to Navision --
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_SendToNavision, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion

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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_H_Insert + "|" + StoreProcConst.USP_R_ST_PR_D_Insert + "|" + StoreProcConst.USP_R_ST_PR_SendToNavision);
                throw ex;
            }
        }
        public int ItemCartUpdate(PrET data, List<PrET> dataD)
        {
            SqlTransaction transaction = null;
            try
            {
                DateTime currentDate = DateTime.Now;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();

                        transaction = conn.BeginTransaction();
                        #region -- header --
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_H_Update, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                            cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE", Convert.ToDateTime(data.PLAN_DELIVERY_DATE));
                            cm.Parameters.AddWithValue("@P_REMARK", data.REMARK);
                            cm.Parameters.AddWithValue("@P_DELETE_FLAG", 0);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_UPDATE_DATE", currentDate);

                            cm.ExecuteNonQuery();
                            #endregion
                        }
                        #endregion
                        #region -- delete detail --
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_D_Delete, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);

                            cm.ExecuteNonQuery();
                            #endregion
                        }
                        #endregion
                        #region -- insert detail --

                        if (dataD != null && dataD.Count > 0)
                        {
                            foreach (var detail in dataD)
                            {
                                using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_D_Update, conn))
                                {
                                    cm.Transaction = transaction;
                                    cm.CommandType = CommandType.StoredProcedure;

                                    #region -- set param --
                                    cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                                    cm.Parameters.AddWithValue("@P_LINE_NO", detail.ROW_NO);
                                    cm.Parameters.AddWithValue("@P_ST_PR_CATEGORY_ID", detail.ST_PR_CATEGORY_ID);
                                    cm.Parameters.AddWithValue("@P_ST_MAP_WH_ITEM_ID", detail.ST_MAP_WH_ITEM_ID);
                                    cm.Parameters.AddWithValue("@P_REQUEST_BY_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_BY_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_TO_BRAND_CODE", detail.REQUEST_TO_BRAND_CODE);
                                    cm.Parameters.AddWithValue("@P_REQUEST_TO_BRANCH_CODE", detail.REQUEST_TO_BRANCH_CODE);
                                    cm.Parameters.AddWithValue("@P_ITEM_CODE", detail.ITEM_CODE);
                                    cm.Parameters.AddWithValue("@P_ITEM_QTY", detail.ITEM_QTY);
                                    cm.Parameters.AddWithValue("@P_ITEM_UNIT", detail.ITEM_UNIT);
                                    cm.Parameters.AddWithValue("@P_REMARK", detail.REMARK);
                                    cm.Parameters.AddWithValue("@P_DELETE_FLAG", 0);
                                    cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                                    cm.Parameters.AddWithValue("@P_UPDATE_DATE", currentDate);
                                    #endregion

                                    cm.ExecuteNonQuery();
                                }
                            }
                        }
                        #endregion
                        transaction.Commit();

                        #region -- Send to Navision --
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_SendToNavision, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_H_Update + "|" + StoreProcConst.USP_R_ST_PR_D_Update + "|" + StoreProcConst.USP_R_ST_PR_D_Delete);
                throw ex;
            }
        }
        public PrET GetPRHeaderByPRCode(string prCode)
        {
            try
            {
                PrET result = new PrET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_H_GetByPRCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@ST_PR_CODE", prCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrSearchResultET();

                            result.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            result.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            result.PR_STATUS_CODE = Convert.ToInt16(reader["PR_STATUS_CODE"]);
                            //result.PR_STATUS_CODE = Convert.ToBoolean(reader["PR_STATUS_CODE"]);
                            result.REMARK = reader["REMARK"].ToString();
                            result.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            result.CREATE_BY = reader["CREATE_BY"].ToString();
                            result.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            result.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            result.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
                            result.REQUEST_TO_BRANCH_CODE = reader["REQUEST_TO_BRANCH_CODE"].ToString();
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_H_GetByPRCode);
                throw ex;
            }
        }
        public List<PrET> GetPRDetailByPRCode(string prCode, out USP_R_ST_PR_D_GetByPRCode_200_RET priceET)
        {
            try
            {
                List<PrET> result = new List<PrET>();
                //List<USP_R_ST_PR_D_GetByPRCode_200_RET> resultDetail = new List<USP_R_ST_PR_D_GetByPRCode_200_RET>();
                var et200 = new USP_R_ST_PR_D_GetByPRCode_200_RET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_D_GetByPRCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@ST_PR_CODE", prCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.ST_PR_CATEGORY_ID = reader["ST_PR_CATEGORY_ID"].ToString();
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
                            newInstance.ST_MAP_WH_ITEM_ID = reader["ST_MAP_WH_ITEM_ID"].ToString();
                            newInstance.ST_WH_ITEM_CATEGORY_NAME = reader["ST_WH_ITEM_CATEGORY_NAME"].ToString();
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_NAME = reader["REQUEST_BY_BRAND_NAME"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_NAME = reader["REQUEST_BY_BRANCH_NAME"].ToString();
                            newInstance.REQUEST_TO_BRAND_CODE = reader["REQUEST_TO_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_NAME = reader["REQUEST_BY_BRAND_NAME"].ToString();
                            newInstance.REQUEST_TO_BRANCH_CODE = reader["REQUEST_TO_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_NAME = reader["REQUEST_BY_BRANCH_NAME"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME = reader["ITEM_NAME"].ToString();
                            newInstance.ITEM_DETAIL = reader["ITEM_DETAIL"].ToString();
                            newInstance.ITEM_QTY = Convert.ToDecimal(reader["ITEM_QTY"]);
                            newInstance.ITEM_UNIT = reader["ITEM_UNIT"].ToString();
                            //newInstance.REQUEST_UOM_CODE = reader["REQUEST_UOM_CODE"].ToString();
                            newInstance.REQUEST_UOM_CODE = reader["ITEM_UNIT"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            newInstance.CREATE_BY = reader["CREATE_BY"].ToString();
                            newInstance.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            newInstance.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            newInstance.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;

                            newInstance.FILE_PATH = reader["FILE_PATH"] != DBNull.Value ? (string)reader["FILE_PATH"] : null;   //Supaneej, 2018-10-11, Add FILE_PATH for Image
                            newInstance.FULL_PATH = reader["FULL_PATH"] != DBNull.Value ? (string)reader["FULL_PATH"] : null;
                            //newInstance.FILE_NAME_WITH_EXTENSION = reader["FILE_NAME_WITH_EXTENSION"].ToString();  //Supaneej, 2018-10-25, Add FILE_PATH for Image
                            newInstance.FILE_NAME_WITH_EXTENSION = reader["FILE_NAME_WITH_EXTENSION"] != DBNull.Value ? (string)reader["FILE_NAME_WITH_EXTENSION"] : null;
                            //newInstance.SalePrice = Convert.ToDecimal(reader["SalePrice"]); //Supaneej, 2018-10-11, display SalePrice for Franchise
                            newInstance.SALE_PRICE = reader["SALE_PRICE"] != DBNull.Value ? (decimal?)reader["SALE_PRICE"] : 0;
                            newInstance.REMAIN_QTY = reader["REMAIN_QTY"] != DBNull.Value ? (decimal?)reader["REMAIN_QTY"] : 0; //Ketsara, 2018-12-06  
                            newInstance.UNIT_PRICE = reader["UNIT_PRICE"] != DBNull.Value ? (decimal?)reader["UNIT_PRICE"] : 0; //ketsara.k,2019-01-24
                            newInstance.ITEM_QTY_AVG = reader["ITEM_QTY_AVG"] != DBNull.Value ? (decimal?)reader["ITEM_QTY_AVG"] : 0; //ketsara.k 2019-01-30



                            result.Add(newInstance);
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                //var newInstance = new USP_R_ST_PR_D_GetByPRCode_200_RET();
                                et200.PRICE_BEFORE_VAT = reader["PRICE_BEFORE_VAT"] != DBNull.Value ? (decimal?)reader["PRICE_BEFORE_VAT"] : 0;
                                et200.VAT = reader["VAT"] != DBNull.Value ? (decimal?)reader["VAT"] : 0;
                                et200.PRICE_INCLUDE_VAT = reader["PRICE_INCLUDE_VAT"] != DBNull.Value ? (decimal?)reader["PRICE_INCLUDE_VAT"] : 0;

                                //resultDetail.Add(newInstance);
                            }
                        }
                    }
                }


                priceET = et200;
                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_D_GetByPRCode);
                throw ex;
            }
        }
        public int PrDelete(PrET data)
        {
            try
            {
                int result = 0;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_H_Delete, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                        cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                        cm.Parameters.AddWithValue("@P_UPDATE_DATE", data.UPDATE_DATE);

                        #endregion

                        result = Convert.ToInt16(cm.ExecuteScalar());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_H_Delete);
                throw ex;
            }
        }
        public string CheckPRDupplicate(string brandCode, string branchCode, DateTime? planDeliveryDate, string fcLocation)
        {
            try
            {
                string result = "";
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_CheckPRDupplicate, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", brandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", branchCode);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE", planDeliveryDate);
                        cm.Parameters.AddWithValue("@P_REQUEST_TO_BRANCH_CODE", fcLocation);

                        cm.ExecuteNonQuery();

                        #endregion
                        var reader = cm.ExecuteReader();

                        while (reader.Read())
                        {
                            result = reader["RESULT_DUPPLICATE"].ToString();
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

        public int PrBackToNew(PrET data)
        {
            try
            {
                int result = 0;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_BACK_TO_NEW, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                        cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                        #endregion

                        result = Convert.ToInt16(cm.ExecuteScalar());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_H_Delete);
                throw ex;
            }
        }
    }
}
