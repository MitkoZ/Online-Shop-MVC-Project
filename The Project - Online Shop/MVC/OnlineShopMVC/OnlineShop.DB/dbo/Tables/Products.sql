CREATE TABLE [dbo].[Products] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [CategoryID] INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Processor]  NVARCHAR (50) NOT NULL,
    [RAM]        INT           NOT NULL,
    [Storage]    INT           NOT NULL,
    [OS]         NVARCHAR (50) NOT NULL,
    [Price]      MONEY         NOT NULL,
    [ImageName]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([ID])
);





