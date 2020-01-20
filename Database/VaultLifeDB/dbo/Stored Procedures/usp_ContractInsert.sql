
CREATE PROC [dbo].[usp_ContractInsert] @ContractID INT
	,@ContractCode VARCHAR(20)
	,@ContractDetail VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[Contract] (
	[ContractID]
	,[ContractCode]
	,[ContractDetail]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @ContractID
	,@ContractCode
	,@ContractDetail
	,@DateInserted
	,@DateUpdated
	,@USR

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
