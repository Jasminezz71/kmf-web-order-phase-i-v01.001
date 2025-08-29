using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.DC
{
    internal sealed class ConfigConst
    {
        public static string CONN_STR_DEF = ConfigurationManager.ConnectionStrings["CONN_STR_DEF"].ConnectionString;
        public static string CONN_STR_DEF_HAVI = ConfigurationManager.ConnectionStrings["CONN_STR_DEF_HAVI"].ConnectionString;
        public static string CONN_STR_DEF_STAGING = ConfigurationManager.ConnectionStrings["CONN_STR_DEF_STAGING"].ConnectionString;
        

    }
}
