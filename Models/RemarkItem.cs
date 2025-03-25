using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models;

[Table("RemarkItems")]
public class RemarkItem
{
    public RemarkItem()
    {
        Machine = null!;  // กำหนดค่าเริ่มต้นเป็น null! เพื่อบอก compiler ว่าเราจะจัดการค่านี้ภายหลัง
    }
    public int Id { get; set; }
    public int MachineId { get; set; }
    public DateTime RecordDate { get; set; }
    public string? ItemText { get; set; }
    
    // Navigation property
    [ForeignKey("MachineId")]
    public virtual  Machine Machine { get; set; }
}