using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.DC.ADMIN;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.ADMIN;

namespace ZEN.SaleAndTranfer.BC.ADMIN
{
    public class BatchHaviBC
    {
        #region ---- Innitial ----

        public BatchHaviVM InnitialCriteria(BatchHaviVM vm)
        {
            try
            {
                if (vm.batchSearchCriteriaVM == null) { vm.batchSearchCriteriaVM = new BatchHaviSearchCriteriaVM(); }
                var ddlBC = new DDLBC();
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchHaviVM InnitialMA(BatchHaviVM vm)
        {
            try
            {
                if (vm.batchVM_MA == null) { vm.batchVM_MA = new BatchHaviET_MA(); }
                var ddlBC = new DDLBC();
                var companyList = ddlBC.GetCompany(DDLModeEnumET.SELECT_ONE);

                vm.batchVM_MA.companyList = companyList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private BatchHaviVM SplitFromData(BatchHaviVM vm)
        {
            try
            {
                if (vm.batchVM_MA.SEND_DATA != null)
                {
                    //vm.batchVM_MA.SEND_DATA = new List<ET.ADMIN.BatchET>();
                    string sendData = vm.batchVM_MA.SEND_DATA.Replace("\r\n", "|");
                    vm.batchVM_MA.SEND_DATA = sendData;
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //============================ Purchase ============================//
        private BatchHaviVM ValidateRunBatchPO(BatchHaviVM vm)
        {
            try
            {
                if (vm.batchVM_MA.SEND_DATA != null)
                {
                    if (vm.batchVM_MA.COMPANY_CODE == null || vm.batchVM_MA.COMPANY_CODE.Trim(' ') == "")
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00009, "บริษัท"));
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchHaviVM RunBatchPO(BatchHaviVM vm)
        {
            try
            {
                vm.MessageList.Clear();
                vm = this.ValidateRunBatchPO(vm);
                if (vm.MessageList.Count() > 0)
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                        return vm;

                vm = SplitFromData(vm);

                int result = 0;
                string processType = AccressTypeConst.MANUAL.ToString();
                BatchHaviDC dc = new BatchHaviDC();
                result = dc.RunBatchPO(vm.batchVM_MA.SEND_DATA, vm.SessionLogin.USER_NAME, processType, vm.batchVM_MA.COMPANY_CODE);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //============================ Warehouse ============================//
        public BatchHaviVM RunBatchRN(BatchHaviVM vm)
        {
            try
            {
                int result = 0;
                string processType = AccressTypeConst.MANUAL.ToString();
                BatchHaviDC dc = new BatchHaviDC();
                result = dc.RunBatchRN(vm.SessionLogin.USER_NAME, processType);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchHaviVM RunBatchSO(BatchHaviVM vm)
        {
            try
            {
                vm = SplitFromData(vm);

                int result = 0;
                string processType = AccressTypeConst.MANUAL.ToString();
                BatchHaviDC dc = new BatchHaviDC();
                result = dc.RunBatchSO(vm.batchVM_MA.SEND_DATA, vm.SessionLogin.USER_NAME, processType);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchHaviVM RunBatchDN(BatchHaviVM vm)
        {
            try
            {
                int result = 0;
                string processType = AccressTypeConst.MANUAL.ToString();
                BatchHaviDC dc = new BatchHaviDC();
                result = dc.RunBatchDN(vm.SessionLogin.USER_NAME, processType);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
