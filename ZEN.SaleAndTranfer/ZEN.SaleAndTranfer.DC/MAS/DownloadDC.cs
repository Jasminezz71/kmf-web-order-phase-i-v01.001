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
    public class DownloadDC
    {
        public List<DownloadSearchResultET> Search(DownloadSearchCriteriaET data)
        {
            try
            {
                List<DownloadSearchResultET> returnData = new List<DownloadSearchResultET>();
                DownloadSearchResultET dataET = new DownloadSearchResultET();

                using (var conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    conn.Open();

                    using (var cm = new SqlCommand(StoreProcConst.USP_R_DOWNLOAD_SearchFiles, conn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;

                        #region -- set param --
                        cm.Parameters.AddWithValue("@P_ROLE_NAME", data.ROLE_NAME);
                        cm.Parameters.AddWithValue("@P_APPLICATION_TYPE", data.APPLICATION_TYPE);
                        #endregion

                        int rowNo = 1;
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            dataET = new DownloadSearchResultET();
                            dataET.ROW_NO = rowNo;
                            dataET.LABEL = reader["LABEL"] != DBNull.Value ? (string)reader["LABEL"] : null;
                            dataET.FILE_PATH = reader["FILE_PATH"] != DBNull.Value ? (string)reader["FILE_PATH"] : null;
                            dataET.DETAIL = reader["DETAIL"] != DBNull.Value ? (string)reader["DETAIL"] : null;
                            dataET.DOWNLOAD_ID = Convert.ToInt16(reader["DOWNLOAD_ID"]);
                            dataET.STATUS = Convert.ToBoolean(reader["STATUS"]);
                            dataET.APPLICATION_TYPE = reader["APPLICATION_TYPE"] != DBNull.Value ? (string)reader["APPLICATION_TYPE"] : null;
                            dataET.CREATE_BY = reader["CREATE_BY"] != DBNull.Value ? (string)reader["CREATE_BY"] : null;
                            dataET.CREATE_DATE = reader["CREATE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["CREATE_DATE"]) : new DateTime?();
                            dataET.UPDATE_BY = reader["UPDATE_BY"] != DBNull.Value ? (string)reader["UPDATE_BY"] : null;
                            dataET.UPDATE_DATE = reader["UPDATE_DATE"] != DBNull.Value ? Convert.ToDateTime(reader["UPDATE_DATE"]) : new DateTime?();
                            returnData.Add(dataET);
                            rowNo++;
                        }
                    }
                }

                return returnData;
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromDC", StoreProcConst.USP_R_DOWNLOAD_SearchFiles);
                throw ex;
            }
        }
    }
}
