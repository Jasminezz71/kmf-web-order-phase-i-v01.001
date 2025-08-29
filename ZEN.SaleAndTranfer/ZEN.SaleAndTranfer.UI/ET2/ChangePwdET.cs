using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
    public class ChangePwdET
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "โปรดระบุ {0} {2} ถึง {1} ตัวอักษร", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "New Password และ Confirm New Password ไม่ตรงกัน")]
        public string ConfirmNewPassword { get; set; }

        public string ChanageBy { get; set; }
    }
}