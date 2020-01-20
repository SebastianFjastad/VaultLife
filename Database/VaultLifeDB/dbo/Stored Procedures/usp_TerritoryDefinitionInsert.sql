
CREATE PROC [dbo].[usp_TerritoryDefinitionInsert] @TerritoryDefinitionCode VARCHAR(20)
	,@TerritoryID INT
	,@ZipOrPostalCode VARCHAR(20) = NULL
	,@IPAddress VARCHAR(30) = NULL
	,@PhysicalCoordinates VARCHAR(30) = NULL
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[TerritoryDefinition] (
	[TerritoryDefinitionCode]
	,[TerritoryID]
	,[ZipOrPostalCode]
	,[IPAddress]
	,[PhysicalCoordinates]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @TerritoryDefinitionCode
	,@TerritoryID
	,@ZipOrPostalCode
	,@IPAddress
	,@PhysicalCoordinates
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [TerritoryDefinitionID]
	,[TerritoryDefinitionCode]
	,[TerritoryID]
	,[ZipOrPostalCode]
	,[IPAddress]
	,[PhysicalCoordinates]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[TerritoryDefinition]
WHERE [TerritoryDefinitionID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
