
CREATE PROC [dbo].[usp_MemberSubscriptionTypeSelect] @MemberSubscriptionTypeID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberSubscriptionTypeID]
	,[MemberSubscriptionTypeCode]
	,[MemberSubscriptionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberSubscriptionType]
WHERE (
		[MemberSubscriptionTypeID] = @MemberSubscriptionTypeID
		OR @MemberSubscriptionTypeID IS NULL
		)

COMMIT
