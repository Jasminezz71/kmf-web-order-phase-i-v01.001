using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZEN.SaleAndTranfer.UI.ET2
{
    public class JsonResultET<T>
    {
        public bool SuccessFlag { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}