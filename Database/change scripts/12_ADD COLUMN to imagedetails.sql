USE [VaultLifeApplication]
GO

ALTER TABLE dbo.[Imagedetails] ADD [ImageTypeID] [int] NOT NULL CONSTRAINT [DF_Imagedetails_ImageTypeID]  DEFAULT ((1))
GO
