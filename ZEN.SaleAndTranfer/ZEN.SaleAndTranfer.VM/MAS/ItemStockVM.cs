using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;

namespace ZEN.SaleAndTranfer.VM.MAS
{
    public class ItemStockVM : BaseVM
    {
        public ItemStockSearchCriteriaVM itemStockSearchCriteriaVM { get; set; }
        public ItemStockSearchResultVM itemStockSearchResultVM { get; set; }
        public ItemStockET_MA itemStockVM_MA { get; set; }
    }
    public class ItemStockSearchCriteriaVM : ItemStockSearchCriteriaET
    {
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
        public List<DDLItemET> appSystemList { get; set; }
        public List<DDLItemET> itemStockTypeList { get; set; }
    }

    public class ItemStockET_MA : ItemStockET
    {
        public List<DDLItemET> appSystemList { get; set; }
        public List<DDLItemET> itemStockTypeList { get; set; }
        public List<ItemStockET> itemStockList { get; set; }
    }

    public class ItemStockSearchResultVM
    {
        public List<ItemStockSearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
