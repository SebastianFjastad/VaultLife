
CREATE PROC [dbo].[usp_ProductLocationDelete] @ProductLocationID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[ProductLocation]
WHERE [ProductLocationID] = @ProductLocationID

COMMIT
