using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DownloadET : BaseET
    {
        //REPORT
        public byte[] EXPORT_DATA { get; set; }
        public string FILE_NAME { get; set; }
        public string CONTENT_TYPE { get; set; }
        public string BRAND_CODE { get; set; }
    }
}
