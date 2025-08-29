using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.AUT;

namespace ZEN.SaleAndTranfer.VM.AUT
{
    public class LoginVM : BaseVM
    {
        public LoginSearchCriteriaVM loginSearchCriteriaVM { get; set; }
        public LoginSearchResultVM loginResultResultVM { get; set; }
        public LoginET_MA loginVM_MA { get; set; }
    }
    public class LoginSearchCriteriaVM : LoginSearchCriteriaET
    {
    }

    public class LoginET_MA : LoginET
    {
    }

    public class LoginSearchResultVM
    {
        public List<UserLoginInfoET> resultList { get; set; }
        public int CountAll { get; set; }
    }
}
