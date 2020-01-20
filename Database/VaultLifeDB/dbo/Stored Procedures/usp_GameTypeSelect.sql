
CREATE PROC [dbo].[usp_GameTypeSelect] @GameTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameTypeID]
	,[GameTypeCode]
	,[GameTypeName]
	,[GameTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameType]
WHERE (
		[GameTypeID] = @GameTypeID
		OR @GameTypeID IS NULL
		)

COMMIT
