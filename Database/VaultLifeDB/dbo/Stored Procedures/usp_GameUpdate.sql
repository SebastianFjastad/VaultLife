
CREATE PROC [dbo].[usp_GameUpdate] @GameID INT
	,@GameCode VARCHAR(20)
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

UPDATE [dbo].[Game]
SET [GameCode] = @GameCode
	,[GameTypeID] = @GameTypeID
	,[GameName] = @GameName
	,[GameDescription] = @GameDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameID] = @GameID

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
WHERE [GameID] = @GameID

-- End Return Select <- do not remove
COMMIT
