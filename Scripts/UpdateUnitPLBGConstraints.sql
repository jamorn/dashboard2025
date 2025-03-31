-- 1. Drop existing foreign key first
ALTER TABLE [bagging].[dbo].[Machines]
DROP CONSTRAINT FK__Machines__UnitId__18EBB532;

-- 2. Now we can drop the unique constraint from UnitPLBG
ALTER TABLE [bagging].[dbo].[UnitPLBG]
DROP CONSTRAINT UQ__UnitPLBG__77667FBFA23A615E;

-- 3. Alter columns to NOT NULL
ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN UnitName varchar(50) NOT NULL;

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ALTER COLUMN CostCenter varchar(10) NOT NULL;

-- 4. Recreate unique constraint
ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT UQ_UnitPLBG_CostCenter UNIQUE (CostCenter);

-- 5. Recreate foreign key in Machines
ALTER TABLE [bagging].[dbo].[Machines]
ADD CONSTRAINT FK_Machines_UnitPLBG 
FOREIGN KEY (CostCenter) REFERENCES UnitPLBG(CostCenter);

-- 6. Add check constraints
ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT CK_UnitPLBG_UnitName CHECK (LEN(TRIM(UnitName)) > 0);

ALTER TABLE [bagging].[dbo].[UnitPLBG]
ADD CONSTRAINT CK_UnitPLBG_CostCenter CHECK (LEN(TRIM(CostCenter)) > 0);
