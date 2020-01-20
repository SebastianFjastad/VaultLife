USE [VaultLifeApplication]
GO

/****** Object:  Table [dbo].[Email]    Script Date: 2014-09-29 12:41:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Email](
	[EmailID] [bigint] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[Priority] [int] NOT NULL,
	[SendAfter] [datetime] NOT NULL,
	[Attempts] [int] NOT NULL,
	[FailedPermanently] [bit] NOT NULL,
	[DateInserted] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[USR] [varchar](200) NOT NULL,
	[RecipientEmailAddress] [varchar](200) NOT NULL,
	[RecipientName] [varchar](100) NOT NULL,
	[EmailSubject] [varchar](128) NOT NULL,
	[EmailBodyText] [text] NOT NULL,
	[FromAddress] [varchar](200) NOT NULL,
	[FromName] [varchar](100) NOT NULL,
	[MemberID] [int] NOT NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

