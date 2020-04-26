CREATE TABLE [dbo].[Newsletters] (
    [NewsletterId] INT NOT NULL,
    [CreatedDate] DATETIME,
    [CreatedBy] INT,
    [Memo] VARCHAR(50) NULL,
    [PublishedDate] DATETIME,
    [IsCurrent] BIT NOT NULL DEFAULT 0,

    PRIMARY KEY CLUSTERED ([NewsletterId] ASC) 
);
