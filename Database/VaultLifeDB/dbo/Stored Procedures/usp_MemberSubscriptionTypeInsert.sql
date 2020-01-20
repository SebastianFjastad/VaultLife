
CREATE PROC [dbo].[usp_MemberSubscriptionTypeInsert] @MemberSubscriptionTypeCode VARCHAR(20)
	,@MemberSubscriptionTypeDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[MemberSubscriptionType] (
	[MemberSubscriptionTypeCode]
	,[MemberSubscriptionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @MemberSubscriptionTypeCode
	,@MemberSubscriptionTypeDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [MemberSubscriptionTypeID]
	,[MemberSubscriptionTypeCode]
	,[MemberSubscriptionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberSubscriptionType]
WHERE [MemberSubscriptionTypeID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
