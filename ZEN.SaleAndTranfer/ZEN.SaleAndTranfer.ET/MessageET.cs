using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class MessageET
    {
        /// <summary>
        /// Message code for display.
        /// </summary>
        /// <remarks>
        /// <value>M0000#???</value>
        /// <example>M00001ERR</example>
        /// </remarks>
        public string MESSAGE_CODE { get; set; }
        public string MESSAGE_CODE_DISP { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string MESSAGE_LANG { get; set; }
        public string MESSAGE_TEXT { get; set; }
        public string MESSAGE_TEXT_FOR_DISPLAY { get; set; }

        public string REMARK { get; set; }
        public bool? ACTIVE_FLAG { get; set; }
    }
}
