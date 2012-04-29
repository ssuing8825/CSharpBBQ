using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace CSharpBbq.Data.Model.Ladder
{ 
    public class PlayerRepository : IPlayerRepository
    {
        LadderDbContext context = new LadderDbContext();

        public IEnumerable<Player> GetAllPlayers(params Expression<Func<Player, object>>[] includeProperties)
        {
            IQueryable<Player> query = context.Players;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.ToList().OrderBy(c=>c.Name);
        }

        public Player GetById(int id)
        {
            return context.Players.Find(id);
        }

        public void InsertOrUpdate(Player player)
        {
            if (player.Id == default(int))
            {
                // New entity
                context.Players.Add(player);
            } else {
                // Existing entity
                context.Players.Attach(player);
                context.Entry(player).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var player = context.Players.Find(id);
            context.Players.Remove(player);
        }

        public void Save()
        {
            context.SaveChanges();
        }
     
    }

    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers(params Expression<Func<Player, object>>[] includeProperties);
		Player GetById(int id);
        void InsertOrUpdate(Player player);
        void Delete(int id);
        void Save();
    }
}