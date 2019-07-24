using Microsoft.AspNet.Identity;
using StatTracker.Models.PlayerModels;
using StatTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StatTracker.WebAPI.Controllers
{
    [Authorize]
    public class StatTrackerController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            PlayerService playerService = CreatePlayerService();
            var players = playerService.GetPlayers();
            return Ok(players);
        }

        public IHttpActionResult Get(int id)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerByID(id);
            return Ok(player);
        }

        public IHttpActionResult Post(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.UpdatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer(id))
                return InternalServerError();

            return Ok();
        }

        private PlayerService CreatePlayerService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var playerService = new PlayerService(userID);

            return playerService;
        }
    }
}
