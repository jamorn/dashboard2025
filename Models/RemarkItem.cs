using System;

namespace BackendAPI.Models;

public class RemarkItem
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public DateTime RecordDate { get; set; }
    public string? ItemText { get; set; }
}