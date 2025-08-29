using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.VM2.ManualBatch
{
    public class RunVM2
    {
		[Required(ErrorMessage = "กรุณากรอกเลขที่ใบสั่งซื้อ")]
		public string PRNo { get; set; }
        public string BatchName { get; set; }
        public string RunBy { get; set; }
        public string RunType { get; set; }
        
        public JsonResultET<string> RunBatchResult { get; set; }


    }
}