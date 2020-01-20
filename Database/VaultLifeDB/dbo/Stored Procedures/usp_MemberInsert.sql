
CREATE PROC [dbo].[usp_MemberInsert] @MemberSubscriptionTypeID INT
	,@MemberCode VARCHAR(20)
	,@IdentityType VARCHAR(20)
	,@EmailAddress VARCHAR(255)
	,@TelephoneHome VARCHAR(20) = NULL
	,@TelephoneOffice VARCHAR(20) = NULL
	,@TelephoneMobile VARCHAR(20)
	,@Gender VARCHAR(1)
	,@Ethnicity VARCHAR(255) = NULL
	,@DateOfBirth DATE
	,@ActiveIndicator BIT
	,@RenewalDate DATE = NULL
	,@AddressID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

INSERT INTO [dbo].[Member] (
	[MemberSubscriptionTypeID]
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
	)
SELECT @MemberSubscriptionTypeID
	,@MemberCode
	,@IdentityType
	,@EmailAddress
	,@TelephoneHome
	,@TelephoneOffice
	,@TelephoneMobile
	,@Gender
	,@Ethnicity
	,@DateOfBirth
	,@ActiveIndicator
	,@RenewalDate
	,@AddressID
	,@DateInserted
	,@DateUpdated
	,@USR

-- Begin Return Select <- do not remove
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
WHERE [MemberID] = SCOPE_IDENTITY()

-- End Return Select <- do not remove
COMMIT
