
CREATE PROC [dbo].[usp_ProductCategoryUpdate] @ProductCategoryID INT
	,@ProductCategoryCode VARCHAR(20)
	,@ProductCategoryName VARCHAR(255)
	,@ProductCategoryDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[ProductCategory]
SET [ProductCategoryCode] = @ProductCategoryCode
	,[ProductCategoryName] = @ProductCategoryName
	,[ProductCategoryDescription] = @ProductCategoryDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ProductCategoryID] = @ProductCategoryID

-- Begin Return Select <- do not remove
SELECT [ProductCategoryID]
	,[ProductCategoryCode]
	,[ProductCategoryName]
	,[ProductCategoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductCategory]
WHERE [ProductCategoryID] = @ProductCategoryID

-- End Return Select <- do not remove
COMMIT
