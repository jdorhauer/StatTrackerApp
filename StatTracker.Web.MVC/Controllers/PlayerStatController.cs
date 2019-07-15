using Microsoft.AspNet.Identity;
using StatTracker.Data;
using StatTracker.Models.PlayerStatModels;
using StatTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatTracker.Web.MVC.Controllers
{
    public class PlayerStatController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: PlayerStat
        [Authorize]
        public ActionResult Index(PlayerStatSelect player)
        {
            var players = _db.Players.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.PlayerID = new SelectList(players, "PlayerID", "FullName");

            var service = CreatePlayerStatService();
            if (player.PlayerID == null && player.YearOfSeason == null)
            {
                var model = service.GetStats().OrderBy(t => t.TeamName).ThenBy(t => t.PlayerID).ThenBy(t => t.YearOfSeason).ThenBy(t => t.GameNumber);
                return View(model);
            }
            else if (player.PlayerID != null && player.YearOfSeason == null)
            {
                var model = service.SelectPlayer(player).OrderBy(p => p.YearOfSeason).ThenBy(p => p.GameNumber);
                return View(model);
            }
            else if (player.PlayerID == null && player.YearOfSeason != null)
            {
                var model = service.SelectSeason(player).OrderBy(p => p.TeamName).ThenBy(p => p.PlayerID).ThenBy(p => p.GameNumber);
                return View(model);
            }
            else
            {
                var model = service.SelectPlayerAndYear(player).OrderBy(p => p.GameNumber);
                return View(model);
            }
        }

        private PlayerStatService CreatePlayerStatService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerStatService(userID);

            return service;
        }

        // GET: PlayerStat/Details/5
        public ActionResult Details(int id, int year, int game)
        {
            var service = CreatePlayerStatService();
            var model = service.GetStatByID(id, year, game);

            return View(model);
        }

        // GET: PlayerStat/Create
        public ActionResult Create()
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));
            var players = _db.Players.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.PlayerID = new SelectList(players, "PlayerID", "FullName");
            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            return View();
        }

        // POST: PlayerStat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerStatCreate playerStat)
        {
            if (!ModelState.IsValid) return View(playerStat);

            var service = CreatePlayerStatService();

            if (service.CreatePlayerStats(playerStat))
            {
                TempData["SaveResult"] = "Stat was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Stat could not be added.");

            return View(playerStat);
        }

        // GET: PlayerStat/Edit/5
        public ActionResult Edit(int id, int year, int game)
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));
            var players = _db.Players.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.PlayerID = new SelectList(players, "PlayerID", "FullName");
            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            var service = CreatePlayerStatService();
            var detail = service.GetStatByID(id, year, game);
            var model =
                new PlayerStatEdit
                {
                    PlayerID = detail.PlayerID,
                    FullName = detail.FullName,
                    YearOfSeason = detail.YearOfSeason,
                    GameNumber = detail.GameNumber,
                    Goals = detail.Goals,
                    Assists = detail.Assists,
                    Shots = detail.Shots
                };

            return View(model);
        }

        // POST: PlayerStat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int year, int game, PlayerStatEdit playerStat)
        {
            if (!ModelState.IsValid) return View(playerStat);

            if (playerStat.PlayerID != id || playerStat.YearOfSeason != year || playerStat.GameNumber != game)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(playerStat);
            }

            var service = CreatePlayerStatService();

            if (service.UpdatePlayerStats(playerStat))
            {
                TempData["SaveResult"] = "Stats were updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Stats could not be updated.");
            return View(playerStat);
        }

        // GET: PlayerStat/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id, int year, int game)
        {
            var service = CreatePlayerStatService();
            var playerStat = service.GetStatByID(id, year, game);

            return View(playerStat);
        }

        // POST: PlayerStat/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id, int year, int game)
        {
            var service = CreatePlayerStatService();

            service.DeletePlayerStats(id, year, game);

            TempData["SaveResult"] = "Stats were deleted.";

            return RedirectToAction("Index");
        }
    }
}
