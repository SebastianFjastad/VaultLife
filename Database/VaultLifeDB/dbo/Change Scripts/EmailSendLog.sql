USE [VaultLifeApplication]
GO

/****** Object:  Table [dbo].[EmailSendLog]    Script Date: 2014-09-29 12:41:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmailSendLog](
	[SendLogID] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailID] [bigint] NOT NULL,
	[SendStatus] [varchar](10) NOT NULL,
	[SendDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailSendLog] PRIMARY KEY CLUSTERED 
(
	[SendLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EmailSendLog]  WITH CHECK ADD  CONSTRAINT [FK_EmailSendLog_Email] FOREIGN KEY([EmailID])
REFERENCES [dbo].[Email] ([EmailID])
GO

ALTER TABLE [dbo].[EmailSendLog] CHECK CONSTRAINT [FK_EmailSendLog_Email]
GO

