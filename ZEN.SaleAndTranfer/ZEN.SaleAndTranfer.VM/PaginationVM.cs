using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.VM
{
    public class PaginationVM
    {
        // Pagination ทุกหน้า
        public int currentPage { get; set; }
        public int countAll { get; set; }
        public int pagination { get; set; }  //แสดงจำนวนปุ่ม จาก config
        public int rowPerPage { get; set; }  //จาก config   
        public int pages { get; set; }
        public List<DDLItemET> perPageList { get; set; }
        public int pagesFrom { get; set; }
        public int pagesTo { get; set; }

        // Pagination DashBoard Top ทุกหน้า
        public int top_currentPage { get; set; }
        public int top_countAll { get; set; }
        public int top_pagination { get; set; }  //แสดงจำนวนปุ่ม จาก config
        public int top_rowPerPage { get; set; }  //จาก config   
        public int top_pages { get; set; }
        public List<DDLItemET> top_perPageList { get; set; }
        public int top_pagesFrom { get; set; }
        public int top_pagesTo { get; set; }

        // Pagination DashBoard Bottom ทุกหน้า
        public int bottom_currentPage { get; set; }
        public int bottom_countAll { get; set; }
        public int bottom_pagination { get; set; }  //แสดงจำนวนปุ่ม จาก config
        public int bottom_rowPerPage { get; set; }  //จาก config   
        public int bottom_pages { get; set; }
        public List<DDLItemET> bottom_perPageList { get; set; }
        public int bottom_pagesFrom { get; set; }
        public int bottom_pagesTo { get; set; }
    }
}
