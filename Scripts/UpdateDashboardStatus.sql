-- 1. Backup data
SELECT Id, Status INTO #TempStatus FROM [bagging].[dbo].[Dashboards];

-- 2. Update existing data to use char
UPDATE [bagging].[dbo].[Dashboards]
SET Status = CASE 
    WHEN Status = 1 THEN 'A'  -- Active
    WHEN Status = 0 THEN 'I'  -- Inactive
    ELSE 'A'
END;

-- 3. Change column type
ALTER TABLE [bagging].[dbo].[Dashboards]
ALTER COLUMN Status char(1) NOT NULL;

-- 4. Add constraint
ALTER TABLE [bagging].[dbo].[Dashboards]
ADD CONSTRAINT CK_Dashboards_Status 
CHECK (Status IN ('A', 'I'));
