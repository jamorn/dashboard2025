using System.Globalization;
using BackendAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
