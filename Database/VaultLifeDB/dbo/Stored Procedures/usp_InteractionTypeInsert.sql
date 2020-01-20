
CREATE PROC [dbo].[usp_InteractionTypeInsert] @InteractionTypeID INT
	,@InteractionTypeCode VARCHAR(20)
	,@InteractionTypeName VARCHAR(255)
	,@InteractionTypeDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[InteractionType] (
	[InteractionTypeID]
	,[InteractionTypeCode]
	,[InteractionTypeName]
	,[InteractionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @InteractionTypeID
	,@InteractionTypeCode
	,@InteractionTypeName
	,@InteractionTypeDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [InteractionTypeID]
	,[InteractionTypeCode]
	,[InteractionTypeName]
	,[InteractionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[InteractionType]
WHERE [InteractionTypeID] = @InteractionTypeID

-- End Return Select <- do not remove
COMMIT
