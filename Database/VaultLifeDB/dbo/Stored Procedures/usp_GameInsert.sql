
CREATE PROC [dbo].[usp_GameInsert] @GameCode VARCHAR(20)
	,@GameTypeID INT
	,@GameName VARCHAR(255)
	,@GameDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[Game] (
	[GameCode]
	,[GameTypeID]
	,[GameName]
	,[GameDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameCode
	,@GameTypeID
	,@GameName
	,@GameDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [GameID]
	,[GameCode]
	,[GameTypeID]
	,[GameName]
	,[GameDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Game]
WHERE [GameID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
