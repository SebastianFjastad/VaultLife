
CREATE PROC [dbo].[usp_MemberAtEventDelete] @MemberID INT
	,@EventID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberAtEvent]
WHERE [MemberID] = @MemberID
	AND [EventID] = @EventID

COMMIT
