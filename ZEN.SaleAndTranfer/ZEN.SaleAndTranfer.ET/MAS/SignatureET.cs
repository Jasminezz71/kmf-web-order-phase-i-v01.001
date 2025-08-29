using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class SignatureET : BaseET
    {
        public int? IMAGE_ID { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public string COMPANY_CODE { get; set; }
        public int? DELETE_FLAG { get; set; }
        public string REMARK { get; set; }

        #region === Upload File ===
        public HttpPostedFileBase FILE { get; set; }
        public string FILE_PATH { get; set; }
        public byte[] ATTACHMENT { get; set; }
        public string FILE_NAME { get; set; }
        public int FILE_SIZE { get; set; }
        public string CONTENT_TYPE { get; set; }
        public string FILE_EXTENSION { get; set; }
        #endregion

    }
}
