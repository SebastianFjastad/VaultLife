CREATE TABLE [dbo].[InteractionType] (
    [InteractionTypeID]          INT           NOT NULL,
    [InteractionTypeCode]        VARCHAR (20)  NOT NULL,
    [InteractionTypeName]        VARCHAR (255) NOT NULL,
    [InteractionTypeDescription] VARCHAR (255) NOT NULL,
    [DateInserted]               DATETIME2 (4) NOT NULL,
    [DateUpdated]                DATETIME2 (4) NOT NULL,
    [USR]                        VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_InteractionType_InteractionTypeID] PRIMARY KEY CLUSTERED ([InteractionTypeID] ASC)
);

