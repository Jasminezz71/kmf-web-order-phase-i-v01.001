using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class BaseET
    {
        public string CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }
}
