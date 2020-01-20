
CREATE PROC [dbo].[usp_ContractDelete] @ContractID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[Contract]
WHERE [ContractID] = @ContractID

COMMIT
