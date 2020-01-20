
CREATE PROC [dbo].[usp_MemberSelect] @MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

SELECT [MemberID]
	,[MemberSubscriptionTypeID]
	,[MemberCode]
	,[IdentityType]
	,[EmailAddress]
	,[TelephoneHome]
	,[TelephoneOffice]
	,[TelephoneMobile]
	,[Gender]
	,[Ethnicity]
	,[DateOfBirth]
	,[ActiveIndicator]
	,[RenewalDate]
	,[AddressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Member]
WHERE (
		[MemberID] = @MemberID
		OR @MemberID IS NULL
		)

COMMIT
