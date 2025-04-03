-- 1. Update existing NULL values with defaults
UPDATE [bagging].[dbo].[KPI]
SET 
    Oee_Target = CASE WHEN Oee_Target IS NULL THEN 85.00 ELSE Oee_Target END,
    GiveAwayMin = CASE WHEN GiveAwayMin IS NULL THEN 25.100 ELSE GiveAwayMin END,
    GiveAwayMax = CASE WHEN GiveAwayMax IS NULL THEN 25.115 ELSE GiveAwayMax END;

-- 2. Alter columns to NOT NULL
ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN Oee_Target decimal(10,2) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN GiveAwayMin decimal(10,3) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN GiveAwayMax decimal(10,3) NOT NULL;

-- 3. Verify the changes
SELECT COLUMNPROPERTY(OBJECT_ID('KPI'), 'Oee_Target', 'AllowsNull') AS Oee_Target_Nullable,
       COLUMNPROPERTY(OBJECT_ID('KPI'), 'GiveAwayMin', 'AllowsNull') AS GiveAwayMin_Nullable,
       COLUMNPROPERTY(OBJECT_ID('KPI'), 'GiveAwayMax', 'AllowsNull') AS GiveAwayMax_Nullable;
