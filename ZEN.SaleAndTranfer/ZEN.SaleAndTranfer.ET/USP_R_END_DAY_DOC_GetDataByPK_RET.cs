using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class USP_R_END_DAY_DOC_GetDataByPK_RET : BaseET
    {
        public int END_DAY_DOC_ID { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? END_DAY_DATE { get; set; }
        public int END_DAY_DOC_TYPE_ID { get; set; }
        public string END_DAY_DOC_TYPE_NAME { get; set; }
        public string FILE_NAME_ORI { get; set; }
        public string FILE_NAME_DEST { get; set; }
        public string FILE_PATH { get; set; }
        public int FILE_SIZE { get; set; }
        public string FILE_CONTENT_TYPE { get; set; }
        public string REMARK { get; set; }
        public bool? DELETE_FLAG { get; set; }
    }
}
