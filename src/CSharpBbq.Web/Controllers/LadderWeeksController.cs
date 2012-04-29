using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpBbq.Data.Model.Ladder;
using CSharpBbq.Web.Models;

namespace CSharpBbq.Web.Controllers
{   
    public class LadderWeeksController : Controller
    {
		private readonly ILadderWeekRepository ladderweekRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LadderWeeksController() : this(new LadderWeekRepository())
        {
        }

        public LadderWeeksController(ILadderWeekRepository ladderweekRepository)
        {
			this.ladderweekRepository = ladderweekRepository;
        }

        //
        // GET: /LadderWeek/

        public ViewResult Index()
        {
            return View(ladderweekRepository.GetAllLadderWeeks(ladderweek => ladderweek.Standings, ladderweek => ladderweek.Matches));
        }

        //
        // GET: /LadderWeek/Details/5

        public ViewResult Details(int id)
        {
            return View(ladderweekRepository.GetById(id));
        }

        //
        // GET: /LadderWeek/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /LadderWeek/Create

        [HttpPost]
        public ActionResult Create(LadderWeek ladderweek)
        {
            if (ModelState.IsValid) {
                ladderweekRepository.InsertOrUpdate(ladderweek);
                ladderweekRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /LadderWeek/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(ladderweekRepository.GetById(id));
        }

        //
        // POST: /LadderWeek/Edit/5

        [HttpPost]
        public ActionResult Edit(LadderWeek ladderweek)
        {
            if (ModelState.IsValid) {
                ladderweekRepository.InsertOrUpdate(ladderweek);
                ladderweekRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /LadderWeek/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(ladderweekRepository.GetById(id));
        }

        //
        // POST: /LadderWeek/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ladderweekRepository.Delete(id);
            ladderweekRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

