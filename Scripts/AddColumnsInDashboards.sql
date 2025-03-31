-- เพิ่ม columns ใหม่
ALTER TABLE [bagging].[dbo].[Dashboards]
ADD 
    ResponsiblePerson varchar(100) NULL,
    LastUpdated datetime NULL,
    Status int NULL;

-- อัพเดทค่าเริ่มต้นสำหรับข้อมูลที่มีอยู่
UPDATE [bagging].[dbo].[Dashboards]
SET 
    ResponsiblePerson = 'default auto-generate',
    LastUpdated = GETDATE(),
    Status = 1;

-- เปลี่ยนเป็น NOT NULL หลังจากอัพเดทข้อมูล
ALTER TABLE [bagging].[dbo].[Dashboards]
ALTER COLUMN ResponsiblePerson varchar(100) NOT NULL;

ALTER TABLE [bagging].[dbo].[Dashboards]
ALTER COLUMN LastUpdated datetime NOT NULL;

ALTER TABLE [bagging].[dbo].[Dashboards]
ALTER COLUMN Status int NOT NULL;

-- เพิ่ม comments
EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'ผู้รับผิดชอบ',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Dashboards',
    @level2type = N'COLUMN', @level2name = 'ResponsiblePerson';

EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'วันที่แก้ไข',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Dashboards',
    @level2type = N'COLUMN', @level2name = 'LastUpdated';

EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'สถานะของข้อมูล',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Dashboards',
    @level2type = N'COLUMN', @level2name = 'Status';
