using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;

namespace ZEN.SaleAndTranfer.BC.CNF
{
    public class ConfigBC
    {
        public static string GetConfigValue(CategoryConfigEnum category, SubCategoryConfigEnum subCategory, ConfigNameEnum configName)
        {
            try
            {
                var dc = new ConfigDC();
                var et = dc.GetConfigET(category, subCategory, configName);
                if (et != null)
                {
                    return et.CONFIG_VALUE;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
