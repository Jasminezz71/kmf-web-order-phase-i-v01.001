using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class USP_R_ST_PR_D_GetByPRCode_200_RET
    {
        //-----ketsara.k-----//
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:F2}")]
        public decimal? PRICE_BEFORE_VAT { get; set; }
        public decimal? VAT { get; set; }
        public decimal? PRICE_INCLUDE_VAT { get; set; }
        public decimal? UNIT_PRICE { get; set; }
    }
}
