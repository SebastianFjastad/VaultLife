
CREATE PROC [dbo].[usp_MemberAtEventInsert] @MemberID INT
	,@EventID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[MemberAtEvent] (
	[MemberID]
	,[EventID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @MemberID
	,@EventID
	,@DateInserted
	,@DateUpdated
	,@USR

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
