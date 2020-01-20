
CREATE PROC [dbo].[usp_ProductInCategoryDelete] @ProductID INT
	,@ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[ProductInCategory]
WHERE [ProductID] = @ProductID
	AND [ProductCategoryID] = @ProductCategoryID

COMMIT
