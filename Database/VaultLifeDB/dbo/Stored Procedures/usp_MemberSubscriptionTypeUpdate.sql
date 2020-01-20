
CREATE PROC [dbo].[usp_MemberSubscriptionTypeUpdate] @MemberSubscriptionTypeID INT
	,@MemberSubscriptionTypeCode VARCHAR(20)
	,@MemberSubscriptionTypeDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[MemberSubscriptionType]
SET [MemberSubscriptionTypeCode] = @MemberSubscriptionTypeCode
	,[MemberSubscriptionTypeDescription] = @MemberSubscriptionTypeDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberSubscriptionTypeID] = @MemberSubscriptionTypeID

-- Begin Return Select <- do not remove
SELECT [MemberSubscriptionTypeID]
	,[MemberSubscriptionTypeCode]
	,[MemberSubscriptionTypeDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberSubscriptionType]
WHERE [MemberSubscriptionTypeID] = @MemberSubscriptionTypeID

-- End Return Select <- do not remove
COMMIT
