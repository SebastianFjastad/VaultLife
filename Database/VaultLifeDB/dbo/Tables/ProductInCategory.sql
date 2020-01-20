CREATE TABLE [dbo].[ProductInCategory] (
    [ProductID]         INT           NOT NULL,
    [ProductCategoryID] INT           NOT NULL,
    [DateInserted]      DATETIME2 (4) NOT NULL,
    [DateUpdated]       DATETIME2 (4) NOT NULL,
    [USR]               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProductInCategory] PRIMARY KEY CLUSTERED ([ProductID] ASC, [ProductCategoryID] ASC),
    CONSTRAINT [FK_ProductInCategory_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]),
    CONSTRAINT [FK_ProductInCategory_ProductCategory_ProductCategoryID] FOREIGN KEY ([ProductCategoryID]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryID])
);

