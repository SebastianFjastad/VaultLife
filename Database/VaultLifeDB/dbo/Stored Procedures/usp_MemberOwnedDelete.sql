
CREATE PROC [dbo].[usp_MemberOwnedDelete] @MemberID INT
	,@OwnerID INT
	,@TerritoryID INT
	,@MemberAcquisitionCampaignID INT
	,@DateFrom DATE
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberOwned]
WHERE [MemberID] = @MemberID
	AND [OwnerID] = @OwnerID
	AND [TerritoryID] = @TerritoryID
	AND [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
	AND [DateFrom] = @DateFrom

COMMIT
