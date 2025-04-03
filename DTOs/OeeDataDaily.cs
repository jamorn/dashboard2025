public class OeeDataDaily
{
    public required int MachineId { get; set; } // รหัสเครื่องจักร
    public required  string RecordDateString { get; set; } 
    public required decimal Availability { get; set; } //    ความพร้อมใช้งาน
    public required decimal Performance { get; set; } // ประสิทธิภาพ
    public required decimal Quality { get; set; } //     คุณภาพ      
    public decimal OEE { get; set; } // ค่า OEE
    public decimal Giveaway { get; set; } // ค่า Giveaway
    public string[]? Remarks { get; set; } // เปลี่ยนจาก string? Remark เป็น string[]? Remarks
    public DateTime LastUpdated { get; set; } = DateTime.Now; // วันที่และเวลาที่มีการอัพเดทข้อมูลล่าสุด
    public required string ResponsiblePerson { get; set; } = "default auto-generate"; // ผู้รับผิดชอบในการบันทึกข้อมูล
    public int Status { get; set; } = 1; // สถานะของข้อมูล OEE

}
public class MachineDTO
{
    public int MachineId { get; set; } // รหัสเครื่องจักร
    public required string MachineName { get; set; }  // ชื่อเครื่องจักร
  
}
public class InitialDataDTO
{
    public List<MachineDTO> Machines { get; set; } = new(); // เขียนแบบนี้ก็ได้ new List<MachineDTO>();
    // public BackendAPI.DTOs.OeeDataDailyLastRecord LastRecords { get; internal set; }
    public List<OeeDataDaily> LastRecords { get; set; } = new();
   
}
