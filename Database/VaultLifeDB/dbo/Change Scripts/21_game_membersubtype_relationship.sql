ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_MemberSubscriptionType] FOREIGN KEY([MemberSubscriptionType])
REFERENCES [dbo].[MemberSubscriptionType] ([MemberSubscriptionTypeID])
GO

ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_MemberSubscriptionType]
GO

