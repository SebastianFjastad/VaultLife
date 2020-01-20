
CREATE PROC [dbo].[usp_NextGameUpdate] @GameID INT
	,@NextGameID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[NextGame]
SET [GameID] = @GameID
	,[NextGameID] = @NextGameID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameID] = @GameID

-- Begin Return Select <- do not remove
SELECT [GameID]
	,[NextGameID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[NextGame]
WHERE [GameID] = @GameID

-- End Return Select <- do not remove
COMMIT
