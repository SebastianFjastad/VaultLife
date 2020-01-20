CREATE TABLE [dbo].[ProductCategory] (
    [ProductCategoryID]          INT           IDENTITY (1, 1) NOT NULL,
    [ProductCategoryCode]        VARCHAR (20)  NOT NULL,
    [ProductCategoryName]        VARCHAR (255) NOT NULL,
    [ProductCategoryDescription] VARCHAR (255) NOT NULL,
    [DateInserted]               DATETIME2 (4) NOT NULL,
    [DateUpdated]                DATETIME2 (4) NOT NULL,
    [USR]                        VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProductCategory_ProductCategoryID] PRIMARY KEY CLUSTERED ([ProductCategoryID] ASC)
);

