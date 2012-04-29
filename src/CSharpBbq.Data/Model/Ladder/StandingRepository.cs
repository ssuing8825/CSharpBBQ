using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CSharpBbq.Data.Model.Ladder;

namespace CSharpBbq.Data.Model.Ladder
{
    public class StandingRepository : IStandingRepository
    {
        readonly LadderDbContext context = new LadderDbContext();

        public IEnumerable<Standing> GetAllStandings(params Expression<Func<Standing, object>>[] includeProperties)
        {
            IQueryable<Standing> query = context.Standings;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public Standing GetById(int id)
        {
            return context.Standings.Find(id);
        }

        public void InsertOrUpdate(Standing standing)
        {
            if (standing.Id == default(int))
            {
                // New entity
                context.Standings.Add(standing);
            }
            else
            {
                // Existing entity
                context.Standings.Attach(standing);
                context.Entry(standing).State = EntityState.Modified;
              
            }
        }

        public void Delete(int id)
        {
            var standing = context.Standings.Find(id);
            context.Standings.Remove(standing);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Standing> Standings(int weekId)
        {
            using (var db = new LadderDbContext())
            {
                var c = from Standing s in db.Standings.Include(l => l.Player)
                        where s.LadderWeek.Id == weekId
                        orderby s.Position
                        select s;
                return c.ToList();
            }
        }

        public List<Standing> CurrentStandings()
        {
            using (var db = new LadderDbContext())
            {
                var c = from Standing s in db.Standings.Include(l => l.Player)
                        where s.LadderWeek.IsCurrent
                        orderby s.Position
                        select s;
                return c.ToList();
            }
        }
    }

    public interface IStandingRepository
    {
        IEnumerable<Standing> GetAllStandings(params Expression<Func<Standing, object>>[] includeProperties);
        Standing GetById(int id);
        void InsertOrUpdate(Standing standing);
        void Delete(int id);
        void Save();
        List<Standing> Standings(int weekId);
        List<Standing> CurrentStandings();
      
    }
}