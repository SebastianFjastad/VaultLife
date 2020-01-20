CREATE TABLE [dbo].[Game] (
    [GameID]          INT           IDENTITY (1, 1) NOT NULL,
    [GameCode]        VARCHAR (20)  NOT NULL,
    [GameTypeID]      INT           NOT NULL,
    [GameName]        VARCHAR (255) NOT NULL,
    [GameDescription] VARCHAR (255) NOT NULL,
    [DateInserted]    DATETIME2 (4) NOT NULL,
    [DateUpdated]     DATETIME2 (4) NOT NULL,
    [USR]             VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Game_GameID] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Game_GameType_GameTypeID] FOREIGN KEY ([GameTypeID]) REFERENCES [dbo].[GameType] ([GameTypeID])
);

