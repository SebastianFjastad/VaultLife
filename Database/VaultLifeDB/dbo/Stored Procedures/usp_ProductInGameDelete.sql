
CREATE PROC [dbo].[usp_ProductInGameDelete] @ProductID INT
	,@MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[ProductInGame]
WHERE [ProductID] = @ProductID
	AND [MemberID] = @MemberID

COMMIT
