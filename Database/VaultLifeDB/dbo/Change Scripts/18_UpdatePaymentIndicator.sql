Create proc UpdateWinIndictor @productPlayedID int

as 
BEGIN

UPDATE ProductPlayed set Winner = 1 where ProductPlayedID =@productPlayedID

END