﻿
CREATE PROC [dbo].[usp_GameTypeDelete] @GameTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[GameType]
WHERE [GameTypeID] = @GameTypeID

COMMIT
