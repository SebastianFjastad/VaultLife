
CREATE PROC [dbo].[usp_GameSelect] @GameID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameID]
	,[GameCode]
	,[GameTypeID]
	,[GameName]
	,[GameDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Game]
WHERE (
		[GameID] = @GameID
		OR @GameID IS NULL
		)

COMMIT
