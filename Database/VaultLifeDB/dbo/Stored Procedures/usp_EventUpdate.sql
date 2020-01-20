
CREATE PROC [dbo].[usp_EventUpdate] @EventID INT
	,@EventCode VARCHAR(20)
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

UPDATE [dbo].[Event]
SET [EventCode] = @EventCode
	,[EventName] = @EventName
	,[EventDescription] = @EventDescription
	,[EventDate] = @EventDate
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [EventID] = @EventID

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
WHERE [EventID] = @EventID

-- End Return Select <- do not remove
COMMIT
