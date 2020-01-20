
CREATE PROC [dbo].[usp_TerritoryDelete] @TerritoryID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[Territory]
WHERE [TerritoryID] = @TerritoryID

COMMIT
