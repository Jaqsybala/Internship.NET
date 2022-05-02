USE TestDB
GO

CREATE PARTITION FUNCTION PartitionByYears (DATE) AS RANGE RIGHT
FOR
VALUES (
	'1997',
	'1998',
	'1999',
	'2000'
	);
GO

CREATE PARTITION SCHEME PartitionTable AS PARTITION PartitionByYears ALL TO ('PRIMARY');
GO

CREATE TABLE TableForPartitioning (
	Id INT,
	FirstName NVARCHAR(25),
	HireDate DATE
	)
GO

--Insert test data
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

BEGIN TRANSACTION

DECLARE @count INT = 8001

BEGIN
	SET NOCOUNT ON;

	WHILE (@count <= 9000)
	BEGIN
		INSERT INTO TableForPartitioning (
			Id,
			FirstName,
			HireDate
			)
		VALUES (
			@count,
			CONCAT (
				'TestUser_',
				CAST(@count AS NVARCHAR)
				),
			CAST('1997' AS DATE)
			)

		SET @count = (@count + 1)
	END
END
GO

SELECT HireDate,
	count(*) AS Count
FROM TableForPartitioning
GROUP BY HireDate
ORDER BY 1
GO

--Show partitions
SELECT SCHEMA_NAME(t.schema_id) AS SchemaName,
	t.name AS TableName,
	i.name AS IndexName,
	p.partition_number AS PartitionNumber,
	f.name AS PartitionFunctionName,
	p.rows AS Rows,
	rv.value AS BoundaryValue,
	CASE 
		WHEN ISNULL(rv.value, rv2.value) IS NULL
			THEN 'N/A'
		ELSE CASE 
				WHEN f.boundary_value_on_right = 0
					AND rv2.value IS NULL
					THEN '>='
				WHEN f.boundary_value_on_right = 0
					THEN '>'
				ELSE '>='
				END + ' ' + ISNULL(CONVERT(VARCHAR(64), rv2.value), 'Min Value') + ' ' + CASE f.boundary_value_on_right
				WHEN 1
					THEN 'and <'
				ELSE 'and <='
				END + ' ' + ISNULL(CONVERT(VARCHAR(64), rv.value), 'Max Value')
		END AS TextComparison
FROM sys.tables AS t
JOIN sys.indexes AS i ON t.object_id = i.object_id
JOIN sys.partitions AS p ON i.object_id = p.object_id
	AND i.index_id = p.index_id
JOIN sys.partition_schemes AS s ON i.data_space_id = s.data_space_id
JOIN sys.partition_functions AS f ON s.function_id = f.function_id
LEFT JOIN sys.partition_range_values AS r ON f.function_id = r.function_id
	AND r.boundary_id = p.partition_number
LEFT JOIN sys.partition_range_values AS rv ON f.function_id = rv.function_id
	AND p.partition_number = rv.boundary_id
LEFT JOIN sys.partition_range_values AS rv2 ON f.function_id = rv2.function_id
	AND p.partition_number - 1 = rv2.boundary_id
WHERE t.name = 'TableForPartitioning'
	AND i.type <= 1
ORDER BY t.name,
	p.partition_number
GO

--Deleting partition by year
TRUNCATE TABLE dbo.TableForPartitioning
WITH (PARTITIONS(1))
GO

--Add new boundary
ALTER PARTITION FUNCTION PartitionByYears () SPLIT RANGE ('1998')
GO