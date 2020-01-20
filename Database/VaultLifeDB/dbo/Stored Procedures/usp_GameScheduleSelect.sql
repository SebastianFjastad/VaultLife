
CREATE PROC [dbo].[usp_GameScheduleSelect] @GameScheduleID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [GameScheduleID]
	,[GameScheduleCode]
	,[ScheduledDateTime]
	,[GameID]
	,[SequenceNumber]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameSchedule]
WHERE (
		[GameScheduleID] = @GameScheduleID
		OR @GameScheduleID IS NULL
		)

COMMIT
