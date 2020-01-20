
CREATE PROC [dbo].[usp_ProductInGameSelect] @ProductID INT
	,@MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [ProductID]
	,[MemberID]
	,[ProductQuantity]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[ProductInGame]
WHERE (
		[ProductID] = @ProductID
		OR @ProductID IS NULL
		)
	AND (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)

COMMIT
