
CREATE PROC [dbo].[usp_ProductCategoryDelete] @ProductCategoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[ProductCategory]
WHERE [ProductCategoryID] = @ProductCategoryID

COMMIT
