using System.Web.Mvc;

namespace ZEN.SaleAndTranfer.UI.Areas.IMPORTANDEXPORT
{
    public class IMPORTANDEXPORTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IMPORTANDEXPORT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IMPORTANDEXPORT_default",
                "IMPORTANDEXPORT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}