
CREATE PROC [dbo].[usp_ProductInCategorySelect] @ProductID INT
	,@ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ProductID]
	,[ProductCategoryID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInCategory]
WHERE (
		[ProductID] = @ProductID
		OR @ProductID IS NULL
		)
	AND (
		[ProductCategoryID] = @ProductCategoryID
		OR @ProductCategoryID IS NULL
		)

COMMIT
