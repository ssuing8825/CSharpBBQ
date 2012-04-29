using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CSharpBbq.Data.Model.Ladder
{ 
    public class MatchRepository : IMatchRepository
    {
        LadderDbContext context = new LadderDbContext();

        public void InsertOrUpdate(Match match)
        {
            if (match.Id == default(int)) {
                // New entity
                context.Matches.Add(match);
            } else {
                // Existing entity
                context.Matches.Attach(match);
                context.Entry(match).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var match = context.Matches.Find(id);
            context.Matches.Remove(match);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }

	public interface IMatchRepository
    {

		void InsertOrUpdate(Match match);
        void Delete(int id);
        void Save();
    }
}