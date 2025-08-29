using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC;
using ZEN.SaleAndTranfer.ET;

namespace ZEN.SaleAndTranfer.BC
{
    public class MessageBC
    {
        public static MessageET GetMessage(string msgCode, params string[] paramList)
        {
            try
            {
                var dc = new MessageDC();
                var msg = dc.GetMessage(msgCode);

                if (paramList != null && paramList.Length > 0)
                {
                    msg.MESSAGE_TEXT_FOR_DISPLAY = string.Format(msg.MESSAGE_TEXT_FOR_DISPLAY, paramList);
                }

                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
