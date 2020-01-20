CREATE TABLE [dbo].[Address] (
    [AddressID]       INT           IDENTITY (1, 1) NOT NULL,
    [AddressType]     VARCHAR (10)  NULL,
    [AddressLine1]    VARCHAR (255) NULL,
    [AddressLine2]    VARCHAR (255) NULL,
    [AddressLine3]    VARCHAR (255) NULL,
    [Country]         VARCHAR (255) NULL,
    [StateOrProvince] VARCHAR (255) NULL,
    [ZipOrPostalCode] VARCHAR (20)  NULL,
    [DateInserted]    DATETIME2 (4) NOT NULL,
    [DateUpdated]     DATETIME2 (4) NOT NULL,
    [USR]             VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Address_AddressID] PRIMARY KEY CLUSTERED ([AddressID] ASC)
);

