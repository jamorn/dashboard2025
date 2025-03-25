using System;

namespace BackendAPI.DTOs;

public class OEEDataDTO
{
    public int Item { get; set; }
    public string? MachineName { get; set; }
    public decimal Availability { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal OEE { get; set; }
    public decimal Giveaway { get; set; }
    public string? Remark { get; set; }
    public string? DateString { get; set; }
    public string? Color { get; set; }
    public decimal OeeTarget { get; set; }
    public decimal GiveAwayMin { get; set; }
    public decimal GiveAwayMax { get; set; }

}
