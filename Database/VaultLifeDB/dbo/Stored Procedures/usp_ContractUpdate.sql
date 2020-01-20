
CREATE PROC [dbo].[usp_ContractUpdate] @ContractID INT
	,@ContractCode VARCHAR(20)
	,@ContractDetail VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[Contract]
SET [ContractID] = @ContractID
	,[ContractCode] = @ContractCode
	,[ContractDetail] = @ContractDetail
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [ContractID] = @ContractID

-- Begin Return Select <- do not remove
SELECT [ContractID]
	,[ContractCode]
	,[ContractDetail]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Contract]
WHERE [ContractID] = @ContractID

-- End Return Select <- do not remove
COMMIT
