CREATE TABLE [dbo].[Owner] (
    [OwnerID]                       INT           IDENTITY (1, 1) NOT NULL,
    [OwnerCode]                     VARCHAR (20)  NOT NULL,
    [OwnerName]                     VARCHAR (255) NOT NULL,
    [OwnerType]                     VARCHAR (255) NOT NULL,
    [BankingDetailBank]             VARCHAR (255) NOT NULL,
    [BankingDetailAccountNumber]    VARCHAR (255) NOT NULL,
    [BankingDetailAccountType]      VARCHAR (255) NOT NULL,
    [BankingDetailBranchCode]       VARCHAR (255) NOT NULL,
    [BankingDetailBranchName]       VARCHAR (255) NOT NULL,
    [BankingDetailDefaultReference] VARCHAR (255) NOT NULL,
    [EmailAddress]                  VARCHAR (255) NOT NULL,
    [ContactPerson]                 VARCHAR (255) NOT NULL,
    [TelephoneOffice]               VARCHAR (20)  NULL,
    [TelephoneMobile]               VARCHAR (20)  NOT NULL,
    [AddressID]                     INT           NOT NULL,
    [DateInserted]                  DATETIME2 (4) NOT NULL,
    [DateUpdated]                   DATETIME2 (4) NOT NULL,
    [USR]                           VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Owner_OwnerID] PRIMARY KEY CLUSTERED ([OwnerID] ASC),
    CONSTRAINT [FK_Owner_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID])
);

