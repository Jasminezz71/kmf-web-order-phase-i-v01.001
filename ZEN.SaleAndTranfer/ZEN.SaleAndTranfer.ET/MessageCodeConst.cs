using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET
{
    public class MessageCodeConst
    {
        /// <summary>
        /// พบความผิดพลาด - {0}
        /// </summary>
        public const string M00001 = "M00001";
        /// <summary>
        /// อัพโหลดได้เฉพาะไฟล์ {0} เท่านั้น
        /// </summary>
        public const string M00002 = "M00002";
        /// <summary>
        /// สามารถอัพโหลดไฟล์ได้ไม่เกินครั้งละ 20 ไฟล์
        /// </summary>
        public const string M00003 = "M00003";
        /// <summary>
        /// กรุณาเลือกไฟล์ในการอัพโหลด
        /// </summary>
        public const string M00004 = "M00004";
        /// <summary>
        /// สามารถอัพโหลดไฟล์ได้ไม่เกินครั้งละ {0} ต่อครั้ง
        /// </summary>
        public const string M00005 = "M00005";
        /// <summary>
        /// อัพโหลดไฟล์สำเร็จ
        /// </summary>
        public const string M00006 = "M00006";
        /// <summary>
        /// ไม่พบข้อมูล
        /// </summary>
        public const string M00007 = "M00007";
        /// <summary>
        /// โปรดระบุ - {0}
        /// </summary>
        public const string M00009 = "M00009";
        /// <summary>
        /// บันทึกสำเร็จ
        /// </summary>
        public const string M00010 = "M00010";
        /// <summary>
        /// บันทึกไม่สำเร็จ
        /// </summary>
        public const string M00011 = "M00011";
        /// <summary>
        /// {0} - ไม่ถูกต้อง
        /// </summary>
        public const string M00012 = "M00012";
        /// <summary>
        /// อัพโหลดได้เฉพาะไฟล์ {0} เท่านั้น
        /// </summary>
        public const string M00015 = "M00015";
        /// <summary>
        /// {0} ต้องมีค่ามากกว่า {1}
        /// </summary>
        public const string M00018 = "M00018";
        /// <summary>
        /// {0} ต้องมีค่าน้อยกว่า {1}
        /// </summary>
        public const string M00019 = "M00019";
        /// <summary>
        /// {0} และ {1} ไม่ตรงกัน
        /// </summary>
        public const string M00030 = "M00030";
        /// <summary>
        /// {0} ต้องอยู่ในรูปแบบ {1}
        /// </summary>
        public const string M00031 = "M00031";
        /// <summary>
        /// คุณต้องการเลือกสินค้าชิ้นนี้ใช่หรือไม่
        /// </summary>
        public const string M00032 = "M00032";
        /// <summary>
        /// คุณต้องการบันทึกข้อมูลใช่หรือไม่
        /// </summary>
        public const string M00033 = "M00033";
        /// <summary>
        /// คุณต้องการล้างหน้าจอใช่หรือไม่
        /// </summary>
        public const string M00034 = "M00034";
        /// <summary>
        /// คุณต้องการบันทึกใช่หรือไม่
        /// </summary>
        public const string M00035 = "M00035";
        /// <summary>
        /// คุณต้องการลบใช่หรือไม่
        /// </summary>
        public const string M00036 = "M00036";
        /// <summary>
        /// คุณต้องการแก้ไขใช่หรือไม่
        /// </summary>
        public const string M00037 = "M00037";
        /// <summary>
        /// คุณต้องการ Reset Password ใช่หรือไม่
        /// </summary>
        public const string M00038 = "M00038";
        /// <summary>
        /// กรุณาเลือกสาขาก่อนสั่งซื้อสินค้าด้วยครับ/ค่ะ
        /// </summary>
        public const string M00039 = "M00039";
        /// <summary>
        /// คุณต้องการแก้ไขใบทำรับสินค้าใช่หรือไม่
        /// </summary>
        public const string M00040 = "M00040";
        /// <summary>
        /// คุณต้องการยกเลิกใบทำรับสินค้าใช่หรือไม่
        /// </summary>
        public const string M00041 = "M00041";
        /// <summary>
        /// คุณต้องการดูรายละเอยีดใบทำรับสินค้าใช่หรือไม่
        /// </summary>
        public const string M00042 = "M00042";
        /// <summary>
        /// คุณต้องการบันทึกใบเตรียมรับสินค้าใช่หรือไม่
        /// </summary>
        public const string M00043 = "M00043";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบตลาด (PR) ใช่หรือไม่
        /// </summary>
        public const string M00044 = "M00044";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบสั่งซื้อ (DO) ใช่หรือไม่
        /// </summary>
        public const string M00045 = "M00045";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูล Master ใช่หรือไม่
        /// </summary>
        public const string M00046 = "M00046";
        /// <summary>
        /// คุณต้องการทำรับสินค้าใช่หรือไม่
        /// </summary>
        public const string M00047 = "M00047";
        /// <summary>
        /// คุณต้องการคืนค่าเริ่มต้นใช่หรือไม่
        /// </summary>
        public const string M00048 = "M00048";
        /// <summary>
        /// คุณต้องการใช้งานผู้ใช้งาน {0} ใช่หรือไม่
        /// </summary>
        public const string M00049 = "M00049";
        /// <summary>
        /// คุณต้องการไม่ใช้งานผู้ใช้งาน {0} ใช่หรือไม่
        /// </summary>
        public const string M00050 = "M00050";
        /// <summary>
        /// Quantity ไม่ถูกต้อง จากการกำหนดค่าการเปลี่ยนหน่วย
        /// </summary>
        public const string M00051 = "M00051";
        /// <summary>
        /// ไม่สามารถดาวน์โหลดไฟล์ได้
        /// </summary>
        public const string M00052 = "M00052";
        /// <summary>
        /// ไม่สามารถอัพโหลดไฟล์ได้ - แถวที่ {0} : {1}
        /// </summary>
        public const string M00053 = "M00053";
        /// <summary>
        /// กรุณา Download File ล่าสุดก่อนการใช้งาน
        /// </summary>
        public const string M00054 = "M00054";
        /// <summary>
        /// ข้อมูลที่ Upload ยังไม่ล่าสุด กรุณา Download File ล่าสุดก่อนการใช้งาน
        /// </summary>
        public const string M00055 = "M00055";
        /// <summary>
        /// Session is expired
        /// </summary>
        public const string M00064 = "M00064";
        /// <summary>
        /// เปลี่ยนหน้าแล้วข้อมูลที่กรอกหายนะ
        /// </summary>
        public const string M00065 = "M00065";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบสั่งซื้อ (GR) ใช่หรือไม่
        /// </summary>
        public const string M00066 = "M00066";
        /// <summary>
        /// สินค้า {0} ชนิดนี้ถูกสั่งมากกว่าจำนวนเฉลี่ย ({1})
        /// </summary>
        public const string M00067 = "M00067";
        /// <summary>
        /// Item {0} มีการรับสินค้าไม่เท่ากับจำนวนที่มาส่ง ({0} = Item_Code : ITEM_NAME)
        /// </summary>
        public const string M00068 = "M00068";
        /// <summary>
        /// คุณต้องการทำรับสินค้าใหม่ใช่หรือไม่
        /// </summary>
        public const string M00069 = "M00069";
        /// <summary>
        /// หยิบสินค้าใส่ตะกร้าเรียบร้อยแล้ว แต่ยังไม่ได้บันทึกข้อมูลลงในระบบ หากต้องการบันทึกให้ ไปที่ตะกร้า และ กดปุ่ม ยืนยันการสั่ง
        /// </summary>
        public const string M00070 = "M00070";
        /// <summary>
        /// ใบตลาดใบนี้ยังไม่ถูกส่งไป HQ สามารถแก้ไขได้ก่อนเวลา 24:00 น.
        /// </summary>
        public const string M00071 = "M00071";
        /// <summary>
        /// ไม่สามารถเปิดการ์ดล่วงหน้าของเดือนถัดไปได้
        /// </summary>
        public const string M00073 = "M00073";
        /// <summary>
        /// ไม่สามารถเปิดการ์ดล่วงหน้าของเดือนถัดไปได้
        /// </summary>
        public const string M00074 = "M00074";
        /// <summary>
        /// {0} ต้องมีค่าน้อยกว่าหรือเท่ากับ {1}
        /// </summary>
        public const string M00075 = "M00075";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบสั่งซื้อ (PO) ใช่หรือไม่
        /// </summary>
        public const string M00076 = "M00076";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบตลาด (SO) ใช่หรือไม่
        /// </summary>
        public const string M00077 = "M00077";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบรับสินค้า (RN) ใช่หรือไม่
        /// </summary>
        public const string M00078 = "M00078";
        /// <summary>
        /// คุณต้องการปรับปรุงข้อมูลใบรับสินค้า (DN) ใช่หรือไม่
        /// </summary>
        public const string M00079 = "M00079";

        /// <summary>
        /// ไม่สามารถเลือกสั่ง Item ที่เป็น Stationery ปะปนกับ Item ของ Warehouse ได้
        /// </summary>
        public const string M00086 = "M00086";

        /// <summary>
        /// รายการในตะกร้าเป็น Stationery ไม่สามารถสั่ง Item ของ Warehouse ปะปนได้
        /// </summary>
        public const string M00087 = "M00087";

        /// <summary>
        /// รายการในตะกร้าเป็น Item ของ Warehouse ไม่สามารถสั่ง Stationery ปะปนได้
        /// </summary>
        public const string M00088 = "M00088";

        /// <summary>
        /// บันทึกข้อมูลสำเร็จ       (End Day Report)
        /// </summary>
        public const string M00089 = "M00089";

        /// <summary>
        /// ลบข้อมูลสำเร็จ         (End Day Report)
        /// </summary>
        public const string M00090 = "M00090";

        /// <summary>
        /// ลบข้อมูลไม่สำเร็จ       (End Day Report)
        /// </summary>
        public const string M00091 = "M00091";

        /// <summary>
        /// ติดปัญหาการบันทึกข้อมูลลง database โปรดลองอีกครั้ง   (End Day Report)
        /// </summary>
        public const string M00092 = "M00092";

        /// <summary>
        /// ติดปัญหาการ save file โปรดลองอีกครั้ง        (End Day Report)
        /// </summary>
        public const string M00093 = "M00093";

        /// <summary>
        /// ยังไม่พบข้อมูลในระบบ        (End Day Report)
        /// </summary>
        public const string M00094 = "M00094";

        /// <summary>
        /// ขนาดของไฟล์ต้องมีค่าน้อยกว่าหรือเท่ากับ 4MB         (End Day Report)
        /// </summary>
        public const string M00095 = "M00095";

        /// <summary>
        /// อัพโหลดได้เฉพาะไฟล์  *.jpg หรือ *.png เท่านั้น        (End Day Report)
        /// </summary>
        public const string M00096 = "M00096";

        /// <summary>
        /// โปรดระบุ วันที่ระบุในหัวเอกสารปิดสิ้นวัน        (End Day Report)
        /// </summary>
        public const string M00097 = "M00097";

        /// <summary>
        /// โปรดระบุ ประเภทเอกสาร        (End Day Report)
        /// </summary>
        public const string M00098 = "M00098";

        /// <summary>
        /// กรุณาเลือกไฟล์ในการอัพโหลด        (End Day Report)
        /// </summary>
        public const string M00099 = "M00099";

        /// <summary>
        /// โปรดทำการประเมินความพึงพอใจผู้ส่งสินค้า
        /// </summary>
        public const string M00084 = "M00084";
        /// <summary>
        /// โปรดทำการประเมินด้าน {0}  ({0} = COMPLAIN_DESCRIPTION)
        /// </summary>
        public const string M00085 = "M00085";
        /// <summary>
        /// คุณต้องการที่จะเปิดให้แก้ไขใหม่ใช่หรือไม่
        /// </summary>
        public const string M00101 = "M00101";
        /// <summary>
        /// คุณต้องการที่จะ Block User ใช่หรือไม่
        /// </summary>
        public const string M00102 = "M00102";
        /// <summary>
        /// คุณต้องการที่จะ Unblock User ใช่หรือไม่
        /// </summary>
        public const string M00103 = "M00103";
    }
}
