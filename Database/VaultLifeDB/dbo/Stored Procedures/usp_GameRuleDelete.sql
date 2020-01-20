﻿
CREATE PROC [dbo].[usp_GameRuleDelete] @GameRuleID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[GameRule]
WHERE [GameRuleID] = @GameRuleID

COMMIT
