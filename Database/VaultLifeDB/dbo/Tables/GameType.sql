CREATE TABLE [dbo].[GameType] (
    [GameTypeID]          INT           IDENTITY (1, 1) NOT NULL,
    [GameTypeCode]        VARCHAR (20)  NOT NULL,
    [GameTypeName]        VARCHAR (255) NOT NULL,
    [GameTypeDescription] VARCHAR (255) NOT NULL,
    [DateInserted]        DATETIME2 (4) NOT NULL,
    [DateUpdated]         DATETIME2 (4) NOT NULL,
    [USR]                 VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GameType_GameTypeID] PRIMARY KEY CLUSTERED ([GameTypeID] ASC)
);

