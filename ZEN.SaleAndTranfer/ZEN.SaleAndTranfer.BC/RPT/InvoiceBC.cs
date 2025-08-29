using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.BC.DDL;
using ZEN.SaleAndTranfer.DC.LOG;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.RPT;

namespace ZEN.SaleAndTranfer.BC.RPT
{
    public class InvoiceBC
    {
        public InvoiceVM InnitialCriteria(InvoiceVM vm)
        {
            try
            {
                DDLBC bc = new DDLBC();
                if (vm.invoiceSearchCriteriaVM == null) vm.invoiceSearchCriteriaVM = new InvoiceSearchCriteriaVM();
                vm.invoiceSearchCriteriaVM.companyList = bc.GetCompany(DDLModeEnumET.SELECT_ALL);
                //vm.invoiceSearchCriteriaVM.brandList = bc.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private InvoiceVM ValidateDataForRevenue(InvoiceVM vm)
        {
            try
            {
                if (vm.invoiceSearchCriteriaVM.COMPANY_CODE == null)
                {
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00009, new string[] { "บริษัท" }));
                }
                if (vm.invoiceSearchCriteriaVM.POSTING_DATE_FROM.HasValue && vm.invoiceSearchCriteriaVM.POSTING_DATE_TO.HasValue)
                {
                    if (vm.invoiceSearchCriteriaVM.POSTING_DATE_FROM.Value > vm.invoiceSearchCriteriaVM.POSTING_DATE_TO.Value)
                    {
                        vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00019, new string[] { "วันที่ Post ตั้งแต่", "ถึง" }));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vm;
        }
        public InvoiceVM ViewInvoice(InvoiceVM vm)
        {
            try
            {
                #region === Validate Data To Navision ===

                vm.MessageList.Clear();
                vm = this.ValidateDataForRevenue(vm);
                if (vm.MessageList.Count > 0)
                {
                    if (vm.MessageList[0].MESSAGE_TYPE.Equals("ERR"))
                    {
                        return vm;
                    }
                }

                #endregion

                string postingDateFrom = string.Empty, postingDateTo = string.Empty;
                if (vm.invoiceSearchCriteriaVM.POSTING_DATE_FROM.HasValue) postingDateFrom = vm.invoiceSearchCriteriaVM.POSTING_DATE_FROM.Value.ToString("yyyy-MM-dd");
                if (vm.invoiceSearchCriteriaVM.POSTING_DATE_TO.HasValue) postingDateTo = vm.invoiceSearchCriteriaVM.POSTING_DATE_TO.Value.ToString("yyyy-MM-dd");
                vm.invoiceVM_MA = new InvoiceET_MA();
                vm.invoiceVM_MA.REPORT_DATA = string.Empty;
                vm.invoiceVM_MA.REPORT_DATA += vm.invoiceSearchCriteriaVM.COMPANY_CODE + "|";
                vm.invoiceVM_MA.REPORT_DATA += vm.invoiceSearchCriteriaVM.P_NO + "|";
                vm.invoiceVM_MA.REPORT_DATA += postingDateFrom + "|";
                vm.invoiceVM_MA.REPORT_DATA += postingDateTo;
                //vm.invoiceVM_MA.REPORT_DATA += vm.invoiceSearchCriteriaVM.BRAND_CODE + "|";
                //vm.invoiceVM_MA.REPORT_DATA += vm.invoiceSearchCriteriaVM.BRANCH_CODE;

                vm.invoiceVM_MA.REPORT_DATA = vm.invoiceVM_MA.REPORT_DATA.Replace("/", "$slh");
                vm.invoiceVM_MA.REPORT_DATA = vm.invoiceVM_MA.REPORT_DATA.Replace(" ", "$spe");
                vm.invoiceVM_MA.REPORT_DATA = vm.invoiceVM_MA.REPORT_DATA.Replace(":", "$smc");
            }
            catch (Exception ex)
            {
                LogDC dcLog = new LogDC();
                dcLog.InsertLogDC(ex, "dummySessionFromBC", "UserBC Function DataForRevenue");
                throw ex;
            }
            return vm;
        }
    }
}
