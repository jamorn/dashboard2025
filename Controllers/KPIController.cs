using BackendAPI.Data;
using BackendAPI.DTOs;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class KPIController : ControllerBase
{
    private readonly KPIService _kpiService;
    private readonly AppDbContext _context;

    public KPIController(KPIService kpiService, AppDbContext context)
    {
        _kpiService = kpiService;
        _context = context;
    }

    [HttpGet("auto-generate")]
    public async Task<ActionResult<List<KPIDataDTO>>> AutoGenerateKPI()
    {
        var currentYear = DateTime.Now.Year;
        var success = await _kpiService.AutoKpi(currentYear);

        if (!success)
        {
            return BadRequest("Failed to generate KPI data");
        }

        // ดึงข้อมูล KPI 2 ปีล่าสุด
        var latestTwoYears = await _context.KPI
            .Select(k => k.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .Take(2)
            .ToListAsync();

        var result = new List<KPIDataDTO>();

        foreach (var year in latestTwoYears)
        {
            var kpiData = new KPIDataDTO { Year = year };

            var kpiByUnits = await (from k in _context.KPI
                                    join u in _context.UnitPLBGs on k.UnitId equals u.UnitId
                                    where k.Year == year
                                    orderby u.UnitName
                                    select new KPIByUnitDTO
                                    {
                                        UnitName = u.UnitName,
                                        Waste_Pellet_Target = k.Waste_Pellet_Target,  // ไม่ต้องมี null check แล้ว
                                        Waste_Film_Target = k.Waste_Film_Target,
                                        GiveAway_Target = k.GiveAway_Target,
                                        Oee_Target = k.Oee_Target,
                                        GiveAwayMin = k.GiveAwayMin,
                                        GiveAwayMax = k.GiveAwayMax
                                    }).ToListAsync();

            kpiData.Units = kpiByUnits;
            result.Add(kpiData);
        }

        return Ok(result);
    }



    public class KPIGenerateResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Year { get; set; }
    }

    public class KPIDataDTO
    {
        public int Year { get; set; }
        public List<KPIByUnitDTO> Units { get; set; } = new List<KPIByUnitDTO>();
    }

    public class KPIByUnitDTO
    {
        public string UnitName { get; set; } = string.Empty;
        public decimal Waste_Pellet_Target { get; set; }  // เปลี่ยนจาก double เป็น decimal
        public decimal Waste_Film_Target { get; set; }
        public decimal GiveAway_Target { get; set; }
        public decimal Oee_Target { get; set; }
        public decimal GiveAwayMin { get; set; }
        public decimal GiveAwayMax { get; set; }
    }

}
