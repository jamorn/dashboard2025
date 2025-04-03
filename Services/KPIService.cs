using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Services;

public class KPIService
{
    private readonly AppDbContext _context;

    public KPIService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AutoKpi(int targetYear)
    {
        try
        {
            // หาปีล่าสุดที่มีข้อมูล KPI
            var latestYear = await _context.KPI
                .MaxAsync(k => k.Year);

            if (targetYear <= latestYear)
                return false; // ไม่ต้องสร้างข้อมูลใหม่

            // ดึงข้อมูล KPI ปีล่าสุด
            var latestKPIs = await _context.KPI
                .Where(k => k.Year == latestYear)
                .OrderBy(k => k.UnitId)
                .ToListAsync();

            // สร้างข้อมูล KPI ใหม่สำหรับปีถัดไป
            var newKPIs = latestKPIs.Select(kpi => new KPI
            {
                Year = targetYear,
                UnitId = kpi.UnitId,
                Waste_Pellet_Target = kpi.Waste_Pellet_Target,
                Waste_Film_Target = kpi.Waste_Film_Target,
                GiveAway_Target = kpi.GiveAway_Target,
                Oee_Target = kpi.Oee_Target,
                GiveAwayMin = kpi.GiveAwayMin,
                GiveAwayMax = kpi.GiveAwayMax
            }).ToList();

            await _context.KPI.AddRangeAsync(newKPIs);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
