Create Procedure GetMaxTransactionTime @GameID int
AS 
BEGIN
SELECT max(DurationRemaining)
FROM TrackingTransaction TT
	INNER JOIN MemberInGame mig ON mig.MemberInGameID = tt.MemberInGameID
	INNER JOIN Game g ON mig.gameId = g.GameID
WHERE g.GameId = @GameID
END