using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.DC2
{
    public class DdlDC2
    {
        public List<USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode_Result> GetPRCategoryBrand(string brandCode)
        {
            try
            {
                List<USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode_Result> result = null;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode(p_BRAND_CODE: brandCode, p_ACTIVE_FLAG: true).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}