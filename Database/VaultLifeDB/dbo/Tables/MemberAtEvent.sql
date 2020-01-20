CREATE TABLE [dbo].[MemberAtEvent] (
    [MemberID]     INT           NOT NULL,
    [EventID]      INT           NOT NULL,
    [DateInserted] DATETIME2 (4) NOT NULL,
    [DateUpdated]  DATETIME2 (4) NOT NULL,
    [USR]          VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_MemberAtEvent_MemberAtEventID] PRIMARY KEY CLUSTERED ([EventID] ASC, [MemberID] ASC),
    CONSTRAINT [FK_MemberAtEvent_Event_EventID] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([EventID]),
    CONSTRAINT [FK_MemberAtEvent_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID])
);

