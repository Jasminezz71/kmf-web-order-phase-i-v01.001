using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class SignatureSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public int IMAGE_ID { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string DELETE_FLAG { get; set; }
        public string DELETE_DISPLAY { get; set; }
        public byte[] IMAGE_BITS { get; set; }
        public string IMAGE_TYPE { get; set; }
    }
}
