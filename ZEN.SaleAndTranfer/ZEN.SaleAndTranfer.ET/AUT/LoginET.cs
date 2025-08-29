using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.AUT
{
    public class LoginET : BaseET
    {
        public string USER_NAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string OLD_PASSWORD { get; set; }
        public string NEW_PASSWORD { get; set; }
        public string NEW_CONFIRM_PASSWORD { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string CAPTCHA { get; set; }
        public string CAPTCHA_CONFIRM { get; set; }
    }
}
