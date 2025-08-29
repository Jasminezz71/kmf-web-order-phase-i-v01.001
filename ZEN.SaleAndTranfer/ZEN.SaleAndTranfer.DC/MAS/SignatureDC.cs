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
    public class SignatureDC
    {
        public List<SignatureSearchResultET> Search(SignatureSearchCriteriaET data)
        {
            try
            {
                List<SignatureSearchResultET> result = new List<SignatureSearchResultET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_SIGNATURE_Search, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                        cm.Parameters.AddWithValue("@P_COMPANY_CODE", data.COMPANY_CODE);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new SignatureSearchResultET();

                            newInstance.ROW_NO = Convert.ToInt16(reader["ROW_NO"]);
                            newInstance.IMAGE_ID = Convert.ToInt16(reader["IMAGE_ID"]);
                            newInstance.BRAND_CODE = reader["BRAND_CODE"].ToString();
                            newInstance.BRAND_NAME = reader["BRAND_NAME"].ToString();
                            newInstance.BRANCH_CODE = reader["BRANCH_CODE"].ToString();
                            newInstance.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                            newInstance.COMPANY_CODE = reader["COMPANY_CODE"].ToString();
                            newInstance.COMPANY_NAME = reader["COMPANY_NAME"].ToString();
                            newInstance.DELETE_FLAG = reader["DELETE_FLAG"].ToString();
                            newInstance.DELETE_DISPLAY = reader["DELETE_DISPLAY"].ToString();
                            newInstance.IMAGE_BITS = (byte[])reader["IMAGE_BITS"];
                            //newInstance.IMAGE_BITS = reader["IMAGE_BITS"].ToString() != string.Empty ? Convert.ToBase64String(Convert.ToByte(reader["IMAGE_BITS"].ToString())
                            newInstance.IMAGE_TYPE = reader["IMAGE_TYPE"].ToString();
                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_SIGNATURE_Search);
                throw ex;
            }
        }
        public int InsertSave(SignatureET data, List<SignatureET> fileUploadList)
        {
            try
            {
                int result = 0;

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_SIGNATURE_Insert, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        foreach (var item in fileUploadList)
                        {
                            cm.Parameters.AddWithValue("@P_BRAND_CODE", data.BRAND_CODE);
                            cm.Parameters.AddWithValue("@P_BRANCH_CODE", data.BRANCH_CODE);
                            cm.Parameters.AddWithValue("@P_ROLE_NAME", data.ROLE_NAME);
                            cm.Parameters.AddWithValue("@P_COMPANY_CODE", data.COMPANY_CODE);
                            cm.Parameters.AddWithValue("@P_IMAGE_NAME", item.FILE_NAME);
                            cm.Parameters.AddWithValue("@P_IMAGE_TYPE", item.CONTENT_TYPE);
                            cm.Parameters.AddWithValue("@P_IMAGE_BITS", item.ATTACHMENT);
                            cm.Parameters.AddWithValue("@P_DELETE_FLAG", data.DELETE_FLAG);
                            cm.Parameters.AddWithValue("@P_REMARK", data.REMARK);
                            cm.Parameters.AddWithValue("@P_CREATE_BY", data.CREATE_BY);
                            cm.Parameters.AddWithValue("@P_CRAETE_DATE", data.CREATE_DATE);
                        }
                        #endregion

                        result = Convert.ToInt16(cm.ExecuteScalar());
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_SIGNATURE_Insert);
                throw ex;
            }
        }
        public int Update(SignatureET data)
        {
            try
            {
                int result = 0;

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_ST_SIGNATURE_Update, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 60;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_IMAGE_ID", data.IMAGE_ID);
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
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_M_ST_SIGNATURE_Update);
                throw ex;
            }
        }
    }
}
