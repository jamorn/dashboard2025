using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RemarkItemConfiguration : IEntityTypeConfiguration<RemarkItem>
{
    public void Configure(EntityTypeBuilder<RemarkItem> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.MachineId)
            .IsRequired();

        builder.Property(r => r.RecordDate)
            .HasColumnType("datetime2(7)")
            .IsRequired();

        builder.Property(r => r.ItemText)
            .HasColumnType("nvarchar(max)")
            .UseCollation("Thai_CI_AS")
            .IsRequired(false);

        builder.HasOne<Dashboard>() // ระบุประเภทของฝั่ง One
            .WithMany()
            .HasForeignKey(r => new { r.MachineId, r.RecordDate }) // กำหนด Composite Foreign Key
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.ItemText)
            .HasMaxLength(500);

        /* Initial Data - Comment out since data already exists in database
        builder.HasData(
            new RemarkItem { 
                Id = 5, 
                MachineId = 3, 
                RecordDate = new DateTime(2025, 3, 6), 
                ItemText = "8.05-12:30 เครื่องจักร Alarm bottom welding unit บ่อยมากตั้งแต่เริ่ม Start แก้ใขทำการถอดชุด Bottom Seal ออกมาเปลี่ยน Teflon ใหม่และทำความสะอาดทุกซอกทุกมุมแต่ก็ยังไม่หาย"
            }
            // Add other initial data here
        );
        */
    }
}