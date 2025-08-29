using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;

namespace ZEN.SaleAndTranfer.DC
{
    public class TB_M_COMPLAIN_DC : BaseDC
    {
        /// <summary>
        /// call store procedure : USP_M_COMPLAIN_GetByType
        /// </summary>
        /// <param name="COMPLAIN_TYPE">COMPLAIN_TYPE</param>
        /// <returns>List<USP_M_COMPLAIN_GetByType_RET></returns>
        public List<USP_M_COMPLAIN_GetByType_RET> GetByType(string COMPLAIN_TYPE, string ST_GR_CODE)
        {
            try
            {
                var result = new List<USP_M_COMPLAIN_GetByType_RET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    #region -- header --
                    using (var cm = new SqlCommand(StoreProcConst.USP_M_COMPLAIN_GetByType, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        cm.Parameters.AddWithValue("@P_COMPLAIN_TYPE", COMPLAIN_TYPE);
                        cm.Parameters.AddWithValue("@P_ST_GR_CODE", ST_GR_CODE);

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            #region -- set field value --
                            var ret = new USP_M_COMPLAIN_GetByType_RET();
                            ret.COMPLAIN_ID = reader["COMPLAIN_ID"] != DBNull.Value ? (int)reader["COMPLAIN_ID"] : 0;
                            ret.COMPLAIN_DESCRIPTION = reader["COMPLAIN_DESCRIPTION"] != DBNull.Value ? (string)reader["COMPLAIN_DESCRIPTION"] : null;
                            ret.COMPLAIN_TYPE = reader["COMPLAIN_TYPE"] != DBNull.Value ? (string)reader["COMPLAIN_TYPE"] : null;
                            ret.ORDER_DISPLAY = reader["ORDER_DISPLAY"] != DBNull.Value ? (int)reader["ORDER_DISPLAY"] : 0;
                            ret.SELECTED_RESULT_ID = reader["SELECTED_RESULT_ID"] != DBNull.Value ? (int)reader["SELECTED_RESULT_ID"] : 0;

                            result.Add(ret);
                            #endregion
                        }
                    }
                    #endregion
                }

                if (result.Count > 0)
                {
                    foreach (var topic in result)
                    {
                        var details = new List<USP_R_RESULT_COMPLAIN_GetByGrCode_RET>();

                        using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                        {
                            conn.Open();

                            #region -- Detail --
                            using (var cm = new SqlCommand(StoreProcConst.USP_R_RESULT_COMPLAIN_GetByGrCode, conn))
                            {
                                cm.CommandType = CommandType.StoredProcedure;

                                cm.Parameters.AddWithValue("@P_ST_GR_CODE", ST_GR_CODE);
                                cm.Parameters.AddWithValue("@P_COMPLAIN_ID", topic.COMPLAIN_ID);

                                var reader = cm.ExecuteReader();
                                while (reader.Read())
                                {
                                    #region -- set field value --
                                    var det = new USP_R_RESULT_COMPLAIN_GetByGrCode_RET();

                                    det.RESULT_ID = reader["RESULT_ID"] != DBNull.Value ? (int)reader["RESULT_ID"] : 0;
                                    det.COMPLAIN_ID = reader["COMPLAIN_ID"] != DBNull.Value ? (int)reader["COMPLAIN_ID"] : 0;
                                    det.RESULT_DESCRIPTION_MAS = reader["RESULT_DESCRIPTION_MAS"] != DBNull.Value ? (string)reader["RESULT_DESCRIPTION_MAS"] : null;
                                    det.RESULT_POINT_MAS = reader["RESULT_POINT_MAS"] != DBNull.Value ? (string)reader["RESULT_POINT_MAS"] : null;
                                    det.GR_COMPLAIN_ID = reader["GR_COMPLAIN_ID"] != DBNull.Value ? (int)reader["GR_COMPLAIN_ID"] : 0;
                                    det.ST_GR_CODE = reader["ST_GR_CODE"] != DBNull.Value ? (string)reader["ST_GR_CODE"] : null;
                                    det.SELECTED_RESULT_ID = reader["SELECTED_RESULT_ID"] != DBNull.Value ? (int)reader["SELECTED_RESULT_ID"] : 0;
                                    det.COMPLAIN_DESCRIPTION = reader["COMPLAIN_DESCRIPTION"] != DBNull.Value ? (string)reader["COMPLAIN_DESCRIPTION"] : null;
                                    det.RESULT_DESCRIPTION = reader["RESULT_DESCRIPTION"] != DBNull.Value ? (string)reader["RESULT_DESCRIPTION"] : null;
                                    det.RESULT_POINT = reader["RESULT_POINT"] != DBNull.Value ? (string)reader["RESULT_POINT"] : null;

                                    details.Add(det);
                                    #endregion
                                }
                            }
                            #endregion

                            topic.Childs = details;
                        }
                    }
                }

                return result.Count > 0 ? result : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
