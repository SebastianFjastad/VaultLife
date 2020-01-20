
CREATE PROC [dbo].[usp_MemberUpdate] @MemberID INT
	,@MemberSubscriptionTypeID INT
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

UPDATE [dbo].[Member]
SET [MemberSubscriptionTypeID] = @MemberSubscriptionTypeID
	,[MemberCode] = @MemberCode
	,[IdentityType] = @IdentityType
	,[EmailAddress] = @EmailAddress
	,[TelephoneHome] = @TelephoneHome
	,[TelephoneOffice] = @TelephoneOffice
	,[TelephoneMobile] = @TelephoneMobile
	,[Gender] = @Gender
	,[Ethnicity] = @Ethnicity
	,[DateOfBirth] = @DateOfBirth
	,[ActiveIndicator] = @ActiveIndicator
	,[RenewalDate] = @RenewalDate
	,[AddressID] = @AddressID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [MemberID] = @MemberID

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
WHERE [MemberID] = @MemberID

-- End Return Select <- do not remove
COMMIT
