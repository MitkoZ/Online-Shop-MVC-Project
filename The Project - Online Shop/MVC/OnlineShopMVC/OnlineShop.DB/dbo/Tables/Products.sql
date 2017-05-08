CREATE TABLE [dbo].[Products] (
    [ID]         INT           NOT NULL,
    [CategoryID] INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Processor]  NVARCHAR (50) NOT NULL,
    [RAM]        INT           NOT NULL,
    [Storage]    INT           NOT NULL,
    [OS]         NVARCHAR (50) NOT NULL,
    [Price]      MONEY         NOT NULL,
    [ImageName]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([ID])
);



