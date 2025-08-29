using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT
{
    public class ImportFileDC
    {
        public void ImportToDB(DataTable d1)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigConst.CONN_STR_DEF))
                {
                    try
                    {
                        conn.Open();

                        transaction = conn.BeginTransaction();

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                        {
                            bulkCopy.DestinationTableName = "dbo.TB_T_ST_MAP_WH_ITEM__201610011";

                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(d1);
                        }

                        transaction.Commit();
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
                throw ex;
            }
        }
    }
}
