ALTER TABLE [dbo].[Matches]
    ADD CONSTRAINT [Match_Challenger] FOREIGN KEY ([ChallengerId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

