
CREATE PROC [dbo].[usp_ProductLocationSelect] @ProductLocationID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ProductLocationID]
	,[ProductID]
	,[GameID]
	,[AdressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductLocation]
WHERE (
		[ProductLocationID] = @ProductLocationID
		OR @ProductLocationID IS NULL
		)

COMMIT
