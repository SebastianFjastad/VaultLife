CREATE TABLE [dbo].[ProductInWatchList] (
    [MemberID]     INT           NOT NULL,
    [ProductID]    INT           NOT NULL,
    [DateInserted] DATETIME2 (4) NOT NULL,
    [DateUpdated]  DATETIME2 (4) NOT NULL,
    [USR]          VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProductInWatchList] PRIMARY KEY CLUSTERED ([MemberID] ASC, [ProductID] ASC),
    CONSTRAINT [FK_ProductInWatchList_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID]),
    CONSTRAINT [FK_ProductInWatchList_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

