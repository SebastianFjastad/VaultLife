CREATE TABLE [dbo].[MemberOwned] (
    [MemberID]                    INT           NOT NULL,
    [OwnerID]                     INT           NOT NULL,
    [TerritoryID]                 INT           NOT NULL,
    [MemberAcquisitionCampaignID] INT           NOT NULL,
    [DateFrom]                    DATE          NOT NULL,
    [DateTo]                      DATE          NOT NULL,
    [ExclusiveIndicator]          BIT           NOT NULL,
    [DateInserted]                DATETIME2 (4) NOT NULL,
    [DateUpdated]                 DATETIME2 (4) NOT NULL,
    [USR]                         VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberOwned] PRIMARY KEY CLUSTERED ([MemberID] ASC, [OwnerID] ASC, [TerritoryID] ASC, [MemberAcquisitionCampaignID] ASC, [DateFrom] ASC),
    CONSTRAINT [FK_MemberOwned_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID]),
    CONSTRAINT [FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID] FOREIGN KEY ([MemberAcquisitionCampaignID]) REFERENCES [dbo].[MemberAcquisitionCampaign] ([MemberAcquisitionCampaignID]),
    CONSTRAINT [FK_MemberOwned_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Owner] ([OwnerID]),
    CONSTRAINT [FK_MemberOwned_Territory_TerritoryID] FOREIGN KEY ([TerritoryID]) REFERENCES [dbo].[Territory] ([TerritoryID])
);

