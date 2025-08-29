using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET.CNF;

namespace ZEN.SaleAndTranfer.DC.CNF
{
    public class ConfigDC
    {
        public ConfigET GetConfigET(CategoryConfigEnum catetory, SubCategoryConfigEnum subCategory, ConfigNameEnum configName)
        {
            try
            {
                ConfigET configET = null;

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_C_CON_GetConfigByPK, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --

                        cm.Parameters.AddWithValue("@P_CATEGORY", catetory.ToString());
                        cm.Parameters.AddWithValue("@P_SUB_CATEGORY", subCategory.ToString());
                        cm.Parameters.AddWithValue("@P_CONFIG_NAME", configName.ToString());

                        #endregion

                        #region -- HET --

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            configET = new ConfigET();

                            configET.CATEGORY = reader["CATEGORY"] != DBNull.Value ? (string)reader["CATEGORY"] : null;
                            configET.SUB_CATEGORY = reader["SUB_CATEGORY"] != DBNull.Value ? (string)reader["SUB_CATEGORY"] : null;
                            configET.CONFIG_NAME = reader["CONFIG_NAME"] != DBNull.Value ? (string)reader["CONFIG_NAME"] : null;
                            configET.CONFIG_VALUE = reader["CONFIG_VALUE"] != DBNull.Value ? (string)reader["CONFIG_VALUE"] : null;
                            configET.ACTIVE_FLAG = reader["ACTIVE_FLAG"] != DBNull.Value ? (bool?)reader["ACTIVE_FLAG"] : null;
                            configET.REMARK = reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            configET.CREATE_BY = reader["CREATE_BY"] != DBNull.Value ? (string)reader["CREATE_BY"] : null;
                            configET.CREATE_DATE = reader["CREATE_DATE"] != DBNull.Value ? (DateTime?)reader["CREATE_DATE"] : null;
                            configET.UPDATE_BY = reader["UPDATE_BY"] != DBNull.Value ? (string)reader["UPDATE_BY"] : null;
                            configET.UPDATE_DATE = reader["UPDATE_DATE"] != DBNull.Value ? (DateTime?)reader["UPDATE_DATE"] : null;
                        }

                        #endregion

                    }
                }

                return configET;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_C_CON_GetConfigByPK);
                throw ex;
            }
        }
    }
}
