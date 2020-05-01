CREATE TABLE [dbo].[Newsletters] (
    [NewsletterId]  INT           IDENTITY (1, 1) NOT NULL,
    [CreatedDate]   DATETIME      NULL,
    [CreatedBy]     INT           NULL,
    [Memo]          VARCHAR (256) NULL,
    [DisplayDate]   VARCHAR (256) NULL,
    [PublishedDate] DATETIME      NULL,
    [IsCurrent]     BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([NewsletterId] ASC)
);

