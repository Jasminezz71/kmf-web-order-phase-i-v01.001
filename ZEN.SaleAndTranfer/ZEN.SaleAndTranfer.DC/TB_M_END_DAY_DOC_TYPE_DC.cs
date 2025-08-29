using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET;

namespace ZEN.SaleAndTranfer.DC
{
    public class TB_M_END_DAY_DOC_TYPE_DC : BaseDC
    {
        public TB_M_END_DAY_DOC_TYPE_RET GetByPK(int id)
        {
            SqlTransaction transaction = null;
            try
            {
                var result = new TB_M_END_DAY_DOC_TYPE_RET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_END_DAY_DOC_TYPE__GetByPK, conn))
                    {
                        cm.Transaction = transaction;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_END_DAY_DOC_TYPE_ID", id);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            result.END_DAY_DOC_TYPE_ID = reader["END_DAY_DOC_TYPE_ID"] != DBNull.Value ? (int)reader["END_DAY_DOC_TYPE_ID"] : 0;
                            result.END_DAY_DOC_TYPE_NAME = reader["END_DAY_DOC_TYPE_NAME"] != DBNull.Value ? (string)reader["END_DAY_DOC_TYPE_NAME"] : null;
                            result.DELETE_FLAG = reader["DELETE_FLAG"] != DBNull.Value ? (bool)reader["DELETE_FLAG"] : false;
                            result.REMARK = reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            result.CREATE_BY = reader["CREATE_BY"] != DBNull.Value ? (string)reader["CREATE_BY"] : null;
                            result.CREATE_DATE = reader["CREATE_DATE"] != DBNull.Value ? (DateTime?)reader["CREATE_DATE"] : null;
                            result.UPDATE_BY = reader["UPDATE_BY"] != DBNull.Value ? (string)reader["UPDATE_BY"] : null;
                            result.UPDATE_DATE = reader["UPDATE_DATE"] != DBNull.Value ? (DateTime?)reader["UPDATE_DATE"] : null;
                            result.DISP_ORDER = reader["DISP_ORDER"] != DBNull.Value ? (int)reader["DISP_ORDER"] : 0;
                            result.FILE_NAME_FORMAT = reader["FILE_NAME_FORMAT"] != DBNull.Value ? (string)reader["FILE_NAME_FORMAT"] : null;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_END_DAY_DOC_GetByPK);
                throw ex;
            }
        }
    }
}
