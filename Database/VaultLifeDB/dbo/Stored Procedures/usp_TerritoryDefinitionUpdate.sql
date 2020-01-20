
CREATE PROC [dbo].[usp_TerritoryDefinitionUpdate] @TerritoryDefinitionID INT
	,@TerritoryDefinitionCode VARCHAR(20)
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

UPDATE [dbo].[TerritoryDefinition]
SET [TerritoryDefinitionCode] = @TerritoryDefinitionCode
	,[TerritoryID] = @TerritoryID
	,[ZipOrPostalCode] = @ZipOrPostalCode
	,[IPAddress] = @IPAddress
	,[PhysicalCoordinates] = @PhysicalCoordinates
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [TerritoryDefinitionID] = @TerritoryDefinitionID

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
WHERE [TerritoryDefinitionID] = @TerritoryDefinitionID

-- End Return Select <- do not remove
COMMIT
