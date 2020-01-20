CREATE TABLE [dbo].[ProductLocation] (
    [ProductLocationID] INT           IDENTITY (1, 1) NOT NULL,
    [ProductID]         INT           NOT NULL,
    [GameID]            INT           NOT NULL,
    [AdressID]          INT           NOT NULL,
    [DateInserted]      DATETIME2 (4) NOT NULL,
    [DateUpdated]       DATETIME2 (4) NOT NULL,
    [USR]               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProductLocation] PRIMARY KEY CLUSTERED ([ProductLocationID] ASC),
    CONSTRAINT [FK_ProductLocation_Game_GameID] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID]),
    CONSTRAINT [FK_ProductLocation_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

