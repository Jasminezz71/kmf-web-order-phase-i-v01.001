using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.DC2;

namespace ZEN.SaleAndTranfer.UI.Util
{
    public class PagingHelper
    {
        public static string HomePage { get { return "HomePage"; } }
        public static string SearchItem { get { return "SearchItem"; } }
        public static string SearchPr { get { return "SearchItem"; } }
        public static string SearchUser { get { return "SearchUser"; } }

        public static int GetPageAmount(int? totalRecords, int? pageSize)
        {
            try
            {
                if (totalRecords == null)
                    return 0;

                if (pageSize == null)
                    return 1;

                if (pageSize.HasValue && pageSize.Value <= 1)
                    return 1;
                var x1 = (Convert.ToDecimal(totalRecords.Value) / Convert.ToDecimal(pageSize.Value));
                var y1 = Math.Ceiling(x1);
                //var y1 = Convert.ToInt32(x1) + 1;
                return Convert.ToInt32(y1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int? PageSize(string pageName)
        {
            try
            {
                var dc = new CommonDC2();
                return dc.GetPageSize(pageName: pageName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}