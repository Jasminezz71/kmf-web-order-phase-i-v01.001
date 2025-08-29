using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC;

namespace ZEN.SaleAndTranfer.BC
{
    public class TB_M_BRANCH_BC : BaseBC
    {
        public bool IsFranchises(string brandCode, string branchCode)
        {
            try
            {
                var dc = new TB_M_BRANCH_DC();
                var result = dc.IsFranchises(brandCode, branchCode);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
