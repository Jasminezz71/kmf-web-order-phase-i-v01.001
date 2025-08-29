using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.BC.IMPORTANDEXPORT
{
    public class BatchBC
    {
        public int StartBatch(string startBy, string appName, string batchName)
        {
            try
            {
                var dc = new BatchDC();
                var batchID = dc.StartBatch(batchName, appName, startBy);
                this.InsertBatchLog(batchID, "Start", null, null, null, startBy);
                return batchID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ErrorBatch(int batchID, string errorMassage)
        {
            try
            {
                var dc = new BatchDC();
                dc.ErrorBatch(batchID, errorMassage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EndBatch(int batchID, string endBy)
        {
            try
            {
                var dc = new BatchDC();
                this.InsertBatchLog(batchID, "End", null, null, null, endBy);
                dc.EndBatch(batchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertBatchLog(int batchID, string logType, string msg1, string msg2, string remark, string log_by)
        {
            try
            {
                var dc = new BatchDC();
                dc.InsertBatchLog(batchID, logType, msg1, msg2, remark, log_by);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
