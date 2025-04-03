namespace BackendAPI.DTOs;

public class InitialDataDTO
{
    public List<MachineDTO> Machines { get; set; } = new();
    public List<OeeDataDaily> LastRecords { get; set; } = new();
}

public class MachineDTO
{
    public int MachineId { get; set; }
    public string MachineName { get; set; } = string.Empty;
}

public class OeeDataDaily
{
    public int MachineId { get; set; }
    public required string RecordDateString { get; set; } = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
    public decimal Availability { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal Giveaway { get; set; }
    public decimal OEE { get; set; }
    public string[]? Remarks { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public required string ResponsiblePerson { get; set; } = "default auto-generate";
    public int Status { get; set; } = 1;
}

/* public class OEEDataDailyDashboardsDTO
{
    public int MachineId { get; set; }
    public required string RecordDateString { get; set; } = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
    public required decimal Availability { get; set; }
    public required decimal Performance { get; set; }
    public required decimal Quality { get; set; }
    public decimal OEE { get; set; }
    public decimal Giveaway { get; set; }
    public string? Remarks { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public required string ResponsiblePerson { get; set; } = "default auto-generate";
    public int Status { get; set; } = 1;
}
 */
public class OeeDataDailyLastRecord
{
    public OeeDataDaily LastRecord_pp12_line_a { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_pp12_line_c { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_pp3_line_a { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_pp3_line_b { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_ppe_line_c { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_ppe_line_d { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_ppc_line_a { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
    public OeeDataDaily LastRecord_ppc_line_b { get; set; } = new() { RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), ResponsiblePerson = "default auto-generate" };
}
public class LastRecordNotFound
{
    public int MachineId { get; set; }
    public string DateString { get; set; } = string.Empty;
    public decimal Availability { get; set; } = 0;
    public decimal Performance { get; set; } = 0;
    public decimal Quality { get; set; } = 0;
    public decimal Giveaway { get; set; } = 0;
    public string? Remark { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}





