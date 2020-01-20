CREATE TABLE [dbo].[NextGame] (
    [GameID]       INT           NOT NULL,
    [NextGameID]   INT           NOT NULL,
    [DateInserted] DATETIME2 (4) NOT NULL,
    [DateUpdated]  DATETIME2 (4) NOT NULL,
    [USR]          VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_NextGame_NextGameID] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_NextGame_Game_GameID] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);

