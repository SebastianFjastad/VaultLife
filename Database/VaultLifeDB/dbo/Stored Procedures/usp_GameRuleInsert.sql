
CREATE PROC [dbo].[usp_GameRuleInsert] @GameRuleCode VARCHAR(20)
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

INSERT INTO [dbo].[GameRule] (
	[GameRuleCode]
	,[GameID]
	,[FilterCriteria]
	,[Schedule]
	,[ChainGameRuleID]
	,[GameRuleDetail]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameRuleCode
	,@GameID
	,@FilterCriteria
	,@Schedule
	,@ChainGameRuleID
	,@GameRuleDetail
	,@DateInserted
	,@DateUpdated
	,@USR

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
WHERE [GameRuleID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
