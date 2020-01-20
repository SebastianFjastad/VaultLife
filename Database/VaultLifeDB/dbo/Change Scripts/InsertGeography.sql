insert into CountryState (CountryID,Statename)
SELECT DISTINCT cs.CountryID
	,s.STATE
FROM Staging s
INNER JOIN Country CS ON s.country = cs.CountryName
WHERE CountryId <> 192

insert into CountryCity (CountryID,CityName,StateID)
SELECT cs.CountryID
	,s.City
	,CS.StateID
FROM Staging s
INNER JOIN CountryState CS ON s.STATE = cs.stateName

SELECT *
FROM Country
INNER JOIN CountryState ON Country.CountryID = CountryState.CountryID
INNER JOIN CountryCity ON CountryCity.StateID = Countrystate.StateID


Alter table member add CityID int  null



