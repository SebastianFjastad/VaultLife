
CREATE PROC [dbo].[usp_EventInsert] @EventCode VARCHAR(20)
	,@EventName VARCHAR(255)
	,@EventDescription VARCHAR(255)
	,@EventDate DATE
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[Event] (
	[EventCode]
	,[EventName]
	,[EventDescription]
	,[EventDate]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @EventCode
	,@EventName
	,@EventDescription
	,@EventDate
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [EventID]
	,[EventCode]
	,[EventName]
	,[EventDescription]
	,[EventDate]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Event]
WHERE [EventID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
