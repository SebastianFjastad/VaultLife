Alter table dbo.TerritoryDefinition
add CountryID int null
Alter table dbo.TerritoryDefinition
add StateID int null
Alter table dbo.TerritoryDefinition
add CityID int null


USE [VaultLifeApplication]
GO

ALTER TABLE [dbo].[TerritoryDefinition]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDefinition_Country_CountryID] FOREIGN KEY(CountryID)
REFERENCES [dbo].[Country] ([CountryID])
GO

ALTER TABLE [dbo].[TerritoryDefinition] CHECK CONSTRAINT [FK_TerritoryDefinition_Country_CountryID]
GO

ALTER TABLE [dbo].[TerritoryDefinition]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDefinition_State_StateID] FOREIGN KEY(StateID)
REFERENCES [dbo].[CountryState] ([StateID])
GO

ALTER TABLE [dbo].[TerritoryDefinition] CHECK CONSTRAINT [FK_TerritoryDefinition_State_StateID]
GO

ALTER TABLE [dbo].[TerritoryDefinition]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDefinition_city_cityID] FOREIGN KEY(cityID)
REFERENCES [dbo].[Countrycity] ([cityID])
GO

ALTER TABLE [dbo].[TerritoryDefinition] CHECK CONSTRAINT [FK_TerritoryDefinition_city_cityID]
GO

