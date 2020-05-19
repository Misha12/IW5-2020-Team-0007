CREATE TABLE [GenreMap] (
    [ID] bigint NOT NULL IDENTITY,
    [MovieID] bigint NOT NULL,
    [GenreID] int NOT NULL,
    CONSTRAINT [PK_GenreMap] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_GenreMap_Genres_GenreID] FOREIGN KEY ([GenreID]) REFERENCES [Genres] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_GenreMap_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [MoviePersons] (
    [ID] bigint NOT NULL IDENTITY,
    [PersonID] bigint NOT NULL,
    [MovieID] bigint NOT NULL,
    [Type] int NOT NULL,
    CONSTRAINT [PK_MoviePersons] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_MoviePersons_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_MoviePersons_Persons_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Persons] ([ID]) ON DELETE CASCADE
);

CREATE INDEX [IX_GenreMap_GenreID] ON [GenreMap] ([GenreID]);
CREATE INDEX [IX_GenreMap_MovieID] ON [GenreMap] ([MovieID]);
CREATE INDEX [IX_MoviePersons_MovieID] ON [MoviePersons] ([MovieID]);
CREATE INDEX [IX_MoviePersons_PersonID] ON [MoviePersons] ([PersonID]);
