
CREATE PROC [dbo].[usp_TerritoryDefinitionDelete] @TerritoryDefinitionID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[TerritoryDefinition]
WHERE [TerritoryDefinitionID] = @TerritoryDefinitionID

COMMIT
