using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
    public class USP_C_CONFIG__SavePopupPromotionItem__Pet
    {
        [Required]
        public string ItemCodes { get; set; }
        public string UpdateBy { get; set; }
    }
}