using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.UI.VM2.ManualBatch
{
	public interface IManualBatchService
	{
		RunVM2 RunInterfaceSTPRToNAV(RunVM2 vm);
	}
}
