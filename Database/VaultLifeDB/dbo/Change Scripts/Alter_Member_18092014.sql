USE [VaultLifeApplication]
GO
alter table dbo.Member add IdentityNumber varchar(100) null
alter table dbo.Member add Industry varchar(100) null
alter table dbo.Member add Designation varchar (20) null
alter table dbo.Member add MaritalStatus varchar (20) null
alter table dbo.Member add Children varchar (20) null

--Please update your VaultLifeApp.edmx file with these new entities