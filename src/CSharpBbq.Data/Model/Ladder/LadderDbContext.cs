using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;


namespace CSharpBbq.Data.Model.Ladder
{
    public class LadderDbContext : DbContext
    {
        public LadderDbContext()
            : base("CSharpBbqLadder") 
        {
        }

        public void ChangeObjectState<T>(T entity, EntityState entityState)
        {
           
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
            ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.ChangeObjectState(entity, entityState);

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ComplexTypeConfiguration<IncludeMetadataConvention>());

            modelBuilder.Entity<Match>()
                    .HasRequired(a => a.Looser)
                    .WithMany()
                    .HasForeignKey(u => u.LooserId);

            modelBuilder.Entity<Match>()
                        .HasRequired(a => a.Winner)
                        .WithMany()
                        .HasForeignKey(u => u.WinnerId).WillCascadeOnDelete(false); ;

            modelBuilder.Entity<Match>()
                     .HasRequired(a => a.Challenger)
                     .WithMany()
                     .HasForeignKey(u => u.ChallengerId).WillCascadeOnDelete(false); ;

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<LadderWeek> LadderWeeks { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Standing> Standings { get; set; }
        public DbSet<Match> Matches { get; set; }

        public class Initializer : DropCreateDatabaseIfModelChanges<LadderDbContext>
        {
            public void InitializeDatabase(LadderDbContext context)
            {
                if (!context.Database.Exists() || !context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }

            protected override void Seed(LadderDbContext context)
            {
                var s = new DateTime(2011, 3, 21);

                for (var i = 0; i < 14; i++)
                {
                    context.LadderWeeks.Add(new LadderWeek
                               {
                                   WeekNumber = i + 1,
                                   StertDate = s.AddDays(i * 7),
                                   EndDate = s.AddDays((i * 7) + 6)
                               });
                }
                context.LadderWeeks.Local[0].IsCurrent = true;

                var p = new List<Player> {new Player() {Name = "Chris R", Email = "chris.russell05@gmail.com", Rank=1},
                new Player() { Name = "Christopher B.", Email = "infobarringer@earthlink.net", Rank=2 },
                new Player() { Name = "Dallas M.", Email = "dmackie@us.ibm.com", Rank=2 },
                new Player() { Name = "Egbert G.", Email = "Gracias@lenovo.com", Rank=3 },
                new Player() { Name = "George S.", Email = "george_seamen@ml.com", Rank=4 },
                new Player() { Name = "Dan V.", Email = "DValentino@colorcon.com", Rank=5 },
                new Player() { Name = "Scott H.", Email = "smhewlett5@yahoo.com", Rank=6 },
                new Player() { Name = "Stan C.", Email = "craft.stan@gmail.com", Rank=7 },
                new Player() { Name = "Steven S.", Email = "rb@csharp.com" , Rank=8},
                new Player() { Name = "Manish P.", Email = "parekhmanish@yahoo.com", Rank=9},
                new Player() { Name = "Mike F." ,Email = "mfarmer@gmail.com", Rank=10},
                new Player() { Name = "Mike V.", Email = "michael.valentino@crbusa.com", Rank=11 },
                new Player() { Name = "Scott M." , Email = "scott_1308@yahoo.com", Rank=12},
                new Player() { Name = "Robbie M.", Email = "robbie@matesevac.com", Rank=13 },
                new Player() { Name = "Sherrie P.", Email = "Sherrie.Peacock@smithmoorelaw.com", Rank=14 }};

                foreach (var player in p)
                {
                    context.Players.Add(player);
                    context.Standings.Add(new Standing { Player = player, Position = player.Rank, LadderWeek = context.LadderWeeks.Local[0] });
                }

                //Dallas over Dan V.
                context.Matches.Add(new Match
                                        {
                                            DateOfMatch = new DateTime(2011, 3, 21),
                                            LadderWeek = context.LadderWeeks.Local[0],
                                            WinnerId = 3,
                                            Winner =context.Players.Local[2],
                                            LooserId = 6,
                                            Looser = context.Players.Local[5],
                                            G1W = 15,
                                            G1L = 2,
                                            G2W = 15,
                                            G2L = 9,
                                            Challenger = context.Players.Local[2],
                                            ChallengerId = 6,
                                            WinnerRank = context.Players.Local[2].Rank,
                                            LooserRank = context.Players.Local[5].Rank

                                        });

                //Mike V over Mike F.
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 21),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 12,
                    Winner = context.Players.Local[11],
                    LooserId = 11,
                    Looser = context.Players.Local[10],
                    G1W = 15,
                    G1L = 10,
                    G2W = 15,
                    G2L = 10,
                    Challenger = context.Players.Local[10],
                    ChallengerId = 11,
                    WinnerRank = context.Players.Local[10].Rank,
                    LooserRank = context.Players.Local[11].Rank
                });

                //Dan V over George S.
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 23),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 6,
                    Winner = context.Players.Local[5],
                    LooserId = 5,
                    Looser = context.Players.Local[4],
                    G1W = 15,
                    G1L = 0,
                    G2W = 15,
                    G2L = 2,
                    Challenger = context.Players.Local[5],
                    ChallengerId = 6,
                    WinnerRank = context.Players.Local[5].Rank,
                    LooserRank = context.Players.Local[4].Rank
                });

                // Stan over Steve
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 23),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 8,
                    Winner = context.Players.Local[7],
                    LooserId = 9,
                    Looser = context.Players.Local[8],
                    G1W = 15,
                    G1L = 5,
                    G2W = 15,
                    G2L = 6,
                    Challenger = context.Players.Local[8],
                    ChallengerId = 9,
                    WinnerRank = context.Players.Local[7].Rank,
                    LooserRank = context.Players.Local[8].Rank
                });

                //8 beat 7
                // Stan over Scott H
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 26),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 8,
                    Winner = context.Players.Local[7],
                    LooserId = 7,
                    Looser = context.Players.Local[6],
                    G1W = 15,
                    G1L = 8,
                    G2W = 14,
                    G2L = 15,
                    G3W = 11,
                    G3L = 10,
                    Challenger = context.Players.Local[6],
                    ChallengerId = 7,
                    WinnerRank = context.Players.Local[7].Rank,
                    LooserRank = context.Players.Local[6].Rank
                });


                //Scott M over Mike F
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 23),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 13,
                    Winner = context.Players.Local[12],
                    LooserId = 11,
                    Looser = context.Players.Local[10],
                    G1W = 15,
                    G1L = 4,
                    G2W = 15,
                    G2L = 6,
                    Challenger = context.Players.Local[12],
                    ChallengerId = 13,
                    WinnerRank = context.Players.Local[12].Rank,
                    LooserRank = context.Players.Local[10].Rank
                });

                //Chris R v Chris B
                context.Matches.Add(new Match
                {
                    DateOfMatch = new DateTime(2011, 3, 24),
                    LadderWeek = context.LadderWeeks.Local[0],
                    WinnerId = 1,
                    Winner = context.Players.Local[0],
                    LooserId = 2,
                    Looser = context.Players.Local[1],
                    G1W = 15,
                    G1L = 0,
                    G2W = 15,
                    G2L = 0,
                    Challenger = context.Players.Local[0],
                    ChallengerId = 1,
                    WinnerRank = context.Players.Local[0].Rank,
                    LooserRank = context.Players.Local[1].Rank
                });

                context.SaveChanges();
                base.Seed(context);
            }
        }
    }
}
