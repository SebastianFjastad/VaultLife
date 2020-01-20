
CREATE PROC [dbo].[usp_InteractionTypeUpdate] @InteractionTypeID INT
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

UPDATE [dbo].[InteractionType]
SET [InteractionTypeID] = @InteractionTypeID
	,[InteractionTypeCode] = @InteractionTypeCode
	,[InteractionTypeName] = @InteractionTypeName
	,[InteractionTypeDescription] = @InteractionTypeDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [InteractionTypeID] = @InteractionTypeID

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
