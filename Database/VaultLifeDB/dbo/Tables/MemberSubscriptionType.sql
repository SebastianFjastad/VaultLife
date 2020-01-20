CREATE TABLE [dbo].[MemberSubscriptionType] (
    [MemberSubscriptionTypeID]          INT           IDENTITY (1, 1) NOT NULL,
    [MemberSubscriptionTypeCode]        VARCHAR (20)  NOT NULL,
    [MemberSubscriptionTypeDescription] VARCHAR (255) NOT NULL,
    [DateInserted]                      DATETIME2 (4) NOT NULL,
    [DateUpdated]                       DATETIME2 (4) NOT NULL,
    [USR]                               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberSubscriptionType_MemberSubscriptionTypeID] PRIMARY KEY CLUSTERED ([MemberSubscriptionTypeID] ASC)
);

