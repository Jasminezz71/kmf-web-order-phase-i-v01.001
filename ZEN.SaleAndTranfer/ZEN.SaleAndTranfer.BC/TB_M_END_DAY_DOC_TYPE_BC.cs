using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC;
using ZEN.SaleAndTranfer.ET;

namespace ZEN.SaleAndTranfer.BC
{
    public class TB_M_END_DAY_DOC_TYPE_BC : BaseBC
    {
        public TB_M_END_DAY_DOC_TYPE_RET GetByPK(int id)
        {
            try
            {
                var dc = new TB_M_END_DAY_DOC_TYPE_DC();
                var ret = dc.GetByPK(id);
                return ret;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
