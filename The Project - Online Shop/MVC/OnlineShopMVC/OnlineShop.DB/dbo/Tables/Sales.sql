CREATE TABLE [dbo].[Sales] (
    [ID]         INT      IDENTITY (1, 1) NOT NULL,
    [ProductID]  INT      NOT NULL,
    [UserID]     INT      NOT NULL,
    [DateBought] DATETIME NOT NULL,
    CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Sales_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);



