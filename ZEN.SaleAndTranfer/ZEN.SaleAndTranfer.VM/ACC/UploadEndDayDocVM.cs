using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;
using System.Web;

namespace ZEN.SaleAndTranfer.VM.ACC
{
    public class UploadEndDayDocVM : BaseVM
    {
        [Display(Name = "* วันที่ระบุในเอกสารปิดสิ้นวัน")]
        public DateTime? EndDayDate { get; set; }
        [Display(Name = "* ประเภทเอกสาร")]
        public int EndDayDocTypeID { get;set;}
        public string EndDayDocTypeDisplay { get; set; }
        public string FileNameDest { get; set; }
        public string FilePath { get; set; }
        [Display(Name = "* เลือกไฟล์")]
        public HttpPostedFileBase SelectedFile { get; set; }
    }
}
