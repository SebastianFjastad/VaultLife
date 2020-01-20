
CREATE PROC [dbo].[usp_ContractSelect] @ContractID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ContractID]
	,[ContractCode]
	,[ContractDetail]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Contract]
WHERE (
		[ContractID] = @ContractID
		OR @ContractID IS NULL
		)

COMMIT
