
CREATE PROC [dbo].[usp_GameScheduleUpdate] @GameScheduleID INT
	,@GameScheduleCode VARCHAR(20)
	,@ScheduledDateTime DATETIME
	,@GameID INT
	,@SequenceNumber INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[GameSchedule]
SET [GameScheduleID] = @GameScheduleID
	,[GameScheduleCode] = @GameScheduleCode
	,[ScheduledDateTime] = @ScheduledDateTime
	,[GameID] = @GameID
	,[SequenceNumber] = @SequenceNumber
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [GameScheduleID] = @GameScheduleID

-- Begin Return Select <- do not remove
SELECT [GameScheduleID]
	,[GameScheduleCode]
	,[ScheduledDateTime]
	,[GameID]
	,[SequenceNumber]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[GameSchedule]
WHERE [GameScheduleID] = @GameScheduleID

-- End Return Select <- do not remove
COMMIT
