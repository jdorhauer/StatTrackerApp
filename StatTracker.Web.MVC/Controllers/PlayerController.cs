using Microsoft.AspNet.Identity;
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
        // GET: Player
        [Authorize]
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerService(userID);
            var model = service.GetPlayers();

            return View(model);
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
            return View();
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
