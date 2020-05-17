CREATE TABLE [Genres] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Movies] (
    [ID] bigint NOT NULL IDENTITY,
    [OriginalName] nvarchar(255) NULL,
    [TitleImageUrl] nvarchar(max) NULL,
    [Country] nvarchar(50) NULL,
    [Length] bigint NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedYear] int NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Persons] (
    [ID] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NULL,
    [Surname] nvarchar(255) NULL,
    [Birthdate] datetime2 NOT NULL,
    [ProfilePictureUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Users] (
    [ID] bigint NOT NULL IDENTITY,
    [Username] nvarchar(255) NOT NULL,
    [Password] nvarchar(512) NOT NULL,
    [Role] int NOT NULL,
    [RegisteredAt] datetime2 NOT NULL,
    [AuthCode] nvarchar(255) NULL,
    [Email] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [GenreMap] (
    [MovieID] bigint NOT NULL,
    [GenreID] int NOT NULL,
    CONSTRAINT [PK_GenreMap] PRIMARY KEY ([MovieID], [GenreID]),
    CONSTRAINT [FK_GenreMap_Genres_GenreID] FOREIGN KEY ([GenreID]) REFERENCES [Genres] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_GenreMap_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [MovieNames] (
    [ID] bigint NOT NULL IDENTITY,
    [Lang] nvarchar(5) NULL,
    [MovieID] bigint NOT NULL,
    [Name] nvarchar(255) NULL,
    CONSTRAINT [PK_MovieNames] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_MovieNames_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [MoviePersons] (
    [PersonID] bigint NOT NULL,
    [MovieID] bigint NOT NULL,
    [Type] int NOT NULL,
    CONSTRAINT [PK_MoviePersons] PRIMARY KEY ([MovieID], [PersonID]),
    CONSTRAINT [FK_MoviePersons_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_MoviePersons_Persons_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Persons] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [Ratings] (
    [ID] bigint NOT NULL IDENTITY,
    [MovieID] bigint NOT NULL,
    [UserID] bigint NOT NULL,
    [Score] int NOT NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Ratings_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Ratings_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [RefreshTokens] (
    [Token] nvarchar(512) NOT NULL,
    [UserID] bigint NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Token]),
    CONSTRAINT [FK_RefreshTokens_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([ID]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_GenreMap_GenreID] ON [GenreMap] ([GenreID]);
GO


CREATE INDEX [IX_MovieNames_MovieID] ON [MovieNames] ([MovieID]);
GO


CREATE INDEX [IX_MoviePersons_PersonID] ON [MoviePersons] ([PersonID]);
GO


CREATE INDEX [IX_Ratings_MovieID] ON [Ratings] ([MovieID]);
GO


CREATE INDEX [IX_Ratings_UserID] ON [Ratings] ([UserID]);
GO


CREATE INDEX [IX_RefreshTokens_UserID] ON [RefreshTokens] ([UserID]);
GO



