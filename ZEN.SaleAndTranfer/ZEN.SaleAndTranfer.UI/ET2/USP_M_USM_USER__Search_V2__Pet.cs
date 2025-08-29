using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
	public class USP_M_USM_USER__Search_V2__Pet
	{ 
		public string Username { get; set; }
		public string EmployeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool? ActiveFlag { get; set; }
		public string Branch { get; set; }
		public string Role { get; set; }

		public string OrderByList { get; set; }
		public int? PageSize { get; set; }
		public int CurrentPageId { get; set; }
	}
}