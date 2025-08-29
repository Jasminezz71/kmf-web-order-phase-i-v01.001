using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class ItemCategoryVM : BaseVM
    {
        public ItemCategorySearchCriteriaVM itemCategorySearchCriteriaVM { get; set; }
        public ItemCategorySearchResultVM itemCategorySearchResultVM { get; set; }
        public ItemCategoryET_MA itemCategoryVM_MA { get; set; }
    }
    public class ItemCategorySearchCriteriaVM : ItemCategorySearchCriteriaET
    {
        public List<DDLItemET> itemCategoryCategoryList { get; set; }
    }

    public class ItemCategoryET_MA : ItemCategoryET
    {
        public List<ItemCategoryET> itemCategoryList { get; set; }
    }

    public class ItemCategorySearchResultVM
    {
        public List<ItemCategorySearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
