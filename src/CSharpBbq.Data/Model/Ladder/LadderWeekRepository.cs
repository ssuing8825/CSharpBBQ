using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CSharpBbq.Data.Model.Ladder;

namespace CSharpBbq.Web.Models
{ 
    public class LadderWeekRepository : ILadderWeekRepository
    {
        LadderDbContext context = new LadderDbContext();

        public IEnumerable<LadderWeek> GetAllLadderWeeks(params Expression<Func<LadderWeek, object>>[] includeProperties)
        {
            IQueryable<LadderWeek> query = context.LadderWeeks;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public LadderWeek GetById(int id)
        {
            return context.LadderWeeks.Find(id);
        }

        public void InsertOrUpdate(LadderWeek ladderweek)
        {
            if (ladderweek.Id == default(int)) {
                // New entity
                context.LadderWeeks.Add(ladderweek);
            } else {
                // Existing entity
                context.LadderWeeks.Attach(ladderweek);
                context.Entry(ladderweek).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ladderweek = context.LadderWeeks.Find(id);
            context.LadderWeeks.Remove(ladderweek);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface ILadderWeekRepository
    {
		IEnumerable<LadderWeek> GetAllLadderWeeks(params Expression<Func<LadderWeek, object>>[] includeProperties);
		LadderWeek GetById(int id);
		void InsertOrUpdate(LadderWeek ladderweek);
        void Delete(int id);
        void Save();
    }
}