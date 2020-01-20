
CREATE PROC [dbo].[usp_MemberAtEventUpdate] @MemberID INT
	,@EventID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[MemberAtEvent]
SET [MemberID] = @MemberID
	,[EventID] = @EventID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberID] = @MemberID
	AND [EventID] = @EventID

-- Begin Return Select <- do not remove
SELECT [MemberID]
	,[EventID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberAtEvent]
WHERE [MemberID] = @MemberID
	AND [EventID] = @EventID

-- End Return Select <- do not remove
COMMIT
