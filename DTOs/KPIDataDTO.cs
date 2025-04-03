namespace BackendAPI.DTOs;

public class KPIDataDTO
{
    public int Year { get; set; }
    public List<KPIByUnitDTO> Units { get; set; } = new();
}

public class KPIByUnitDTO
{
    public string UnitName { get; set; } = string.Empty;
    public decimal Waste_Pellet_Target { get; set; }
    public decimal Waste_Film_Target { get; set; }
    public decimal GiveAway_Target { get; set; }
    public decimal Oee_Target { get; set; }
    public decimal GiveAwayMin { get; set; }
    public decimal GiveAwayMax { get; set; }
}
