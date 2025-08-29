using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.DC
{
    public class TB_M_BRANCH_DC : BaseDC
    {
        public bool IsFranchises(string brandCode, string branchCode)
        {
            try
            {
                var outputResult = false;
                var strConn = ConfigConst.CONN_STR_DEF;
                using (var conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand(StoreProcConst.USP_M_BRANCH__IsFranchises, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@P_BRAND_CODE", brandCode);
                        cmd.Parameters.AddWithValue("@P_BRANCH_CODE", branchCode);

                        var resultObj = cmd.ExecuteScalar();

                        if (resultObj != null)
                        {
                            if (resultObj.ToString() == "1" )
                            {
                                outputResult = true;
                            }
                            else
                            {
                                outputResult = false;
                            }
                            //outputResult = resultObj == "1" ? true : false;
                            //outputResult = (bool)resultObj;
                        }
                    }
                }

                return outputResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
