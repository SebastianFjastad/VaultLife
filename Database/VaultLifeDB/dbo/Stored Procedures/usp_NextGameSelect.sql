
CREATE PROC [dbo].[usp_NextGameSelect] @GameID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameID]
	,[NextGameID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[NextGame]
WHERE (
		[GameID] = @GameID
		OR @GameID IS NULL
		)

COMMIT
