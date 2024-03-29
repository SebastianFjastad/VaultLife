USE [VaultLifeApplication]
GO
/****** Object:  StoredProcedure [dbo].[UserTransactions]    Script Date: 2014-09-27 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UserTransactions]
	@memberID varchar(100)
AS 
BEGIN
select b.GameName, b.GameDescription , a.USR, a.DateUpdated from dbo.MemberInGame a 
join dbo.Game b on b.GameID  =  a.GameID
where a.USR = @memberID and a.Winindicator = 1 and a.PaymentIndicator = 1 order by a.DateUpdated desc
END


-- Note, both Winindicator and PaymentIndicator must be true to return any rows





