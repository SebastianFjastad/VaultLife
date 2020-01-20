
CREATE PROC [dbo].[usp_GameTemplateSelect] @GameTemplateID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameTemplateID]
	,[GameTemplateCode]
	,[GameTypeID]
	,[GameRuleID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameTemplate]
WHERE (
		[GameTemplateID] = @GameTemplateID
		OR @GameTemplateID IS NULL
		)

COMMIT
