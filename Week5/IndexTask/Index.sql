USE AdventureWorksDW2019;

SELECT t.NAME AS TableName,
	s.Name AS SchemaName,
	p.rows,
	SUM(a.total_pages) * 8 AS TotalSpaceKB,
	CAST(ROUND(((SUM(a.total_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS TotalSpaceMB,
	SUM(a.used_pages) * 8 AS UsedSpaceKB,
	CAST(ROUND(((SUM(a.used_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS UsedSpaceMB,
	(SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB,
	CAST(ROUND(((SUM(a.total_pages) - SUM(a.used_pages)) * 8) / 1024.00, 2) AS NUMERIC(36, 2)) AS UnusedSpaceMB
FROM sys.tables t
INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id
INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID
	AND i.index_id = p.index_id
INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE t.NAME NOT LIKE 'dt%'
	AND t.is_ms_shipped = 0
	AND i.OBJECT_ID > 255
GROUP BY t.Name,
	s.Name,
	p.Rows
ORDER BY TotalSpaceMB DESC,
	t.Name

USE AdventureWorksDW2019
GO

CREATE PROCEDURE AddTestCodeTBL @count INT = 1
AS
BEGIN
	SET NOCOUNT ON;

	WHILE (@count <= 15000000)
	BEGIN
		INSERT INTO dbo.FactProductInventory (
			ProductKey,
			DateKey,
			MovementDate
			)
		VALUES (
			@count,
			0,
			SYSDATETIME()
			)

		SET @count = (@count + 1)
	END
END
GO

EXEC AddTestCodeTBL

CREATE NONCLUSTERED INDEX IX_FactProductInventory_ProductKey ON dbo.FactProductInventory (ProductKey)

SET STATISTICS TIME ON
GO

SELECT ProductKey
FROM [AdventureWorksDW2019].[dbo].[FactProductInventory]
ORDER BY ProductKey
GO

SET STATISTICS TIME OFF