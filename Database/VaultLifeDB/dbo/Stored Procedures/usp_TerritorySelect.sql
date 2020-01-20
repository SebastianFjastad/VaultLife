
CREATE PROC [dbo].[usp_TerritorySelect] @TerritoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [TerritoryID]
	,[TerritoryCode]
	,[OwnerID]
	,[ContractID]
	,[TerritoryName]
	,[TerritoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Territory]
WHERE (
		[TerritoryID] = @TerritoryID
		OR @TerritoryID IS NULL
		)

COMMIT
