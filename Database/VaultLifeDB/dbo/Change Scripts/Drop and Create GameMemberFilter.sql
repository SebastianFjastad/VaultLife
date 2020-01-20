USE [VaultLifeApplication]
GO

/****** Object:  Table [dbo].[GameMemberFilter]    Script Date: 2014/08/29 04:24:14 PM ******/
DROP TABLE [dbo].[GameMemberFilter]
GO

/****** Object:  Table [dbo].[GameMemberFilter]    Script Date: 2014/08/29 04:24:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GameMemberFilter](
	[GameMemberFilterID] [int] IDENTITY(1,1) NOT NULL,
	[GameID] [int] NOT NULL,
	[CountryID] [int] NULL,
	[StateID] [int] NULL,
	[CityID] [int] NULL,
	[AgeBandID] [int] NULL,
	[GenderID] [int] NULL,
	[Territory] [int] NULL,
	[MemberSubscriptionTypeID] [int] NULL,
 CONSTRAINT [PK_GameMemberFilter] PRIMARY KEY CLUSTERED 
(
	[GameMemberFilterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


