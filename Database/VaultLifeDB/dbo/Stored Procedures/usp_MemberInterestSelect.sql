
CREATE PROC [dbo].[usp_MemberInterestSelect] @MemberID INT
	,@ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberInterest]
WHERE (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)
	AND (
		[ProductCategoryID] = @ProductCategoryID
		OR @ProductCategoryID IS NULL
		)

COMMIT
