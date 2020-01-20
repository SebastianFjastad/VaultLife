
CREATE PROC [dbo].[usp_ProductLocationInsert] @ProductID INT
	,@GameID INT
	,@AdressID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[ProductLocation] (
	[ProductID]
	,[GameID]
	,[AdressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ProductID
	,@GameID
	,@AdressID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
SELECT [ProductLocationID]
	,[ProductID]
	,[GameID]
	,[AdressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductLocation]
WHERE [ProductLocationID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
