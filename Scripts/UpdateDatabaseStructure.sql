-- 1. Backup ฐานข้อมูลก่อน
BACKUP DATABASE [bagging] 
TO DISK = N'D:\DatabaseBackup\bagging_beforeUpdate.bak'
WITH NOFORMAT, NOINIT, 
NAME = N'bagging-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;

-- 2. Backup ข้อมูลจากตารางเดิมไว้ใน temp tables
SELECT * INTO #TempUnitPLBG FROM [bagging].[dbo].[UnitPLBG];
SELECT * INTO #TempMachines FROM [bagging].[dbo].[Machines];

-- 3. Drop existing constraints
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK__Machines__UnitId__18EBB532')
    ALTER TABLE [bagging].[dbo].[Machines] DROP CONSTRAINT FK__Machines__UnitId__18EBB532;

IF EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ__UnitPLBG__77667FBFA23A615E')
    ALTER TABLE [bagging].[dbo].[UnitPLBG] DROP CONSTRAINT UQ__UnitPLBG__77667FBFA23A615E;

-- 4. อัพเดท UnitPLBG ก่อน (เพราะ Machines จะอ้างอิงมา)
ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN UnitName varchar(50) NOT NULL;

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN CostCenter varchar(10) NOT NULL;

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT UQ_UnitPLBG_CostCenter UNIQUE (CostCenter);

-- 5. อัพเดท Machines
EXEC sp_rename 'Machines.UnitId', 'CostCenter', 'COLUMN';

ALTER TABLE [bagging].[dbo].[Machines]
ALTER COLUMN CostCenter varchar(10) NOT NULL;

-- 6. สร้าง Foreign Key และ Check Constraints
ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT FK_Machines_UnitPLBG 
FOREIGN KEY (CostCenter) REFERENCES UnitPLBG(CostCenter);

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT CK_UnitPLBG_UnitName CHECK (LEN(TRIM(UnitName)) > 0);

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT CK_UnitPLBG_CostCenter CHECK (LEN(TRIM(CostCenter)) > 0);

ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT CK_Machines_CostCenter CHECK (LEN(TRIM(CostCenter)) > 0);

-- 7. Verify data
SELECT * FROM [bagging].[dbo].[UnitPLBG];
SELECT * FROM [bagging].[dbo].[Machines];

-- 8. Drop temp tables if everything is OK
DROP TABLE #TempUnitPLBG;
DROP TABLE #TempMachines;
