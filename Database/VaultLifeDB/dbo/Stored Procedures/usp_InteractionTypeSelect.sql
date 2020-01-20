
CREATE PROC [dbo].[usp_InteractionTypeSelect] @InteractionTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [InteractionTypeID]
	,[InteractionTypeCode]
	,[InteractionTypeName]
	,[InteractionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[InteractionType]
WHERE (
		[InteractionTypeID] = @InteractionTypeID
		OR @InteractionTypeID IS NULL
		)

COMMIT
