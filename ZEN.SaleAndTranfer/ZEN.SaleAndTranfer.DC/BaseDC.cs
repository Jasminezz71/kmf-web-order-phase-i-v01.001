using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;

namespace ZEN.SaleAndTranfer.DC
{
    public class BaseDC
    {
        public int getSqlTimeout()
        {
            try
            {
                int timeout;

                ConfigDC configDC = new ConfigDC();
                timeout = Convert.ToInt16(configDC.GetConfigET(CategoryConfigEnum.WEB, SubCategoryConfigEnum.SQLSERVER, ConfigNameEnum.TIMEOUT).CONFIG_VALUE);

                return timeout;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
