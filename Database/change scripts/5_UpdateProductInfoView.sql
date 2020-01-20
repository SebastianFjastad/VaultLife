
ALTER VIEW [dbo].[ProductInfo]
AS
SELECT        p.ProductName, p.ProductDescription, pig.PriceInGame, pig.ReferencePrice, p.ProductPrice, dbo.ProductLocation.Address, dbo.ProductLocation.DeliveryAgentName, g.GameID, p.terms, p.link, 
                         dbo.Imagedetails.ImageID
FROM            dbo.Game AS g Inner JOIN
                         dbo.ProductInGame AS pig ON  pig.GameID = g.GameID INNER JOIN
                         dbo.Product AS p ON p.ProductID = pig.ProductID INNER JOIN
                         dbo.Imagedetails ON dbo.Imagedetails.ProductId = p.ProductID INNER JOIN
                         dbo.ProductLocation ON pig.ProductInGameID = dbo.ProductLocation.ProductInGameID

GO


