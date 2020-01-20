
CREATE PROC [dbo].[usp_MemberAcquisitionCampaignSelect] @MemberAcquisitionCampaignID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberAcquisitionCampaignID]
	,[MemberAcquisitionCampaignCode]
	,[OwnerID]
	,[ContractID]
	,[MemberAcquisitionCampaignName]
	,[MemberAcquisitionCampaignDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[MemberAcquisitionCampaign]
WHERE (
		[MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
		OR @MemberAcquisitionCampaignID IS NULL
		)

COMMIT
