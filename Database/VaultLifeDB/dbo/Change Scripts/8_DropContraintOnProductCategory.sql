USE [VaultLifeDev]
GO

ALTER TABLE [dbo].[MemberInterest] DROP CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID]
GO


/* DON'T RUN THIS 
ALTER TABLE [dbo].[MemberInterest]  WITH CHECK ADD  CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryID])
GO

ALTER TABLE [dbo].[MemberInterest] CHECK CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID]
GO
*/

