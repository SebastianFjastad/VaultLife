
CREATE PROC [dbo].[usp_ProductCategoryInsert] @ProductCategoryCode VARCHAR(20)
	,@ProductCategoryName VARCHAR(255)
	,@ProductCategoryDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[ProductCategory] (
	[ProductCategoryCode]
	,[ProductCategoryName]
	,[ProductCategoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ProductCategoryCode
	,@ProductCategoryName
	,@ProductCategoryDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [ProductCategoryID]
	,[ProductCategoryCode]
	,[ProductCategoryName]
	,[ProductCategoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductCategory]
WHERE [ProductCategoryID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
