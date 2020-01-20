
CREATE PROC [dbo].[usp_MemberSubscriptionTypeDelete] @MemberSubscriptionTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberSubscriptionType]
WHERE [MemberSubscriptionTypeID] = @MemberSubscriptionTypeID

COMMIT
