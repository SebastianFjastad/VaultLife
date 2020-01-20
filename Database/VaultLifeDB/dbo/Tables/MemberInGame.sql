CREATE TABLE [dbo].[MemberInGame] (
    [GameID]       INT           NOT NULL,
    [MemberID]     INT           NOT NULL,
    [DateInserted] DATETIME2 (4) NOT NULL,
    [DateUpdated]  DATETIME2 (4) NOT NULL,
    [USR]          VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberInGame] PRIMARY KEY CLUSTERED ([GameID] ASC, [MemberID] ASC),
    CONSTRAINT [FK_MemberInGame_Game_GameID] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID]),
    CONSTRAINT [FK_MemberInGame_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID])
);

