using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BackendAPI.Models; // Ensure the namespace containing 'Unit' is included

namespace BackendAPI.Models;

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
      
        // Seed data สำหรับ RemarkItems เป็นค่าเริ่มต้นสำหรับ RemarkItems
        // ใส่ comment ไว้ Remark Items data ถูกเพิ่มผ่าน SSMS แล้ว ด้วยคำสั่ง SQL แล้ว
        /* ให้ เปิดออกกรณีที่ต้องการใช้งาน เมื่อสร้างโปรเจคใหม่ ที่เครื่อง computer เครื่องอื่น
       builder.OwnsMany(m => m.RemarkItems).HasData(
            new RemarkItem { Id = 1, MachineId = 6, RecordDate = new DateTime(2025, 2, 24), ItemText = "10.30 ไฟฟ้าดับ ไม่จ่ายไฟเข้า Main power" },
            new RemarkItem { Id = 2, MachineId = 6, RecordDate = new DateTime(2025, 2, 24), ItemText = "11.20-13.00 ไฟฟ้าดับ ไม่จ่ายไฟเข้า Main power" },
            new RemarkItem { Id = 3, MachineId = 6, RecordDate = new DateTime(2025, 2, 24), ItemText = "รอ ME หาสาเหตุ" },
            new RemarkItem { Id = 4, MachineId = 5, RecordDate = new DateTime(2025, 2, 27), ItemText = "10.00-11.30 MI เข้ามาแก้ไข ชุด Palletizer ทำงานค้างPallet alert PB address 31 ตรวจสอบพบว่า สายไฟ หลุดใน ชุด Control Motor" },
            new RemarkItem { Id = 5, MachineId = 3, RecordDate = new DateTime(2025, 3, 6), ItemText = "8.05-12:30 เครื่องจักร Alarm bottom welding unit บ่อยมากตั้งแต่เริ่ม Start แก้ใขทำการถอดชุด Bottom Seal ออกมาเปลี่ยน Teflon ใหม่และทำความสะอาดทุกซอกทุกมุมแต่ก็ยังไม่หาย" },
            new RemarkItem { Id = 6, MachineId = 3, RecordDate = new DateTime(2025, 3, 6), ItemText = "ทำการ Carlibate ใหม่แล้วก็ยังไม่หาย ไฟสถานะขึ้นเป็น 0 เมื่อชุด Bottom ทำงานBag ไปปรับไป" },
            new RemarkItem { Id = 7, MachineId = 3, RecordDate = new DateTime(2025, 3, 6), ItemText = "รอกะบ่ายมาแก้ใขต่อ wo 10875187  MI Check & Repair Bottom seal Line A Temp swing 14.30 - 16.30  MI เปลื่ยน Sealing Bar ใหม่ทั้งชุด" },
            new RemarkItem { Id = 8, MachineId = 4, RecordDate = new DateTime(2025, 3, 11), ItemText = "02.00  roller feed film ชำรุด ยางเสื่อมสภาพหลุดต้องเปลี่ยนใหม่" },
            new RemarkItem { Id = 9, MachineId = 4, RecordDate = new DateTime(2025, 3, 11), ItemText = "ออก noti no.10875717 ให้ช่างเข้าแก้ไขตอนเช้า" },
            new RemarkItem { Id = 10, MachineId = 3, RecordDate = new DateTime(2025, 3, 14), ItemText = "Bottom Seal ไม่ทำงาน Alarm bottom welding unit  ไฟไม่เข้า Start" },
            new RemarkItem { Id = 11, MachineId = 3, RecordDate = new DateTime(2025, 3, 23), ItemText = "Tooth Belt Loading Plate Damage" }
        );
        */
        /* 
       === RemarkItems Data ===
       ข้อมูล RemarkItems ถูกเพิ่มผ่าน SSMS โดยใช้คำสั่ง SQL ด้านล่าง
       สำหรับการ deploy ให้รันคำสั่ง SQL นี้เพื่อเพิ่มข้อมูลเริ่มต้น:

       INSERT INTO [bagging].[dbo].[RemarkItems] ([MachineId], [RecordDate], [ItemText])
       VALUES
           (6, '2025-02-24', '10.30 ไฟฟ้าดับ ไม่จ่ายไฟเข้า Main power'),
           (6, '2025-02-24', '11.20-13.00 ไฟฟ้าดับ ไม่จ่ายไฟเข้า Main power'),
           (6, '2025-02-24', 'รอ ME หาสาเหตุ'),
           (5, '2025-02-27', '10.00-11.30 MI เข้ามาแก้ไข ชุด Palletizer ทำงานค้างPallet alert PB address 31 ตรวจสอบพบว่า สายไฟ หลุดใน ชุด Control Motor'),
           (3, '2025-03-06', '8.05-12:30 เครื่องจักร Alarm bottom welding unit บ่อยมากตั้งแต่เริ่ม Start แก้ใขทำการถอดชุด Bottom Seal ออกมาเปลี่ยน Teflon ใหม่และทำความสะอาดทุกซอกทุกมุมแต่ก็ยังไม่หาย'),
           (3, '2025-03-06', 'ทำการ Carlibate ใหม่แล้วก็ยังไม่หาย ไฟสถานะขึ้นเป็น 0 เมื่อชุด Bottom ทำงานBag ไปปรับไป'),
           (3, '2025-03-06', 'รอกะบ่ายมาแก้ใขต่อ wo 10875187  MI Check & Repair Bottom seal Line A Temp swing 14.30 - 16.30  MI เปลื่ยน Sealing Bar ใหม่ทั้งชุด'),
           (4, '2025-03-11', '02.00  roller feed film ชำรุด ยางเสื่อมสภาพหลุดต้องเปลี่ยนใหม่'),
           (4, '2025-03-11', 'ออก noti no.10875717 ให้ช่างเข้าแก้ไขตอนเช้า'),
           (3, '2025-03-14', 'Bottom Seal ไม่ทำงาน Alarm bottom welding unit  ไฟไม่เข้า Start'),
           (3, '2025-03-23', 'Tooth Belt Loading Plate Damage');
       */

        // กำหนดความสัมพันธ์ระหว่าง Machine และ RemarkItems
        // เมื่อ run กับเครื่อง pc ใหม่ ให้ปิด comment ด้านล่าง แล้วเปิดด้านบนแทน
                // Define foreign key relationship
        builder.HasOne(m => m.Unit)
               .WithMany()
               .HasForeignKey(m => m.UnitId)
               .HasPrincipalKey(u => u.costcenter);
        builder.HasMany<RemarkItem>()
            .WithOne(r => r.Machine)
            .HasForeignKey(r => r.MachineId)
            .OnDelete(DeleteBehavior.Cascade);
        /*
        ความหมายและการทำงาน:
                1.OwnsMany หมายถึง:

                Machine เป็นเจ้าของ (owner) ของ RemarkItems
                RemarkItems ไม่สามารถมีอยู่ได้โดยไม่มี Machine
                เมื่อลบ Machine รายการ RemarkItems ที่เกี่ยวข้องจะถูกลบด้วย
                2.การ Map กับฐานข้อมูล:
                -- RemarkItems table จะมี foreign key ไปยัง Machine
                CREATE TABLE RemarkItems (
                    Id INT PRIMARY KEY,
                    MachineId INT FOREIGN KEY REFERENCES Machines(MachineId),
                    RecordDate DATETIME,
                    ItemText NVARCHAR(MAX)
                );
             
                3.ความสัมพันธ์ในโค้ด:

                -Machine มี collection ของ RemarkItems
                -RemarkItem มี reference กลับไปยัง Machine
                -EF Core จะจัดการความสัมพันธ์นี้โดยอัตโนมัติ
                สิ่งที่ไม่ต้องทำแล้ว:
                -ไม่ต้องกำหนด foreign key constraints เอง
                -ไม่ต้องจัดการการลบแบบ cascade เอง
                -ไม่ต้องกำหนด navigation properties เพิ่มเติม
                -เมื่อใช้ OwnsMany แล้ว Entity Framework Core จะจัดการทุกอย่างให้อัตโนมัติตามมาตรฐานของ owned entity relationships
        */
    }
}
/*

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            /* 
            === Database Schema ===
            CREATE TABLE [bagging].[dbo].[Machines] (
                MachineId INT PRIMARY KEY,
                MachineName VARCHAR(50),
                MachineClass VARCHAR(10),
                MachineActive BIT,
                UnitId VARCHAR(10),
                MachineLine VARCHAR(50),
                FOREIGN KEY (UnitId) REFERENCES UnitPLBG(costcenter)
            );

            === Initial Data ===
            INSERT INTO [bagging].[dbo].[Machines] 
            (MachineId, MachineName, MachineClass, MachineActive, UnitId, MachineLine) VALUES
            (1, 'PP12/A', 'g1', 1, '10111203', 'A'),
            (2, 'PP12/C', 'g1', 1, '10111203', 'C'),
            (3, 'PP3/A', 'g1', 1, '10111204', 'A'),
            (4, 'PP3/B', 'g1', 1, '10111204', 'B'),
            (5, 'PPE/C', 'g1', 1, '10111205', 'C'),
            (6, 'PPE/D', 'g1', 1, '10111205', 'D'),
            (7, 'PPC/A', 'g1', 1, '10111206', 'A'),
            (8, 'PPC/B', 'g1', 1, '10111206', 'B'),
            (9, 'HDPE/A', 'g1', 1, '10111202', 'A');
            */

            // Table configuration
             /*
            builder.ToTable("Machines", "dbo");

            // Primary key
            builder.HasKey(m => m.MachineId);
            builder.Property(m => m.MachineId)
                  .ValueGeneratedNever();

            // Properties
            builder.Property(m => m.MachineName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(m => m.MachineClass)
                  .HasMaxLength(10)
                  .IsRequired();

            builder.Property(m => m.UnitId)
                  .HasMaxLength(10)
                  .IsRequired();

            builder.Property(m => m.MachineLine)
                  .HasMaxLength(50)
                  .IsRequired();

            // Relationships
           
            builder.HasOne(m => m.Unit)
                  .WithMany()
                  .HasForeignKey(m => m.UnitId)
                  .HasPrincipalKey(u => u.CostCenter)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsMany(m => m.RemarkItems);
         */
            /* Note: RemarkItems data should be added using SQL script:
            See RemarkItems.sql for the insert statements
           
        }
    } */

