
CREATE PROC [dbo].[usp_ProductInWatchListUpdate] @MemberID INT
	,@ProductID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[ProductInWatchList]
SET [MemberID] = @MemberID
	,[ProductID] = @ProductID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberID] = @MemberID
	AND [ProductID] = @ProductID

-- Begin Return Select <- do not remove
SELECT [MemberID]
	,[ProductID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInWatchList]
WHERE [MemberID] = @MemberID
	AND [ProductID] = @ProductID

-- End Return Select <- do not remove
COMMIT
