namespace BackendAPI.DTOs;

public class DashboardRecordDTO
{
    public int MachineId { get; set; }
    public string MachineName { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }
    public string RecordDateString { get; set; } = string.Empty;
    public decimal Availability { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal OEE { get; set; }
    public decimal Giveaway { get; set; }
    public string[] Remarks { get; set; } = Array.Empty<string>();
    public string ResponsiblePerson { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
    public int Status { get; set; }
}
