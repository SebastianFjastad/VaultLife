
CREATE PROC [dbo].[usp_WebsiteInteractionInsert] @WebsiteInteractionID INT
	,@MemberID INT
	,@InteractionTypeID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[WebsiteInteraction] (
	[WebsiteInteractionID]
	,[MemberID]
	,[InteractionTypeID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @WebsiteInteractionID
	,@MemberID
	,@InteractionTypeID
	,@DateInserted
	,@DateUpdated
	,@USR

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
