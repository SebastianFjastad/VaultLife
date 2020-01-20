
CREATE PROC [dbo].[usp_AddressSelect] @AddressID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

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
WHERE (
		[AddressID] = @AddressID
		OR @AddressID IS NULL
		)

COMMIT
