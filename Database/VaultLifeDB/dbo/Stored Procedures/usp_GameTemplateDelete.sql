
CREATE PROC [dbo].[usp_GameTemplateDelete] @GameTemplateID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[GameTemplate]
WHERE [GameTemplateID] = @GameTemplateID

COMMIT
