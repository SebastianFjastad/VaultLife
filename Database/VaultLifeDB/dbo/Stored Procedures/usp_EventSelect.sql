
CREATE PROC [dbo].[usp_EventSelect] @EventID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [EventID]
	,[EventCode]
	,[EventName]
	,[EventDescription]
	,[EventDate]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Event]
WHERE (
		[EventID] = @EventID
		OR @EventID IS NULL
		)

COMMIT
