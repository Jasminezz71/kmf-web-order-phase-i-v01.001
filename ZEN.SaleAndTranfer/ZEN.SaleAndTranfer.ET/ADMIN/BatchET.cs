using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.ADMIN
{
    public class BatchET : BaseET
    {
        public string ST_PR_CODE { get; set; }
        public string ST_GR_CODE { get; set; }
        public List<BatchET> StPrCodeList { get; set; }
        public string PROCESS_BY { get; set; }
    }
}
