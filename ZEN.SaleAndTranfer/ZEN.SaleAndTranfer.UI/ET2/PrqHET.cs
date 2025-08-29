using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
    public class PrqHET
    {
        [Required]
        public DateTime? PlanDeliveryDate { get; set; }

        [Required]
        public string OrderBy { get; set; }
        
        [Required]
        public string MobileNo { get; set; }


        public string PoCode { get; set; }
        public string Remark { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public int? CustDummy { get; set; }

        public string City { get; set; }

        public string isSaleAdmin { get; set; }

        public string SaleAdminCode { get; set; }

        public string ConfigValue { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PrCode { get; set; }
       
        public string CreateBy { get; set; }
        public string BranchCode { get; set; }

        public string BrandCode { get; set; }
        public int? StPrCategoryID { get; set; }

        public decimal? SumTotalPrice { get; set; }

        public int CheckTransport { get; set; }

        public List<BasketItemET> Detail { get; set; }

    }
}