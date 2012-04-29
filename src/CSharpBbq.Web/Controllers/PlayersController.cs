using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpBbq.Data.Model.Ladder;
using CSharpBbq.Web.Models;

namespace CSharpBbq.Web.Controllers
{   
    public class PlayersController : Controller
    {
		private readonly IPlayerRepository playerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PlayersController() : this(new PlayerRepository())
        {
        }

        public PlayersController(IPlayerRepository playerRepository)
        {
			this.playerRepository = playerRepository;
        }

        //
        // GET: /Player/

        public ViewResult Index()
        {
            return View(playerRepository.GetAllPlayers(player => player.Standings));
        }

        //
        // GET: /Player/Details/5

        public ViewResult Details(int id)
        {
            return View(playerRepository.GetById(id));
        }

        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Player/Create

        [HttpPost]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid) {
                playerRepository.InsertOrUpdate(player);
                playerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Player/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(playerRepository.GetById(id));
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid) {
                playerRepository.InsertOrUpdate(player);
                playerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Player/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(playerRepository.GetById(id));
        }

        //
        // POST: /Player/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            playerRepository.Delete(id);
            playerRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

