-- 1. สร้าง Stored Procedure
CREATE OR ALTER PROCEDURE [dbo].[sp_AutoGenerateKPI]
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentYear int = YEAR(GETDATE());
    DECLARE @LatestYear int;
    
    -- หาปีล่าสุดที่มีข้อมูล KPI
    SELECT @LatestYear = MAX([Year]) FROM [bagging].[dbo].[KPI];
    
    -- ถ้าปีปัจจุบันมากกว่าปีล่าสุดที่มีข้อมูล ให้สร้างข้อมูลใหม่
    IF @CurrentYear > @LatestYear
    BEGIN
        -- สร้าง KPI ใหม่จากข้อมูลปีล่าสุด
        INSERT INTO [bagging].[dbo].[KPI] (
            [Year], [UnitId], 
            [Waste_Pellet_Target], [Waste_Film_Target],
            [GiveAway_Target], [Oee_Target],
            [GiveAwayMin], [GiveAwayMax]
        )
        SELECT 
            @CurrentYear, [UnitId],
            [Waste_Pellet_Target], [Waste_Film_Target],
            [GiveAway_Target], [Oee_Target],
            [GiveAwayMin], [GiveAwayMax]
        FROM [bagging].[dbo].[KPI]
        WHERE [Year] = @LatestYear;

        -- Log การทำงาน
        INSERT INTO [bagging].[dbo].[JobLog] (
            JobName, ExecutionTime, Status, Message
        )
        VALUES (
            'AutoGenerateKPI',
            GETDATE(),
            'Success',
            'Generated KPI data for year ' + CAST(@CurrentYear AS varchar)
        );
    END
END;
GO

-- 2. สร้าง SQL Job
USE [msdb];
GO

-- สร้าง Job
DECLARE @JobName nvarchar(100) = N'AutoGenerateKPI_Job';

-- ลบ Job เดิมถ้ามีอยู่
IF EXISTS (SELECT job_id FROM msdb.dbo.sysjobs WHERE name = @JobName)
    EXEC msdb.dbo.sp_delete_job @job_name = @JobName;

-- สร้าง Job ใหม่
EXEC msdb.dbo.sp_add_job
    @job_name = @JobName,
    @description = N'Auto generate KPI data for new year',
    @enabled = 1;

-- กำหนด Job Server
EXEC msdb.dbo.sp_add_jobserver
    @job_name = @JobName;

-- สร้าง Job Step
EXEC msdb.dbo.sp_add_jobstep
    @job_name = @JobName,
    @step_name = N'Run AutoGenerateKPI',
    @subsystem = N'TSQL',
    @command = N'EXEC [bagging].[dbo].[sp_AutoGenerateKPI]',
    @database_name = N'bagging';

-- กำหนด Schedule (ทุกวันที่ 1 มกราคม เวลา 00:01)
EXEC msdb.dbo.sp_add_schedule
    @schedule_name = N'YearlyKPIGeneration',
    @freq_type = 16,           -- Monthly
    @freq_interval = 1,        -- Day 1
    @freq_recurrence_factor = 12,  -- Every 12 months
    @active_start_time = 100;  -- 00:01 AM

-- ผูก Schedule กับ Job
EXEC msdb.dbo.sp_attach_schedule
    @job_name = @JobName,
    @schedule_name = N'YearlyKPIGeneration';
GO
