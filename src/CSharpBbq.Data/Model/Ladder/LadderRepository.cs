using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CSharpBbq.Data.Model.Ladder
{
    public class LadderRepository : IRepository
    {
        public List<LadderWeek> LadderWeeks()
        {
            using (var db = new LadderDbContext())
            {
                var c = from LadderWeek s in db.LadderWeeks.Include(l => l.Standings)
                        select s;

                return c.ToList();
            }
        }
        public List<Match> Matches(int weekId)
        {
            using (var db = new LadderDbContext())
            {
                var c = from Match s in db.Matches.Include(w => w.Winner).Include(l => l.Looser)
                        where s.LadderWeek.Id == weekId
                        orderby s.DateOfMatch
                        select s;
                return c.ToList();
            }
        }

        public List<Match> PlayerMatches(int playerId)
        {
            using (var db = new LadderDbContext())
            {
                var c = from Match s in db.Matches.Include(w => w.Winner).Include(l => l.Looser).Include(t => t.LadderWeek)
                        where s.LooserId == playerId || s.WinnerId == playerId
                        orderby s.DateOfMatch
                        select s;
                return c.ToList();
            }
        }
        public int GetPlayerStanding(int weekId, int playerId)
        {
            using (var db = new LadderDbContext())
            {
                return (from s in db.Standings
                        where s.LadderWeekId == weekId && s.PlayerId == playerId
                        select s.Position).First();

            }

        }
        public List<Match> CurrentMatches()
        {
            using (var db = new LadderDbContext())
            {
                var c = from Match s in db.Matches.Include(w => w.Winner).Include(l => l.Looser)
                        where s.LadderWeek.IsCurrent
                        orderby s.DateOfMatch
                        select s;
                return c.ToList();
            }
        }
        public IEnumerable<Match> GetAllMatches(params Expression<Func<Match, object>>[] includeProperties)
        {
            using (var db = new LadderDbContext())
            {
                IQueryable<Match> query = db.Matches;
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }

        }
        public LadderWeek CurrentWeek()
        {
            using (var db = new LadderDbContext())
            {
                return db.LadderWeeks.Where(c => c.IsCurrent).First();
            }
        }

        public LadderWeek LadderWeek(int weekId)
        {
            using (var db = new LadderDbContext())
            {
                return db.LadderWeeks.Find(weekId);
            }
        }


        public Player Player(int playerId)
        {
            using (var db = new LadderDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var c = from Player s in db.Players.Include("Standings").Include("Standings.LadderWeek")
                        where s.Id == playerId
                        select s;
                return c.First();
            }
        }
        public Match GetMatchById(int id)
        {
            using (var db = new LadderDbContext())
            {
                return db.Matches.Find(id);
            }
        }

    }
}
