
CREATE PROC [dbo].[usp_GameScheduleDelete] @GameScheduleID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[GameSchedule]
WHERE [GameScheduleID] = @GameScheduleID

COMMIT
