
CREATE PROC [dbo].[usp_ProductUpdate] @ProductID INT
	,@ProductSKUCode VARCHAR(50)
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

UPDATE [dbo].[Product]
SET [ProductSKUCode] = @ProductSKUCode
	,[OwnerID] = @OwnerID
	,[ContractID] = @ContractID
	,[ProductName] = @ProductName
	,[ProductDescription] = @ProductDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ProductID] = @ProductID

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
WHERE [ProductID] = @ProductID

-- End Return Select <- do not remove
COMMIT
