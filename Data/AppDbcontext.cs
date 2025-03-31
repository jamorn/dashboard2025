using BackendAPI.Configurations;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Dashboard> Dashboards { get; set; } // ตรวจสอบว่ามี class Dashboard หรือไม่
        public DbSet<RemarkItem> RemarkItems { get; set; } // ตรวจสอบว่ามี class RemarkItem หรือไม่
        public DbSet<Machine> Machines { get; set; }
        public DbSet<UnitPLBG> UnitPLBGs { get; set; }
        public DbSet<KPI> KPI { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new DashboardConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new UnitPLBGConfiguration());
            modelBuilder.ApplyConfiguration(new KPIConfiguration());

            // Configure RemarkItem relationship
            modelBuilder.Entity<RemarkItem>()
                .HasOne(r => r.Dashboard)
                .WithMany(d => d.RemarkItems)
                .HasForeignKey(r => new { r.MachineId, r.RecordDate })
                .HasPrincipalKey(d => new { d.MachineId, d.RecordDate });

            /*
            อธิบายการกำหนดความสัมพันธ์ระหว่าง RemarkItem และ Dashboard:

                modelBuilder.Entity<RemarkItem>()

                เริ่มการกำหนดค่าสำหรับ entity RemarkItem
                .HasOne(r => r.Dashboard)

                RemarkItem มีความสัมพันธ์กับ Dashboard แบบ 1:1
                หมายถึง RemarkItem หนึ่งรายการจะเชื่อมกับ Dashboard หนึ่งรายการ
                .WithMany(d => d.RemarkItems)

                Dashboard หนึ่งรายการสามารถมี RemarkItems ได้หลายรายการ
                เป็นการกำหนดความสัมพันธ์แบบ 1:Many
                .HasForeignKey(r => new { r.MachineId, r.RecordDate })

                กำหนด Composite Foreign Key ใน RemarkItem
                ใช้ทั้ง MachineId และ RecordDate เป็น foreign key
                ต้องมีทั้งสองค่านี้เพื่อระบุว่า remark นี้เชื่อมกับ dashboard ไหน
                .HasPrincipalKey(d => new { d.MachineId, d.RecordDate })

                กำหนด Composite Principal Key ใน Dashboard
                ใช้ MachineId และ RecordDate เป็นคีย์หลักในการอ้างอิง

             ตัวอย่างความสัมพันธ์:   
                Dashboard                     RemarkItems
    -----------------            -------------------
    MachineId: 1                 MachineId: 1
    RecordDate: 2025-03-31      RecordDate: 2025-03-31
                                ItemText: "เครื่องเสีย"

                                MachineId: 1
                                RecordDate: 2025-03-31
                                ItemText: "ซ่อมเสร็จแล้ว"
            */

        }
    }
}
