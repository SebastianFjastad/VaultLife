
CREATE PROC [dbo].[usp_MemberInGameInsert] @GameID INT
	,@MemberID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[MemberInGame] (
	[GameID]
	,[MemberID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameID
	,@MemberID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [GameID]
	,[MemberID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberInGame]
WHERE [GameID] = @GameID
	AND [MemberID] = @MemberID

-- End Return Select <- do not remove
COMMIT
