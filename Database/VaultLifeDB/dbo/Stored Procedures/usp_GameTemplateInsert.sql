
CREATE PROC [dbo].[usp_GameTemplateInsert] @GameTemplateID INT
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

INSERT INTO [dbo].[GameTemplate] (
	[GameTemplateID]
	,[GameTemplateCode]
	,[GameTypeID]
	,[GameRuleID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameTemplateID
	,@GameTemplateCode
	,@GameTypeID
	,@GameRuleID
	,@DateInserted
	,@DateUpdated
	,@USR

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
