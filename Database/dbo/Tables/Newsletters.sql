CREATE TABLE [dbo].[Newsletters] (
    [NewsletterId] INT IDENTITY(1,1) NOT NULL,
    [CreatedDate] DATETIME,
    [CreatedBy] INT,
    [Memo] VARCHAR(256) NULL,
    [DisplayDate] VARCHAR(256) NULL ,
    [PublishedDate] DATETIME,
    [IsCurrent] BIT NOT NULL DEFAULT 0,

    PRIMARY KEY CLUSTERED ([NewsletterId] ASC) 
);