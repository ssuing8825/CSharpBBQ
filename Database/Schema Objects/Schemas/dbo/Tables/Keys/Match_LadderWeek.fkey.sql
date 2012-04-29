ALTER TABLE [dbo].[Matches]
    ADD CONSTRAINT [Match_LadderWeek] FOREIGN KEY ([LadderWeekId]) REFERENCES [dbo].[LadderWeeks] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

