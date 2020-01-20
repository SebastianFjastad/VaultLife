
CREATE PROC [dbo].[usp_ProductInWatchListInsert] @MemberID INT
	,@ProductID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[ProductInWatchList] (
	[MemberID]
	,[ProductID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @MemberID
	,@ProductID
	,@DateInserted
	,@DateUpdated
	,@USR

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
