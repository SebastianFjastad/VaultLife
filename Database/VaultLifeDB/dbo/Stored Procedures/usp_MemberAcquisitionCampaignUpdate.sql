
CREATE PROC [dbo].[usp_MemberAcquisitionCampaignUpdate] @MemberAcquisitionCampaignID INT
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

UPDATE [dbo].[MemberAcquisitionCampaign]
SET [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID
	,[MemberAcquisitionCampaignCode] = @MemberAcquisitionCampaignCode
	,[OwnerID] = @OwnerID
	,[ContractID] = @ContractID
	,[MemberAcquisitionCampaignName] = @MemberAcquisitionCampaignName
	,[MemberAcquisitionCampaignDescription] = @MemberAcquisitionCampaignDescription
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID

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
