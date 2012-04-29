CREATE TABLE [dbo].[Matches] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [WinnerId]     INT      NOT NULL,
    [LooserId]     INT      NOT NULL,
    [ChallengerId] INT      NOT NULL,
    [LadderWeekId] INT      NOT NULL,
    [DateOfMatch]  DATETIME NULL,
    [G1W]          SMALLINT NOT NULL,
    [G1L]          SMALLINT NOT NULL,
    [G2W]          SMALLINT NOT NULL,
    [G2L]          SMALLINT NOT NULL,
    [G3W]          SMALLINT NULL,
    [G3L]          SMALLINT NULL,
    [WinnerRank]   SMALLINT NOT NULL,
    [LooserRank]   SMALLINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

