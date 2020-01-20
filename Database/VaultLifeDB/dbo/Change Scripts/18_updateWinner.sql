ALTER proc [dbo].[UpdateWinIndictor] @productPlayedID int, @winner int

as 
BEGIN

UPDATE ProductPlayed WITH (SNAPSHOT) set Winner = @winner where ProductPlayedID =@productPlayedID

END

GO