
CREATE PROC [dbo].[usp_AddressDelete] @AddressID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[Address]
WHERE [AddressID] = @AddressID

COMMIT
