
CREATE PROC [dbo].[usp_MemberAtEventSelect] @MemberID INT
	,@EventID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberID]
	,[EventID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberAtEvent]
WHERE (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)
	AND (
		[EventID] = @EventID
		OR @EventID IS NULL
		)

COMMIT
