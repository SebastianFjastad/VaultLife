
CREATE PROC [dbo].[usp_ProductInWatchListSelect] @MemberID INT
	,@ProductID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberID]
	,[ProductID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInWatchList]
WHERE (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)
	AND (
		[ProductID] = @ProductID
		OR @ProductID IS NULL
		)

COMMIT
