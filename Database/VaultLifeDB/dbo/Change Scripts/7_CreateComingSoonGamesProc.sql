CREATE proc FindComingSoonGames
 @memberId int
 as 
 begin 
 SELECT g.GameId
FROM Game g
INNER JOIN GameRule gr ON g.gameId = gr.gameID
inner Join MemberInGame mig on g.gameID = Mig.GameID
WHERE G.gamestate   IN (
		'prepare'
		,'released'
		,'active'
		)
and mig.MemberID = @memberId
and gr.GameRuleCode ='StartGame'
order by  gr.ExcecuteTime

end