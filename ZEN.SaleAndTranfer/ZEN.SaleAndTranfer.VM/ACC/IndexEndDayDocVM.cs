using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;


namespace ZEN.SaleAndTranfer.VM.ACC
{
    public class IndexEndDayDocVM : BaseVM
    {        
        [Display(Name="* วันที่ระบุในเอกสารปิดสิ้นวัน")]
        public DateTime? EndDayDate { get; set; }
        public int END_DAY_DOC_ID { get; set; }
        //public int EndDayDocTypeID { get; set; }
        //public string EndDayDocTypeDisplay { get; set; }
        public List<USP_R_END_DAY_DOC_GetByEndDayDate_RET> RETs { get; set; }
        public USP_R_END_DAY_DOC_GetDataByPK_RET RET { get; set; }
    }
}
