
CREATE PROC [dbo].[usp_MemberOwnedSelect] @MemberID INT
	,@OwnerID INT
	,@TerritoryID INT
	,@MemberAcquisitionCampaignID INT
	,@DateFrom DATE
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

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
WHERE (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)
	AND (
		[OwnerID] = @OwnerID
		OR @OwnerID IS NULL
		)
	AND (
		[TerritoryID] = @TerritoryID
		OR @TerritoryID IS NULL
		)
	AND (
		[MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
		OR @MemberAcquisitionCampaignID IS NULL
		)
	AND (
		[DateFrom] = @DateFrom
		OR @DateFrom IS NULL
		)

COMMIT
