using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.MAS
{
    public class DownloadSearchResultET : BaseET
    {
        public int ROW_NO { get; set; }
        public string LABEL { get; set; }
        public string FILE_PATH { get; set; }
        public string DETAIL { get; set; }
        public int DOWNLOAD_ID { get; set; }
        public bool STATUS { get; set; }
        public string APPLICATION_TYPE { get; set; }
    }
}
