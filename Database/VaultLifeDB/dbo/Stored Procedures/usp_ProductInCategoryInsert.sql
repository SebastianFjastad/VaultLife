
CREATE PROC [dbo].[usp_ProductInCategoryInsert] @ProductID INT
	,@ProductCategoryID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[ProductInCategory] (
	[ProductID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ProductID
	,@ProductCategoryID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [ProductID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInCategory]
WHERE [ProductID] = @ProductID
	AND [ProductCategoryID] = @ProductCategoryID

-- End Return Select <- do not remove
COMMIT
