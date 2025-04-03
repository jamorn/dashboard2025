using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Configurations
{
    public class KPIConfiguration : IEntityTypeConfiguration<KPI>
    {
        public void Configure(EntityTypeBuilder<KPI> builder)
        {
            builder.HasKey(k => k.Item);

            builder.Property(k => k.UnitId).IsRequired();
            builder.Property(k => k.Year).IsRequired();
            
            builder.Property(k => k.Waste_Pellet_Target)
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            builder.Property(k => k.Waste_Film_Target)
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            builder.Property(k => k.GiveAway_Target)
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            builder.Property(k => k.Oee_Target)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(k => k.GiveAwayMin)
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            builder.Property(k => k.GiveAwayMax)
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            // Relationship
            builder.HasOne(k => k.Unit)
                .WithMany()
                .HasForeignKey(k => k.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
