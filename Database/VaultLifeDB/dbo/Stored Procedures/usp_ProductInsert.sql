
CREATE PROC [dbo].[usp_ProductInsert] @ProductSKUCode VARCHAR(50)
	,@OwnerID INT
	,@ContractID INT
	,@ProductName VARCHAR(255)
	,@ProductDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[Product] (
	[ProductSKUCode]
	,[OwnerID]
	,[ContractID]
	,[ProductName]
	,[ProductDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ProductSKUCode
	,@OwnerID
	,@ContractID
	,@ProductName
	,@ProductDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
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
WHERE [ProductID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
