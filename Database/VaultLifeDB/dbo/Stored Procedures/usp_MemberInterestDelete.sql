
CREATE PROC [dbo].[usp_MemberInterestDelete] @MemberID INT
	,@ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberInterest]
WHERE [MemberID] = @MemberID
	AND [ProductCategoryID] = @ProductCategoryID

COMMIT
