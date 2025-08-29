using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.CNF
{
    public class ConfigET : BaseET
    {
        public string CATEGORY { get; set; }
        public string SUB_CATEGORY { get; set; }
        public string CONFIG_NAME { get; set; }
        public string CONFIG_VALUE { get; set; }
        public bool? ACTIVE_FLAG { get; set; }
        public string REMARK { get; set; }
    }
}
