using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class ItemAndItemCategoryVM : BaseVM
    {
        public ItemAndItemCategorySearchCriteriaVM itemAndItemCategorySearchCriteriaVM { get; set; }
        public ItemAndItemCategorySearchResultVM itemAndItemCategorySearchResultVM { get; set; }
        public ItemAndItemCategoryET_MA itemAndItemCategoryVM_MA { get; set; }
    }
    public class ItemAndItemCategorySearchCriteriaVM : ItemAndItemCategorySearchCriteriaET
    {
    }

    public class ItemAndItemCategoryET_MA : ItemAndItemCategoryET
    {
    }

    public class ItemAndItemCategorySearchResultVM
    {
        public List<ItemAndItemCategorySearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
