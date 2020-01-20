CREATE TABLE [dbo].[DisplayItem](
	[DisplayItemID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[DisplayItemType] [varchar](50) NULL,
	[DisplayItemURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_DisplayItem] PRIMARY KEY CLUSTERED 
(
	[DisplayItemID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[DisplayItem]  WITH CHECK ADD  CONSTRAINT [FK_DisplayItem_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO

ALTER TABLE [dbo].[DisplayItem] CHECK CONSTRAINT [FK_DisplayItem_Product_ProductID]
GO


