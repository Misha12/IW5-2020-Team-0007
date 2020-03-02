CREATE TABLE [dbo].[MovieNames]
(
	[Lang] NVARCHAR(5) NOT NULL PRIMARY KEY,
	[MovieID] BIGINT NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,

	CONSTRAINT [FK_MovieNames_Movie] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([ID])
)
