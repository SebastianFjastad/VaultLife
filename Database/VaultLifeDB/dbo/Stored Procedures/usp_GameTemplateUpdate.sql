
CREATE PROC [dbo].[usp_GameTemplateUpdate] @GameTemplateID INT
	,@GameTemplateCode VARCHAR(20)
	,@GameTypeID INT
	,@GameRuleID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[GameTemplate]
SET [GameTemplateID] = @GameTemplateID
	,[GameTemplateCode] = @GameTemplateCode
	,[GameTypeID] = @GameTypeID
	,[GameRuleID] = @GameRuleID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameTemplateID] = @GameTemplateID

-- Begin Return Select <- do not remove
SELECT [GameTemplateID]
	,[GameTemplateCode]
	,[GameTypeID]
	,[GameRuleID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameTemplate]
WHERE [GameTemplateID] = @GameTemplateID

-- End Return Select <- do not remove
COMMIT
