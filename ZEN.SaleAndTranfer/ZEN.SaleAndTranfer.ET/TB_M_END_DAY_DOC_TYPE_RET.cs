using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class TB_M_END_DAY_DOC_TYPE_RET : BaseET
    {
        public int END_DAY_DOC_TYPE_ID { get; set; }
        public string END_DAY_DOC_TYPE_NAME { get; set; }
        public bool DELETE_FLAG { get; set; }
        public string REMARK { get; set; }
        public string CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public int DISP_ORDER { get; set; }
        public string FILE_NAME_FORMAT { get; set; }

    }
}
