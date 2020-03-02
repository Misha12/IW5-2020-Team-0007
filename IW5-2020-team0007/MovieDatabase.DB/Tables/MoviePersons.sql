CREATE TABLE [dbo].[MoviePersons]
(
	[MovieID] BIGINT NOT NULL,
	[PersonID] BIGINT NOT NULL,
	[Type] INT NOT NULL,

	CONSTRAINT [MoviePerson_PK] PRIMARY KEY (MovieID, PersonID),
	CONSTRAINT [FK_MoviePerson_Movies] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([ID]),
	CONSTRAINT [FK_MoviePerson_Persons] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Persons]([ID])
);
