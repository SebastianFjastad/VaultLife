
CREATE PROC [dbo].[usp_WebsiteInteractionSelect] @WebsiteInteractionID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [WebsiteInteractionID]
	,[MemberID]
	,[InteractionTypeID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[WebsiteInteraction]
WHERE (
		[WebsiteInteractionID] = @WebsiteInteractionID
		OR @WebsiteInteractionID IS NULL
		)

COMMIT
