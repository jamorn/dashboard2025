using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateDashboard")]
        public async Task<IActionResult> CreateDashboard([FromBody] OeeDataDaily oeeData)
        {
            if (oeeData == null)
            {
                return BadRequest("Dashboard data is invalid.");
            }

            try
            {
                var recordDate = DateTime.ParseExact(oeeData.RecordDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                
                // ตรวจสอบข้อมูลซ้ำ
                var existingData = await _context.Dashboards
                    .FirstOrDefaultAsync(d => d.MachineId == oeeData.MachineId && 
                                            d.RecordDate.Date == recordDate.Date);

                if (existingData != null)
                {
                    return BadRequest("Data for this date already exists");
                }

                // ดึง Machine ก่อนสร้าง Dashboard
                var machine = await _context.Machines.FindAsync(oeeData.MachineId);
                if (machine == null)
                {
                    return BadRequest($"Machine with ID {oeeData.MachineId} not found");
                }

                var dashboard = new Dashboard
                {
                    MachineId = oeeData.MachineId,
                    Machine = machine,  // กำหนดค่า Machine
                    RecordDate = recordDate,
                    Availability = oeeData.Availability,
                    Performance = oeeData.Performance,
                    Quality = oeeData.Quality,
                    OEE = oeeData.OEE,  // ใช้ค่า OEE ที่ส่งมาโดยตรง
                    Giveaway = oeeData.Giveaway,
                    ResponsiblePerson = oeeData.ResponsiblePerson,
                    LastUpdated = DateTime.Now,
                    Status = oeeData.Status
                };

                _context.Dashboards.Add(dashboard);
                await _context.SaveChangesAsync();

                // บันทึก Remark ถ้ามีข้อมูล
                if (oeeData.Remarks?.Any() == true)
                {
                    var remarkItems = oeeData.Remarks.Select(remark => new RemarkItem
                    {
                        MachineId = oeeData.MachineId,
                        RecordDate = recordDate,
                        ItemText = remark,
                        Machine = dashboard.Machine,
                        Dashboard = dashboard
                    }).ToList();

                    _context.RemarkItems.AddRange(remarkItems);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { Message = "Data saved successfully", Id = dashboard.Id });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving data: {ex.Message}");
            }
        }

        [HttpGet("GetDashboard/{id}")]
        public async Task<ActionResult<Dashboard>> GetDashboard(int id)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);

            if (dashboard == null)
            {
                return NotFound();
            }

            return dashboard;
        }

        [HttpGet("latest-records")]
        public async Task<ActionResult<List<DashboardRecordDTO>>> GetLatestRecords()
        {
            try
            {
                var latestRecords = await _context.Dashboards
                    .Include(d => d.RemarkItems)
                    .Include(d => d.Machine)
                    .OrderByDescending(d => d.RecordDate)
                    .ThenByDescending(d => d.LastUpdated)  // เรียงตามเวลาที่อัพเดทล่าสุดเป็นลำดับที่สอง
                    .Take(10)
                    .Select(d => new DashboardRecordDTO
                    {
                        MachineId = d.MachineId,
                        MachineName = d.Machine.MachineName,
                        RecordDate = d.RecordDate,
                        RecordDateString = d.RecordDate.ToString("yyyy-MM-dd"),
                        Availability = d.Availability,
                        Performance = d.Performance,
                        Quality = d.Quality,
                        OEE = d.OEE,
                        Giveaway = d.Giveaway,
                        Remarks = d.RemarkItems
                            .OrderByDescending(r => r.Id)  // เรียง remarks ล่าสุดขึ้นก่อน
                            .Select(r => r.ItemText ?? string.Empty)  // แปลง null เป็น empty string
                            .ToArray(),
                        ResponsiblePerson = d.ResponsiblePerson,
                        LastUpdated = d.LastUpdated,
                        Status = d.Status
                    })
                    .ToListAsync();

                if (!latestRecords.Any())
                {
                    return NotFound("No dashboard records found");
                }

                return Ok(latestRecords);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving latest records: {ex.Message}");
            }
        }

        [HttpGet("initoee")]
        public async Task<ActionResult<InitialDataDTO>> GetInitialData()
        {
            try
            {
                // ดึงรายการเครื่องจักรที่ active
                var machines = await _context.Machines
                    .Where(m => m.MachineActive)
                    .OrderBy(m => m.MachineName)
                    .Select(m => new MachineDTO { MachineId = m.MachineId, MachineName = m.MachineName })
                    .ToListAsync();

                // ดึงข้อมูลล่าสุดของแต่ละเครื่อง
                var lastRecords = await _context.Dashboards
                    .Where(d => machines.Select(m => m.MachineId).Contains(d.MachineId))
                    .Include(d => d.RemarkItems)
                    .GroupBy(d => d.MachineId)
                    .Select(g => g.OrderByDescending(d => d.RecordDate)
                                 .ThenByDescending(d => d.LastUpdated)
                                 .First())
                    .Select(d => new OeeDataDaily
                    {
                        MachineId = d.MachineId,
                        RecordDateString = d.RecordDate.ToString("yyyy-MM-dd"),
                        Availability = d.Availability,
                        Performance = d.Performance,
                        Quality = d.Quality,
                        OEE = d.OEE,
                        Giveaway = d.Giveaway,
                        Remarks = d.RemarkItems
                            .OrderByDescending(r => r.Id)
                            .Select(r => r.ItemText ?? string.Empty)
                            .ToArray(),
                        LastUpdated = d.LastUpdated,
                        ResponsiblePerson = d.ResponsiblePerson,
                        Status = d.Status
                    })
                    .ToListAsync();

                // เพิ่มข้อมูลเริ่มต้นสำหรับเครื่องที่ไม่มีข้อมูล
                var machinesWithNoRecord = machines
                    .Where(m => !lastRecords.Any(r => r.MachineId == m.MachineId))
                    .Select(m => new OeeDataDaily
                    {
                        MachineId = m.MachineId,
                        RecordDateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                        Availability = 0,
                        Performance = 0,
                        Quality = 0,
                        OEE = 0,
                        Giveaway = 0,
                        Remarks = new[] { "ไม่มีข้อมูล" },
                        LastUpdated = DateTime.Now,
                        ResponsiblePerson = "default auto-generate",
                        Status = 1
                    });

                lastRecords.AddRange(machinesWithNoRecord);

                return Ok(new InitialDataDTO
                {
                    Machines = machines,
                    LastRecords = lastRecords.OrderBy(r => r.MachineId).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting initial data: {ex.Message}");
            }
        }
    }
}