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
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.costcenter)
                .IsRequired()
                .HasMaxLength(10);

            // กำหนดความสัมพันธ์กับ Machine
            builder.HasMany(u => u.Machines)
                .WithOne(m => m.Unit)
                .HasForeignKey(m => m.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}