using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ZEN.SaleAndTranfer.UI.DC2
{
    public class ManualBatchDC2
    {
        internal USP_R_BATCH_MASTER_Result RunMaster(USP_R_BATCH_MASTER__Pet pet)
        {
            try
            {
                USP_R_BATCH_MASTER_Result result = null;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_BATCH_MASTER(cREATE_BY: pet.CreateBy, pROCESS_TYPE: pet.ProcessType).FirstOrDefault();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

		internal bool RunInterfaceSTPTSale(ManualBatchET pet)
		{
			try
			{
				bool affected = false;
				using (var db = new MainEntities())
				{
					var flagParam = new ObjectParameter("ResultFlag", typeof(int));
					var msgParam = new ObjectParameter("ResultMsg", typeof(string));
					var result = db.USP_R_BATCH_SEND_ST_PR_SALE_MANUAL_KMF(pRNO: pet.PRNo, pRPCESS_BY: pet.CreateBy, pROCESS_TYPE: pet.ProcessType, flagParam, msgParam);

					int flag = (int)flagParam.Value;
					string msg = (string)msgParam.Value;

					if (result == 1)
					{
						affected = true;
						//		
						//using (var trx = db.Database.BeginTransaction())
						//{
						//	try
						//	{
						//		db.USP_R_BATCH_SEND_ST_PR_SALE_MANUAL_KMF(pRNO: pet.PRNo, pRPCESS_BY: pet.CreateBy, pROCESS_TYPE: pet.ProcessType);
						//		db.SaveChanges();
						//		trx.Commit();
						//	}
						//	catch (Exception tex)
						//	{
						//		trx.Rollback();
						//		throw tex;
						//	}
						//}
					}
					else if (result == 0)
					{
						affected = false;
					}

					return affected;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		
	}
}