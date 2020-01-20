
CREATE PROC [dbo].[usp_InteractionTypeDelete] @InteractionTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[InteractionType]
WHERE [InteractionTypeID] = @InteractionTypeID

COMMIT
