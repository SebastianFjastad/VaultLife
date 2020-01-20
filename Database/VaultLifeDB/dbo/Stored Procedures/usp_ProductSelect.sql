
CREATE PROC [dbo].[usp_ProductSelect] @ProductID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ProductID]
	,[ProductSKUCode]
	,[OwnerID]
	,[ContractID]
	,[ProductName]
	,[ProductDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Product]
WHERE (
		[ProductID] = @ProductID
		OR @ProductID IS NULL
		)

COMMIT
