
CREATE PROC [dbo].[usp_ProductInCategoryUpdate] @ProductID INT
	,@ProductCategoryID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[ProductInCategory]
SET [ProductID] = @ProductID
	,[ProductCategoryID] = @ProductCategoryID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ProductID] = @ProductID
	AND [ProductCategoryID] = @ProductCategoryID

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
