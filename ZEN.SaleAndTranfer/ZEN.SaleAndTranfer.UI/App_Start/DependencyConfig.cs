using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ZEN.SaleAndTranfer.UI.BC2;
using ZEN.SaleAndTranfer.UI.VM2.ManualBatch;

public static class DependencyConfig
{
	public static void Register()
	{
		var container = new UnityContainer();
		container.RegisterType<IManualBatchService, ManualBatchBC2>();
		DependencyResolver.SetResolver(new UnityDependencyResolver(container));
	}
}