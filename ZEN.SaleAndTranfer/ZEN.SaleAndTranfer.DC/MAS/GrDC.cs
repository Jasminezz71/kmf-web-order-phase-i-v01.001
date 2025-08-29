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
    public class GrDC
    {
        public List<GrSearchResultET> SearchGr(GrSearchCriteriaET data, string SortField, string Sorttype, out int countAll)
        {
            try
            {
                countAll = 0;
                List<GrSearchResultET> result = new List<GrSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_SEARCH, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_REQUEST_BY_BRAND_CODE", data.REQUEST_BY_BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_REQUEST_BY_BRANCH_CODE", data.REQUEST_BY_BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_ST_PR_CODE", data.ST_PR_CODE);
                        cm.Parameters.AddWithValue("@P_ST_DO_CODE", data.ST_DO_CODE);
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", data.ST_GR_CODE);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_FROM", data.PLAN_DELIVERY_DATE_FROM);
                        cm.Parameters.AddWithValue("@P_PLAN_DELIVERY_DATE_TO", data.PLAN_DELIVERY_DATE_TO);
                        cm.Parameters.AddWithValue("@P_PAGE_INDEX", data.PAGE_INDEX);
                        cm.Parameters.AddWithValue("@P_PAGE_SIZE", data.ROW_PER_PAGE);
                        cm.Parameters.AddWithValue("@P_SORT_BY", SortField);
                        cm.Parameters.AddWithValue("@P_SORT_DIRECTION", Sorttype);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new GrSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt32(reader["ROW_NO"]);
                            newInstance.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            newInstance.ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            newInstance.ST_GR_CODE = reader["ST_GR_CODE"].ToString();
                            newInstance.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            newInstance.LOCATION_CODE = reader["LOCATION_CODE"].ToString();
                            newInstance.SEND_FLAG = reader["SEND_FLAG"].ToString() != string.Empty ? Convert.ToBoolean(reader["SEND_FLAG"]) : false;
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_SEARCH);
                throw ex;
            }
        }
        public GrET GetGRHeaderByGRCode(string grCode)
        {
            try
            {
                GrET result = new GrET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_H_GetByGRCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", grCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new PrSearchResultET();

                            result.ST_PR_CODE = reader["ST_PR_CODE"].ToString();
                            result.ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            result.ST_GR_CODE = reader["ST_GR_CODE"].ToString();
                            result.PLAN_DELIVERY_DATE = reader["PLAN_DELIVERY_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["PLAN_DELIVERY_DATE"]) : null;
                            result.RECEIVE_DATE = reader["RECEIVE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["RECEIVE_DATE"]) : null;
                            result.RECEIVE_BY = reader["RECEIVE_BY"].ToString();
                            result.REFERANCE_NO = reader["REFERANCE_NO"].ToString();
                            result.REMARK = reader["REMARK"].ToString();
                            result.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            result.CREATE_BY = reader["CREATE_BY"].ToString();
                            result.CREATE_DATE = reader["CREATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["CREATE_DATE"]) : null;
                            result.UPDATE_BY = reader["UPDATE_BY"].ToString();
                            result.UPDATE_DATE = reader["UPDATE_DATE"].ToString() != string.Empty ? new DateTime?((DateTime)reader["UPDATE_DATE"]) : null;
                            //result.DO_STATUS_CODE = reader["DO_STATUS_CODE"] != DBNull.Value ? (int)reader["DO_STATUS_CODE"] : 0;
                            result.DO_STATUS_CODE = Convert.ToInt32(reader["DO_STATUS_CODE"]);
                            result.DO_STATUS_NAME = reader["DO_STATUS_NAME"].ToString();
                            
                            
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_H_GetByGRCode);
                throw ex;
            }
        }
        public List<GrET> GetGRDetailByGRCode(string grCode)
        {
            try
            {
                List<GrET> result = new List<GrET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_D_GetByGRCode, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", grCode);

                        #endregion

                        int row = 1;
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new GrET();

                            newInstance.ROW_NO = row;
                            newInstance.LINE_NO = Convert.ToInt32(reader["LINE_NO"]);
                            newInstance.ST_GR_CODE = reader["ST_GR_CODE"].ToString();
                            newInstance.ST_DO_CODE = reader["ST_DO_CODE"].ToString();
                            newInstance.ST_PR_CATEGORY_ID = reader["ST_PR_CATEGORY_ID"].ToString();
                            newInstance.ST_PR_CATEGORY_NAME = reader["ST_PR_CATEGORY_NAME"].ToString();
                            newInstance.ST_MAP_WH_ITEM_ID = reader["ST_MAP_WH_ITEM_ID"].ToString();
                            newInstance.ITEM_CODE = reader["ITEM_CODE"].ToString();
                            newInstance.ITEM_NAME_TH = reader["ITEM_NAME_TH"].ToString();
                            newInstance.ITEM_NAME_TH2 = reader["ITEM_NAME_TH2"].ToString();
                            newInstance.QTY_REQUEST = reader["QTY_REQUEST"].ToString();
                            newInstance.REQUEST_UOM_CODE = reader["REQUEST_UOM_CODE"].ToString();
                            newInstance.QTY_SEND = reader["QTY_SEND"].ToString();
                            newInstance.SEND_UOM_CODE = reader["SEND_UOM_CODE"].ToString();
                            newInstance.REQUEST_BY_BRAND_CODE = reader["REQUEST_BY_BRAND_CODE"].ToString();
                            newInstance.REQUEST_BY_BRANCH_CODE = reader["REQUEST_BY_BRANCH_CODE"].ToString();
                            newInstance.REQUEST_TO_BRAND_CODE = reader["REQUEST_TO_BRAND_CODE"].ToString();
                            newInstance.REQUEST_TO_BRANCH_CODE = reader["REQUEST_TO_BRANCH_CODE"].ToString();
                            newInstance.QTY_RECEIVE = reader["QTY_RECEIVE"].ToString();
                            newInstance.RECEIVE_UOM_CODE = reader["RECEIVE_UOM_CODE"].ToString();
                            newInstance.REMARK = reader["REMARK"].ToString();
                            newInstance.DELETE_FLAG = Convert.ToBoolean(reader["DELETE_FLAG"]);
                            newInstance.LOCATION_CODE = reader["LOCATION_CODE"].ToString();
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
                            newInstance.QTY_RECEIVED = reader["QTY_RECEIVED"] != DBNull.Value ? (decimal?)reader["QTY_RECEIVED"] : 0;
                            
                          
                            
                            result.Add(newInstance);
                            row++;
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_D_GetByGRCode);
                throw ex;
            }
        }
        public int UpdateGR(GrET data, List<GrET> dataD)
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
                        using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_H_Update, conn))
                        {
                            cm.Transaction = transaction;
                            cm.CommandType = CommandType.StoredProcedure;

                            #region -- set param --
                            cm.Parameters.AddWithValue("@P_ST_GR_CODE", data.ST_GR_CODE);
                            cm.Parameters.AddWithValue("@P_RECEIVE_BY", data.RECEIVE_BY);
                            cm.Parameters.AddWithValue("@P_RECEIVE_DATE", data.RECEIVE_DATE);
                            cm.Parameters.AddWithValue("@P_REFERANCE_NO", data.REFERANCE_NO);
                            cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                            cm.Parameters.AddWithValue("@P_DO_STATUS_CODE", data.DO_STATUS_CODE);

                            #endregion

                            cm.ExecuteNonQuery();
                        }
                        #endregion
                        #region -- detail --

                        if (dataD != null && dataD.Count > 0)
                        {
                            foreach (var detail in dataD)
                            {
                                using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_D_Update, conn))
                                {
                                    cm.Transaction = transaction;
                                    cm.CommandType = CommandType.StoredProcedure;

                                    #region -- set param --
                                    cm.Parameters.AddWithValue("@P_ST_GR_CODE", data.ST_GR_CODE);
                                    cm.Parameters.AddWithValue("@P_LINE_NO", detail.LINE_NO);
                                    cm.Parameters.AddWithValue("@P_QTY_RECEIVE", detail.QTY_RECEIVE);
                                    cm.Parameters.AddWithValue("@P_UPDATE_BY", data.UPDATE_BY);
                                    #endregion

                                    cm.ExecuteNonQuery();
                                }
                            }
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_H_Update + "|" + StoreProcConst.USP_R_ST_GR_D_Update);
                throw ex;
            }
        }
        public int GrDelete(GrET data)
        {
            try
            {
                int result = 0;
                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_Delete, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", data.ST_GR_CODE);
                        cm.Parameters.AddWithValue("@P_UDPATE_BY", data.UPDATE_BY);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_Delete);
                throw ex;
            }
        }

        //Supaneej, 2019-01-24, Update Complain 
        public int ComplainUpdate(GrET data, List<GrET> dataD, List<USP_M_COMPLAIN_GetByType_PET> complainPETs)
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
                        #region -- complain --

                        if (complainPETs != null && complainPETs.Count > 0)
                        {
                            foreach (var detail in complainPETs)
                            {
                                using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_GR_COMPLAIN_Save, conn))
                                {
                                    cm.Transaction = transaction;
                                    cm.CommandType = CommandType.StoredProcedure;

                                    #region -- set param --
                                    cm.Parameters.AddWithValue("@P_ST_GR_CODE", data.ST_GR_CODE);
                                    cm.Parameters.AddWithValue("@P_COMPLAIN_ID", detail.COMPLAIN_ID);
                                    cm.Parameters.AddWithValue("@P_RESULT_ID", detail.RESULT_ID);
                                    cm.Parameters.AddWithValue("@P_CREATE_BY", detail.CREATE_BY);
                                    #endregion

                                    cm.ExecuteNonQuery();
                                }
                            }
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_GR_COMPLAIN_Save);
                throw ex;
            }
        }
    }
}
