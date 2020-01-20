USE [VaultLifeApplication]
GO

alter table GameTemplate drop column GameRuleID
alter table GameTemplate add GameRuleCode varchar(255)
alter table GameTemplate add GameRuleDetail varchar(255)
alter table GameTemplate add OrderInGame int
