
CREATE PROC [dbo].[usp_GameRuleUpdate] @GameRuleID INT
	,@GameRuleCode VARCHAR(20)
	,@GameID INT
	,@FilterCriteria VARCHAR(255)
	,@Schedule INT
	,@ChainGameRuleID INT
	,@GameRuleDetail VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[GameRule]
SET [GameRuleCode] = @GameRuleCode
	,[GameID] = @GameID
	,[FilterCriteria] = @FilterCriteria
	,[Schedule] = @Schedule
	,[ChainGameRuleID] = @ChainGameRuleID
	,[GameRuleDetail] = @GameRuleDetail
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameRuleID] = @GameRuleID

-- Begin Return Select <- do not remove
SELECT [GameRuleID]
	,[GameRuleCode]
	,[GameID]
	,[FilterCriteria]
	,[Schedule]
	,[ChainGameRuleID]
	,[GameRuleDetail]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameRule]
WHERE [GameRuleID] = @GameRuleID

-- End Return Select <- do not remove
COMMIT
