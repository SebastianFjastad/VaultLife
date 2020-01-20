CREATE TABLE [dbo].[WebsiteInteraction] (
    [WebsiteInteractionID] INT           NOT NULL,
    [MemberID]             INT           NOT NULL,
    [InteractionTypeID]    INT           NOT NULL,
    [DateInserted]         DATETIME2 (4) NOT NULL,
    [DateUpdated]          DATETIME2 (4) NOT NULL,
    [USR]                  VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_WebsiteInteraction] PRIMARY KEY CLUSTERED ([WebsiteInteractionID] ASC),
    CONSTRAINT [FK_WebsiteInteraction_InteractionType_InteractionTypeID] FOREIGN KEY ([InteractionTypeID]) REFERENCES [dbo].[InteractionType] ([InteractionTypeID]),
    CONSTRAINT [FK_WebsiteInteraction_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID])
);

