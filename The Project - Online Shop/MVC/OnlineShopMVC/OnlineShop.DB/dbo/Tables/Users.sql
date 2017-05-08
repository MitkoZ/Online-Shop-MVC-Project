CREATE TABLE [dbo].[Users] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (200) NOT NULL,
    [PasswordSalt] NVARCHAR (200) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Email]        NVARCHAR (50)  NOT NULL,
    [CityID]       INT            NOT NULL,
    [Address]      NVARCHAR (50)  NOT NULL,
    [CardNumber]   NVARCHAR (50)  NOT NULL,
    [IsAdmin]      BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Users_Cities] FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([ID]),
    CONSTRAINT [UQ_Users_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);







