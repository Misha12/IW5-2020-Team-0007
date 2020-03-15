CREATE TABLE [dbo].[Movies]
(
	[ID] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[OriginalName] VARCHAR(255) NOT NULL,
	[GenreID] INT NOT NULL,
	[TitleImage] NVARCHAR(MAX) NULL,
	[Country] VARCHAR(20) NOT NULL,
	[Length] BIGINT NOT NULL,
	[Description] NVARCHAR(MAX),

	CONSTRAINT [FK_Movies_Genre] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genres]([ID])
)
