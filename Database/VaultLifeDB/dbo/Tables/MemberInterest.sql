CREATE TABLE [dbo].[MemberInterest] (
    [MemberID]          INT           NOT NULL,
    [ProductCategoryID] INT           NOT NULL,
    [DateInserted]      DATETIME2 (4) NOT NULL,
    [DateUpdated]       DATETIME2 (4) NOT NULL,
    [USR]               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberInterest] PRIMARY KEY CLUSTERED ([MemberID] ASC, [ProductCategoryID] ASC),
    CONSTRAINT [FK_MemberInterest_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID]),
    CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID] FOREIGN KEY ([ProductCategoryID]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryID])
);

