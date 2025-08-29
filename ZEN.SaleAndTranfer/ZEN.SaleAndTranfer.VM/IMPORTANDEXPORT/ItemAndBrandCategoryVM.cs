using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class ItemAndBrandCategoryVM : BaseVM
    {
        public ItemAndBrandCategorySearchCriteriaVM itemAndBrandCategorySearchCriteriaVM { get; set; }
        public ItemAndBrandCategorySearchResultVM itemAndBrandCategorySearchResultVM { get; set; }
        public ItemAndBrandCategoryET_MA itemAndBrandCategoryVM_MA { get; set; }
    }
    public class ItemAndBrandCategorySearchCriteriaVM : ItemAndBrandCategorySearchCriteriaET
    {
    }

    public class ItemAndBrandCategoryET_MA : ItemAndBrandCategoryET
    {
    }

    public class ItemAndBrandCategorySearchResultVM
    {
        public List<ItemAndBrandCategorySearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
