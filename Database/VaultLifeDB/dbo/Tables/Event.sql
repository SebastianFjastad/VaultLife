CREATE TABLE [dbo].[Event] (
    [EventID]          INT           IDENTITY (1, 1) NOT NULL,
    [EventCode]        VARCHAR (20)  NOT NULL,
    [EventName]        VARCHAR (255) NOT NULL,
    [EventDescription] VARCHAR (255) NOT NULL,
    [EventDate]        DATE          NOT NULL,
    [DateInserted]     DATETIME2 (4) NOT NULL,
    [DateUpdated]      DATETIME2 (4) NOT NULL,
    [USR]              VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Event_EventID] PRIMARY KEY CLUSTERED ([EventID] ASC)
);

