CREATE TABLE [dbo].[Articles]
(
	[ArticleId] INT NOT NULL PRIMARY KEY,
	[NewsletterId] INT NOT NULL,

	[Text] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT Article_Newsletter
		FOREIGN KEY (NewsletterId)
		REFERENCES Newsletters (NewsletterId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);