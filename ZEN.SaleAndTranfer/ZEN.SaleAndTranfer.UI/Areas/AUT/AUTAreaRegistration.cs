using System.Web.Mvc;

namespace ZEN.SaleAndTranfer.UI.Areas.AUT
{
    public class AUTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AUT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AUT_default",
                "AUT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}