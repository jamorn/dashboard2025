using System.Globalization;
using System.Linq;
using BackendAPI.DTOs;
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
    [HttpGet("machine-oee-data")]
    public async Task<ActionResult<MachineOEEData>> GetMachineOEEData()
    {
        try
        {
            var latestDate = await _context.Dashboards.MaxAsync(o => o.RecordDate);
            var startDate = latestDate.AddDays(-29);

            var machines = await _context.Machines
                .Where(m => m.MachineActive) // กรองเฉพาะเครื่องที่ active
                .ToListAsync();
            var machineOEEData = new MachineOEEData();

            foreach (var machine in machines)
            {
                var oeeData = _context.Dashboards
                    .Include(d => d.Machine)
                        .ThenInclude(m => m.Unit)  // Include Unit data
                    .Include(d => d.RemarkItems)
                    .Where(d => d.Machine.MachineName == machine.MachineName 
                           && d.RecordDate >= startDate 
                           && d.RecordDate <= latestDate)
                    .AsEnumerable()
                    .Join(
                        _context.KPI.AsEnumerable(),
                        oee => oee.Machine.Unit?.UnitId ?? 0,  // Join using UnitId with null check
                        kpi => kpi.UnitId,
                        (oee, kpi) => new { OEEData = oee, KPI = kpi }
                    )
                    .Where(x => x.KPI.Year == DateTime.Now.Year)
                    .OrderBy(o => o.OEEData.RecordDate)
                    .ToList();

                var oeeDataDTOs = oeeData.Select((o, index) => new OEEDataDTO
                {
                    Item = index + 1,
                    MachineName = o.OEEData.Machine.MachineName,
                    Availability = o.OEEData.Availability,
                    Performance = o.OEEData.Performance,
                    Quality = o.OEEData.Quality,
                    OEE = o.OEEData.OEE,
                    Giveaway = o.OEEData.Giveaway,
                    Remarks = o.OEEData.RemarkItems
                        .Select(r => r.ItemText)
                        .Where(text => text != null)
                        .Cast<string>()
                        .ToArray(),
                    DateString = o.OEEData.RecordDate.ToString("yyyy-MM-dd"),
                    Color = o.OEEData.OEE > o.KPI.Oee_Target ? "#059918" : "#ff0000",
                    OeeTarget = o.KPI.Oee_Target ?? 0,
                    GiveAwayMin = o.KPI.GiveAwayMin ?? 0,
                    TitleOEE= "OEE Machine "+o.OEEData.Machine.MachineName,
                    TitleGiveAway= "GiveAway  Machine "+o.OEEData.Machine.MachineName,
                    GiveAwayMax = o.KPI.GiveAwayMax ?? 0,

                }).ToList();

                // เติมข้อมูลวันที่ที่ขาดหายไป
                var allDates = Enumerable.Range(0, 30).Select(d => startDate.AddDays(d)).ToList();
                var existingDates = oeeDataDTOs
                    .Where(o => !string.IsNullOrEmpty(o.DateString))
                    .Select(o => DateTime.ParseExact(o.DateString!, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToList();
                var missingDates = allDates.Except(existingDates).ToList();

                foreach (var date in missingDates)
                {
                    oeeDataDTOs.Add(new OEEDataDTO
                    {
                        Item = oeeDataDTOs.Count() + 1,
                        MachineName = machine.MachineName,
                        Availability = 0,
                        Performance = 0,
                        Quality = 0,
                        OEE = 0,
                        Giveaway = 0,
                        Remarks = new[] { "เครื่องจักรหยุดทำงาน" },
                        DateString = date.ToString("yyyy-MM-dd"),
                        Color = "#ff0000",
                        OeeTarget = _context.KPI.FirstOrDefault(k => k.Year == date.Year)?.Oee_Target ?? 0,
                        GiveAwayMin = _context.KPI.FirstOrDefault(k => k.Year == date.Year)?.GiveAwayMin ?? 0,
                        GiveAwayMax = _context.KPI.FirstOrDefault(k => k.Year == date.Year)?.GiveAwayMax ?? 0,
                        TitleOEE = "OEE Machine " + machine.MachineName,
                        TitleGiveAway = "GiveAway  Machine " + machine.MachineName,
                    });
                }

                // กำหนด List<OEEDataDTO> ให้กับ property ที่ถูกต้องใน MachineOEEData
                switch (machine.MachineName)
                {
                    case "PP12/A":
                        machineOEEData.OEEDataListPP12A = oeeDataDTOs.ToList();
                        break;
                    case "PP12/C":
                        machineOEEData.OEEDataListPP12C = oeeDataDTOs.ToList();
                        break;
                    case "PP3/A":
                        machineOEEData.OEEDataListPP3A = oeeDataDTOs.ToList();
                        break;
                    case "PP3/B":
                        machineOEEData.OEEDataListPP3B = oeeDataDTOs.ToList();
                        break;
                    case "PPE/C":
                        machineOEEData.OEEDataListPPEC = oeeDataDTOs.ToList();
                        break;
                    case "PPE/D":
                        machineOEEData.OEEDataListPPED = oeeDataDTOs.ToList();
                        break;
                    case "PPC/A":
                        machineOEEData.OEEDataListPPCA = oeeDataDTOs.ToList();
                        break;
                    case "PPC/B":
                        machineOEEData.OEEDataListPPCB = oeeDataDTOs.ToList();
                        break;
                    case "HDPE/A":
                        machineOEEData.OEEDataListHDPEA = oeeDataDTOs.ToList();
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

    /* [HttpPost("save-oee-data")]
    public async Task<IActionResult> SaveOEEData([FromBody] OEEDataDTO oeeData)
    { 

        try
        {
            var recordDate = DateTime.ParseExact(oeeData.DateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var machine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == oeeData.Machine);

            if (machine == null)
            {
                return BadRequest("Invalid machine ID.");
            }

            var dashboardRecord = new Dashboard
            {
                MachineId = oeeData.Machine,
                RecordDate = recordDate,
                Availability = oeeData.Availability,
                Performance = oeeData.Performance,
                Quality = oeeData.Quality,
                OEE = oeeData.OEE,
                Giveaway = oeeData.GiveAway
            };

            _context.dashboards.Add(dashboardRecord);
            await _context.SaveChangesAsync();

            if (oeeData.Remark != null && oeeData.Remark.Any())
            {
                foreach (var remarkText in oeeData.Remark)
                {
                    var remarkItem = new RemarkItem
                    {
                        MachineId = oeeData.Machine,
                        RecordDate = recordDate,
                        ItemText = remarkText
                    };
                    _context.RemarkItems.Add(remarkItem);
                }
                await _context.SaveChangesAsync();
            }

            return Ok("Data saved successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest("Error saving data: " + ex.Message);
        }

    } */
}

}
