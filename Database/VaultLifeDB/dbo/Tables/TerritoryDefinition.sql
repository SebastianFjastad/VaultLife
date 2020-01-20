CREATE TABLE [dbo].[TerritoryDefinition] (
    [TerritoryDefinitionID]   INT           IDENTITY (1, 1) NOT NULL,
    [TerritoryDefinitionCode] VARCHAR (20)  NOT NULL,
    [TerritoryID]             INT           NOT NULL,
    [ZipOrPostalCode]         VARCHAR (20)  NULL,
    [IPAddress]               VARCHAR (30)  NULL,
    [PhysicalCoordinates]     VARCHAR (30)  NULL,
    [DateInserted]            DATETIME2 (4) NOT NULL,
    [DateUpdated]             DATETIME2 (4) NOT NULL,
    [USR]                     VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_TerritoryDefinition] PRIMARY KEY CLUSTERED ([TerritoryDefinitionID] ASC),
    CONSTRAINT [FK_TerritoryDefinition_Territory_TerritoryID] FOREIGN KEY ([TerritoryID]) REFERENCES [dbo].[Territory] ([TerritoryID])
);

