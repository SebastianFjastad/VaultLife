
CREATE PROC [dbo].[usp_ProductCategorySelect] @ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ProductCategoryID]
	,[ProductCategoryCode]
	,[ProductCategoryName]
	,[ProductCategoryDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductCategory]
WHERE (
		[ProductCategoryID] = @ProductCategoryID
		OR @ProductCategoryID IS NULL
		)

COMMIT
