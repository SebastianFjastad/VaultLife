CREATE TABLE [dbo].[Territory] (
    [TerritoryID]          INT           IDENTITY (1, 1) NOT NULL,
    [TerritoryCode]        VARCHAR (20)  NOT NULL,
    [OwnerID]              INT           NOT NULL,
    [ContractID]           INT           NOT NULL,
    [TerritoryName]        VARCHAR (255) NOT NULL,
    [TerritoryDescription] VARCHAR (255) NOT NULL,
    [DateInserted]         DATETIME2 (4) NOT NULL,
    [DateUpdated]          DATETIME2 (4) NOT NULL,
    [USR]                  VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Territory] PRIMARY KEY CLUSTERED ([TerritoryID] ASC),
    CONSTRAINT [FK_Territory_Contract_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Territory_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Owner] ([OwnerID])
);

