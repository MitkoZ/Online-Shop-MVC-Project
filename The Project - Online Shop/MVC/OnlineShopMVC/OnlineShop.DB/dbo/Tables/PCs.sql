CREATE TABLE [dbo].[PCs] (
    [ID]        INT           NOT NULL,
    [ProductID] INT           NOT NULL,
    [VideoCard] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PCs] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PCs_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID])
);

