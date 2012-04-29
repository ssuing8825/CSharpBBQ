ALTER TABLE [dbo].[Standings]
    ADD CONSTRAINT [Standing_LadderWeek] FOREIGN KEY ([LadderWeekId]) REFERENCES [dbo].[LadderWeeks] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

