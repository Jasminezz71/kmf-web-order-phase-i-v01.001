using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET;

namespace ZEN.SaleAndTranfer.VM.ACC
{
    public class SendDocVM : BaseVM
    {
        public int SupportId { get; set; }
        //public virtual ICollection<FileDetail> FileDetails { get; set; }
        [DisplayName("เลือกไฟล์")]
        //public List<FileDetail> FileDetails { get; set; }
        public USP_R_END_DAY_DOC_InsertData_PET endDayPET { get; set; }
        public string BRAND_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public DateTime? END_DAY_DATE { get; set; }
        public int END_DAY_DOC_TYPE_ID { get; set; }
        public string FILE_NAME_ORI { get; set; }
        public string FILE_NAME_DEST { get; set; }
        public string FILE_PATH { get; set; }
        public int FILE_SIZE { get; set; }
        public string FILE_CONTENT_TYPE { get; set; }        
        public string DELETE_FLAG { get; set; }
        public string CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public string UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }

        //[FileSize(10240)]
        //[FileTypes("jpg,jpeg,png")]
        //public HttpPostedFileBase File { get; set; }

        //[DisplayName("อัพโหลดไฟล์")]
        //public HttpPostedFileBase File1 { get; set; }

        //[DisplayName("File 2")]
        //public HttpPostedFileBase File2 { get; set; }

        //[DisplayName("File 3")]
        //public HttpPostedFileBase File3 { get; set; }
        //public EndDayTypeET_MA endDayET_MA { get; set; }
    }
    //public class EndDayTypeET_MA
    //{
    //    public List<DDLItemET> endDayTypeList { get; set; }
    //}
    //public class FileDetail
    //{
    //    public Guid Id { get; set; }
    //    public string FileName { get; set; }
    //    public int FileSize { get; set; }
    //    public string FilePath { get; set; }
    //    public string FileContentType { get; set; }
    //    public string Extension { get; set; }
    //    public virtual SendDocVM Support { get; set; }

    //}
}
