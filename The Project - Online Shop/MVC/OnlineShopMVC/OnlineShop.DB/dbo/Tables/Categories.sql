CREATE TABLE [dbo].[Categories] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Categories_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

