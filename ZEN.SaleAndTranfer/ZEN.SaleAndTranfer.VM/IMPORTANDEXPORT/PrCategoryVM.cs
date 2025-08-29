using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.IMPORTANDEXPORT;

namespace ZEN.SaleAndTranfer.VM.IMPORTANDEXPORT
{
    public class PrCategoryVM : BaseVM
    {
        public PrCategorySearchCriteriaVM prCategorySearchCriteriaVM { get; set; }
        public PrCategorySearchResultVM prCategorySearchResultVM { get; set; }
        public PrCategoryET_MA prCategoryVM_MA { get; set; }
    }
    public class PrCategorySearchCriteriaVM : PrCategorySearchCriteriaET
    {
    }

    public class PrCategoryET_MA : PrCategoryET
    {
    }

    public class PrCategorySearchResultVM
    {
        public List<PrCategorySearchResultET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
