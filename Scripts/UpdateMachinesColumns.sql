-- 1. เปลี่ยน columns เป็น NOT NULL
ALTER TABLE [bagging].[dbo].[Machines]
ALTER COLUMN MachineActive bit NOT NULL;

ALTER TABLE [bagging].[dbo].[Machines]
ALTER COLUMN MachineLine varchar(50) NOT NULL;

-- 2. เพิ่ม check constraint ป้องกันค่าว่าง
ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT CK_Machines_MachineLine 
CHECK (LEN(TRIM(MachineLine)) > 0);
