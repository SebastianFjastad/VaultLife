
CREATE PROC [dbo].[usp_TerritoryUpdate] @TerritoryID INT
	,@TerritoryCode VARCHAR(20)
	,@OwnerID INT
	,@ContractID INT
	,@TerritoryName VARCHAR(255)
	,@TerritoryDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[Territory]
SET [TerritoryCode] = @TerritoryCode
	,[OwnerID] = @OwnerID
	,[ContractID] = @ContractID
	,[TerritoryName] = @TerritoryName
	,[TerritoryDescription] = @TerritoryDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [TerritoryID] = @TerritoryID

-- Begin Return Select <- do not remove
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
WHERE [TerritoryID] = @TerritoryID

-- End Return Select <- do not remove
COMMIT
