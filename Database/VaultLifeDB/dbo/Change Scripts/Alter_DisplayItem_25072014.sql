/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]		
			   
Engineer: Daniel Souchon		
--------------------------------------------------------------------------------------
*/
DELETE FROM [dbo].[DisplayItem]
GO

ALTER TABLE [dbo].[DisplayItem] ADD [DeleteOnSave] [bit] NOT NULL
GO

ALTER TABLE [dbo].[DisplayItem] ADD  CONSTRAINT [DF_DisplayItem_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[DisplayItem] ADD  CONSTRAINT [DF_DisplayItem_DeleteOnSave]  DEFAULT ((0)) FOR [DeleteOnSave]
GO
