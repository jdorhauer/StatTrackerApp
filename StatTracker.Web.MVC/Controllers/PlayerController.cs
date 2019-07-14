using Microsoft.AspNet.Identity;
using StatTracker.Data;
using StatTracker.Models.PlayerModels;
using StatTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatTracker.Web.MVC.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Player
        [Authorize]
        public ActionResult Index(PlayerSelect player)
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            var service = CreatePlayerService();
            if (player.TeamID == null)
            {
                var model = service.GetPlayers().OrderBy(t => t.TeamName).ThenBy(p => p.PlayerID);
                return View(model);
            }
            else
            {
                var model = service.GetPlayerByTeam(player);
                return View(model);
            }
        }

        private PlayerService CreatePlayerService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerService(userID);

            return service;
        }

        // GET: Player/Details
        public ActionResult Details(int id)
        {
            var svc = CreatePlayerService();
            var model = svc.GetPlayerByID(id);

            return View(model);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));
            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate player)
        {
            if (!ModelState.IsValid) return View(player);

            var service = CreatePlayerService();

            if (service.CreatePlayer(player))
            {
                TempData["SaveResult"] = "Player was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be added.");

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));
            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            var service = CreatePlayerService();
            var detail = service.GetPlayerByID(id);
            var model =
                new PlayerEdit
                {
                    PlayerID = detail.PlayerID,
                    FullName = detail.FullName,
                    PlayerPosition = detail.PlayerPosition,
                    TeamID = detail.TeamID
                };
            return View(model);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlayerEdit player)
        {
            if (!ModelState.IsValid) return View(player);

            if (player.PlayerID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(player);
            }

            var service = CreatePlayerService();

            if (service.UpdatePlayer(player))
            {
                TempData["SaveResult"] = "Player info was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player info could not be updated.");
            return View(player);
        }

        // GET: Player/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePlayerService();
            var player = service.GetPlayerByID(id);

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePlayerService();

            service.DeletePlayer(id);

            TempData["SaveResult"] = "Player was deleted.";

            return RedirectToAction("Index");
        }
    }
}
