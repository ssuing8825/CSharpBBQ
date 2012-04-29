ALTER TABLE [dbo].[Standings]
    ADD CONSTRAINT [Player_Standings] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

