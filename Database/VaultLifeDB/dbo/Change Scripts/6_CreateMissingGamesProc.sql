USE [VaultLifeApplication]
GO

/****** Object:  StoredProcedure [dbo].[GetMissingGames]    Script Date: 2014-11-04 11:19:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[GetMissingGames] 
	@MemberId int
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select gameID from Game where gameID not in (Select distinct gameID from memberingame where memberId = @MemberId) and Game.GameState in ('prepare','released','active')
END

GO


