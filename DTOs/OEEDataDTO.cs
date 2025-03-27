using System;

namespace BackendAPI.DTOs;
// (Data Transfer Object) คือ class ที่ใช้สำหรับเก็บข้อมูลที่จะส่งไปยัง client หรือจาก client มายัง server
// โดย DTO จะมีเฉพาะ properties ที่จำเป็นต่อการแสดงผลหรือการรับข้อมูล ไม่มี method ใดๆ ใน DTO
// ในที่นี้เราใช้ DTO สำหรับแสดงข้อมูล OEE ของเครื่องจักร
// โดยมี properties ดังนี้

public class OEEDataDTO
{ // chart สำหรับแสดงข้อมูล OEE
    public int Item { get; set; }
    public string? MachineName { get; set; } // ชื่อเครื่องจักร
    public decimal Availability { get; set; } //    ความพร้อมใช้งาน
    public decimal Performance { get; set; } // ประสิทธิภาพ
    public decimal Quality { get; set; } //     คุณภาพ      
    public decimal OEE { get; set; } // ค่า OEE
    public decimal Giveaway { get; set; } // ค่า Giveaway
    public string[]? Remarks { get; set; } // เปลี่ยนจาก string? Remark เป็น string[]? Remarks
    public required string DateString { get; set; } // เปลี่ยนเป็น non-nullable
    public required string Color { get; set; }
    public decimal OeeTarget { get; set; }
    public decimal GiveAwayTarget { get; set; } // ค่า Giveaway Target
    public decimal GiveAwayMin { get; set; } //     ค่า Giveaway Min ค่าควบคุม ต่ำสุด
    public decimal GiveAwayMax { get; set; } //    ค่า Giveaway Max ค่าควบคุม สูงสุด
    public required string? TitleOEE { get; set; }     // สามารถเป็น null ได้ แต่ต้องถูกกำหนดค่า
    public required string TitleGiveAway { get; set; } // ไม่สามารถเป็น null ได้และต้องถูกกำหนดค่า
   

}
