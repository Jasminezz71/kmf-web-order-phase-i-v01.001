using System.Web.Mvc;

namespace ZEN.SaleAndTranfer.UI.Areas.MAS
{
    public class MASAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MAS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MAS_default",
                "MAS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}