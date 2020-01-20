
CREATE PROC [dbo].[usp_GameTypeInsert] @GameTypeCode VARCHAR(20)
	,@GameTypeName VARCHAR(255)
	,@GameTypeDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[GameType] (
	[GameTypeCode]
	,[GameTypeName]
	,[GameTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameTypeCode
	,@GameTypeName
	,@GameTypeDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [GameTypeID]
	,[GameTypeCode]
	,[GameTypeName]
	,[GameTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameType]
WHERE [GameTypeID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
