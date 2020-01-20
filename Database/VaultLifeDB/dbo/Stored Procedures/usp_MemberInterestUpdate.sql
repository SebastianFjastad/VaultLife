
CREATE PROC [dbo].[usp_MemberInterestUpdate] @MemberID INT
	,@ProductCategoryID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[MemberInterest]
SET [MemberID] = @MemberID
	,[ProductCategoryID] = @ProductCategoryID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberID] = @MemberID
	AND [ProductCategoryID] = @ProductCategoryID

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
