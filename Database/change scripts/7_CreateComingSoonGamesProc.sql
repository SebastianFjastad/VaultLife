
/****** Object:  StoredProcedure [dbo].[FindComingSoonGames]    Script Date: 2014-11-10 03:36:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[FindComingSoonGames]
 @memberId int
 as 
 begin 
 if @memberId != 0 
 BEGIN

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
END

ELSE
BEGIN
SELECT   g.GameId
	FROM Game g
	INNER JOIN GameRule gr ON g.gameId = gr.gameID
	
	WHERE G.gamestate   IN (
			'prepare'
			,'released'
			,'active'
			)
	
	and gr.GameRuleCode ='StartGame'
	order by  gr.ExcecuteTime
END

end