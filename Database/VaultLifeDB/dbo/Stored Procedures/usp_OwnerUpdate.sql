
CREATE PROC [dbo].[usp_OwnerUpdate] @OwnerID INT
	,@OwnerCode VARCHAR(20)
	,@OwnerName VARCHAR(255)
	,@OwnerType VARCHAR(255)
	,@BankingDetailBank VARCHAR(255)
	,@BankingDetailAccountNumber VARCHAR(255)
	,@BankingDetailAccountType VARCHAR(255)
	,@BankingDetailBranchCode VARCHAR(255)
	,@BankingDetailBranchName VARCHAR(255)
	,@BankingDetailDefaultReference VARCHAR(255)
	,@EmailAddress VARCHAR(255)
	,@ContactPerson VARCHAR(255)
	,@TelephoneOffice VARCHAR(20) = NULL
	,@TelephoneMobile VARCHAR(20)
	,@AddressID INT
	,@DateInserted DATETIME2
	,@DateUpdated DATETIME2
	,@USR VARCHAR(200)
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

UPDATE [dbo].[Owner]
SET [OwnerCode] = @OwnerCode
	,[OwnerName] = @OwnerName
	,[OwnerType] = @OwnerType
	,[BankingDetailBank] = @BankingDetailBank
	,[BankingDetailAccountNumber] = @BankingDetailAccountNumber
	,[BankingDetailAccountType] = @BankingDetailAccountType
	,[BankingDetailBranchCode] = @BankingDetailBranchCode
	,[BankingDetailBranchName] = @BankingDetailBranchName
	,[BankingDetailDefaultReference] = @BankingDetailDefaultReference
	,[EmailAddress] = @EmailAddress
	,[ContactPerson] = @ContactPerson
	,[TelephoneOffice] = @TelephoneOffice
	,[TelephoneMobile] = @TelephoneMobile
	,[AddressID] = @AddressID
	,[DateInserted] = @DateInserted
	,[DateUpdated] = @DateUpdated
	,[USR] = @USR
WHERE [OwnerID] = @OwnerID

-- Begin Return Select <- do not remove
SELECT [OwnerID]
	,[OwnerCode]
	,[OwnerName]
	,[OwnerType]
	,[BankingDetailBank]
	,[BankingDetailAccountNumber]
	,[BankingDetailAccountType]
	,[BankingDetailBranchCode]
	,[BankingDetailBranchName]
	,[BankingDetailDefaultReference]
	,[EmailAddress]
	,[ContactPerson]
	,[TelephoneOffice]
	,[TelephoneMobile]
	,[AddressID]
	,[DateInserted]
	,[DateUpdated]
	,[USR]
FROM [dbo].[Owner]
WHERE [OwnerID] = @OwnerID

-- End Return Select <- do not remove
COMMIT
