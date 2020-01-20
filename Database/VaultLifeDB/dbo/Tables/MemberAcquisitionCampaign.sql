CREATE TABLE [dbo].[MemberAcquisitionCampaign] (
    [MemberAcquisitionCampaignID]          INT           NOT NULL,
    [MemberAcquisitionCampaignCode]        VARCHAR (20)  NOT NULL,
    [OwnerID]                              INT           NOT NULL,
    [ContractID]                           INT           NOT NULL,
    [MemberAcquisitionCampaignName]        VARCHAR (255) NOT NULL,
    [MemberAcquisitionCampaignDescription] VARCHAR (255) NOT NULL,
    [DateInserted]                         DATETIME2 (4) NOT NULL,
    [DateUpdated]                          DATETIME2 (4) NOT NULL,
    [USR]                                  VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberAcquisitionCampaign] PRIMARY KEY CLUSTERED ([MemberAcquisitionCampaignID] ASC),
    CONSTRAINT [FK_MemberAcquisitionCampaign_Contract_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_MemberAcquisitionCampaign_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Owner] ([OwnerID])
);

