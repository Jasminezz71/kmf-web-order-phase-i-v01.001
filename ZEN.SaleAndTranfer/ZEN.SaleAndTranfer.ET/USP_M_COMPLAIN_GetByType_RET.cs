using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class USP_M_COMPLAIN_GetByType_RET :BaseET
    {
        public int COMPLAIN_ID { get; set; }
        public string COMPLAIN_DESCRIPTION { get; set; }
        public string COMPLAIN_TYPE { get; set; }
        public int ORDER_DISPLAY { get; set; }

        public int SELECTED_RESULT_ID { get; set; }

        public List<USP_R_RESULT_COMPLAIN_GetByGrCode_RET> Childs { get; set; }
    }
}
