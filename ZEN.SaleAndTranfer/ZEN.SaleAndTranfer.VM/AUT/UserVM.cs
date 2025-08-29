using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.VM.AUT
{
    public class UserVM : BaseVM
    {
        public UserSearchCriteriaVM userSearchCriteriaVM { get; set; }
        public UserSearchResultVM userSearchResultVM { get; set; }
        public UserET_MA userVM_MA { get; set; }
    }
    public class UserSearchCriteriaVM : UserLoginInfoET
    {
        public List<DDLItemET> activeFlagList { get; set; }
    }

    public class UserET_MA : UserLoginInfoET
    {
        public string PAGE_MODE { get; set; }
        public List<DDLItemET> activeFlagList { get; set; }
        public List<DDLItemET> brandList { get; set; }
        public List<DDLItemET> branchList { get; set; }
    }

    public class UserSearchResultVM
    {
        public List<UserLoginInfoET> resultList { get; set; }
        public int countAll { get; set; }
    }
}
