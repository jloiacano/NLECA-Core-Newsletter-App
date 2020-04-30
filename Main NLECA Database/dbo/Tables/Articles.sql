CREATE TABLE [dbo].[Articles]
(
	[ArticleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[NewsletterId] INT NOT NULL,
	[Sequence] INT NOT NULL,
	[ImageFileLocation] VARCHAR(256) NULL,
	[ArticleType] INT NOT NULL DEFAULT 0,
	[Text] VARCHAR(MAX) NOT NULL, 
	[AddedBy] INT NOT NULL,
	[DateAdded] DATETIME NOT NULL,
    CONSTRAINT Article_Newsletter
		FOREIGN KEY (NewsletterId)
		REFERENCES Newsletters (NewsletterId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);