using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class USP_R_RESULT_COMPLAIN_GetByGrCode_RET :BaseET
    {
        public int RESULT_ID { get; set; }
        //public int SELECTED_RESULT_ID { get; set; }
        public int COMPLAIN_ID { get; set; }
        public string RESULT_DESCRIPTION_MAS { get; set; }
        public string RESULT_POINT_MAS { get; set; }
        public int GR_COMPLAIN_ID { get; set; }
        public string ST_GR_CODE { get; set; }
        public int SELECTED_RESULT_ID { get; set; }
        public string COMPLAIN_DESCRIPTION { get; set; }
        public string RESULT_DESCRIPTION { get; set; }
        public string RESULT_POINT { get; set; }
        public bool DELETE_FLAG { get; set; }
        public string REMARK { get; set; }

        //public string CREATE_BY { get; set; }
        //public DateTime? CREATE_DATE { get; set; }
        //public string UPDATE_BY { get; set; }
        //public DateTime? UPDATE_DATE { get; set; }

        public USP_M_COMPLAIN_GetByType_RET Parent { get; set; }
    }
}
