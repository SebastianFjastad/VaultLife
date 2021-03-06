USE [VaultLifeApplication]
GO
/****** Object:  Table [dbo].[EmailConfig]    Script Date: 2014-09-29 12:42:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailConfig](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigDescription] [varchar](50) NULL,
	[Status] [char](1) NOT NULL,
	[DateInserted] [datetime] NOT NULL,
	[DefaultFromName] [varchar](100) NULL,
	[DefaultFromAddress] [varchar](150) NULL,
	[smtpUsername] [varchar](100) NULL,
	[smtpPassword] [varchar](100) NULL,
	[smtpServerURI] [varchar](100) NULL,
	[smtpEnableSSL] [bit] NULL,
	[DefaultMaxRetries] [int] NULL,
	[smtpServerPort] [varchar](10) NULL,
 CONSTRAINT [PK_EmailConfig] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[EmailConfig] ON 

INSERT [dbo].[EmailConfig] ([ConfigID], [ConfigDescription], [Status], [DateInserted], [DefaultFromName], [DefaultFromAddress], [smtpUsername], [smtpPassword], [smtpServerURI], [smtpEnableSSL], [DefaultMaxRetries], [smtpServerPort]) VALUES (2, N'Default', N'A', CAST(0x0000A3B500CE8B00 AS DateTime), N'VaultLife Admin', N'admin@vaultlife.com', N'vaultlife201408@gmail.com', N'v@ultl1f3', N'smtp.gmail.com', 1, 4, N'587')
SET IDENTITY_INSERT [dbo].[EmailConfig] OFF
