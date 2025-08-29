using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZEN.SaleAndTranfer.UI.Startup))]
namespace ZEN.SaleAndTranfer.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			DependencyConfig.Register();
		}
    }
}
