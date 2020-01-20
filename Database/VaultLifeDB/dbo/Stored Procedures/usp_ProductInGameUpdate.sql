
CREATE PROC [dbo].[usp_ProductInGameUpdate] @ProductID INT
	,@MemberID INT
	,@ProductQuantity INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[ProductInGame]
SET [ProductID] = @ProductID
	,[MemberID] = @MemberID
	,[ProductQuantity] = @ProductQuantity
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ProductID] = @ProductID
	AND [MemberID] = @MemberID

-- Begin Return Select <- do not remove
SELECT [ProductID]
	,[MemberID]
	,[ProductQuantity]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInGame]
WHERE [ProductID] = @ProductID
	AND [MemberID] = @MemberID

-- End Return Select <- do not remove
COMMIT
