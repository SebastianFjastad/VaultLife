
CREATE PROC [dbo].[usp_GameTypeOwnedSelect] @GameTypeOwnedID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameTypeOwnedID]
	,[GameTypeOwnedCode]
	,[OwnerID]
	,[GameTypeID]
	,[ContractID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameTypeOwned]
WHERE (
		[GameTypeOwnedID] = @GameTypeOwnedID
		OR @GameTypeOwnedID IS NULL
		)

COMMIT
