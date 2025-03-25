using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DashboardConfiguration : IEntityTypeConfiguration<Dashboard>
{
    public void Configure(EntityTypeBuilder<Dashboard> builder)
    {
        builder.Property(d => d.Availability).HasColumnType("decimal(18, 2)");
        builder.Property(d => d.Performance).HasColumnType("decimal(18, 2)");
        builder.Property(d => d.Quality).HasColumnType("decimal(18, 2)");
        builder.Property(d => d.OEE).HasColumnType("decimal(18, 2)");
        builder.Property(d => d.Giveaway).HasColumnType("decimal(18, 3)");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Dashboard_Availability", "[Availability] >= 0 AND [Availability] <= 100");
            t.HasCheckConstraint("CK_Dashboard_Performance", "[Performance] >= 0 AND [Performance] <= 100");
            t.HasCheckConstraint("CK_Dashboard_Quality", "[Quality] >= 0 AND [Quality] <= 100");
            t.HasCheckConstraint("CK_Dashboard_OEE", "[OEE] >= 0 AND [OEE] <= 100");
            t.HasCheckConstraint("CK_Dashboard_Giveaway", "[Giveaway] >= 0 AND [Giveaway] <= 25.30");
        });
    }
}
