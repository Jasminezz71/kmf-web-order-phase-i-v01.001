using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.UI.DC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.VM2.Usr;

namespace ZEN.SaleAndTranfer.UI.BC2
{
	public class UserBC2
	{
		internal SearchVM2 Search_OnInit(string username)
		{
			try
			{
				var vm = new SearchVM2();
				vm.Pet = new USP_M_USM_USER__Search_V2__Pet() { Username = username };
				return vm;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal SearchVM2 Search(SearchVM2 vm)
		{
			try
			{
				var dc = new UserDC2();
				vm.Result = dc.SearchUser(pet: vm.Pet);
				vm.SearchCountAll = dc.SearchUserCountAll(pet: vm.Pet);
				return vm;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal SearchVM2 SearchCus(SearchVM2 vm)
		{
			try
			{
				var dc = new UserDC2();
				vm.Result = dc.Search(pet: vm.Pet);
				vm.SearchCountAll = dc.SearchCountAll(pet: vm.Pet);
				return vm;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal DoVM2 Do_OnInit(string username)
		{
			try
			{
				var vm = new DoVM2();

				if (string.IsNullOrWhiteSpace(username))
				{
					vm.Pet = new UserET2();
					vm.Pet.Mode = "Insert";
				}
				else
				{
					var dc = new UserDC2();
					vm.Pet = dc.GetByPK(username: username);
					vm.Pet.Mode = "Update";
				}

				return vm;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		internal void Save(DoVM2 vm)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(vm.Pet.Mode) && vm.Pet.Mode == "Insert")
				{
					vm.Pet.Password = vm.Pet.Username;
					vm.Pet.PasswordHash = PasswordHashBC.CreateHash(password: vm.Pet.Password);
				}
				var dc = new UserDC2();
				vm.SaveResult = dc.Save(et: vm.Pet);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal bool ResetPwd(ResetPwdVM2 vm)
		{
			try
			{
				vm.Pet.Password = vm.Pet.Username;
				vm.Pet.PasswordHash = PasswordHashBC.CreateHash(password: vm.Pet.Password);
				var dc = new UserDC2();
				dc.UpdatePwd(pet: vm.Pet);
				return true;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		internal ChangePwdVM2 ChangePwd(ChangePwdVM2 vm)
		{
			try
			{
				if (vm.Pet.NewPassword == vm.Pet.Username)
				{
					vm.SuccessFlag = false;
					vm.Msg = "รหัสของท่านไม่ปลอดภัย โปรดลองอีกครั้ง";
					return vm;
				}

				var pet = new USP_M_USM_USER__UpdatePwd__Pet();
				pet.Username = vm.Pet.Username;
				pet.PasswordHash = PasswordHashBC.CreateHash(password: vm.Pet.NewPassword);
				pet.UpdateBy = vm.Pet.ChanageBy;

				var dc = new UserDC2();
				dc.UpdatePwd(pet: pet);

				vm.SuccessFlag = true;

				return vm;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}