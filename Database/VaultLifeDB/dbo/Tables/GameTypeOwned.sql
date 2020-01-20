CREATE TABLE [dbo].[GameTypeOwned] (
    [GameTypeOwnedID]   INT           NOT NULL,
    [GameTypeOwnedCode] VARCHAR (20)  NOT NULL,
    [OwnerID]           INT           NOT NULL,
    [GameTypeID]        INT           NOT NULL,
    [ContractID]        INT           NOT NULL,
    [DateInserted]      DATETIME2 (4) NOT NULL,
    [DateUpdated]       DATETIME2 (4) NOT NULL,
    [USR]               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GameTypeOwned] PRIMARY KEY CLUSTERED ([GameTypeOwnedID] ASC),
    CONSTRAINT [FK_GameTypeOwned_Contract_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_GameTypeOwned_GameType_GameTypeID] FOREIGN KEY ([GameTypeID]) REFERENCES [dbo].[GameType] ([GameTypeID]),
    CONSTRAINT [FK_GameTypeOwned_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Owner] ([OwnerID])
);

