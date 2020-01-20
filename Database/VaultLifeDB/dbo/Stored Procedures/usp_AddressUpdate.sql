
CREATE PROC [dbo].[usp_AddressUpdate] @AddressID INT
	,@AddressType VARCHAR(10) = NULL
	,@AddressLine1 VARCHAR(255) = NULL
	,@AddressLine2 VARCHAR(255) = NULL
	,@AddressLine3 VARCHAR(255) = NULL
	,@Country VARCHAR(255) = NULL
	,@StateOrProvince VARCHAR(255) = NULL
	,@ZipOrPostalCode VARCHAR(20) = NULL
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[Address]
SET [AddressType] = @AddressType
	,[AddressLine1] = @AddressLine1
	,[AddressLine2] = @AddressLine2
	,[AddressLine3] = @AddressLine3
	,[Country] = @Country
	,[StateOrProvince] = @StateOrProvince
	,[ZipOrPostalCode] = @ZipOrPostalCode
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [AddressID] = @AddressID

-- Begin Return Select <- do not remove
SELECT [AddressID]
	,[AddressType]
	,[AddressLine1]
	,[AddressLine2]
	,[AddressLine3]
	,[Country]
	,[StateOrProvince]
	,[ZipOrPostalCode]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Address]
WHERE [AddressID] = @AddressID

-- End Return Select <- do not remove
COMMIT
