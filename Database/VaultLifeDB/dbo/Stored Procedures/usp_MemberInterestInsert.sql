
CREATE PROC [dbo].[usp_MemberInterestInsert] @MemberID INT
	,@ProductCategoryID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[MemberInterest] (
	[MemberID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @MemberID
	,@ProductCategoryID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [MemberID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberInterest]
WHERE [MemberID] = @MemberID
	AND [ProductCategoryID] = @ProductCategoryID

-- End Return Select <- do not remove
COMMIT
