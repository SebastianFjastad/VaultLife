
CREATE PROC [dbo].[usp_WebsiteInteractionUpdate] @WebsiteInteractionID INT
	,@MemberID INT
	,@InteractionTypeID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[WebsiteInteraction]
SET [WebsiteInteractionID] = @WebsiteInteractionID
	,[MemberID] = @MemberID
	,[InteractionTypeID] = @InteractionTypeID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [WebsiteInteractionID] = @WebsiteInteractionID

-- Begin Return Select <- do not remove
SELECT [WebsiteInteractionID]
	,[MemberID]
	,[InteractionTypeID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[WebsiteInteraction]
WHERE [WebsiteInteractionID] = @WebsiteInteractionID

-- End Return Select <- do not remove
COMMIT
