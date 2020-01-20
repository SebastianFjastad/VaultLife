
CREATE PROC [dbo].[usp_MemberInGameSelect] @GameID INT
	,@MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameID]
	,[MemberID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberInGame]
WHERE (
		[GameID] = @GameID
		OR @GameID IS NULL
		)
	AND (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)

COMMIT
