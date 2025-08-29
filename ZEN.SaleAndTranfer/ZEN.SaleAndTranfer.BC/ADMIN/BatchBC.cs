using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.ADMIN;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.VM.ADMIN;

namespace ZEN.SaleAndTranfer.BC.ADMIN
{
    public class BatchBC
    {
        private BatchVM SplitPrCodeFromData(BatchVM vm)
        {
            try
            {
                if (vm.batchVM_MA.ST_PR_CODE != null)
                {
                    vm.batchVM_MA.StPrCodeList = new List<ET.ADMIN.BatchET>();
                    string stPrCode = vm.batchVM_MA.ST_PR_CODE.Replace("\r\n", "|");
                    vm.batchVM_MA.ST_PR_CODE = stPrCode;
                    //var split = stPrCode.Split('|');
                    //ET.ADMIN.BatchET batchPr = new ET.ADMIN.BatchET();
                    //foreach (var item in split)
                    //{
                    //    batchPr = new ET.ADMIN.BatchET();
                    //    var stPrCodeTrim = item.Trim(' ');
                    //    batchPr.PROCESS_BY = vm.SessionLogin.USER_NAME;
                    //    batchPr.ST_PR_CODE = stPrCodeTrim;
                    //    vm.batchVM_MA.StPrCodeList.Add(batchPr);
                    //}
                }
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchVM RunBatchPR(BatchVM vm)
        {
            try
            {
                vm = SplitPrCodeFromData(vm);

                int result = 0;
                BatchDC dc = new BatchDC();
                result = dc.RunBatchPR(vm.batchVM_MA.ST_PR_CODE, vm.SessionLogin.USER_NAME);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchVM RunBatchDO(BatchVM vm)
        {
            try
            {
                int result = 0;
                BatchDC dc = new BatchDC();
                result = dc.RunBatchDO();
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchVM RunBatchGR(BatchVM vm)
        {
            try
            {
                string grRepleaceData = string.Empty;
                if (vm.batchVM_MA.ST_GR_CODE != null) grRepleaceData = vm.batchVM_MA.ST_GR_CODE.Replace("\r\n", "|");
                int result = 0;
                BatchDC dc = new BatchDC();
                result = dc.RunBatchGR(grRepleaceData, vm.SessionLogin.USER_NAME);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BatchVM RunBatchMaster(BatchVM vm)
        {
            try
            {
                int result = 0;
                string processType = AccressTypeConst.MANUAL.ToString();
                BatchDC dc = new BatchDC();
                result = dc.RunBatchMaster(vm.SessionLogin.USER_NAME, processType);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BatchVM ExportSaesTranTOERP(BatchVM vm)
        {
            try
            {
                int result = 0;
                BatchDC dc = new BatchDC();
                vm.batchVM_MA = new BatchET_MA();
                vm.batchVM_MA.brandCode = vm.batchSearchCriteriaVM.BRAND_CODE;
                vm.batchVM_MA.branchCode = vm.batchSearchCriteriaVM.BRANCH_CODE;
                vm.batchVM_MA.fromDate = vm.batchSearchCriteriaVM.START_DATE;
                vm.batchVM_MA.toDate = vm.batchSearchCriteriaVM.END_DATE;
                result = dc.ExportSaesTranTOERP(vm.batchVM_MA.brandCode,vm.batchVM_MA.branchCode,vm.SessionLogin.USER_NAME,vm.batchVM_MA.fromDate,vm.batchVM_MA.toDate);
                if (result > 0) vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00010));
                else vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00011));
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BatchVM InnitialCriteria(BatchVM vm)
        {
            try
            {
                if (vm.batchSearchCriteriaVM == null) { vm.batchSearchCriteriaVM = new BatchSearchCriteriaVM(); }
                var ddlDC = new DDLDC();
                var brandList = ddlDC.GetBrandByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchList = ddlDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                if (vm.batchSearchCriteriaVM.BRAND_CODE != null)
                {
                    branchList = ddlDC.GetBranchbyBrandAndUsername(
                                                    vm.batchSearchCriteriaVM.BRAND_CODE,
                                                    vm.SessionLogin.USER_NAME
                                                    , DDLModeEnumET.SELECT_ALL);
                }


                vm.batchSearchCriteriaVM.brandList = brandList;
                vm.batchSearchCriteriaVM.branchList = branchList;
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
