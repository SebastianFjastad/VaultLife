ALTER VIEW [dbo].[MembersWhoPlayed]
AS
SELECT        dbo.Member.MemberID, dbo.Game.GameID, dbo.ProductPlayed.Winner as WinIndicator, dbo.ProductPlayed.ClickInterval, dbo.MemberInGame.PaymentIndicator
FROM            dbo.Game INNER JOIN
                         dbo.MemberInGame ON dbo.Game.GameID = dbo.MemberInGame.GameID INNER JOIN
                         dbo.ProductPlayed ON dbo.MemberInGame.MemberInGameID = dbo.ProductPlayed.MemberInGameID INNER JOIN
                         dbo.Member ON dbo.MemberInGame.MemberID = dbo.Member.MemberID

