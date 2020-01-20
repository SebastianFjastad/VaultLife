
CREATE PROC [dbo].[usp_MemberAcquisitionCampaignDelete] @MemberAcquisitionCampaignID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[MemberAcquisitionCampaign]
WHERE [MemberAcquisitionCampaignID] = @MemberAcquisitionCampaignID

COMMIT
