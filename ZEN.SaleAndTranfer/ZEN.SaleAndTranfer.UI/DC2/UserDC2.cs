using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.DC2
{
	public class UserDC2
	{
		public List<USP_M_USM_USER__Search_V2_Result> Search(USP_M_USM_USER__Search_V2__Pet pet)
		{
			try
			{
				List<USP_M_USM_USER__Search_V2_Result> result = null;

				using (var db = new MainEntities())
				{
					result = db.USP_M_USM_USER__Search_V2(
						 username: pet.Username,
						  employeeID: pet.EmployeeID,
						   firstName: pet.FirstName,
							lastName: pet.LastName,
							 email: pet.Email,
							  activeFlag: pet.ActiveFlag,
							   branch: pet.Branch,
							   roleName: pet.Role,
								orderByList: pet.OrderByList,
								 pageSize: pet.PageSize,
								  currentPageId: pet.CurrentPageId
						).ToList();
				}

				return result;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}


		public List<USP_M_USM_USER__Search_V2_Result> SearchUser(USP_M_USM_USER__Search_V2__Pet pet)
		{
			try
			{
				List<USP_M_USM_USER__Search_V2_Result> result = null;

				using (var db = new MainEntities())
				{
					result = db.USP_M_USM_USER__Search(
						 username: pet.Username,
						  employeeID: pet.EmployeeID,
						   firstName: pet.FirstName,
							lastName: pet.LastName,
							 email: pet.Email,
							  activeFlag: pet.ActiveFlag,
							   branch: pet.Branch,
							   roleName: pet.Role,
								orderByList: pet.OrderByList,
								 pageSize: pet.PageSize,
								  currentPageId: pet.CurrentPageId
						).ToList();
				}

				return result;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public int SearchCountAll(USP_M_USM_USER__Search_V2__Pet pet)
		{
			try
			{
				int? result = 0;

				using (var db = new MainEntities())
				{
					result = db.USP_M_USM_USER__SearchCountAll_V2(
						 username: pet.Username,
						  employeeID: pet.EmployeeID,
						   firstName: pet.FirstName,
							lastName: pet.LastName,
							 email: pet.Email,
							  activeFlag: pet.ActiveFlag,
							   branch: pet.Branch,
							   roleName: pet.Role,
								orderByList: pet.OrderByList,
								 pageSize: pet.PageSize,
								  currentPageId: pet.CurrentPageId
						).FirstOrDefault();
				}

				return result != null && result.HasValue ? result.Value : 0;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public int SearchUserCountAll(USP_M_USM_USER__Search_V2__Pet pet)
		{
			try
			{
				int? result = 0;

				using (var db = new MainEntities())
				{
					result = db.USP_M_USM_USER__SearchCountAll(
						 username: pet.Username,
						  employeeID: pet.EmployeeID,
						   firstName: pet.FirstName,
							lastName: pet.LastName,
							 email: pet.Email,
							  activeFlag: pet.ActiveFlag,
							   branch: pet.Branch,
							   roleName: pet.Role,
								orderByList: pet.OrderByList,
								 pageSize: pet.PageSize,
								  currentPageId: pet.CurrentPageId
						).FirstOrDefault();
				}

				return result != null && result.HasValue ? result.Value : 0;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal UserET2 GetByPK(string username)
		{
			try
			{
				UserET2 result = null;

				using (var db = new MainEntities())
				{
					result = (from ret in db.USP_M_USM_USER__GetByPK_V2(username: username)
							  select new UserET2()
							  {
								  ActiveFlag = ret.ACTIVE_FLAG,
								  Branch = ret.BranchCode,
								  Command = null,
								  Email = ret.EMAIL,
								  EmployeeID = ret.EMPLOYEE_ID,
								  FirstNameEn = ret.FIRST_NAME_EN,
								  LastNameEn = ret.LAST_NAME_EN,
								  FirstNameTh = ret.FIRST_NAME_TH,
								  LastNameTh = ret.LAST_NAME_TH,
								  MobileNo = ret.MOBILE_NO,
								  Mode = null,
								  PasswordHash = ret.PASSWORD_HASH,
								  PhoneExt = ret.PHONE_EXT,
								  PhoneNo = ret.PHONE_NO,
								  Role = ret.ROLE_NAME,
								  SaveBy = ret.UPDATE_BY,
								  Username = ret.USER_NAME,
								  UserUsmType = ret.USER_USM_TYPE
							  }
							  ).FirstOrDefault();
				}

				return result;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public USP_M_USM_USER__Save_Result Save(UserET2 et)
		{
			try
			{
				USP_M_USM_USER__Save_Result result = null;
				using (var db = new MainEntities())
				{
					using (var trx = db.Database.BeginTransaction())
					{
						try
						{
							result = db.USP_M_USM_USER__Save(username: et.Username, passwrodHash: et.PasswordHash, employeeID: et.EmployeeID, firstNameTh: et.FirstNameTh, lastNameTh: et.LastNameTh, email: et.Email, activeFlag: et.ActiveFlag, saveBy: et.SaveBy, mode: et.Mode, roleName: et.Role, branchCode: et.Branch).FirstOrDefault();

							db.SaveChanges();
							trx.Commit();
						}
						catch (Exception eex)
						{
							trx.Rollback();
							throw eex;
						}
					}
				}
				return result;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public void UpdatePwd(USP_M_USM_USER__UpdatePwd__Pet pet)
		{
			try
			{
				using (var db = new MainEntities())
				{
					db.USP_M_USM_USER__UpdatePwd(username: pet.Username, passwordHash: pet.PasswordHash, updateBy: pet.UpdateBy);
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}