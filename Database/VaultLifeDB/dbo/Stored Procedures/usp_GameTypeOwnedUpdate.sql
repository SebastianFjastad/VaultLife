
CREATE PROC [dbo].[usp_GameTypeOwnedUpdate] @GameTypeOwnedID INT
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

UPDATE [dbo].[GameTypeOwned]
SET [GameTypeOwnedID] = @GameTypeOwnedID
	,[GameTypeOwnedCode] = @GameTypeOwnedCode
	,[OwnerID] = @OwnerID
	,[GameTypeID] = @GameTypeID
	,[ContractID] = @ContractID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameTypeOwnedID] = @GameTypeOwnedID

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
