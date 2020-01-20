CREATE TABLE [dbo].[Product] (
    [ProductID]          INT           IDENTITY (1, 1) NOT NULL,
    [ProductSKUCode]     VARCHAR (50)  NOT NULL,
    [OwnerID]            INT           NOT NULL,
    [ContractID]         INT           NOT NULL,
    [ProductName]        VARCHAR (255) NOT NULL,
    [ProductDescription] VARCHAR (255) NOT NULL,
    [DateInserted]       DATETIME2 (4) NOT NULL,
    [DateUpdated]        DATETIME2 (4) NOT NULL,
    [USR]                VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Product_ProductID] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_Contract_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Product_Owner_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Owner] ([OwnerID])
);

