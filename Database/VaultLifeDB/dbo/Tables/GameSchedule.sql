CREATE TABLE [dbo].[GameSchedule] (
    [GameScheduleID]    INT           NOT NULL,
    [GameScheduleCode]  VARCHAR (20)  NOT NULL,
    [ScheduledDateTime] DATETIME      NOT NULL,
    [GameID]            INT           NOT NULL,
    [SequenceNumber]    INT           NOT NULL,
    [DateInserted]      DATETIME2 (4) NOT NULL,
    [DateUpdated]       DATETIME2 (4) NOT NULL,
    [USR]               VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GameSchedule] PRIMARY KEY CLUSTERED ([GameScheduleID] ASC),
    CONSTRAINT [FK_GameSchedule_Game_GameID] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);

