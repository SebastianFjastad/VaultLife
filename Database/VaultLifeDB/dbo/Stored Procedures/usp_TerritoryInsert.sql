
CREATE PROC [dbo].[usp_TerritoryInsert] @TerritoryCode VARCHAR(20)
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

INSERT INTO [dbo].[Territory] (
	[TerritoryCode]
	,[OwnerID]
	,[ContractID]
	,[TerritoryName]
	,[TerritoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @TerritoryCode
	,@OwnerID
	,@ContractID
	,@TerritoryName
	,@TerritoryDescription
	,@DateInserted
	,@DateUpdated
	,@USR

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
WHERE [TerritoryID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
