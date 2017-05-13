CREATE TABLE [dbo].[Smartphones] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [ProductID]   INT           NOT NULL,
    [Camera]      NVARCHAR (50) NOT NULL,
    [SIMCardType] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Smartphones_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID])
);



