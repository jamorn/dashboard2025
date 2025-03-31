-- 1. อัพเดทข้อมูลที่เป็น NULL ให้มีค่าก่อน
UPDATE [bagging].[dbo].[UnitPLBG]
SET UnitName = 'Default Name'
WHERE UnitName IS NULL;

UPDATE [bagging].[dbo].[UnitPLBG]
SET CostCenter = 'DEFAULT'
WHERE CostCenter IS NULL;

-- 2. เปลี่ยน column เป็น NOT NULL
ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN UnitName varchar(50) NOT NULL;

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN CostCenter varchar(10) NOT NULL;
