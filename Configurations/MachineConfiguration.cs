using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Models;

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.Property(m => m.MachineName)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(m => m.MachineClass)
            .IsRequired()
            .HasColumnType("nvarchar(20)");
        
        builder.Property(m => m.MachineActive)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(m => m.CostCenter)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(m => m.MachineLine)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .UseCollation("Thai_CI_AS");

        // ความสัมพันธ์กับ UnitPLBG
        builder.HasOne(m => m.Unit)
            .WithMany(u => u.Machines)
            .HasForeignKey(m => m.CostCenter)
            .HasPrincipalKey(u => u.CostCenter)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<RemarkItem>()
            .WithOne(r => r.Machine)
            .HasForeignKey(r => r.MachineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

