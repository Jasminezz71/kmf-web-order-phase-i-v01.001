using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class UnitConvertSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string UNIT_FROM { get; set; }
        public string UNIT_TO { get; set; }
        public string NEW_UNIT_TO { get; set; }
        public string MUL { get; set; }
        public string DIV { get; set; }
        public string CONST { get; set; }
    }
}
