
CREATE PROC [dbo].[usp_GameScheduleInsert] @GameScheduleID INT
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

INSERT INTO [dbo].[GameSchedule] (
	[GameScheduleID]
	,[GameScheduleCode]
	,[ScheduledDateTime]
	,[GameID]
	,[SequenceNumber]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @GameScheduleID
	,@GameScheduleCode
	,@ScheduledDateTime
	,@GameID
	,@SequenceNumber
	,@DateInserted
	,@DateUpdated
	,@USR

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
