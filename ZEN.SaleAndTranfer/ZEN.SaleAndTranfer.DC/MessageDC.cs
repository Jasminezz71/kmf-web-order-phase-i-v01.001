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
    public class MessageDC
    {
        public MessageET GetMessage(string msgCode)
        {
            try
            {
                List<MessageET> result = new List<MessageET>();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_M_GetMessage, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --

                        cm.Parameters.AddWithValue("@P_MESSAGE_CODE", msgCode);

                        #endregion

                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            var newInstance = new MessageET();

                            newInstance.MESSAGE_CODE_DISP = reader["MESSAGE_CODE_DISP"] != DBNull.Value ? (string)reader["MESSAGE_CODE_DISP"] : null;
                            newInstance.MESSAGE_CODE = reader["MESSAGE_CODE"] != DBNull.Value ? (string)reader["MESSAGE_CODE"] : null;
                            newInstance.MESSAGE_TYPE = reader["MESSAGE_TYPE"] != DBNull.Value ? (string)reader["MESSAGE_TYPE"] : null;
                            newInstance.MESSAGE_TEXT_FOR_DISPLAY = reader["MESSAGE_TEXT_FOR_DISPLAY"] != DBNull.Value ? (string)reader["MESSAGE_TEXT_FOR_DISPLAY"] : null;
                            newInstance.MESSAGE_TEXT = reader["MESSAGE_TEXT"] != DBNull.Value ? (string)reader["MESSAGE_TEXT"] : null;
                            newInstance.ACTIVE_FLAG = reader["ACTIVE_FLAG"] != DBNull.Value ? new bool?((bool)reader["ACTIVE_FLAG"]) : null;
                            newInstance.REMARK = reader["REMARK"] != DBNull.Value ? (string)reader["REMARK"] : null;
                            newInstance.ACTIVE_FLAG = reader["ACTIVE_FLAG"] != DBNull.Value ? new bool?((bool)reader["ACTIVE_FLAG"]) : null;

                            result.Add(newInstance);
                        }
                    }
                }

                return result.Count > 0 ? result[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
