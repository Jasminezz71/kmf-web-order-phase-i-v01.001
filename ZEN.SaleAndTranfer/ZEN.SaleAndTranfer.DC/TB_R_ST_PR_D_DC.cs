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
    public class TB_R_ST_PR_D_DC : BaseDC
    {
        public USP_R_ST_PR_GetAlertAvgPRQty__RET GetAlertAvgPRQty(USP_R_ST_PR_GetAlertAvgPRQty__PET pet, out bool needAlertFlag)
        {
            SqlTransaction transaction = null;
            try
            {
                needAlertFlag = false;
                var result = new USP_R_ST_PR_GetAlertAvgPRQty__RET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_ST_PR_GetAlertAvgPRQty, conn))
                    {
                        cm.Transaction = transaction;
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_BRAND_CODE", pet.BrandCode);
                        cm.Parameters.AddWithValue("@P_BRANCH_CODE", pet.BranchCode);
                        cm.Parameters.AddWithValue("@P_ITEM_CODE", pet.ItemCode);
                        cm.Parameters.AddWithValue("@P_ITEM_UOM", pet.ItemUOM);
                        cm.Parameters.AddWithValue("@P_ORDER_QTY", pet.OrderQty);
                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            needAlertFlag = reader["NeedAlertFlag"] != DBNull.Value ? (bool)reader["NeedAlertFlag"] : false;
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                result.MessageCode = reader["MESSAGE_CODE"] != DBNull.Value ? (string)reader["MESSAGE_CODE"] : null;
                                result.MessageType = reader["MESSAGE_TYPE"] != DBNull.Value ? (string)reader["MESSAGE_TYPE"] : null;
                                result.MessageFormat = reader["MESSAGE_FORMAT"] != DBNull.Value ? (string)reader["MESSAGE_FORMAT"] : null;
                                result.MessageDispText = reader["MESSAGE_DISP_TEXT"] != DBNull.Value ? (string)reader["MESSAGE_DISP_TEXT"] : null;
                            }
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySession", StoreProcConst.USP_R_ST_PR_GetAlertAvgPRQty);
                throw ex;
            }
        }
    }
}
