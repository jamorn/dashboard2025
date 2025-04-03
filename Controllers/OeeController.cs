using System.Globalization;
using System.Linq;
using BackendAPI.DTOs;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class OEEController : ControllerBase
{
    private readonly Data.AppDbContext _context;

    public OEEController(Data.AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("GetOEEDaily")]
    public async Task<ActionResult<MachineOEEData>> GetOEEDaily()
    {
        try
        {
            var latestDate = await _context.Dashboards.MaxAsync(o => o.RecordDate);
            var startDate = latestDate.AddDays(-29);

            var machines = await _context.Machines
                .Where(m => m.MachineActive)
                .ToListAsync();
            var machineOEEData = new MachineOEEData();

            foreach (var machine in machines)
            {
                // ดึง KPI ปีล่าสุดของแต่ละ unit มาใช้ก่อน ในกรณีที่ยังไม่มีการกำหนดค่าในปีใหม่
                var latestKpi = await _context.KPI
                    .Where(k => k.UnitId == machine.Unit.UnitId)
                    .OrderByDescending(k => k.Year)
                    .FirstOrDefaultAsync();

                if (latestKpi == null)
                {
                    return BadRequest($"No KPI data found for machine {machine.MachineName}");
                }

                var oeeData = _context.Dashboards
                    .Where(d => d.MachineId == machine.MachineId 
                           && d.RecordDate >= startDate 
                           && d.RecordDate <= latestDate)
                    .Join(
                        _context.Machines,
                        d => d.MachineId,
                        m => m.MachineId,
                        (dashboard, machine) => new { Dashboard = dashboard, Machine = machine }
                    )
                    .Join(
                        _context.UnitPLBGs,
                        dm => dm.Machine.CostCenter,
                        u => u.CostCenter,
                        (dm, unit) => new { dm.Dashboard, dm.Machine, Unit = unit }
                    )
                    .Join(
                        _context.KPI
                            .Where(k => k.UnitId == machine.Unit.UnitId)
                            .OrderByDescending(k => k.Year),  // เรียงปีล่าสุดก่อน
                        dmu => dmu.Unit.UnitId,
                        kpi => kpi.UnitId,
                        (dmu, kpi) => new { dmu.Dashboard, dmu.Machine, dmu.Unit, KPI = kpi }
                    )
                    .OrderBy(x => x.Dashboard.RecordDate)
                    .Select(result => new OEEDataDTO
                    {
                        Item = 0, // Placeholder, will be updated later
                        MachineName = result.Machine.MachineName,
                        Availability = result.Dashboard.Availability,
                        Performance = result.Dashboard.Performance,
                        Quality = result.Dashboard.Quality,
                        OEE = result.Dashboard.OEE,
                        Giveaway = result.Dashboard.Giveaway,
                        Remarks = result.Dashboard.RemarkItems
                            .Select(r => r.ItemText)
                            .Where(text => text != null)
                            .Cast<string>()
                            .ToArray(),
                        DateString = result.Dashboard.RecordDate.ToString("yyyy-MM-dd"),
                        Color = result.Dashboard.OEE > latestKpi.Oee_Target ? "#059918" : "#ff0000",
                        OeeTarget = latestKpi.Oee_Target,  // ใช้ค่าจาก KPI ปีล่าสุดเสมอ
                        GiveAwayMin = latestKpi.GiveAwayMin,
                        GiveAwayMax = latestKpi.GiveAwayMax,
                        TitleOEE = "OEE Machine " + result.Machine.MachineName,
                        TitleGiveAway = "GiveAway Machine " + result.Machine.MachineName,
                    })
                    .ToList();

                // เติมข้อมูลวันที่ที่ขาดหายไป
                var allDates = Enumerable.Range(0, 30).Select(d => startDate.AddDays(d)).ToList();
                var existingDates = oeeData
                    .Where(o => !string.IsNullOrEmpty(o.DateString))
                    .Select(o => DateTime.ParseExact(o.DateString!, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToList();
                var missingDates = allDates.Except(existingDates).ToList();

                foreach (var date in missingDates)
                {
                    oeeData.Add(new OEEDataDTO
                    {
                        Item = oeeData.Count() + 1,
                        MachineName = machine.MachineName,
                        Availability = 0,
                        Performance = 0,
                        Quality = 0,
                        OEE = 0,
                        Giveaway = 0,
                        Remarks = new[] { "เครื่องจักรหยุดทำงาน" },
                        DateString = date.ToString("yyyy-MM-dd"),
                        Color = "#ff0000",
                        OeeTarget = latestKpi.Oee_Target,  // ใช้ค่าจาก KPI ปีล่าสุด
                        GiveAwayMin = latestKpi.GiveAwayMin,
                        GiveAwayMax = latestKpi.GiveAwayMax,
                        TitleOEE = "OEE Machine " + machine.MachineName,
                        TitleGiveAway = "GiveAway Machine " + machine.MachineName,
                    });
                }

                // กำหนด List<OEEDataDTO> ให้กับ property ที่ถูกต้องใน MachineOEEData
                switch (machine.MachineName)
                {
                    case "PP12/A":
                        machineOEEData.OEEDataListPP12A = oeeData.ToList();
                        break;
                    case "PP12/C":
                        machineOEEData.OEEDataListPP12C = oeeData.ToList();
                        break;
                    case "PP3/A":
                        machineOEEData.OEEDataListPP3A = oeeData.ToList();
                        break;
                    case "PP3/B":
                        machineOEEData.OEEDataListPP3B = oeeData.ToList();
                        break;
                    case "PPE/C":
                        machineOEEData.OEEDataListPPEC = oeeData.ToList();
                        break;
                    case "PPE/D":
                        machineOEEData.OEEDataListPPED = oeeData.ToList();
                        break;
                    case "PPC/A":
                        machineOEEData.OEEDataListPPCA = oeeData.ToList();
                        break;
                    case "PPC/B":
                        machineOEEData.OEEDataListPPCB = oeeData.ToList();
                        break;
                    case "HDPE/A":
                        machineOEEData.OEEDataListHDPEA = oeeData.ToList();
                        break;
                }
            }

            return Ok(machineOEEData);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving OEE data: {ex.Message}");
        }
    }

   
    
}

}
