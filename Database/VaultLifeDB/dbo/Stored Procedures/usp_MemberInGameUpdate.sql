
CREATE PROC [dbo].[usp_MemberInGameUpdate] @GameID INT
	,@MemberID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[MemberInGame]
SET [GameID] = @GameID
	,[MemberID] = @MemberID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameID] = @GameID
	AND [MemberID] = @MemberID

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
