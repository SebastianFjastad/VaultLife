
CREATE PROC [dbo].[usp_AddressInsert] @AddressType VARCHAR(10) = NULL
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

INSERT INTO [dbo].[Address] (
	[AddressType]
	,[AddressLine1]
	,[AddressLine2]
	,[AddressLine3]
	,[Country]
	,[StateOrProvince]
	,[ZipOrPostalCode]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @AddressType
	,@AddressLine1
	,@AddressLine2
	,@AddressLine3
	,@Country
	,@StateOrProvince
	,@ZipOrPostalCode
	,@DateInserted
	,@DateUpdated
	,@USR

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
WHERE [AddressID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
