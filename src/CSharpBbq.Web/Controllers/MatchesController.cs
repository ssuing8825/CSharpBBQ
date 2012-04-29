using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpBbq.Data.Model.Ladder;
using CSharpBbq.Web.Models;

namespace CSharpBbq.Web.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMatchRepository matchRepository;
        private readonly IRepository ladderRepository;
        private IStandingRepository standingsRepository = new StandingRepository();

        // If you are using Dependency Injection, you can delete the following constructor
        public MatchesController()
            : this(new PlayerRepository(), new MatchRepository(), new LadderRepository())
        {
        }

        public MatchesController(IPlayerRepository playerRepository, IMatchRepository matchRepository, IRepository ladderRepository)
        {
            this.playerRepository = playerRepository;
            this.matchRepository = matchRepository;
            this.ladderRepository = ladderRepository;
        }

        //
        // GET: /Match/

        public ViewResult Index()
        {
            return View(ladderRepository.GetAllMatches(match => match.Winner, match => match.Looser, match => match.LadderWeek));
        }
        public ViewResult IndexReadOnly()
        {
            return View(ladderRepository.GetAllMatches(match => match.Winner, match => match.Looser, match => match.LadderWeek).OrderBy(c => c.DateOfMatch));
        }

        //
        // GET: /Match/Details/5

        public ViewResult Details(int id)
        {
            return View(ladderRepository.GetMatchById(id));
        }

        //
        // GET: /Match/Create

        public ActionResult Create()
        {
            ViewBag.PossibleWinners = playerRepository.GetAllPlayers();
            ViewBag.PossibleLoosers = playerRepository.GetAllPlayers();
            ViewBag.PossibleLadderWeeks = ladderRepository.LadderWeeks();
            return View();
        }

        //
        // POST: /Match/Create

        [HttpPost]
        public ActionResult Create(Match match)
        {
            match.WinnerRank = (short)this.ladderRepository.GetPlayerStanding(match.LadderWeekId, match.WinnerId);
            match.LooserRank = (short)this.ladderRepository.GetPlayerStanding(match.LadderWeekId, match.LooserId);

            if (ModelState.IsValid)
            {
                matchRepository.InsertOrUpdate(match);
                matchRepository.Save();
            //    UpdateStandings(match.LadderWeekId, match.LadderWeekId);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleWinners = playerRepository.GetAllPlayers();
                ViewBag.PossibleLoosers = playerRepository.GetAllPlayers();
                ViewBag.PossibleLadderWeeks = ladderRepository.LadderWeeks();
                return View();
            }
        }

        //private void UpdateStandings(int weekNumber, int weekId)
        //{
        //    var standings = (from c in standingsRepository.Standings(weekId)
        //                     select new PlayerViewModel { CurrentRank = c.Position, Player = c.Player }).ToList();

        //    foreach (var p in standings)
        //    {
        //        p.Matches = ladderRepository.PlayerMatches(p.Player.Id);
        //        p.Wins = p.Matches.Where(c => c.LadderWeek.WeekNumber <= weekNumber).Count(c => c.Winner.Id == p.Player.Id);
        //        p.Loses = p.Matches.Where(c => c.LadderWeek.WeekNumber <= weekNumber).Count(c => c.Looser.Id == p.Player.Id);
        //        p.LastMatch = p.Matches.Count > 0 ? p.Matches.Max(c => c.DateOfMatch).Value.ToShortDateString() : "No Match on Record";
        //        var winpoints = p.Matches.Where(c => c.LadderWeek.WeekNumber <= weekNumber && c.WinnerId == p.Player.Id).Sum(c => c.GetWinnerPoints());
        //        var loosePoints = p.Matches.Where(c => c.LadderWeek.WeekNumber <= weekNumber && c.LooserId == p.Player.Id).Sum(c => c.GetLooserPoints());
        //        p.Points = winpoints + loosePoints;
        //    }

        //    standings.OrderByDescending(c => c.Points).ToList();

        //    var i = 1;
        //    foreach (var p in standings)
        //    {
        //        standingsRepository.Standings

        //        p.CurrentRank = i;
        //        i++;
        //        standingsRepository.InsertOrUpdate(p);
        //    }
        //}

        //
        // GET: /Match/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleWinners = playerRepository.GetAllPlayers();
            ViewBag.PossibleLoosers = playerRepository.GetAllPlayers();
            ViewBag.PossibleLadderWeeks = ladderRepository.LadderWeeks();
            return View(ladderRepository.GetMatchById(id));
        }

        //
        // POST: /Match/Edit/5

        [HttpPost]
        public ActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                matchRepository.InsertOrUpdate(match);
                matchRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleWinners = playerRepository.GetAllPlayers();
                ViewBag.PossibleLoosers = playerRepository.GetAllPlayers();
                return View();
            }
        }

        //
        // GET: /Match/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ladderRepository.GetMatchById(id));
        }

        //
        // POST: /Match/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            matchRepository.Delete(id);
            matchRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

