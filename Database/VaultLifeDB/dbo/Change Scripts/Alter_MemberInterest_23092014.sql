USE [VaultLifeApplication]
GO
alter table dbo.MemberInterest add MemberInterestAutomotive bit not null
alter table dbo.MemberInterest add MemberInterestArtCollectibles bit not null
alter table dbo.MemberInterest add MemberInterestHomeAppliances bit not null
alter table dbo.MemberInterest add MemberInterestToys bit not null
alter table dbo.MemberInterest add MemberInterestEntertainment bit not null
alter table dbo.MemberInterest add MemberInterestDecorDesign bit not null
alter table dbo.MemberInterest add MemberInterestHealthBeauty bit not null
alter table dbo.MemberInterest add MemberInterestTravel bit not null
alter table dbo.MemberInterest add MemberInterestFashionAccessories bit not null
alter table dbo.MemberInterest add MemberInterestExperiences bit not null
alter table dbo.MemberInterest add MemberInterestTechnology bit not null
alter table dbo.MemberInterest add MemberInterestWiningDining bit not null
alter table dbo.MemberInterest add MemberInterestOther varchar(500) null
--Make sure MemberInterest table is empty before running this update, or else the not null property will fail
--Please update your VaultLifeApp.edmx file with these new entities