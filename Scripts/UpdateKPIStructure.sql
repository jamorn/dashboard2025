-- 1. Backup existing data
SELECT * INTO #TempKPI FROM [bagging].[dbo].[KPI];

-- 2. Drop existing constraints if they exist
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_Waste_Pellet_Target')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_Waste_Pellet_Target;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_Waste_Film_Target')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_Waste_Film_Target;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_GiveAway_Target')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_GiveAway_Target;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_Oee_Target')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_Oee_Target;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_GiveAway_Range')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_GiveAway_Range;

IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_KPI_GiveAway_Limits')
    ALTER TABLE [bagging].[dbo].[KPI] DROP CONSTRAINT CK_KPI_GiveAway_Limits;

-- 3. Update nullable columns to non-nullable with default values
UPDATE [bagging].[dbo].[KPI]
SET 
    Waste_Pellet_Target = ISNULL(Waste_Pellet_Target, 0),
    Waste_Film_Target = ISNULL(Waste_Film_Target, 0),
    GiveAway_Target = ISNULL(GiveAway_Target, 0),
    Oee_Target = ISNULL(Oee_Target, 85.00),
    GiveAwayMin = ISNULL(GiveAwayMin, 25.100),
    GiveAwayMax = ISNULL(GiveAwayMax, 25.115);

-- 4. Modify column definitions
ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN Waste_Pellet_Target decimal(10,3) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN Waste_Film_Target decimal(10,3) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN GiveAway_Target decimal(10,3) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN Oee_Target decimal(10,2) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN GiveAwayMin decimal(10,3) NOT NULL;

ALTER TABLE [bagging].[dbo].[KPI]
ALTER COLUMN GiveAwayMax decimal(10,3) NOT NULL;

-- 5. Add constraints
ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_Waste_Pellet_Target 
CHECK (Waste_Pellet_Target >= 0 AND Waste_Pellet_Target <= 100);

ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_Waste_Film_Target 
CHECK (Waste_Film_Target >= 0 AND Waste_Film_Target <= 100);

ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_GiveAway_Target 
CHECK (GiveAway_Target >= 0 AND GiveAway_Target <= 100);

ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_Oee_Target 
CHECK (Oee_Target >= 0 AND Oee_Target <= 100);

ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_GiveAway_Range 
CHECK (GiveAwayMin <= GiveAwayMax);

ALTER TABLE [bagging].[dbo].[KPI]
ADD CONSTRAINT CK_KPI_GiveAway_Limits
CHECK (GiveAwayMin >= 0 AND GiveAwayMax <= 25.300);

-- 6. Drop temp table
DROP TABLE #TempKPI;
