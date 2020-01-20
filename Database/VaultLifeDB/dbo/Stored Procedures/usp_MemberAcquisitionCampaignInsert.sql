
CREATE PROC [dbo].[usp_MemberAcquisitionCampaignInsert] @MemberAcquisitionCampaignID INT
	,@MemberAcquisitionCampaignCode VARCHAR(20)
	,@OwnerID INT
	,@ContractID INT
	,@MemberAcquisitionCampaignName VARCHAR(255)
	,@MemberAcquisitionCampaignDescription VARCHAR(255)
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[MemberAcquisitionCampaign] (
	[MemberAcquisitionCampaignID]
	,[MemberAcquisitionCampaignCode]
	,[OwnerID]
	,[ContractID]
	,[MemberAcquisitionCampaignName]
	,[MemberAcquisitionCampaignDescription]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
	)
SELECT @MemberAcquisitionCampaignID
	,@MemberAcquisitionCampaignCode
	,@OwnerID
	,@ContractID
	,@MemberAcquisitionCampaignName
	,@MemberAcquisitionCampaignDescription
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
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
WHERE [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID

-- End Return Select <- do not remove
COMMIT
