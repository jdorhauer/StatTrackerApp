using Microsoft.AspNet.Identity;
using StatTracker.Data;
using StatTracker.Models.TeamStatModels;
using StatTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatTracker.Web.MVC.Controllers
{
    public class TeamStatController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: TeamStat
        public ActionResult Index(TeamSelect team)
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            var service = CreateTeamStatService();
            if (team.TeamID == null && team.YearOfSeason == null)
            {
                var model = service.GetTeamStats().OrderBy(t => t.TeamID).ThenBy(t => t.YearOfSeason).ThenBy(t => t.GameNumber);
                return View(model);
            }
            else if (team.TeamID != null && team.YearOfSeason == null)
            {
                var model = service.SelectTeam(team).OrderBy(t => t.YearOfSeason).ThenBy(t => t.GameNumber);
                return View(model);
            }
            else if (team.TeamID == null && team.YearOfSeason != null)
            {
                var model = service.SelectSeason(team).OrderBy(t => t.TeamID).ThenBy(t => t.GameNumber);
                return View(model);
            }
            else
            {
                var model = service.SelectTeamAndYear(team).OrderBy(t => t.GameNumber);
                return View(model);
            }

        }

        private TeamStatService CreateTeamStatService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TeamStatService(userID);

            return service;
        }

        // GET: TeamStat/Details/5
        public ActionResult Details(int id, int year, int game)
        {
            var service = CreateTeamStatService();
            var model = service.GetTeamStatByID(id, year, game);

            return View(model);
        }

        // GET: TeamStat/Create
        public ActionResult Create()
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            return View();
        }

        // POST: TeamStat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamStatCreate teamStat)
        {
            if (!ModelState.IsValid) return View(teamStat);

            var service = CreateTeamStatService();

            if (service.CreateTeamStats(teamStat))
            {
                TempData["SaveResult"] = "Stat was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Stat could not be added.");

            return View(teamStat);
        }

        // GET: TeamStat/Edit/5
        public ActionResult Edit(int id, int year, int game)
        {
            var teams = _db.Teams.ToList().Where(t => t.CoachID == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.TeamID = new SelectList(teams, "TeamID", "TeamName");

            var service = CreateTeamStatService();
            var detail = service.GetTeamStatByID(id, year, game);
            var model =
                new TeamStatEdit
                {
                    TeamID = detail.TeamID,
                    TeamName = detail.TeamName,
                    YearOfSeason = detail.YearOfSeason,
                    GameNumber = detail.GameNumber,
                    PowerPlays = detail.PowerPlays,
                    PowerPlayGoals = detail.PowerPlayGoals,
                    PenaltyKills = detail.PenaltyKills,
                    PenaltyKillGoalsAgainst = detail.PenaltyKillGoalsAgainst,
                    GoalsFor = detail.GoalsFor,
                    GoalsAgainst = detail.GoalsAgainst
                };

            return View(model);
        }

        // POST: TeamStat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int year, int game, TeamStatEdit teamStat)
        {
            if (!ModelState.IsValid) return View(teamStat);

            if (teamStat.TeamID != id || teamStat.YearOfSeason != year || teamStat.GameNumber != game)
            {
                ModelState.AddModelError("", "ID mismatch");
                return View(teamStat);
            }

            var service = CreateTeamStatService();

            if (service.UpdateTeamStats(teamStat))
            {
                TempData["SaveResult"] = "Stats were updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Stats could not be updated.");
            return View(teamStat);
        }

        // GET: TeamStat/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id, int year, int game)
        {
            var service = CreateTeamStatService();
            var teamStat = service.GetTeamStatByID(id, year, game);

            return View(teamStat);
        }

        // POST: TeamStat/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStat(int id, int year, int game)
        {
            var service = CreateTeamStatService();

            service.DeleteTeamStats(id, year, game);

            TempData["SaveResult"] = "Stats were deleted.";

            return RedirectToAction("Index");
        }
    }
}
