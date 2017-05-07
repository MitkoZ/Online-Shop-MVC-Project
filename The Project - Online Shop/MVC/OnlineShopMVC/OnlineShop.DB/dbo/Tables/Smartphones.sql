CREATE TABLE [dbo].[Smartphones] (
    [ID]          INT           NOT NULL,
    [ProductID]   INT           NOT NULL,
    [Camera]      NVARCHAR (50) NOT NULL,
    [SIMCardType] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Smartphones_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID])
);

