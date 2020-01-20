
CREATE PROC [dbo].[usp_GameTypeOwnedDelete] @GameTypeOwnedID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[GameTypeOwned]
WHERE [GameTypeOwnedID] = @GameTypeOwnedID

COMMIT
