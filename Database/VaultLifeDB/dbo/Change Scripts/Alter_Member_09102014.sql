USE [VaultLifeApplication]
GO
alter table dbo.Product add Terms varchar(500) null
alter table dbo.Product add URL varchar(500) null

--Please update your VaultLifeApp.edmx file with these new entities