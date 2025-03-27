using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* /*ก่อนแก้ 
public class RemarkItemConfiguration : IEntityTypeConfiguration<RemarkItem>
{
   
    /*
    public void Configure(EntityTypeBuilder<RemarkItem> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.HasOne(r => r.Machine)
            .WithMany()
            .HasForeignKey(r => r.MachineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.ItemText)
            .HasMaxLength(500); // กำหนดความยาวสูงสุดของข้อความ
    }

    
}*/
// หลังจากแก้
public class RemarkItemConfiguration : IEntityTypeConfiguration<RemarkItem>
{
    public void Configure(EntityTypeBuilder<RemarkItem> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne<Dashboard>() // ระบุประเภทของฝั่ง One
            .WithMany()
            .HasForeignKey(r => new { r.MachineId, r.RecordDate }) // กำหนด Composite Foreign Key
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.ItemText)
            .HasMaxLength(500);
    }
}