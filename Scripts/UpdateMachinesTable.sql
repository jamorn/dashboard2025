-- 1. Drop existing constraints if they exist
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Machines_UnitPLBG')
    ALTER TABLE [bagging].[dbo].[Machines] DROP CONSTRAINT FK_Machines_UnitPLBG;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Machines_CostCenter')
    ALTER TABLE [bagging].[dbo].[Machines] DROP CONSTRAINT CK_Machines_CostCenter;

-- 2. Update column name from UnitId to CostCenter
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Machines') AND name = 'UnitId')
    EXEC sp_rename 'Machines.UnitId', 'CostCenter', 'COLUMN';

-- 3. Update column properties
ALTER TABLE [bagging].[dbo].[Machines]
ALTER COLUMN CostCenter varchar(10) NOT NULL;

-- 4. Add foreign key constraint
ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT FK_Machines_UnitPLBG 
FOREIGN KEY (CostCenter) REFERENCES UnitPLBG(CostCenter);

-- 5. Add check constraint
ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT CK_Machines_CostCenter CHECK (LEN(TRIM(CostCenter)) > 0);
