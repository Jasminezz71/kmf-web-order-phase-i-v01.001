using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZEN.SaleAndTranfer.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //Custom route for reports
            routes.MapPageRoute(
             "ReportRoute",                                         // Route name
             "Reports/View/{rptmode}/{reportname}/{*parameters}",   // URL
             "~/Reports/ReportView.aspx"                            // File
             );
        }
    }
}
