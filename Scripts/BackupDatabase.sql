-- Backup ฐานข้อมูลก่อนทำการเปลี่ยนแปลง
DECLARE @BackupFileName NVARCHAR(255)
SET @BackupFileName = N'E:\DatabaseBackup\bagging_' + 
    FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + '.bak'

BACKUP DATABASE [bagging] 
TO DISK = @BackupFileName
WITH INIT, -- ใช้ INIT เพื่อสร้างไฟล์ใหม่ทุกครั้ง
NAME = N'bagging-Full Database Backup', 
STATS = 10;

-- แสดงชื่อไฟล์ backup
PRINT 'Database backed up to: ' + @BackupFileName;
