using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.DC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;
using ZEN.SaleAndTranfer.UI.VM2.ManualBatch;

namespace ZEN.SaleAndTranfer.UI.BC2
{
    public class ManualBatchBC2 : IManualBatchService
	{
		private readonly ManualBatchDC2 _dc;

		public ManualBatchBC2() : this(new ManualBatchDC2()) { }
		public ManualBatchBC2(ManualBatchDC2 dc) => _dc = dc;

		public RunVM2 RunInterfaceSTPRToNAV(RunVM2 vm)
		{

			var result = _dc.RunInterfaceSTPTSale(new ManualBatchET
			{
				CreateBy = vm.RunBy,
				ProcessType = vm.RunType,
				PRNo = vm.PRNo
			});

            if (result)
            {
                vm.RunBatchResult = new JsonResultET<string>() { SuccessFlag = true, Msg = $"ส่งข้อมูล {vm.PRNo} เข้า NAV เรียบร้อย ", Data = null };
            }
            else
            {
				vm.RunBatchResult = new JsonResultET<string>() { SuccessFlag = false, Msg = "ไม่สามารถส่งข้อมูลไป NAV ได้", Data = null };

			}

            return vm;
		}

		internal RunVM2 Run(RunVM2 vm)
        {
            try
            {
                var dc = new ManualBatchDC2();

                if (vm.BatchName == "Master")
                {
                    var dcResult = dc.RunMaster(pet: new USP_R_BATCH_MASTER__Pet() { CreateBy = vm.RunBy, ProcessType = "MANUAL" });

                    if (dcResult != null)
                    {
                        vm.RunBatchResult = new JsonResultET<string>()
                        {
                            Data = dcResult.BATCH_NO.ToString(),
                            Msg = dcResult.BATCH_STATUS,
                            SuccessFlag = dcResult.BATCH_STATUS == "COMPLETE" ? true : false
                        };
                    }
                    else
                    {
                        vm.RunBatchResult = new JsonResultET<string>()
                        {
                            Data = null,
                            Msg = "Batch Fail",
                            SuccessFlag = false
                        };
                    }
                }
                else
                {
                    throw new Exception("Invalid Batch Name.");
                }
                        
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

		
	
    }
}