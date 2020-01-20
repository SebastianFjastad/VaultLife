
CREATE PROC [dbo].[usp_GameTypeOwnedInsert] @GameTypeOwnedID INT
	,@GameTypeOwnedCode VARCHAR(20)
	,@OwnerID INT
	,@GameTypeID INT
	,@ContractID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[GameTypeOwned] (
	[GameTypeOwnedID]
	,[GameTypeOwnedCode]
	,[OwnerID]
	,[GameTypeID]
	,[ContractID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameTypeOwnedID
	,@GameTypeOwnedCode
	,@OwnerID
	,@GameTypeID
	,@ContractID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [GameTypeOwnedID]
	,[GameTypeOwnedCode]
	,[OwnerID]
	,[GameTypeID]
	,[ContractID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameTypeOwned]
WHERE [GameTypeOwnedID] = @GameTypeOwnedID

-- End Return Select <- do not remove
COMMIT
