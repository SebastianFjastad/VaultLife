CREATE VIEW [dbo].[getRecentSales]
AS
SELECT top 5  g.GameID
 ,g.GameName
 ,g.GameDescription
 ,dbo.GameRule.ExcecuteTime
 ,dbo.ProductInGame.PriceInGame
 ,dbo.ProductInGame.ReferencePrice
 ,ImageID = (Select  top 1 imageID from ImageDetails where dbo.ProductInGame.ProductID = dbo.Imagedetails.ProductId)
FROM dbo.Game AS g
INNER JOIN dbo.GameRule ON g.GameID = dbo.GameRule.GameID
INNER JOIN dbo.ProductInGame ON g.GameID = dbo.ProductInGame.GameID
--INNER JOIN dbo.Imagedetails ON dbo.ProductInGame.ProductID = dbo.Imagedetails.ProductId
WHERE (dbo.GameRule.GameRuleCode = 'RESOLVEPOTENTIALWINNERS')
AND G.GameState='COMPLETED'