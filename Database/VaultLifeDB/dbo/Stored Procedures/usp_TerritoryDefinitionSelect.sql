
CREATE PROC [dbo].[usp_TerritoryDefinitionSelect] @TerritoryDefinitionID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

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
WHERE (
		[TerritoryDefinitionID] = @TerritoryDefinitionID
		OR @TerritoryDefinitionID IS NULL
		)

COMMIT
