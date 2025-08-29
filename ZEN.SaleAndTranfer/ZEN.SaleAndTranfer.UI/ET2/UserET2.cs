using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
	public class UserET2
	{
		[Required]
		public string Username { get; set; }
		public string PasswordHash { get; set; }

		[Required]
		public string EmployeeID { get; set; }
		
		[Required]
		public string FirstNameTh { get; set; }

		[Required]
		public string LastNameTh { get; set; }
		public string FirstNameEn { get; set; }
		public string LastNameEn { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
				
		public string PhoneNo { get; set; }
		public string PhoneExt { get; set; }
		public string MobileNo { get; set; }

		[Required]
		public bool? ActiveFlag { get; set; }
		public string UserUsmType { get; set; }
		public string SaveBy { get; set; }
		public string Command { get; set; }

		[Required]
		public string Branch { get; set; }

		[Required]
		public string Role { get; set; }

		public string Mode { get; set; }

		public string Password { get; set; }

	}
}