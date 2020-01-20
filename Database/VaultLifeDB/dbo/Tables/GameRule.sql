CREATE TABLE [dbo].[GameRule] (
    [GameRuleID]      INT           IDENTITY (1, 1) NOT NULL,
    [GameRuleCode]    VARCHAR (20)  NOT NULL,
    [GameID]          INT           NOT NULL,
    [FilterCriteria]  VARCHAR (255) NOT NULL,
    [Schedule]        INT           NOT NULL,
    [ChainGameRuleID] INT           NOT NULL,
    [GameRuleDetail]  VARCHAR (255) NOT NULL,
    [DateInserted]    DATETIME2 (4) NOT NULL,
    [DateUpdated]     DATETIME2 (4) NOT NULL,
    [USR]             VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GameRule] PRIMARY KEY CLUSTERED ([GameRuleID] ASC),
    CONSTRAINT [FK_GameRule_Game_GameID] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);

