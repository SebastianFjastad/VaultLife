
CREATE PROC [dbo].[usp_ProductLocationUpdate] @ProductLocationID INT
	,@ProductID INT
	,@GameID INT
	,@AdressID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[ProductLocation]
SET [ProductID] = @ProductID
	,[GameID] = @GameID
	,[AdressID] = @AdressID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ProductLocationID] = @ProductLocationID

-- Begin Return Select <- do not remove
SELECT [ProductLocationID]
	,[ProductID]
	,[GameID]
	,[AdressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductLocation]
WHERE [ProductLocationID] = @ProductLocationID

-- End Return Select <- do not remove
COMMIT
