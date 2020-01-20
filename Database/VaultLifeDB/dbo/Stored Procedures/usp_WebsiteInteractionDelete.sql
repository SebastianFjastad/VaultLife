
CREATE PROC [dbo].[usp_WebsiteInteractionDelete] @WebsiteInteractionID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[WebsiteInteraction]
WHERE [WebsiteInteractionID] = @WebsiteInteractionID

COMMIT
