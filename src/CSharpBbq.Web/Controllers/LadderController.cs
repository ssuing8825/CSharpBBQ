using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpBbq.Data.Model.Ladder;
using CSharpBbq.Web.Models;

namespace CSharpBbq.Web.Controllers
{
    public class LadderController : Controller
    {
        private IRepository repository = new LadderRepository();
        private IStandingRepository standingsRepository = new StandingRepository();


        //  [OutputCache(Duration = 32000, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View("Home", CreateWeekData(repository.CurrentWeek().Id));
        }

        //   [OutputCache(Duration = 32000, VaryByParam = "none")]
        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }
        // [OutputCache(Duration = 32000, VaryByParam = "none")]
        public ActionResult Legal()
        {
            return View();
        }

        //  [OutputCache(Duration = 32000, VaryByParam = "id")]
        public ActionResult Week(int id)
        {
            return View("Index", CreateWeekData(id));
        }
        private LadderWeekViewModel CreateWeekData(int id)
        {
            var data = new LadderWeekViewModel
            {
                LadderWeek = repository.LadderWeek(id),
                Matches = repository.Matches(id),
                Standings = (from c in standingsRepository.Standings(id)
                             select new PlayerViewModel { CurrentRank = c.Position, Player = c.Player }).ToList()
            };

            foreach (var p in data.Standings)
            {
                p.Matches = repository.PlayerMatches(p.Player.Id);
                p.Wins = p.Matches.Where(c => c.LadderWeek.WeekNumber <= data.LadderWeek.WeekNumber).Count(c => c.Winner.Id == p.Player.Id);
                p.Loses = p.Matches.Where(c => c.LadderWeek.WeekNumber <= data.LadderWeek.WeekNumber).Count(c => c.Looser.Id == p.Player.Id);
                p.LastMatch = p.Matches.Count > 0 ? p.Matches.Max(c => c.DateOfMatch).Value.ToShortDateString() : "No Match on Record";
                var winpoints = p.Matches.Where(c => c.LadderWeek.WeekNumber <= data.LadderWeek.WeekNumber && c.WinnerId == p.Player.Id).Sum(c => c.GetWinnerPoints());
                var loosePoints = p.Matches.Where(c => c.LadderWeek.WeekNumber <= data.LadderWeek.WeekNumber && c.LooserId == p.Player.Id).Sum(c => c.GetLooserPoints());
                p.Points = winpoints + loosePoints;

            }
            data.Standings = data.Standings.OrderByDescending(c => c.Points).ToList();

            return data;
        }

        public ActionResult SingleWeekResult()
        {
            return PartialView();
        }
        //   [OutputCache(Duration = 32000, VaryByParam = "id")]
        public ActionResult Player(int id)
        {
            var data = new PlayerViewModel { Player = repository.Player(id) };

            data.Matches = repository.PlayerMatches(id);
            data.Wins = data.Matches.Count(c => c.Winner.Id == data.Player.Id);
            data.Loses = data.Matches.Count(c => c.Looser.Id == data.Player.Id);
            data.LastMatch = data.Matches.Count > 0 ? data.Matches.Max(c => c.DateOfMatch).Value.ToShortDateString() : "No Match on Record";
            data.CurrentRank = standingsRepository.CurrentStandings().Where(c => c.Player.Id == id).First().Position;
            return View(data);
        }

        public ActionResult Weeks()
        {
            var lw = repository.LadderWeeks();

            return PartialView(lw);
        }
    }
}
