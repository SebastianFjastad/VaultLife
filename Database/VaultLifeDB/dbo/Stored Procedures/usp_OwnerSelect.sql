
CREATE PROC [dbo].[usp_OwnerSelect] @OwnerID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

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
WHERE (
		[OwnerID] = @OwnerID
		OR @OwnerID IS NULL
		)

COMMIT
