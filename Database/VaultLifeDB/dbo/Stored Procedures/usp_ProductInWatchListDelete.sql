
CREATE PROC [dbo].[usp_ProductInWatchListDelete] @MemberID INT
	,@ProductID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[ProductInWatchList]
WHERE [MemberID] = @MemberID
	AND [ProductID] = @ProductID

COMMIT
