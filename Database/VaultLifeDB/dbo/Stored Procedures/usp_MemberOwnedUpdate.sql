
CREATE PROC [dbo].[usp_MemberOwnedUpdate] @MemberID INT
	,@OwnerID INT
	,@TerritoryID INT
	,@MemberAcquisitionCampaignID INT
	,@DateFrom DATE
	,@DateTo DATE
	,@ExclusiveIndicator BIT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[MemberOwned]
SET [MemberID] = @MemberID
	,[OwnerID] = @OwnerID
	,[TerritoryID] = @TerritoryID
	,[MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
	,[DateFrom] = @DateFrom
	,[DateTo] = @DateTo
	,[ExclusiveIndicator] = @ExclusiveIndicator
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberID] = @MemberID
	AND [OwnerID] = @OwnerID
	AND [TerritoryID] = @TerritoryID
	AND [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
	AND [DateFrom] = @DateFrom

-- Begin Return Select <- do not remove
SELECT [MemberID]
	,[OwnerID]
	,[TerritoryID]
	,[MemberAcquisitionCampaignID]
	,[DateFrom]
	,[DateTo]
	,[ExclusiveIndicator]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberOwned]
WHERE [MemberID] = @MemberID
	AND [OwnerID] = @OwnerID
	AND [TerritoryID] = @TerritoryID
	AND [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
	AND [DateFrom] = @DateFrom

-- End Return Select <- do not remove
COMMIT
