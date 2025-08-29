using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.BC.DDL
{
    public class DDLBC
    {
        public List<DDLItemET> GetBrandByUsername(DDLModeEnumET mode, string username)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var BrandByUsername = ddlDC.GetBrandByUsername(mode, username);
                return BrandByUsername;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchByUsername(DDLModeEnumET mode, string username)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var BranchByUsername = ddlDC.GetBranchByUsername(mode, username);
                return BranchByUsername;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchbyBrand(DDLModeEnumET mode, string brandCode)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var BranchByUsername = ddlDC.GetBranchbyBrand(brandCode, mode);
                return BranchByUsername;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DDLItemET> GetBranchbyBrandAndUsername(DDLModeEnumET mode, string brandCode, string username)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var BranchBrandAndUsername = ddlDC.GetBranchbyBrandAndUsername(brandCode, username, mode);
                return BranchBrandAndUsername;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DDLItemET> GetCompany(DDLModeEnumET mode)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var BrandByUsername = ddlDC.GetCompany(mode);
                return BrandByUsername;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Supaneej, 2018-11-08, End Day (ACC)  
        public List<DDLItemET> GetEndDayType(DDLModeEnumET mode)
        {
            try
            {
                DDLDC ddlDC = new DDLDC();
                var EndDayType = ddlDC.GetEndDayType(mode);
                return EndDayType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
