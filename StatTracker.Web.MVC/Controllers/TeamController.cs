using Microsoft.AspNet.Identity;
using StatTracker.Data;
using StatTracker.Models.TeamModels;
using StatTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatTracker.Web.MVC.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        [Authorize]
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TeamService(userID);
            var model = service.GetTeams();

            return View(model);
        }

        private TeamService CreateTeamService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TeamService(userID);

            return service;
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var service = CreateTeamService();
            var model = service.GetTeamByID(id);

            return View(model);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreate team)
        {
            if (!ModelState.IsValid) return View(team);

            var service = CreateTeamService();

            if (service.CreateTeam(team))
            {
                TempData["SaveResult"] = "Team was added.";
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateTeamService();
            var detail = service.GetTeamByID(id);
            var model =
                new TeamEdit
                {
                    TeamID = detail.TeamID,
                    TeamName = detail.TeamName,
                    TeamDivision = detail.TeamDivision
                };
            return View(model);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit team)
        {
            if (!ModelState.IsValid) return View(team);

            if (team.TeamID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(team);
            }

            var service = CreateTeamService();

            if (service.UpdateTeam(team))
            {
                TempData["SaveResult"] = "Team was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Team info could not be updated.");
            return View(team);
        }

        // GET: Team/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateTeamService();
            var team = service.GetTeamByID(id);

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTeamService();

            service.DeleteTeam(id);

            TempData["SaveResult"] = "Team was deleted";

            return RedirectToAction("Index");
        }
    }
}
