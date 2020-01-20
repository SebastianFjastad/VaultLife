
CREATE PROC [dbo].[usp_GameRuleSelect] @GameRuleID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

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
WHERE (
		[GameRuleID] = @GameRuleID
		OR @GameRuleID IS NULL
		)

COMMIT
