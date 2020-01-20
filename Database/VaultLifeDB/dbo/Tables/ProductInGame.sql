CREATE TABLE [dbo].[ProductInGame] (
    [ProductID]       INT           NOT NULL,
    [MemberID]        INT           NOT NULL,
    [ProductQuantity] INT           NOT NULL,
    [DateInserted]    DATETIME2 (4) NOT NULL,
    [DateUpdated]     DATETIME2 (4) NOT NULL,
    [USR]             VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProductInGame] PRIMARY KEY CLUSTERED ([ProductID] ASC, [MemberID] ASC),
    CONSTRAINT [FK_ProductInGame_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID]),
    CONSTRAINT [FK_ProductInGame_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

