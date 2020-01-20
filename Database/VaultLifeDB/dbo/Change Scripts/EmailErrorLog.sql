USE [VaultLifeApplication]
GO

/****** Object:  Table [dbo].[EmailErrorLog]    Script Date: 2014-09-29 12:41:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmailErrorLog](
	[EmailErrorID] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailID] [bigint] NOT NULL,
	[RecipientEmailAddress] [varchar](200) NOT NULL,
	[MemberID] [int] NOT NULL,
	[ErrorCode] [varchar](50) NOT NULL,
	[ErrorDescription] [text] NOT NULL,
	[SendAttempt] [int] NOT NULL,
	[DateInserted] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailErrorLog] PRIMARY KEY CLUSTERED 
(
	[EmailErrorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EmailErrorLog]  WITH CHECK ADD  CONSTRAINT [FK_Email_EmailErrorLog] FOREIGN KEY([EmailID])
REFERENCES [dbo].[Email] ([EmailID])
GO

ALTER TABLE [dbo].[EmailErrorLog] CHECK CONSTRAINT [FK_Email_EmailErrorLog]
GO

