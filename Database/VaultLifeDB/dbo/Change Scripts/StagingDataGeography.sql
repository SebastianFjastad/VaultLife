USE [VaultLifeApplication]
GO
/****** Object:  Table [dbo].[Staging]    Script Date: 2014-09-12 03:25:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staging](
	[Country] [varchar](max) NULL,
	[State] [varchar](max) NULL,
	[City] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Sydney')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Albury')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Armidale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Bathurst')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Blue Mountains')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Broken Hill')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Campbelltown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Cessnock')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Dubbo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Goulburn')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Grafton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Lithgow')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Newcastle')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Orange')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Queanbeyan')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Tamworth')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Wagga Wagga')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'New South Wales', N'Wollongong')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Northern Territory', N'Darwin')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Northern Territory', N'Palmerston')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Brisbane')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Bundaberg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Cairns')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Caloundra')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Gladstone')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Gold Coast Gympie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Hervey Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Mackay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Maryborough')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Mount Isa')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Rockhampton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Sunshine Coast')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Toowoomba')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Queensland', N'Townsville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Adelaide')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Mount Barker')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Mount Gambier')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Murray Bridge')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Port Adelaide')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Port Augusta')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Port Pirie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Port Lincoln')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Victor Harbor')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'South Australia', N'Whyalla')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Tasmania', N'Hobart')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Tasmania', N'Burnie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Tasmania', N'Devonport')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Tasmania', N'Launceston')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Melbourne')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Ararat')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Bairnsdale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Benalla')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Ballarat')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Bendigo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Geelong')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Hamilton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Horsham')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Moe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Morwell')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Mildura')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Sale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Shepparton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Swan Hill')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Traralgon')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Wangaratta')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Warrnambool')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Victoria', N'Wodonga')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Perth')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Albany')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Bunbury')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Busselton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Fremantle')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Geraldton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Joondalup')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Kalgoorlie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Mandurah')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Karratha')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Australia', N'Western Australia', N'Rockingham')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Abuja')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Benin City')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Calabar')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Enugu')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Ibadan')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Ilorin')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Kaduna')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Kano')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Lagos')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Maiduguri')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Onitsha')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Port Harcourt')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Warri')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'Nigeria', N'Nigeria', N'Zaria')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Aberdeen')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Addo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Adelaide')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Adendorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Alexandria')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Alice')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Alicedale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Aliwal North')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Balfour')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Barkly East')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Bathurst')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Bedford')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Berlin')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Bethelsdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Burgersdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Butterworth')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Cala')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Cathcart')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Clarkebury')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Cofimvaba')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Colchester')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Cookhouse')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Cradock')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Despatch')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Dordrecht')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'East London')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Elliot')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Engcobo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Flagstaff')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Fort Beaufort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Gonubie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Graaff-Reinet')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Grahamstown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Hankey')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Hofmeyr')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Humansdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Jansenville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Jeffreys Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'King William''s Town')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Kirkwood')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Lady Frere')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Lady Grey')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Maclear')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Madadeni')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Matatiele')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Middelburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Molteno')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Mount Fletcher')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Mthatha')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Patensie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Paterson')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Port Alfred')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Port Elizabeth')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Queenstown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Somerset East')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'St Francis Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Steynsburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Stutterheim')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Tarkastad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Uitenhage')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Eastern Cape', N'Whittlesea')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Allanridge')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Arlington')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Bethlehem')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Bethulie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Bloemfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Boshof')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Bothaville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Botshabelo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Brandfort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Bultfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Clarens')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Clocolan')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Cornelia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Dealesville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Deneysville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Dewetsdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Edenburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Edenville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Excelsior')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Fauresmith')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Ficksburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Fouriesburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Frankfort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Harrismith')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Heilbron')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Hennenman')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Hertzogville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Hobhouse')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Hoopstad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Jacobsdal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Jagersfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Kestell')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Koffiefontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Koppies')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Kroonstad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Ladybrand')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Lindley')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Luckhoff')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Marquard')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Parys')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Paul Roux')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Petrus Steyn')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Petrusburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Philippolis')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Phuthaditjhaba')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Reddersburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Reitz')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Rosendal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Rouxville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Sasolburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Senekal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Smithfield')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Springfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Steynsrus')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Thaba Nchu')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Theunissen')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Trompsburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Tweeling')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Tweespruit')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Ventersburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Verkeerdevlei')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Viljoenskroon')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Villiers')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Vrede')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Vredefort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Warden')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Welkom')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Wepener')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Wesselsbron')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Winburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Free State', N'Zastron')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Alberton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Alexandra')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Atteridgeville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Bapsfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Benoni')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Boipatong')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Boksburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Brakpan')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Bronkhorstspruit')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Carletonville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Centurion')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Cullinan')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Daveyton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Devon')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Duduza')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Edenvale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Germiston')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Hammanskraal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Heidelberg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Irene')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Isando')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Johannesburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Katlehong')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Kempton Park')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Kromdraai')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Krugersdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'KwaThema')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Lenasia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Mamelodi')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Meyerton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Midrand')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Nigel')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Pretoria')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Randburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Randfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Roodepoort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Rooihuiskraal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Sandton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Sebokeng')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Sharpeville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Soshanguve')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Soweto')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Springs')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Tembisa')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Tsakane')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Vanderbijlpark')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Vereeniging')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Gauteng', N'Westonaria')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Amanzimtoti')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Anerley')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ballito')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Bergville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Bulwer')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Cato Ridge')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Colenso')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Dannhauser')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Darnall')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Doonside')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Dundee')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Durban')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Empangeni')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Eshowe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Estcourt')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Glencoe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Greytown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Hibberdene')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Hillcrest')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Hluhluwe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Howick')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ifafa Beach')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Illovo Beach')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Impendle')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Inanda')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ingwavuma')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Isipingo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Kloof')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Kokstad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'KwaMashu')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ladysmith')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Mahlabatini')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Margate')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Melmoth')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Merrivale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Mkuze')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Mount Edgecombe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Mtubatuba')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Mtunzini')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'New Germany')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Newcastle')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Nongoma')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Paulpietersburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Pennington')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Pietermaritzburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Pinetown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Pomeroy')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Pongola')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Port Edward')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Port Shepstone')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Queensburgh')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Richards Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Scottburgh')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Shelly Beach')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Tongaat')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Tugela Ferry')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ubombo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Ulundi')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umbogintwini')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umhlanga Rocks')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umkomaas')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umlazi')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umtentweni')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umzinto')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Umzumbe')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Utrecht')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Uvongo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Verulam')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Virginia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Vryheid')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Wartburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Wasbank')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Westville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'KwaZulu-Natal', N'Winterton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Bela-Bela')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Duiwelskloof')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Giyani')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Groblersdal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Hoedspruit')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Lephalale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Louis Trichardt')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Marble Hall')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Modimolle')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Mokopane')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Musina')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Naboomspruit')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Phalaborwa')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Polokwane')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Thabazimbi')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Thohoyandou')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Limpopo', N'Vaalwater')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Acornhoek')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Amersfoort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Amsterdam')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Badplaas')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Balfour')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Barberton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Belfast')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Bethal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Bosbokrand')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Breyten')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Carolina')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Delmas')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Ermelo')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Graskop')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Greylingstad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Hazyview')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Komatipoort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Kriel')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'KwaMhlanga')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Lydenburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Machadodorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Malelane')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Middelburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Nelspruit')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Piet Retief')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Sabie')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Secunda')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Standerton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Volksrust')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Wakkerstroom')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'White River')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Mpumalanga', N'Witbank')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Bloemhof')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Brits')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Christiana')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Coligny')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Delareyville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Ganyesa')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Ga-Rankuwa')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Lichtenburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Mafikeng')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Potchefstroom')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Rustenburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Schweizer-Reneke')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North west', N'Stilfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Vryburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Wolmaransstad')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West', N'Zeerust')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West Province', N'Hartbeespoort')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West Province', N'Klerksdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'North West Province', N'Mmabatho')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Alexander Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Barkly West')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Britstown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Calvinia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Carnarvon')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Colesberg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Danielskuil')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'De Aar')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Delportshoop')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Douglas')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Fraserburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Groblershoop')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Hopetown')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Kimberley')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Prieska')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Richmond')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Springbok')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Strydenburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Upington')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Victoria West')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Northern Cape', N'Warrenton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Albertinia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Ashton')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Atlantis')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Barrydale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Beaufort West')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Bellville')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Bitterfontein')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Bonnievale')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Bredasdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Caledon')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Calitzdorp')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Cape Town')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Ceres')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Citrusdal')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Clanwilliam')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Claremont')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Constantia')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Darling')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'De Doorns')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Elim')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Franschhoek')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'George')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Graafwater')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Grabouw')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Heidelberg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Hermanus')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Hopefield')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Khayelitsha')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Knysna')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Laingsburg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Mossel Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Paarl')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Piketberg')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Plettenberg Bay')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Somerset West')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Stellenbosch')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Strand')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Swellendam')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Tulbagh')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Wellington')
GO
INSERT [dbo].[Staging] ([Country], [State], [City]) VALUES (N'South Africa', N'Western Cape', N'Worcester')
GO
