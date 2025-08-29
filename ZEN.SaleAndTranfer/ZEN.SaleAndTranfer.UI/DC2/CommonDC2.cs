using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.DC2
{
    public class CommonDC2
    {
        public int? GetPageSize(string pageName)
        {
            try
            {
                int? result = 0;

                using (var db = new MainEntities())
                {
                    result = db.USP_COMMON_SearchResult__GetPageSize(pageName: pageName).FirstOrDefault();
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