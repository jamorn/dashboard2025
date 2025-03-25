using System;

namespace BackendAPI.Models;

public class Dashboard
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public DateTime RecordDate { get; set; }
    public decimal Availability { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal OEE { get; set; }
    public decimal Giveaway { get; set; }
}
