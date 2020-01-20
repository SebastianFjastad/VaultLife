USE [VaultLifeApplication]
GO

/****** Object:  Table [dbo].[TrackingTransaction]    Script Date: 2014-09-04 11:30:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TrackingTransaction](
	[TrackingTransactionID] [int] IDENTITY(1,1) NOT NULL,
	[MemberInGameID] [int] NOT NULL,
	[TimeInitiated] [datetime2](4) NULL,
	[TimePaused] [datetime2](4) NULL,
	[TimeResumed] [datetime2](4) NULL,
	[TimeCompleted] [datetime2](4) NULL,
	[DurationRemaining] [float] NULL,
	[DateInserted] [datetime2](4) NULL,
	[DateModified] [datetime2](4) NULL,
 CONSTRAINT [PK_TrackingTransaction] PRIMARY KEY CLUSTERED 
(
	[TrackingTransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TrackingTransaction]  WITH CHECK ADD  CONSTRAINT [FK_TrackingTransaction_MemberInGame] FOREIGN KEY([MemberInGameID])
REFERENCES [dbo].[MemberInGame] ([MemberInGameID])
GO

ALTER TABLE [dbo].[TrackingTransaction] CHECK CONSTRAINT [FK_TrackingTransaction_MemberInGame]
GO


