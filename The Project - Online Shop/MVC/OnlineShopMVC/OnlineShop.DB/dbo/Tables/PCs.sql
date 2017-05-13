CREATE TABLE [dbo].[PCs] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [ProductID] INT           NOT NULL,
    [VideoCard] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PCs_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID])
);



