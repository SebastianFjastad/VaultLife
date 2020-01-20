
CREATE PROC [dbo].[usp_NextGameInsert] @GameID INT
	,@NextGameID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[NextGame] (
	[GameID]
	,[NextGameID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameID
	,@NextGameID
	,@DateInserted
	,@DateUpdated
	,@USR

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
