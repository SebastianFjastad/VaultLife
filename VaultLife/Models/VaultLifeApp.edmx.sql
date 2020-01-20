
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2014 17:06:24
-- Generated from EDMX file: C:\IT\projects\VaultLife\Vaultlife\Models\VaultLifeApp.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VaultLifeApplication];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CountryCity_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CountryCity] DROP CONSTRAINT [FK_CountryCity_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryCity_CountryState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CountryCity] DROP CONSTRAINT [FK_CountryCity_CountryState];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryState_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CountryState] DROP CONSTRAINT [FK_CountryState_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_Currency_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Currency] DROP CONSTRAINT [FK_Currency_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_DisplayItem_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DisplayItem] DROP CONSTRAINT [FK_DisplayItem_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_DisplayItem_Product_ProductID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DisplayItem] DROP CONSTRAINT [FK_DisplayItem_Product_ProductID];
GO
IF OBJECT_ID(N'[dbo].[FK_Game_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Game] DROP CONSTRAINT [FK_Game_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_Game_Game1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Game] DROP CONSTRAINT [FK_Game_Game1];
GO
IF OBJECT_ID(N'[dbo].[FK_GameInteraction_GameSchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameInteraction] DROP CONSTRAINT [FK_GameInteraction_GameSchedule];
GO
IF OBJECT_ID(N'[dbo].[FK_GameInteration_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameInteraction] DROP CONSTRAINT [FK_GameInteration_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameRule_Game_GameID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameRule] DROP CONSTRAINT [FK_GameRule_Game_GameID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameRule_GameTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameRule] DROP CONSTRAINT [FK_GameRule_GameTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_GameSchedule_Game_GameID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameSchedule] DROP CONSTRAINT [FK_GameSchedule_Game_GameID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTemplate_GameType_GameTypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameTemplate] DROP CONSTRAINT [FK_GameTemplate_GameType_GameTypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTypeOwned_Contract_ContractID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameTypeOwned] DROP CONSTRAINT [FK_GameTypeOwned_Contract_ContractID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTypeOwned_GameType_GameTypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameTypeOwned] DROP CONSTRAINT [FK_GameTypeOwned_GameType_GameTypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTypeOwned_Owner_OwnerID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameTypeOwned] DROP CONSTRAINT [FK_GameTypeOwned_Owner_OwnerID];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageItem_Language]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LanguageItem] DROP CONSTRAINT [FK_LanguageItem_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_CountryState]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_CountryState];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Member] DROP CONSTRAINT [FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberAcquisitionCampaign_Contract_ContractID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberAcquisitionCampaign] DROP CONSTRAINT [FK_MemberAcquisitionCampaign_Contract_ContractID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberAcquisitionCampaign_Owner_OwnerID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberAcquisitionCampaign] DROP CONSTRAINT [FK_MemberAcquisitionCampaign_Owner_OwnerID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberAtEvent_Event_EventID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberAtEvent] DROP CONSTRAINT [FK_MemberAtEvent_Event_EventID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberAtEvent_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberAtEvent] DROP CONSTRAINT [FK_MemberAtEvent_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberInGame_Game_GameID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberInGame] DROP CONSTRAINT [FK_MemberInGame_Game_GameID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberInGame_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberInGame] DROP CONSTRAINT [FK_MemberInGame_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberInterest_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberInterest] DROP CONSTRAINT [FK_MemberInterest_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberInterest_ProductCategory_ProductCategoryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberInterest] DROP CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberOwned_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberOwned] DROP CONSTRAINT [FK_MemberOwned_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberOwned] DROP CONSTRAINT [FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberOwned_Owner_OwnerID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberOwned] DROP CONSTRAINT [FK_MemberOwned_Owner_OwnerID];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberOwned_Territory_TerritoryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberOwned] DROP CONSTRAINT [FK_MemberOwned_Territory_TerritoryID];
GO
IF OBJECT_ID(N'[dbo].[FK_NextGame_Game_GameID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NextGame] DROP CONSTRAINT [FK_NextGame_Game_GameID];
GO
IF OBJECT_ID(N'[dbo].[FK_Owner_Address_AddressID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Owner] DROP CONSTRAINT [FK_Owner_Address_AddressID];
GO
IF OBJECT_ID(N'[dbo].[FK_Owner_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Owner] DROP CONSTRAINT [FK_Owner_Currency];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTransaction_MemberInGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentTransaction] DROP CONSTRAINT [FK_PaymentTransaction_MemberInGame];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Contract_ContractID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Contract_ContractID];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Owner_OwnerID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Owner_OwnerID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCategory_ProductCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCategory] DROP CONSTRAINT [FK_ProductCategory_ProductCategory1];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInCategory_Product_ProductID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInCategory] DROP CONSTRAINT [FK_ProductInCategory_Product_ProductID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInCategory_ProductCategory_ProductCategoryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInCategory] DROP CONSTRAINT [FK_ProductInCategory_ProductCategory_ProductCategoryID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInGame_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInGame] DROP CONSTRAINT [FK_ProductInGame_Currency];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInGame_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInGame] DROP CONSTRAINT [FK_ProductInGame_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInGame_Product_ProductID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInGame] DROP CONSTRAINT [FK_ProductInGame_Product_ProductID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInWatchList_GameID_GameID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInWatchList] DROP CONSTRAINT [FK_ProductInWatchList_GameID_GameID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInWatchList_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductInWatchList] DROP CONSTRAINT [FK_ProductInWatchList_Member_MemberID];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductLocation_ProductInGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductLocation] DROP CONSTRAINT [FK_ProductLocation_ProductInGame];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPlayed_MemberInGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPlayed] DROP CONSTRAINT [FK_ProductPlayed_MemberInGame];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPlayed_ProductInGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPlayed] DROP CONSTRAINT [FK_ProductPlayed_ProductInGame];
GO
IF OBJECT_ID(N'[dbo].[FK_SerialNumber_ProductLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SerialNumber] DROP CONSTRAINT [FK_SerialNumber_ProductLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_Territory_Contract_ContractID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Territory] DROP CONSTRAINT [FK_Territory_Contract_ContractID];
GO
IF OBJECT_ID(N'[dbo].[FK_Territory_Owner_OwnerID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Territory] DROP CONSTRAINT [FK_Territory_Owner_OwnerID];
GO
IF OBJECT_ID(N'[dbo].[FK_TerritoryDefinition_Territory_TerritoryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TerritoryDefinition] DROP CONSTRAINT [FK_TerritoryDefinition_Territory_TerritoryID];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackingTransaction_MemberInGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackingTransaction] DROP CONSTRAINT [FK_TrackingTransaction_MemberInGame];
GO
IF OBJECT_ID(N'[dbo].[FK_Voucher_ProductLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Voucher] DROP CONSTRAINT [FK_Voucher_ProductLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_WebsiteInteraction_InteractionType_InteractionTypeID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WebsiteInteraction] DROP CONSTRAINT [FK_WebsiteInteraction_InteractionType_InteractionTypeID];
GO
IF OBJECT_ID(N'[dbo].[FK_WebsiteInteraction_Member_MemberID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WebsiteInteraction] DROP CONSTRAINT [FK_WebsiteInteraction_Member_MemberID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Contract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contract];
GO
IF OBJECT_ID(N'[dbo].[Country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Country];
GO
IF OBJECT_ID(N'[dbo].[CountryCity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountryCity];
GO
IF OBJECT_ID(N'[dbo].[CountryState]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountryState];
GO
IF OBJECT_ID(N'[dbo].[Currency]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Currency];
GO
IF OBJECT_ID(N'[dbo].[DisplayItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DisplayItem];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Game]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Game];
GO
IF OBJECT_ID(N'[dbo].[GameInteraction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameInteraction];
GO
IF OBJECT_ID(N'[dbo].[GameMemberFilter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameMemberFilter];
GO
IF OBJECT_ID(N'[dbo].[GameRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameRule];
GO
IF OBJECT_ID(N'[dbo].[GameSchedule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameSchedule];
GO
IF OBJECT_ID(N'[dbo].[GameTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameTemplate];
GO
IF OBJECT_ID(N'[dbo].[GameType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameType];
GO
IF OBJECT_ID(N'[dbo].[GameTypeOwned]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameTypeOwned];
GO
IF OBJECT_ID(N'[dbo].[InteractionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InteractionType];
GO
IF OBJECT_ID(N'[dbo].[Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Language];
GO
IF OBJECT_ID(N'[dbo].[LanguageItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LanguageItem];
GO
IF OBJECT_ID(N'[dbo].[Member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Member];
GO
IF OBJECT_ID(N'[dbo].[MemberAcquisitionCampaign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberAcquisitionCampaign];
GO
IF OBJECT_ID(N'[dbo].[MemberAtEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberAtEvent];
GO
IF OBJECT_ID(N'[dbo].[MemberInGame]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberInGame];
GO
IF OBJECT_ID(N'[dbo].[MemberInterest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberInterest];
GO
IF OBJECT_ID(N'[dbo].[MemberOwned]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberOwned];
GO
IF OBJECT_ID(N'[dbo].[MemberSubscriptionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberSubscriptionType];
GO
IF OBJECT_ID(N'[dbo].[NextGame]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NextGame];
GO
IF OBJECT_ID(N'[dbo].[Owner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Owner];
GO
IF OBJECT_ID(N'[dbo].[PaymentConfiguration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentConfiguration];
GO
IF OBJECT_ID(N'[dbo].[PaymentTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentTransaction];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[ProductInCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductInCategory];
GO
IF OBJECT_ID(N'[dbo].[ProductInGame]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductInGame];
GO
IF OBJECT_ID(N'[dbo].[ProductInWatchList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductInWatchList];
GO
IF OBJECT_ID(N'[dbo].[ProductLocation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductLocation];
GO
IF OBJECT_ID(N'[dbo].[ProductPlayed]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPlayed];
GO
IF OBJECT_ID(N'[dbo].[ProductProvider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductProvider];
GO
IF OBJECT_ID(N'[dbo].[SerialNumber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SerialNumber];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Territory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Territory];
GO
IF OBJECT_ID(N'[dbo].[TerritoryDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TerritoryDefinition];
GO
IF OBJECT_ID(N'[dbo].[TrackingTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackingTransaction];
GO
IF OBJECT_ID(N'[dbo].[Voucher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Voucher];
GO
IF OBJECT_ID(N'[dbo].[WebsiteInteraction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WebsiteInteraction];
GO
IF OBJECT_ID(N'[VaultLifeApplicationModelStoreContainer].[ResponseCatalog]', 'U') IS NOT NULL
    DROP TABLE [VaultLifeApplicationModelStoreContainer].[ResponseCatalog];
GO
IF OBJECT_ID(N'[VaultLifeApplicationModelStoreContainer].[PlayableGamesByMember]', 'U') IS NOT NULL
    DROP TABLE [VaultLifeApplicationModelStoreContainer].[PlayableGamesByMember];
GO
IF OBJECT_ID(N'[VaultLifeApplicationModelStoreContainer].[ProductInfo]', 'U') IS NOT NULL
    DROP TABLE [VaultLifeApplicationModelStoreContainer].[ProductInfo];
GO
IF OBJECT_ID(N'[VaultLifeApplicationModelStoreContainer].[ShowWinners]', 'U') IS NOT NULL
    DROP TABLE [VaultLifeApplicationModelStoreContainer].[ShowWinners];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [AddressID] int IDENTITY(1,1) NOT NULL,
    [AddressType] varchar(10)  NULL,
    [AddressLine1] varchar(255)  NULL,
    [AddressLine2] varchar(255)  NULL,
    [AddressLine3] varchar(255)  NULL,
    [Country] varchar(255)  NULL,
    [StateOrProvince] varchar(255)  NULL,
    [ZipOrPostalCode] varchar(20)  NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [CityName] varchar(300)  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Contracts'
CREATE TABLE [dbo].[Contracts] (
    [ContractID] int  NOT NULL,
    [ContractCode] varchar(20)  NOT NULL,
    [ContractDetail] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'CountryStates'
CREATE TABLE [dbo].[CountryStates] (
    [StateID] int  NOT NULL,
    [CountryID] int  NULL,
    [StateName] nvarchar(300)  NULL
);
GO

-- Creating table 'DisplayItems'
CREATE TABLE [dbo].[DisplayItems] (
    [DisplayItemID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [DisplayItemType] varchar(50)  NULL,
    [DisplayItemURL] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [DeleteOnSave] bit  NOT NULL,
    [LanguageID] int  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [EventID] int IDENTITY(1,1) NOT NULL,
    [EventCode] varchar(20)  NOT NULL,
    [EventName] varchar(255)  NOT NULL,
    [EventDescription] varchar(255)  NOT NULL,
    [EventDate] datetime  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameID] int IDENTITY(1,1) NOT NULL,
    [GameCode] varchar(20)  NOT NULL,
    [GameTypeID] int  NOT NULL,
    [GameName] varchar(255)  NOT NULL,
    [GameDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [GameCreationStatus] varchar(50)  NULL,
    [GameState] varchar(50)  NULL,
    [NumberOfWinners] int  NOT NULL,
    [NextGameID] int  NULL,
    [MemberSubscriptionType] int  NULL
);
GO

-- Creating table 'GameSchedules'
CREATE TABLE [dbo].[GameSchedules] (
    [GameScheduleID] int  NOT NULL,
    [GameScheduleCode] varchar(20)  NOT NULL,
    [ScheduledDateTime] datetime  NOT NULL,
    [GameID] int  NOT NULL,
    [SequenceNumber] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'GameTypes'
CREATE TABLE [dbo].[GameTypes] (
    [GameTypeID] int IDENTITY(1,1) NOT NULL,
    [GameTypeCode] varchar(20)  NOT NULL,
    [GameTypeName] varchar(255)  NOT NULL,
    [GameTypeDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'GameTypeOwneds'
CREATE TABLE [dbo].[GameTypeOwneds] (
    [GameTypeOwnedID] int  NOT NULL,
    [GameTypeOwnedCode] varchar(20)  NOT NULL,
    [OwnerID] int  NOT NULL,
    [GameTypeID] int  NOT NULL,
    [ContractID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'InteractionTypes'
CREATE TABLE [dbo].[InteractionTypes] (
    [InteractionTypeID] int  NOT NULL,
    [InteractionTypeCode] varchar(20)  NOT NULL,
    [InteractionTypeName] varchar(255)  NOT NULL,
    [InteractionTypeDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [MemberID] int IDENTITY(1,1) NOT NULL,
    [MemberSubscriptionTypeID] int  NOT NULL,
    [IdentityType] varchar(20)  NOT NULL,
    [EmailAddress] varchar(255)  NOT NULL,
    [TelephoneHome] varchar(20)  NULL,
    [TelephoneOffice] varchar(20)  NULL,
    [TelephoneMobile] varchar(20)  NOT NULL,
    [Gender] varchar(1)  NOT NULL,
    [Ethnicity] varchar(255)  NULL,
    [DateOfBirth] datetime  NOT NULL,
    [ActiveIndicator] bit  NOT NULL,
    [RenewalDate] datetime  NULL,
    [AddressID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [ASPUserId] nvarchar(128)  NULL,
    [CountryID] int  NULL,
    [StateID] int  NULL,
    [FirstName] nvarchar(255)  NULL,
    [LastName] nvarchar(255)  NULL,
    [MemberAcquisitionCampaignCode] varchar(20)  NULL,
    [CityID] int  NULL,
    [IdentityNumber] varchar(100)  NULL,
    [Industry] varchar(100)  NULL,
    [Designation] varchar(20)  NULL,
    [MaritalStatus] varchar(20)  NULL,
    [Children] varchar(20)  NULL
);
GO

-- Creating table 'MemberAcquisitionCampaigns'
CREATE TABLE [dbo].[MemberAcquisitionCampaigns] (
    [MemberAcquisitionCampaignID] int  NOT NULL,
    [MemberAcquisitionCampaignCode] varchar(20)  NOT NULL,
    [OwnerID] int  NOT NULL,
    [ContractID] int  NOT NULL,
    [MemberAcquisitionCampaignName] varchar(255)  NOT NULL,
    [MemberAcquisitionCampaignDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'MemberAtEvents'
CREATE TABLE [dbo].[MemberAtEvents] (
    [MemberAtEventID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [EventID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'MemberInGames'
CREATE TABLE [dbo].[MemberInGames] (
    [MemberInGameID] int IDENTITY(1,1) NOT NULL,
    [GameID] int  NOT NULL,
    [MemberID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [WinIndicator] bit  NULL,
    [PaymentIndicator] bit  NULL
);
GO

-- Creating table 'MemberInterests'
CREATE TABLE [dbo].[MemberInterests] (
    [MemberInterestID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ProductCategoryID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [MemberInterestAutomotive] bit  NOT NULL,
    [MemberInterestArtCollectibles] bit  NOT NULL,
    [MemberInterestHomeAppliances] bit  NOT NULL,
    [MemberInterestToys] bit  NOT NULL,
    [MemberInterestEntertainment] bit  NOT NULL,
    [MemberInterestDecorDesign] bit  NOT NULL,
    [MemberInterestHealthBeauty] bit  NOT NULL,
    [MemberInterestTravel] bit  NOT NULL,
    [MemberInterestFashionAccessories] bit  NOT NULL,
    [MemberInterestExperiences] bit  NOT NULL,
    [MemberInterestTechnology] bit  NOT NULL,
    [MemberInterestWiningDining] bit  NOT NULL,
    [MemberInterestOther] varchar(500)  NULL
);
GO

-- Creating table 'MemberOwneds'
CREATE TABLE [dbo].[MemberOwneds] (
    [MemberOwnedID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [OwnerID] int  NOT NULL,
    [TerritoryID] int  NOT NULL,
    [MemberAcquisitionCampaignID] int  NOT NULL,
    [DateFrom] datetime  NOT NULL,
    [DateTo] datetime  NOT NULL,
    [ExclusiveIndicator] bit  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'MemberSubscriptionTypes'
CREATE TABLE [dbo].[MemberSubscriptionTypes] (
    [MemberSubscriptionTypeID] int IDENTITY(1,1) NOT NULL,
    [MemberSubscriptionTypeCode] varchar(20)  NOT NULL,
    [MemberSubscriptionTypeDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'NextGames'
CREATE TABLE [dbo].[NextGames] (
    [NextGameID] int  NOT NULL,
    [GameID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [OwnerID] int IDENTITY(1,1) NOT NULL,
    [OwnerCode] varchar(20)  NOT NULL,
    [OwnerName] varchar(255)  NOT NULL,
    [OwnerType] varchar(255)  NOT NULL,
    [BankingDetailBank] varchar(255)  NOT NULL,
    [BankingDetailAccountNumber] varchar(255)  NOT NULL,
    [BankingDetailAccountType] varchar(255)  NOT NULL,
    [BankingDetailBranchCode] varchar(255)  NOT NULL,
    [BankingDetailBranchName] varchar(255)  NOT NULL,
    [BankingDetailDefaultReference] varchar(255)  NOT NULL,
    [EmailAddress] varchar(255)  NOT NULL,
    [ContactPerson] varchar(255)  NOT NULL,
    [TelephoneOffice] varchar(20)  NULL,
    [TelephoneMobile] varchar(20)  NOT NULL,
    [AddressID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [CurrencyID] int  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int IDENTITY(1,1) NOT NULL,
    [ProductSKUCode] varchar(50)  NOT NULL,
    [OwnerID] int  NOT NULL,
    [ContractID] int  NOT NULL,
    [ProductName] varchar(255)  NOT NULL,
    [ProductDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [IsExclusive] bit  NOT NULL,
    [SOH] int  NULL,
    [AvailableSOH] int  NULL,
    [ProductPrice] decimal(10,2)  NULL
);
GO

-- Creating table 'ProductCategories'
CREATE TABLE [dbo].[ProductCategories] (
    [ProductCategoryID] int IDENTITY(1,1) NOT NULL,
    [ProductCategoryCode] varchar(20)  NOT NULL,
    [ProductCategoryName] varchar(255)  NOT NULL,
    [ProductCategoryDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [ParentProductCategoryID] int  NULL
);
GO

-- Creating table 'ProductInCategories'
CREATE TABLE [dbo].[ProductInCategories] (
    [ProductInCategoryID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [ProductCategoryID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'ProductInWatchLists'
CREATE TABLE [dbo].[ProductInWatchLists] (
    [ProductInWatchListID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [IsExpired] bit  NULL,
    [GameID] int  NOT NULL
);
GO

-- Creating table 'ProductPlayeds'
CREATE TABLE [dbo].[ProductPlayeds] (
    [ProductPlayedID] int IDENTITY(1,1) NOT NULL,
    [ProductInGameID] int  NULL,
    [MemberInGameID] int  NULL,
    [VoucherID] int  NULL,
    [SerialNumberID] int  NULL,
    [DeliveryAgentName] nvarchar(255)  NULL,
    [DeliveryAgentReferenceNumber] nvarchar(255)  NULL,
    [ClickInterval] int  NULL,
    [Winner] bit  NOT NULL,
    [PaymentConfirmed] datetime  NULL,
    [DateInserted] datetime  NULL
);
GO

-- Creating table 'SerialNumbers'
CREATE TABLE [dbo].[SerialNumbers] (
    [SerialNumberID] int IDENTITY(1,1) NOT NULL,
    [Serial] nvarchar(100)  NOT NULL,
    [ProductLocationID] int  NOT NULL,
    [Used] bit  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUsed] datetime  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Territories'
CREATE TABLE [dbo].[Territories] (
    [TerritoryID] int IDENTITY(1,1) NOT NULL,
    [TerritoryCode] varchar(20)  NOT NULL,
    [OwnerID] int  NOT NULL,
    [ContractID] int  NOT NULL,
    [TerritoryName] varchar(255)  NOT NULL,
    [TerritoryDescription] varchar(255)  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'TerritoryDefinitions'
CREATE TABLE [dbo].[TerritoryDefinitions] (
    [TerritoryDefinitionID] int IDENTITY(1,1) NOT NULL,
    [TerritoryDefinitionCode] varchar(20)  NOT NULL,
    [TerritoryID] int  NOT NULL,
    [ZipOrPostalCode] varchar(20)  NULL,
    [IPAddress] varchar(30)  NULL,
    [PhysicalCoordinates] varchar(30)  NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'Vouchers'
CREATE TABLE [dbo].[Vouchers] (
    [VoucherID] int IDENTITY(1,1) NOT NULL,
    [VoucherNumber] nvarchar(100)  NOT NULL,
    [ProductLocationID] int  NOT NULL,
    [Used] bit  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUsed] datetime  NULL
);
GO

-- Creating table 'WebsiteInteractions'
CREATE TABLE [dbo].[WebsiteInteractions] (
    [WebsiteInteractionID] int  NOT NULL,
    [MemberID] int  NOT NULL,
    [InteractionTypeID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'ProductProviders'
CREATE TABLE [dbo].[ProductProviders] (
    [ProductProviderID] int IDENTITY(1,1) NOT NULL,
    [ProductProviderName] nvarchar(255)  NULL,
    [Currency] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductLocations'
CREATE TABLE [dbo].[ProductLocations] (
    [ProductLocationID] int IDENTITY(1,1) NOT NULL,
    [ProductInGameID] int  NOT NULL,
    [Address] nvarchar(600)  NOT NULL,
    [DeliveryAgentName] nvarchar(255)  NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryID] int  NOT NULL,
    [CountryName] nvarchar(300)  NULL
);
GO

-- Creating table 'Currencies'
CREATE TABLE [dbo].[Currencies] (
    [CurrencyID] int IDENTITY(1,1) NOT NULL,
    [CurrencyCode] nvarchar(10)  NULL,
    [CurrencySymbol] nvarchar(10)  NULL,
    [CountryID] int  NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [LanguageID] int IDENTITY(1,1) NOT NULL,
    [LanguageName] varchar(50)  NOT NULL,
    [LanguageTwoLetters] varchar(2)  NOT NULL,
    [LanguageThreeLetters] varchar(10)  NULL
);
GO

-- Creating table 'LanguageItems'
CREATE TABLE [dbo].[LanguageItems] (
    [LanguageItemID] int IDENTITY(1,1) NOT NULL,
    [LanguageID] int  NOT NULL,
    [LanguageItemKey] varchar(250)  NOT NULL,
    [LanguageItemValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CountryCities'
CREATE TABLE [dbo].[CountryCities] (
    [CityID] int  NOT NULL,
    [CountryID] int  NOT NULL,
    [CityName] varchar(300)  NOT NULL,
    [StateID] int  NOT NULL
);
GO

-- Creating table 'GameInteractions'
CREATE TABLE [dbo].[GameInteractions] (
    [GameInteractionID] int  NOT NULL,
    [GameScheduleID] int  NOT NULL,
    [MemberID] int  NOT NULL,
    [InteractionType] varchar(20)  NOT NULL,
    [GameInteractionValue] float  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL
);
GO

-- Creating table 'PaymentConfigurations'
CREATE TABLE [dbo].[PaymentConfigurations] (
    [PaymentConfigurationID] int  NOT NULL,
    [PaymentGatewayURL] varchar(100)  NULL,
    [MerchantPassword] varchar(50)  NULL,
    [TerminalID] varchar(50)  NULL,
    [Status] char(1)  NULL
);
GO

-- Creating table 'PaymentTransactions'
CREATE TABLE [dbo].[PaymentTransactions] (
    [PaymentTransactionID] int IDENTITY(1,1) NOT NULL,
    [MemberInGameID] int  NOT NULL,
    [Action] nchar(1)  NULL,
    [Member] varchar(50)  NULL,
    [CurrencyCode] nchar(10)  NULL,
    [Address] varchar(200)  NULL,
    [City] varchar(50)  NULL,
    [StateCode] varchar(50)  NULL,
    [Zip] varchar(15)  NULL,
    [CountryCode] varchar(70)  NULL,
    [email] varchar(80)  NULL,
    [Amount] decimal(19,4)  NULL,
    [TrackID] varchar(50)  NULL,
    [MerchantIP] varchar(50)  NULL,
    [ClientIP] varchar(50)  NULL,
    [UDF1] varchar(50)  NULL,
    [UDF2] varchar(50)  NULL,
    [UDF3] varchar(50)  NULL,
    [UDF4] varchar(50)  NULL,
    [UDF5] varchar(50)  NULL,
    [Result] varchar(100)  NULL,
    [ResponseCode] varchar(20)  NULL,
    [AuthCode] varchar(20)  NULL,
    [TranID] varchar(30)  NULL,
    [TransactionRequestDateTime] datetime  NULL,
    [TransactionResponseDateTime] datetime  NULL,
    [PaymentStatus] nchar(10)  NULL
);
GO

-- Creating table 'ResponseCatalogs'
CREATE TABLE [dbo].[ResponseCatalogs] (
    [ResponseCatalogID] int IDENTITY(1,1) NOT NULL,
    [ResponseCode] nchar(10)  NULL,
    [ResponseDescription] varchar(150)  NULL
);
GO

-- Creating table 'PlayableGamesByMembers'
CREATE TABLE [dbo].[PlayableGamesByMembers] (
    [GameName] varchar(255)  NOT NULL,
    [ProductName] varchar(255)  NOT NULL,
    [ProductDescription] varchar(255)  NOT NULL,
    [PriceInGame] decimal(10,2)  NULL,
    [DisplayItemType] varchar(50)  NULL,
    [DisplayItemURL] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [MemberID] int  NOT NULL,
    [GameID] int  NOT NULL,
    [GameState] varchar(50)  NULL
);
GO

-- Creating table 'ProductInfoes'
CREATE TABLE [dbo].[ProductInfoes] (
    [gameId] int  NOT NULL,
    [ProductName] varchar(255)  NOT NULL,
    [ProductDescription] varchar(255)  NOT NULL,
    [PriceInGame] decimal(10,2)  NULL,
    [DisplayItemURL] nvarchar(max)  NULL
);
GO

-- Creating table 'ShowWinners'
CREATE TABLE [dbo].[ShowWinners] (
    [GameID] int  NOT NULL,
    [GameName] varchar(255)  NOT NULL,
    [GameDescription] varchar(255)  NOT NULL,
    [GameState] varchar(50)  NULL,
    [MemberID] int  NOT NULL,
    [WinIndicator] bit  NULL,
    [FirstName] nvarchar(255)  NULL,
    [LastName] nvarchar(255)  NULL,
    [EmailAddress] varchar(255)  NOT NULL,
    [TelephoneHome] varchar(20)  NULL,
    [TelephoneMobile] varchar(20)  NOT NULL
);
GO

-- Creating table 'GameMemberFilters'
CREATE TABLE [dbo].[GameMemberFilters] (
    [GameMemberFilterID] int IDENTITY(1,1) NOT NULL,
    [GameID] int  NOT NULL,
    [CountryID] int  NULL,
    [StateID] int  NULL,
    [CityID] int  NULL,
    [AgeBandID] int  NULL,
    [GenderID] int  NULL,
    [Territory] int  NULL,
    [MemberSubscriptionTypeID] int  NULL
);
GO

-- Creating table 'ProductInGames'
CREATE TABLE [dbo].[ProductInGames] (
    [ProductInGameID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [GameID] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [CurrencyID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [PriceInGame] decimal(10,2)  NULL,
    [ReferencePrice] decimal(10,2)  NULL
);
GO

-- Creating table 'TrackingTransactions'
CREATE TABLE [dbo].[TrackingTransactions] (
    [TrackingTransactionID] int IDENTITY(1,1) NOT NULL,
    [MemberInGameID] int  NOT NULL,
    [TimeInitiated] datetime  NULL,
    [TimePaused] datetime  NULL,
    [TimeResumed] datetime  NULL,
    [TimeCompleted] datetime  NULL,
    [DurationRemaining] float  NULL,
    [DateInserted] datetime  NULL,
    [DateModified] datetime  NULL
);
GO

-- Creating table 'GameTemplates'
CREATE TABLE [dbo].[GameTemplates] (
    [GameTemplateID] int  NOT NULL,
    [GameTemplateCode] varchar(20)  NOT NULL,
    [GameTypeID] int  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [GameRuleCode] varchar(255)  NULL,
    [GameRuleDetail] varchar(255)  NULL,
    [OrderInGame] int  NULL
);
GO

-- Creating table 'GameRules'
CREATE TABLE [dbo].[GameRules] (
    [GameRuleID] int IDENTITY(1,1) NOT NULL,
    [GameRuleCode] varchar(50)  NOT NULL,
    [GameID] int  NOT NULL,
    [FilterCriteria] varchar(255)  NULL,
    [Schedule] int  NOT NULL,
    [ChainGameRuleID] int  NULL,
    [GameRuleDetail] varchar(255)  NOT NULL,
    [ExcecuteTime] datetime  NOT NULL,
    [DateInserted] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [USR] varchar(200)  NOT NULL,
    [GameTemplateID] int  NOT NULL,
    [ExecuteHhMmSs] varchar(8)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AddressID] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([AddressID] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ContractID] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [PK_Contracts]
    PRIMARY KEY CLUSTERED ([ContractID] ASC);
GO

-- Creating primary key on [StateID] in table 'CountryStates'
ALTER TABLE [dbo].[CountryStates]
ADD CONSTRAINT [PK_CountryStates]
    PRIMARY KEY CLUSTERED ([StateID] ASC);
GO

-- Creating primary key on [DisplayItemID] in table 'DisplayItems'
ALTER TABLE [dbo].[DisplayItems]
ADD CONSTRAINT [PK_DisplayItems]
    PRIMARY KEY CLUSTERED ([DisplayItemID] ASC);
GO

-- Creating primary key on [EventID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([EventID] ASC);
GO

-- Creating primary key on [GameID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameID] ASC);
GO

-- Creating primary key on [GameScheduleID] in table 'GameSchedules'
ALTER TABLE [dbo].[GameSchedules]
ADD CONSTRAINT [PK_GameSchedules]
    PRIMARY KEY CLUSTERED ([GameScheduleID] ASC);
GO

-- Creating primary key on [GameTypeID] in table 'GameTypes'
ALTER TABLE [dbo].[GameTypes]
ADD CONSTRAINT [PK_GameTypes]
    PRIMARY KEY CLUSTERED ([GameTypeID] ASC);
GO

-- Creating primary key on [GameTypeOwnedID] in table 'GameTypeOwneds'
ALTER TABLE [dbo].[GameTypeOwneds]
ADD CONSTRAINT [PK_GameTypeOwneds]
    PRIMARY KEY CLUSTERED ([GameTypeOwnedID] ASC);
GO

-- Creating primary key on [InteractionTypeID] in table 'InteractionTypes'
ALTER TABLE [dbo].[InteractionTypes]
ADD CONSTRAINT [PK_InteractionTypes]
    PRIMARY KEY CLUSTERED ([InteractionTypeID] ASC);
GO

-- Creating primary key on [MemberID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([MemberID] ASC);
GO

-- Creating primary key on [MemberAcquisitionCampaignID] in table 'MemberAcquisitionCampaigns'
ALTER TABLE [dbo].[MemberAcquisitionCampaigns]
ADD CONSTRAINT [PK_MemberAcquisitionCampaigns]
    PRIMARY KEY CLUSTERED ([MemberAcquisitionCampaignID] ASC);
GO

-- Creating primary key on [MemberAtEventID] in table 'MemberAtEvents'
ALTER TABLE [dbo].[MemberAtEvents]
ADD CONSTRAINT [PK_MemberAtEvents]
    PRIMARY KEY CLUSTERED ([MemberAtEventID] ASC);
GO

-- Creating primary key on [MemberInGameID] in table 'MemberInGames'
ALTER TABLE [dbo].[MemberInGames]
ADD CONSTRAINT [PK_MemberInGames]
    PRIMARY KEY CLUSTERED ([MemberInGameID] ASC);
GO

-- Creating primary key on [MemberInterestID] in table 'MemberInterests'
ALTER TABLE [dbo].[MemberInterests]
ADD CONSTRAINT [PK_MemberInterests]
    PRIMARY KEY CLUSTERED ([MemberInterestID] ASC);
GO

-- Creating primary key on [MemberOwnedID] in table 'MemberOwneds'
ALTER TABLE [dbo].[MemberOwneds]
ADD CONSTRAINT [PK_MemberOwneds]
    PRIMARY KEY CLUSTERED ([MemberOwnedID] ASC);
GO

-- Creating primary key on [MemberSubscriptionTypeID] in table 'MemberSubscriptionTypes'
ALTER TABLE [dbo].[MemberSubscriptionTypes]
ADD CONSTRAINT [PK_MemberSubscriptionTypes]
    PRIMARY KEY CLUSTERED ([MemberSubscriptionTypeID] ASC);
GO

-- Creating primary key on [NextGameID] in table 'NextGames'
ALTER TABLE [dbo].[NextGames]
ADD CONSTRAINT [PK_NextGames]
    PRIMARY KEY CLUSTERED ([NextGameID] ASC);
GO

-- Creating primary key on [OwnerID] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([OwnerID] ASC);
GO

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [ProductCategoryID] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([ProductCategoryID] ASC);
GO

-- Creating primary key on [ProductInCategoryID] in table 'ProductInCategories'
ALTER TABLE [dbo].[ProductInCategories]
ADD CONSTRAINT [PK_ProductInCategories]
    PRIMARY KEY CLUSTERED ([ProductInCategoryID] ASC);
GO

-- Creating primary key on [ProductInWatchListID] in table 'ProductInWatchLists'
ALTER TABLE [dbo].[ProductInWatchLists]
ADD CONSTRAINT [PK_ProductInWatchLists]
    PRIMARY KEY CLUSTERED ([ProductInWatchListID] ASC);
GO

-- Creating primary key on [ProductPlayedID] in table 'ProductPlayeds'
ALTER TABLE [dbo].[ProductPlayeds]
ADD CONSTRAINT [PK_ProductPlayeds]
    PRIMARY KEY CLUSTERED ([ProductPlayedID] ASC);
GO

-- Creating primary key on [SerialNumberID] in table 'SerialNumbers'
ALTER TABLE [dbo].[SerialNumbers]
ADD CONSTRAINT [PK_SerialNumbers]
    PRIMARY KEY CLUSTERED ([SerialNumberID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [TerritoryID] in table 'Territories'
ALTER TABLE [dbo].[Territories]
ADD CONSTRAINT [PK_Territories]
    PRIMARY KEY CLUSTERED ([TerritoryID] ASC);
GO

-- Creating primary key on [TerritoryDefinitionID] in table 'TerritoryDefinitions'
ALTER TABLE [dbo].[TerritoryDefinitions]
ADD CONSTRAINT [PK_TerritoryDefinitions]
    PRIMARY KEY CLUSTERED ([TerritoryDefinitionID] ASC);
GO

-- Creating primary key on [VoucherID] in table 'Vouchers'
ALTER TABLE [dbo].[Vouchers]
ADD CONSTRAINT [PK_Vouchers]
    PRIMARY KEY CLUSTERED ([VoucherID] ASC);
GO

-- Creating primary key on [WebsiteInteractionID] in table 'WebsiteInteractions'
ALTER TABLE [dbo].[WebsiteInteractions]
ADD CONSTRAINT [PK_WebsiteInteractions]
    PRIMARY KEY CLUSTERED ([WebsiteInteractionID] ASC);
GO

-- Creating primary key on [ProductProviderID] in table 'ProductProviders'
ALTER TABLE [dbo].[ProductProviders]
ADD CONSTRAINT [PK_ProductProviders]
    PRIMARY KEY CLUSTERED ([ProductProviderID] ASC);
GO

-- Creating primary key on [ProductLocationID] in table 'ProductLocations'
ALTER TABLE [dbo].[ProductLocations]
ADD CONSTRAINT [PK_ProductLocations]
    PRIMARY KEY CLUSTERED ([ProductLocationID] ASC);
GO

-- Creating primary key on [CountryID] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [CurrencyID] in table 'Currencies'
ALTER TABLE [dbo].[Currencies]
ADD CONSTRAINT [PK_Currencies]
    PRIMARY KEY CLUSTERED ([CurrencyID] ASC);
GO

-- Creating primary key on [LanguageID] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([LanguageID] ASC);
GO

-- Creating primary key on [LanguageItemID] in table 'LanguageItems'
ALTER TABLE [dbo].[LanguageItems]
ADD CONSTRAINT [PK_LanguageItems]
    PRIMARY KEY CLUSTERED ([LanguageItemID] ASC);
GO

-- Creating primary key on [CityID] in table 'CountryCities'
ALTER TABLE [dbo].[CountryCities]
ADD CONSTRAINT [PK_CountryCities]
    PRIMARY KEY CLUSTERED ([CityID] ASC);
GO

-- Creating primary key on [GameInteractionID] in table 'GameInteractions'
ALTER TABLE [dbo].[GameInteractions]
ADD CONSTRAINT [PK_GameInteractions]
    PRIMARY KEY CLUSTERED ([GameInteractionID] ASC);
GO

-- Creating primary key on [PaymentConfigurationID] in table 'PaymentConfigurations'
ALTER TABLE [dbo].[PaymentConfigurations]
ADD CONSTRAINT [PK_PaymentConfigurations]
    PRIMARY KEY CLUSTERED ([PaymentConfigurationID] ASC);
GO

-- Creating primary key on [PaymentTransactionID] in table 'PaymentTransactions'
ALTER TABLE [dbo].[PaymentTransactions]
ADD CONSTRAINT [PK_PaymentTransactions]
    PRIMARY KEY CLUSTERED ([PaymentTransactionID] ASC);
GO

-- Creating primary key on [ResponseCatalogID] in table 'ResponseCatalogs'
ALTER TABLE [dbo].[ResponseCatalogs]
ADD CONSTRAINT [PK_ResponseCatalogs]
    PRIMARY KEY CLUSTERED ([ResponseCatalogID] ASC);
GO

-- Creating primary key on [GameName], [ProductName], [ProductDescription], [Active], [MemberID], [GameID] in table 'PlayableGamesByMembers'
ALTER TABLE [dbo].[PlayableGamesByMembers]
ADD CONSTRAINT [PK_PlayableGamesByMembers]
    PRIMARY KEY CLUSTERED ([GameName], [ProductName], [ProductDescription], [Active], [MemberID], [GameID] ASC);
GO

-- Creating primary key on [gameId], [ProductName], [ProductDescription] in table 'ProductInfoes'
ALTER TABLE [dbo].[ProductInfoes]
ADD CONSTRAINT [PK_ProductInfoes]
    PRIMARY KEY CLUSTERED ([gameId], [ProductName], [ProductDescription] ASC);
GO

-- Creating primary key on [GameID], [GameName], [GameDescription], [MemberID], [EmailAddress], [TelephoneMobile] in table 'ShowWinners'
ALTER TABLE [dbo].[ShowWinners]
ADD CONSTRAINT [PK_ShowWinners]
    PRIMARY KEY CLUSTERED ([GameID], [GameName], [GameDescription], [MemberID], [EmailAddress], [TelephoneMobile] ASC);
GO

-- Creating primary key on [GameMemberFilterID] in table 'GameMemberFilters'
ALTER TABLE [dbo].[GameMemberFilters]
ADD CONSTRAINT [PK_GameMemberFilters]
    PRIMARY KEY CLUSTERED ([GameMemberFilterID] ASC);
GO

-- Creating primary key on [ProductInGameID] in table 'ProductInGames'
ALTER TABLE [dbo].[ProductInGames]
ADD CONSTRAINT [PK_ProductInGames]
    PRIMARY KEY CLUSTERED ([ProductInGameID] ASC);
GO

-- Creating primary key on [TrackingTransactionID] in table 'TrackingTransactions'
ALTER TABLE [dbo].[TrackingTransactions]
ADD CONSTRAINT [PK_TrackingTransactions]
    PRIMARY KEY CLUSTERED ([TrackingTransactionID] ASC);
GO

-- Creating primary key on [GameTemplateID] in table 'GameTemplates'
ALTER TABLE [dbo].[GameTemplates]
ADD CONSTRAINT [PK_GameTemplates]
    PRIMARY KEY CLUSTERED ([GameTemplateID] ASC);
GO

-- Creating primary key on [GameRuleID] in table 'GameRules'
ALTER TABLE [dbo].[GameRules]
ADD CONSTRAINT [PK_GameRules]
    PRIMARY KEY CLUSTERED ([GameRuleID] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AddressID] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [FK_Owner_Address_AddressID]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[Addresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Owner_Address_AddressID'
CREATE INDEX [IX_FK_Owner_Address_AddressID]
ON [dbo].[Owners]
    ([AddressID]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [ASPUserId] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Member_AspNetUsers]
    FOREIGN KEY ([ASPUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Member_AspNetUsers'
CREATE INDEX [IX_FK_Member_AspNetUsers]
ON [dbo].[Members]
    ([ASPUserId]);
GO

-- Creating foreign key on [ContractID] in table 'GameTypeOwneds'
ALTER TABLE [dbo].[GameTypeOwneds]
ADD CONSTRAINT [FK_GameTypeOwned_Contract_ContractID]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[Contracts]
        ([ContractID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTypeOwned_Contract_ContractID'
CREATE INDEX [IX_FK_GameTypeOwned_Contract_ContractID]
ON [dbo].[GameTypeOwneds]
    ([ContractID]);
GO

-- Creating foreign key on [ContractID] in table 'MemberAcquisitionCampaigns'
ALTER TABLE [dbo].[MemberAcquisitionCampaigns]
ADD CONSTRAINT [FK_MemberAcquisitionCampaign_Contract_ContractID]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[Contracts]
        ([ContractID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberAcquisitionCampaign_Contract_ContractID'
CREATE INDEX [IX_FK_MemberAcquisitionCampaign_Contract_ContractID]
ON [dbo].[MemberAcquisitionCampaigns]
    ([ContractID]);
GO

-- Creating foreign key on [ContractID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Contract_ContractID]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[Contracts]
        ([ContractID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Contract_ContractID'
CREATE INDEX [IX_FK_Product_Contract_ContractID]
ON [dbo].[Products]
    ([ContractID]);
GO

-- Creating foreign key on [ContractID] in table 'Territories'
ALTER TABLE [dbo].[Territories]
ADD CONSTRAINT [FK_Territory_Contract_ContractID]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[Contracts]
        ([ContractID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Territory_Contract_ContractID'
CREATE INDEX [IX_FK_Territory_Contract_ContractID]
ON [dbo].[Territories]
    ([ContractID]);
GO

-- Creating foreign key on [StateID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Member_CountryState]
    FOREIGN KEY ([StateID])
    REFERENCES [dbo].[CountryStates]
        ([StateID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Member_CountryState'
CREATE INDEX [IX_FK_Member_CountryState]
ON [dbo].[Members]
    ([StateID]);
GO

-- Creating foreign key on [ProductID] in table 'DisplayItems'
ALTER TABLE [dbo].[DisplayItems]
ADD CONSTRAINT [FK_DisplayItem_Product_ProductID]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DisplayItem_Product_ProductID'
CREATE INDEX [IX_FK_DisplayItem_Product_ProductID]
ON [dbo].[DisplayItems]
    ([ProductID]);
GO

-- Creating foreign key on [EventID] in table 'MemberAtEvents'
ALTER TABLE [dbo].[MemberAtEvents]
ADD CONSTRAINT [FK_MemberAtEvent_Event_EventID]
    FOREIGN KEY ([EventID])
    REFERENCES [dbo].[Events]
        ([EventID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberAtEvent_Event_EventID'
CREATE INDEX [IX_FK_MemberAtEvent_Event_EventID]
ON [dbo].[MemberAtEvents]
    ([EventID]);
GO

-- Creating foreign key on [GameTypeID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_GameType_GameTypeID]
    FOREIGN KEY ([GameTypeID])
    REFERENCES [dbo].[GameTypes]
        ([GameTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_GameType_GameTypeID'
CREATE INDEX [IX_FK_Game_GameType_GameTypeID]
ON [dbo].[Games]
    ([GameTypeID]);
GO

-- Creating foreign key on [GameID] in table 'GameSchedules'
ALTER TABLE [dbo].[GameSchedules]
ADD CONSTRAINT [FK_GameSchedule_Game_GameID]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameSchedule_Game_GameID'
CREATE INDEX [IX_FK_GameSchedule_Game_GameID]
ON [dbo].[GameSchedules]
    ([GameID]);
GO

-- Creating foreign key on [GameID] in table 'MemberInGames'
ALTER TABLE [dbo].[MemberInGames]
ADD CONSTRAINT [FK_MemberInGame_Game_GameID]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberInGame_Game_GameID'
CREATE INDEX [IX_FK_MemberInGame_Game_GameID]
ON [dbo].[MemberInGames]
    ([GameID]);
GO

-- Creating foreign key on [NextGameID] in table 'NextGames'
ALTER TABLE [dbo].[NextGames]
ADD CONSTRAINT [FK_NextGame_Game_GameID]
    FOREIGN KEY ([NextGameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GameTypeID] in table 'GameTypeOwneds'
ALTER TABLE [dbo].[GameTypeOwneds]
ADD CONSTRAINT [FK_GameTypeOwned_GameType_GameTypeID]
    FOREIGN KEY ([GameTypeID])
    REFERENCES [dbo].[GameTypes]
        ([GameTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTypeOwned_GameType_GameTypeID'
CREATE INDEX [IX_FK_GameTypeOwned_GameType_GameTypeID]
ON [dbo].[GameTypeOwneds]
    ([GameTypeID]);
GO

-- Creating foreign key on [OwnerID] in table 'GameTypeOwneds'
ALTER TABLE [dbo].[GameTypeOwneds]
ADD CONSTRAINT [FK_GameTypeOwned_Owner_OwnerID]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[Owners]
        ([OwnerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTypeOwned_Owner_OwnerID'
CREATE INDEX [IX_FK_GameTypeOwned_Owner_OwnerID]
ON [dbo].[GameTypeOwneds]
    ([OwnerID]);
GO

-- Creating foreign key on [InteractionTypeID] in table 'WebsiteInteractions'
ALTER TABLE [dbo].[WebsiteInteractions]
ADD CONSTRAINT [FK_WebsiteInteraction_InteractionType_InteractionTypeID]
    FOREIGN KEY ([InteractionTypeID])
    REFERENCES [dbo].[InteractionTypes]
        ([InteractionTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WebsiteInteraction_InteractionType_InteractionTypeID'
CREATE INDEX [IX_FK_WebsiteInteraction_InteractionType_InteractionTypeID]
ON [dbo].[WebsiteInteractions]
    ([InteractionTypeID]);
GO

-- Creating foreign key on [MemberSubscriptionTypeID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID]
    FOREIGN KEY ([MemberSubscriptionTypeID])
    REFERENCES [dbo].[MemberSubscriptionTypes]
        ([MemberSubscriptionTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID'
CREATE INDEX [IX_FK_Member_MemberSubscriptionType_MemberSubscriptionTypeID]
ON [dbo].[Members]
    ([MemberSubscriptionTypeID]);
GO

-- Creating foreign key on [MemberID] in table 'MemberAtEvents'
ALTER TABLE [dbo].[MemberAtEvents]
ADD CONSTRAINT [FK_MemberAtEvent_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberAtEvent_Member_MemberID'
CREATE INDEX [IX_FK_MemberAtEvent_Member_MemberID]
ON [dbo].[MemberAtEvents]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'MemberInGames'
ALTER TABLE [dbo].[MemberInGames]
ADD CONSTRAINT [FK_MemberInGame_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberInGame_Member_MemberID'
CREATE INDEX [IX_FK_MemberInGame_Member_MemberID]
ON [dbo].[MemberInGames]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'MemberInterests'
ALTER TABLE [dbo].[MemberInterests]
ADD CONSTRAINT [FK_MemberInterest_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberInterest_Member_MemberID'
CREATE INDEX [IX_FK_MemberInterest_Member_MemberID]
ON [dbo].[MemberInterests]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'MemberOwneds'
ALTER TABLE [dbo].[MemberOwneds]
ADD CONSTRAINT [FK_MemberOwned_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberOwned_Member_MemberID'
CREATE INDEX [IX_FK_MemberOwned_Member_MemberID]
ON [dbo].[MemberOwneds]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'ProductInWatchLists'
ALTER TABLE [dbo].[ProductInWatchLists]
ADD CONSTRAINT [FK_ProductInWatchList_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInWatchList_Member_MemberID'
CREATE INDEX [IX_FK_ProductInWatchList_Member_MemberID]
ON [dbo].[ProductInWatchLists]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'WebsiteInteractions'
ALTER TABLE [dbo].[WebsiteInteractions]
ADD CONSTRAINT [FK_WebsiteInteraction_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WebsiteInteraction_Member_MemberID'
CREATE INDEX [IX_FK_WebsiteInteraction_Member_MemberID]
ON [dbo].[WebsiteInteractions]
    ([MemberID]);
GO

-- Creating foreign key on [OwnerID] in table 'MemberAcquisitionCampaigns'
ALTER TABLE [dbo].[MemberAcquisitionCampaigns]
ADD CONSTRAINT [FK_MemberAcquisitionCampaign_Owner_OwnerID]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[Owners]
        ([OwnerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberAcquisitionCampaign_Owner_OwnerID'
CREATE INDEX [IX_FK_MemberAcquisitionCampaign_Owner_OwnerID]
ON [dbo].[MemberAcquisitionCampaigns]
    ([OwnerID]);
GO

-- Creating foreign key on [MemberAcquisitionCampaignID] in table 'MemberOwneds'
ALTER TABLE [dbo].[MemberOwneds]
ADD CONSTRAINT [FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID]
    FOREIGN KEY ([MemberAcquisitionCampaignID])
    REFERENCES [dbo].[MemberAcquisitionCampaigns]
        ([MemberAcquisitionCampaignID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID'
CREATE INDEX [IX_FK_MemberOwned_MemberAcquisitionCampaign_MemberAcquisitionCampaignID]
ON [dbo].[MemberOwneds]
    ([MemberAcquisitionCampaignID]);
GO

-- Creating foreign key on [MemberInGameID] in table 'ProductPlayeds'
ALTER TABLE [dbo].[ProductPlayeds]
ADD CONSTRAINT [FK_ProductPlayed_MemberInGame]
    FOREIGN KEY ([MemberInGameID])
    REFERENCES [dbo].[MemberInGames]
        ([MemberInGameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPlayed_MemberInGame'
CREATE INDEX [IX_FK_ProductPlayed_MemberInGame]
ON [dbo].[ProductPlayeds]
    ([MemberInGameID]);
GO

-- Creating foreign key on [ProductCategoryID] in table 'MemberInterests'
ALTER TABLE [dbo].[MemberInterests]
ADD CONSTRAINT [FK_MemberInterest_ProductCategory_ProductCategoryID]
    FOREIGN KEY ([ProductCategoryID])
    REFERENCES [dbo].[ProductCategories]
        ([ProductCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberInterest_ProductCategory_ProductCategoryID'
CREATE INDEX [IX_FK_MemberInterest_ProductCategory_ProductCategoryID]
ON [dbo].[MemberInterests]
    ([ProductCategoryID]);
GO

-- Creating foreign key on [OwnerID] in table 'MemberOwneds'
ALTER TABLE [dbo].[MemberOwneds]
ADD CONSTRAINT [FK_MemberOwned_Owner_OwnerID]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[Owners]
        ([OwnerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberOwned_Owner_OwnerID'
CREATE INDEX [IX_FK_MemberOwned_Owner_OwnerID]
ON [dbo].[MemberOwneds]
    ([OwnerID]);
GO

-- Creating foreign key on [TerritoryID] in table 'MemberOwneds'
ALTER TABLE [dbo].[MemberOwneds]
ADD CONSTRAINT [FK_MemberOwned_Territory_TerritoryID]
    FOREIGN KEY ([TerritoryID])
    REFERENCES [dbo].[Territories]
        ([TerritoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberOwned_Territory_TerritoryID'
CREATE INDEX [IX_FK_MemberOwned_Territory_TerritoryID]
ON [dbo].[MemberOwneds]
    ([TerritoryID]);
GO

-- Creating foreign key on [OwnerID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Owner_OwnerID]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[Owners]
        ([OwnerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Owner_OwnerID'
CREATE INDEX [IX_FK_Product_Owner_OwnerID]
ON [dbo].[Products]
    ([OwnerID]);
GO

-- Creating foreign key on [OwnerID] in table 'Territories'
ALTER TABLE [dbo].[Territories]
ADD CONSTRAINT [FK_Territory_Owner_OwnerID]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[Owners]
        ([OwnerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Territory_Owner_OwnerID'
CREATE INDEX [IX_FK_Territory_Owner_OwnerID]
ON [dbo].[Territories]
    ([OwnerID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductInCategories'
ALTER TABLE [dbo].[ProductInCategories]
ADD CONSTRAINT [FK_ProductInCategory_Product_ProductID]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInCategory_Product_ProductID'
CREATE INDEX [IX_FK_ProductInCategory_Product_ProductID]
ON [dbo].[ProductInCategories]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductInWatchLists'
ALTER TABLE [dbo].[ProductInWatchLists]
ADD CONSTRAINT [FK_ProductInWatchList_Product_ProductID]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInWatchList_Product_ProductID'
CREATE INDEX [IX_FK_ProductInWatchList_Product_ProductID]
ON [dbo].[ProductInWatchLists]
    ([ProductID]);
GO

-- Creating foreign key on [ProductCategoryID] in table 'ProductInCategories'
ALTER TABLE [dbo].[ProductInCategories]
ADD CONSTRAINT [FK_ProductInCategory_ProductCategory_ProductCategoryID]
    FOREIGN KEY ([ProductCategoryID])
    REFERENCES [dbo].[ProductCategories]
        ([ProductCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInCategory_ProductCategory_ProductCategoryID'
CREATE INDEX [IX_FK_ProductInCategory_ProductCategory_ProductCategoryID]
ON [dbo].[ProductInCategories]
    ([ProductCategoryID]);
GO

-- Creating foreign key on [TerritoryID] in table 'TerritoryDefinitions'
ALTER TABLE [dbo].[TerritoryDefinitions]
ADD CONSTRAINT [FK_TerritoryDefinition_Territory_TerritoryID]
    FOREIGN KEY ([TerritoryID])
    REFERENCES [dbo].[Territories]
        ([TerritoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TerritoryDefinition_Territory_TerritoryID'
CREATE INDEX [IX_FK_TerritoryDefinition_Territory_TerritoryID]
ON [dbo].[TerritoryDefinitions]
    ([TerritoryID]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [ProductLocationID] in table 'SerialNumbers'
ALTER TABLE [dbo].[SerialNumbers]
ADD CONSTRAINT [FK_SerialNumber_ProductLocation]
    FOREIGN KEY ([ProductLocationID])
    REFERENCES [dbo].[ProductLocations]
        ([ProductLocationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SerialNumber_ProductLocation'
CREATE INDEX [IX_FK_SerialNumber_ProductLocation]
ON [dbo].[SerialNumbers]
    ([ProductLocationID]);
GO

-- Creating foreign key on [ProductLocationID] in table 'Vouchers'
ALTER TABLE [dbo].[Vouchers]
ADD CONSTRAINT [FK_Voucher_ProductLocation]
    FOREIGN KEY ([ProductLocationID])
    REFERENCES [dbo].[ProductLocations]
        ([ProductLocationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Voucher_ProductLocation'
CREATE INDEX [IX_FK_Voucher_ProductLocation]
ON [dbo].[Vouchers]
    ([ProductLocationID]);
GO

-- Creating foreign key on [CountryID] in table 'CountryStates'
ALTER TABLE [dbo].[CountryStates]
ADD CONSTRAINT [FK_CountryState_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[Countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryState_Country'
CREATE INDEX [IX_FK_CountryState_Country]
ON [dbo].[CountryStates]
    ([CountryID]);
GO

-- Creating foreign key on [CountryID] in table 'Currencies'
ALTER TABLE [dbo].[Currencies]
ADD CONSTRAINT [FK_Currency_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[Countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Currency_Country'
CREATE INDEX [IX_FK_Currency_Country]
ON [dbo].[Currencies]
    ([CountryID]);
GO

-- Creating foreign key on [CountryID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Member_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[Countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Member_Country'
CREATE INDEX [IX_FK_Member_Country]
ON [dbo].[Members]
    ([CountryID]);
GO

-- Creating foreign key on [LanguageID] in table 'LanguageItems'
ALTER TABLE [dbo].[LanguageItems]
ADD CONSTRAINT [FK_LanguageItem_Language]
    FOREIGN KEY ([LanguageID])
    REFERENCES [dbo].[Languages]
        ([LanguageID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageItem_Language'
CREATE INDEX [IX_FK_LanguageItem_Language]
ON [dbo].[LanguageItems]
    ([LanguageID]);
GO

-- Creating foreign key on [CountryID] in table 'CountryCities'
ALTER TABLE [dbo].[CountryCities]
ADD CONSTRAINT [FK_CountryCity_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[Countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryCity_Country'
CREATE INDEX [IX_FK_CountryCity_Country]
ON [dbo].[CountryCities]
    ([CountryID]);
GO

-- Creating foreign key on [StateID] in table 'CountryCities'
ALTER TABLE [dbo].[CountryCities]
ADD CONSTRAINT [FK_CountryCity_CountryState]
    FOREIGN KEY ([StateID])
    REFERENCES [dbo].[CountryStates]
        ([StateID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryCity_CountryState'
CREATE INDEX [IX_FK_CountryCity_CountryState]
ON [dbo].[CountryCities]
    ([StateID]);
GO

-- Creating foreign key on [CurrencyID] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [FK_Owner_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[Currencies]
        ([CurrencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Owner_Currency'
CREATE INDEX [IX_FK_Owner_Currency]
ON [dbo].[Owners]
    ([CurrencyID]);
GO

-- Creating foreign key on [LanguageID] in table 'DisplayItems'
ALTER TABLE [dbo].[DisplayItems]
ADD CONSTRAINT [FK_DisplayItem_Language]
    FOREIGN KEY ([LanguageID])
    REFERENCES [dbo].[Languages]
        ([LanguageID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DisplayItem_Language'
CREATE INDEX [IX_FK_DisplayItem_Language]
ON [dbo].[DisplayItems]
    ([LanguageID]);
GO

-- Creating foreign key on [ParentProductCategoryID] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [FK_ProductCategory_ProductCategory1]
    FOREIGN KEY ([ParentProductCategoryID])
    REFERENCES [dbo].[ProductCategories]
        ([ProductCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory_ProductCategory1'
CREATE INDEX [IX_FK_ProductCategory_ProductCategory1]
ON [dbo].[ProductCategories]
    ([ParentProductCategoryID]);
GO

-- Creating foreign key on [GameScheduleID] in table 'GameInteractions'
ALTER TABLE [dbo].[GameInteractions]
ADD CONSTRAINT [FK_GameInteraction_GameSchedule]
    FOREIGN KEY ([GameScheduleID])
    REFERENCES [dbo].[GameSchedules]
        ([GameScheduleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameInteraction_GameSchedule'
CREATE INDEX [IX_FK_GameInteraction_GameSchedule]
ON [dbo].[GameInteractions]
    ([GameScheduleID]);
GO

-- Creating foreign key on [MemberID] in table 'GameInteractions'
ALTER TABLE [dbo].[GameInteractions]
ADD CONSTRAINT [FK_GameInteration_Member_MemberID]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([MemberID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameInteration_Member_MemberID'
CREATE INDEX [IX_FK_GameInteration_Member_MemberID]
ON [dbo].[GameInteractions]
    ([MemberID]);
GO

-- Creating foreign key on [MemberInGameID] in table 'PaymentTransactions'
ALTER TABLE [dbo].[PaymentTransactions]
ADD CONSTRAINT [FK_PaymentTransaction_MemberInGame]
    FOREIGN KEY ([MemberInGameID])
    REFERENCES [dbo].[MemberInGames]
        ([MemberInGameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTransaction_MemberInGame'
CREATE INDEX [IX_FK_PaymentTransaction_MemberInGame]
ON [dbo].[PaymentTransactions]
    ([MemberInGameID]);
GO

-- Creating foreign key on [CurrencyID] in table 'ProductInGames'
ALTER TABLE [dbo].[ProductInGames]
ADD CONSTRAINT [FK_ProductInGame_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[Currencies]
        ([CurrencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInGame_Currency'
CREATE INDEX [IX_FK_ProductInGame_Currency]
ON [dbo].[ProductInGames]
    ([CurrencyID]);
GO

-- Creating foreign key on [GameID] in table 'ProductInGames'
ALTER TABLE [dbo].[ProductInGames]
ADD CONSTRAINT [FK_ProductInGame_Game]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInGame_Game'
CREATE INDEX [IX_FK_ProductInGame_Game]
ON [dbo].[ProductInGames]
    ([GameID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductInGames'
ALTER TABLE [dbo].[ProductInGames]
ADD CONSTRAINT [FK_ProductInGame_Product_ProductID]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInGame_Product_ProductID'
CREATE INDEX [IX_FK_ProductInGame_Product_ProductID]
ON [dbo].[ProductInGames]
    ([ProductID]);
GO

-- Creating foreign key on [ProductInGameID] in table 'ProductLocations'
ALTER TABLE [dbo].[ProductLocations]
ADD CONSTRAINT [FK_ProductLocation_ProductInGame]
    FOREIGN KEY ([ProductInGameID])
    REFERENCES [dbo].[ProductInGames]
        ([ProductInGameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductLocation_ProductInGame'
CREATE INDEX [IX_FK_ProductLocation_ProductInGame]
ON [dbo].[ProductLocations]
    ([ProductInGameID]);
GO

-- Creating foreign key on [ProductInGameID] in table 'ProductPlayeds'
ALTER TABLE [dbo].[ProductPlayeds]
ADD CONSTRAINT [FK_ProductPlayed_ProductInGame]
    FOREIGN KEY ([ProductInGameID])
    REFERENCES [dbo].[ProductInGames]
        ([ProductInGameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPlayed_ProductInGame'
CREATE INDEX [IX_FK_ProductPlayed_ProductInGame]
ON [dbo].[ProductPlayeds]
    ([ProductInGameID]);
GO

-- Creating foreign key on [MemberInGameID] in table 'TrackingTransactions'
ALTER TABLE [dbo].[TrackingTransactions]
ADD CONSTRAINT [FK_TrackingTransaction_MemberInGame]
    FOREIGN KEY ([MemberInGameID])
    REFERENCES [dbo].[MemberInGames]
        ([MemberInGameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackingTransaction_MemberInGame'
CREATE INDEX [IX_FK_TrackingTransaction_MemberInGame]
ON [dbo].[TrackingTransactions]
    ([MemberInGameID]);
GO

-- Creating foreign key on [GameTypeID] in table 'GameTemplates'
ALTER TABLE [dbo].[GameTemplates]
ADD CONSTRAINT [FK_GameTemplate_GameType_GameTypeID]
    FOREIGN KEY ([GameTypeID])
    REFERENCES [dbo].[GameTypes]
        ([GameTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTemplate_GameType_GameTypeID'
CREATE INDEX [IX_FK_GameTemplate_GameType_GameTypeID]
ON [dbo].[GameTemplates]
    ([GameTypeID]);
GO

-- Creating foreign key on [GameID] in table 'GameRules'
ALTER TABLE [dbo].[GameRules]
ADD CONSTRAINT [FK_GameRule_Game_GameID]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameRule_Game_GameID'
CREATE INDEX [IX_FK_GameRule_Game_GameID]
ON [dbo].[GameRules]
    ([GameID]);
GO

-- Creating foreign key on [GameTemplateID] in table 'GameRules'
ALTER TABLE [dbo].[GameRules]
ADD CONSTRAINT [FK_GameRule_GameTemplate]
    FOREIGN KEY ([GameTemplateID])
    REFERENCES [dbo].[GameTemplates]
        ([GameTemplateID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameRule_GameTemplate'
CREATE INDEX [IX_FK_GameRule_GameTemplate]
ON [dbo].[GameRules]
    ([GameTemplateID]);
GO

-- Creating foreign key on [GameID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_Game]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GameID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_Game1]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MemberSubscriptionType] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_MemberSubscriptionType]
    FOREIGN KEY ([MemberSubscriptionType])
    REFERENCES [dbo].[MemberSubscriptionTypes]
        ([MemberSubscriptionTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_MemberSubscriptionType'
CREATE INDEX [IX_FK_Game_MemberSubscriptionType]
ON [dbo].[Games]
    ([MemberSubscriptionType]);
GO

-- Creating foreign key on [GameID] in table 'ProductInWatchLists'
ALTER TABLE [dbo].[ProductInWatchLists]
ADD CONSTRAINT [FK_ProductInWatchList_GameID_GameID]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInWatchList_GameID_GameID'
CREATE INDEX [IX_FK_ProductInWatchList_GameID_GameID]
ON [dbo].[ProductInWatchLists]
    ([GameID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------