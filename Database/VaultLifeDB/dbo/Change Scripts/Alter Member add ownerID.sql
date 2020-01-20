
ALTER table Member ADD OwnerID int not null

ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Owner_OwnerD] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([OwnerID])
GO

ALTER TABLE [dbo].[Member] CHECK CONSTRAINT  [FK_Member_Owner_OwnerD]
GO


