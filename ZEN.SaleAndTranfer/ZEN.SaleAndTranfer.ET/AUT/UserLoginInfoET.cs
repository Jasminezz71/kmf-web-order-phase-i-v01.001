using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.ET.AUT
{
    public class UserLoginInfoET : BaseET
    {
        public int ROW_NO { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string NEW_PASSWORD { get; set; }
        public string NEW_PASSWORD_CONFIRM { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public string FIRST_NAME_TH { get; set; }
        public string LAST_NAME_TH { get; set; }
        public string FIRST_NAME_EN { get; set; }
        public string LAST_NAME_EN { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NO { get; set; }
        public string PHONE_EXT { get; set; }
        public string MOBILE_NO { get; set; }
        public string ACTIVE_FLAG { get; set; }
        public string ACTIVE_FLAG_DISPLAY { get; set; }
        public string USER_USM_TYPE { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }
        public string LOCKED_FLAG { get; set; }
        public string ROLE_NAME { get; set; }
        public string APPROVE_CONFIG_CODE { get; set; }
        public bool IS_APPROVER { get; set; }
        public bool IS_ADMIN { get; set; }
        public string IS_SALE_ADMIN { get; set; }
        public string SALE_ADMIN_CODE { get; set; }
        public string IS_RESET_PWD { get; set; }
        public bool FRANCHISES_FLAG { get; set; }

        public List<DDLItemET> branchList { get; set; }



        public int PAGE_INDEX { get; set; }
        public int ROW_PER_PAGE { get; set; }

        public int BRANCH_COUNT { get; set; }

        public List<UserLoginInfoET> StBranchCodeList { get; set; }
        public string RESON { get; set; }
    }
}
