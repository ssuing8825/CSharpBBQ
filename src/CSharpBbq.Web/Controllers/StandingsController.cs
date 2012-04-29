using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpBbq.Data.Model.Ladder;
using CSharpBbq.Web.Models;

namespace CSharpBbq.Web.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IStandingRepository standingRepository;
        private readonly ILadderWeekRepository ladderWeekRepository;
        private IRepository repository = new LadderRepository();

        // If you are using Dependency Injection, you can delete the following constructor
        public StandingsController()
            : this(new StandingRepository(), new LadderRepository(), new LadderWeekRepository())
        {
        }

        public StandingsController(IStandingRepository standingRepository, IRepository repository, ILadderWeekRepository ladderWeekRepository)
        {
            this.standingRepository = standingRepository;
            this.repository = repository;
            this.ladderWeekRepository = ladderWeekRepository;

        }

        //
        // GET: /Standing/

        public ViewResult Index()
        {
            return View(standingRepository.GetAllStandings());
        }

        //
        // GET: /Standing/Details/5

        public ViewResult Details(int id)
        {
            return View(standingRepository.GetById(id));
        }

        //
        // GET: /Standing/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Standing/Create

        [HttpPost]
        public ActionResult Create(Standing standing)
        {
            if (ModelState.IsValid)
            {
                standingRepository.InsertOrUpdate(standing);
                standingRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Standing/Edit/5

        public ActionResult Edit(int id)
        {
            return View(id);
        }

        //
        // POST: /Standing/Edit/5

        [HttpPost]
        public ActionResult Edit(Standing standing)
        {
            if (ModelState.IsValid)
            {
                standingRepository.InsertOrUpdate(standing);



                standingRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
          
        }

        //
        // GET: /Standing/Delete/5

        public ActionResult Delete(int id)
        {
            return View(standingRepository.GetById(id));
        }

        public ActionResult CreateNewWeek(int id)
        {
            var currentWeek = repository.CurrentWeek();
            var newStandings = GetNewStandings(currentWeek.WeekNumber, id);

            var position = 1;
            foreach (var s in newStandings.OrderByDescending(g => g.TotalPoints))
            {
                s.Position = position;
                position++;
                standingRepository.InsertOrUpdate(s);
            }

            standingRepository.Save();

            currentWeek.IsCurrent = false;
            ladderWeekRepository.InsertOrUpdate(currentWeek);

            var nextWeek = ladderWeekRepository.GetById(id);
            nextWeek.IsCurrent = true;
            ladderWeekRepository.InsertOrUpdate(nextWeek);

            return View("Edit", id);
        }
        private List<Standing> GetNewStandings(int currentWeekNumber, int nextWeekId)
        {
            var matches = repository.GetAllMatches(c => c.LadderWeek, c => c.Winner, c => c.Looser);

            var currentStandings = standingRepository.CurrentStandings();
            var newStandings = new List<Standing>();
            foreach (var currentStanding in currentStandings)
            {
                var winpoints = matches.Where(c => c.LadderWeek.WeekNumber <= currentWeekNumber && c.WinnerId == currentStanding.PlayerId).Sum(c => c.GetWinnerPoints());
                var loosePoints = matches.Where(c => c.LadderWeek.WeekNumber <= currentWeekNumber && c.LooserId == currentStanding.PlayerId).Sum(c => c.GetLooserPoints());
                newStandings.Add(new Standing { LadderWeekId = nextWeekId, PlayerId = currentStanding.PlayerId, TotalPoints = winpoints + loosePoints });
            }
            return newStandings;
        }
        //
        // POST: /Standing/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            standingRepository.Delete(id);
            standingRepository.Save();

            return RedirectToAction("Index");
        }


        public JsonResult GridData(int ladderWeekId, int rows, int page)
        {
            var count = standingRepository.GetAllStandings().Count();
            var pageData = standingRepository.GetAllStandings(c => c.Player).Where(c => c.LadderWeekId == ladderWeekId).Select(c => new { StandingId = c.Id, c.Player.Name, c.Position, c.PlayerId, c.LadderWeekId }).OrderBy(c => c.Position);
            return Json(new
                            {
                                page = page,
                                records = count,
                                rows = pageData
                            }, JsonRequestBehavior.AllowGet);
        }

    }
}

