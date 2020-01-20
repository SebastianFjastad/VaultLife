
CREATE PROC [dbo].[usp_ProductInGameInsert] @ProductID INT
	,@MemberID INT
	,@ProductQuantity INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[ProductInGame] (
	[ProductID]
	,[MemberID]
	,[ProductQuantity]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ProductID
	,@MemberID
	,@ProductQuantity
	,@DateInserted
	,@DateUpdated
	,@USR

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
