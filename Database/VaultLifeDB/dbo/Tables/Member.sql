CREATE TABLE [dbo].[Member] (
    [MemberID]                 INT           IDENTITY (1, 1) NOT NULL,
    [MemberSubscriptionTypeID] INT           NOT NULL,
    [MemberCode]               VARCHAR (20)  NOT NULL,
    [IdentityType]             VARCHAR (20)  NOT NULL,
    [EmailAddress]             VARCHAR (255) NOT NULL,
    [TelephoneHome]            VARCHAR (20)  NULL,
    [TelephoneOffice]          VARCHAR (20)  NULL,
    [TelephoneMobile]          VARCHAR (20)  NOT NULL,
    [Gender]                   VARCHAR (1)   NOT NULL,
    [Ethnicity]                VARCHAR (255) NULL,
    [DateOfBirth]              DATE          NOT NULL,
    [ActiveIndicator]          BIT           NOT NULL,
    [RenewalDate]              DATE          NULL,
    [AddressID]                INT           NOT NULL,
    [DateInserted]             DATETIME2 (4) NOT NULL,
    [DateUpdated]              DATETIME2 (4) NOT NULL,
    [USR]                      VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Member_MemberID] PRIMARY KEY CLUSTERED ([MemberID] ASC),
    CONSTRAINT [FK_Member_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]),
    CONSTRAINT [FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID] FOREIGN KEY ([MemberSubscriptionTypeID]) REFERENCES [dbo].[MemberSubscriptionType] ([MemberSubscriptionTypeID])
);

