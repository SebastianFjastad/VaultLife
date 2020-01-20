CREATE TABLE [dbo].[Contract] (
    [ContractID]     INT           NOT NULL,
    [ContractCode]   VARCHAR (20)  NOT NULL,
    [ContractDetail] VARCHAR (255) NOT NULL,
    [DateInserted]   DATETIME2 (4) NOT NULL,
    [DateUpdated]    DATETIME2 (4) NOT NULL,
    [USR]            VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED ([ContractID] ASC)
);

