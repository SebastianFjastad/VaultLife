
CREATE PROC [dbo].[usp_MemberInGameDelete] @GameID INT
	,@MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberInGame]
WHERE [GameID] = @GameID
	AND [MemberID] = @MemberID

COMMIT
