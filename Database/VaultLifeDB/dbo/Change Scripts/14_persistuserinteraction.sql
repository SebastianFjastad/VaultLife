USE [VaultLifeApplication]
GO

/ Object:  StoredProcedure [dbo].[PersistUserInteraction]    Script Date: 2014-12-02 03:50:59 PM /
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PersistUserInteraction] @ProductInGameID INT
 ,@memberInGameID INT
 ,@OffSet INT
 
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 -- Insert statements for procedure here
 INSERT INTO ProductPlayed (
  ProductInGameID
  ,MemberInGameID
  ,ClickInterval
  ,DateInserted
  ,Winner
  )
 VALUES (
  @ProductInGameID
  ,@memberInGameID
  ,@OffSet
  ,GETDATE()
  ,0
  )
END

GO