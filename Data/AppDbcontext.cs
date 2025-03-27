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
            modelBuilder.ApplyConfiguration(new DashboardConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new RemarkItemConfiguration());
            modelBuilder.ApplyConfiguration(new UnitPLBGConfiguration());
        }
    }
}
