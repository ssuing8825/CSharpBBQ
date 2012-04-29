using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CSharpBbq.Data.Model.Ladder
{
    public interface IRepository
    {
        List<Match> CurrentMatches();
        LadderWeek CurrentWeek();
        List<LadderWeek> LadderWeeks();
        LadderWeek LadderWeek(int weekId);
        List<Match> Matches(int weekId);
        Player Player(int playerId);
        List<Match> PlayerMatches(int playerId);
        IEnumerable<Match> GetAllMatches(params Expression<Func<Match, object>>[] includeProperties);
        
        Match GetMatchById(int id);
        int GetPlayerStanding(int weekId, int playerId);

    }
}
