CREATE TABLE [dbo].[GameTemplate] (
    [GameTemplateID]   INT           NOT NULL,
    [GameTemplateCode] VARCHAR (20)  NOT NULL,
    [GameTypeID]       INT           NOT NULL,
    [GameRuleID]       INT           NOT NULL,
    [DateInserted]     DATETIME2 (4) NOT NULL,
    [DateUpdated]      DATETIME2 (4) NOT NULL,
    [USR]              VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GameTemplate] PRIMARY KEY CLUSTERED ([GameTemplateID] ASC),
    CONSTRAINT [FK_GameTemplate_GameRule_GameRuleID] FOREIGN KEY ([GameRuleID]) REFERENCES [dbo].[GameRule] ([GameRuleID]),
    CONSTRAINT [FK_GameTemplate_GameType_GameTypeID] FOREIGN KEY ([GameTypeID]) REFERENCES [dbo].[GameType] ([GameTypeID])
);

