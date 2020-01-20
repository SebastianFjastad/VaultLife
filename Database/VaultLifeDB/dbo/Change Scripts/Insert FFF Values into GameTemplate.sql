use VaultLifeApplication

update GameTemplate
set 
GameTemplateID = 1, 
GameTemplateCode = 'FFF Rule 1', 
GameRuleCode = 'PrepareGameRule',
GameRuleDetail = 'PrepareGameRule', 
GameTypeID = 1, 
OrderInGame =1, 
DateUpdated = getdate(), 
DateInserted = getdate(), 
USR = 'system'
where GameTemplateID = 1

insert into GameTemplate (GameTemplateID, GameTemplateCode, GameRuleCode, GameRuleDetail, GameTypeID, OrderInGame, DateUpdated, DateInserted, USR)
select 2, 'FFF Rule 2', 'StartGame', 'StartGame', 1, 2, getdate(), getdate() , 'system'
union
select 3, 'FFF Rule 3', 'ResolvePotentialWinners', 'ResolvePotentialWinners', 1, 3, getdate(), getdate() , 'system'
union
select 4, 'FFF Rule 4', 'ResolveActualWinners', 'ResolveActualWinners', 1, 4, getdate(), getdate() , 'system'