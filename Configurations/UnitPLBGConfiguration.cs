using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Configurations
{
    public class UnitPLBGConfiguration : IEntityTypeConfiguration<UnitPLBG>
    {
        public void Configure(EntityTypeBuilder<UnitPLBG> builder)
        {
            builder.HasKey(u => u.UnitId);

            builder.Property(u => u.UnitName)
                .HasColumnType("varchar(50)")
                .UseCollation("Thai_CI_AS")
                .IsRequired(false);

            builder.Property(u => u.CostCenter)
                .HasColumnType("varchar(10)")
                .UseCollation("Thai_CI_AS")
                .IsRequired(false);

            // กำหนดความสัมพันธ์กับ Machine
            builder.HasMany(u => u.Machines)
                .WithOne(m => m.Unit)
                .HasForeignKey(m => m.CostCenter)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}