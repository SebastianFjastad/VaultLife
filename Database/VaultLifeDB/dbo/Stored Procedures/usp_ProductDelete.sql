
CREATE PROC [dbo].[usp_ProductDelete] @ProductID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[Product]
WHERE [ProductID] = @ProductID

COMMIT
