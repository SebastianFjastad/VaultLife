
CREATE PROC [dbo].[usp_GameTypeUpdate] @GameTypeID INT
	,@GameTypeCode VARCHAR(20)
	,@GameTypeName VARCHAR(255)
	,@GameTypeDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[GameType]
SET [GameTypeCode] = @GameTypeCode
	,[GameTypeName] = @GameTypeName
	,[GameTypeDescription] = @GameTypeDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameTypeID] = @GameTypeID

-- Begin Return Select <- do not remove
SELECT [GameTypeID]
	,[GameTypeCode]
	,[GameTypeName]
	,[GameTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameType]
WHERE [GameTypeID] = @GameTypeID

-- End Return Select <- do not remove
COMMIT
