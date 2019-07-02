using StatTracker.Data;
using StatTracker.Models.PlayerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Services
{
    public class PlayerService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly Guid _userID;

        public PlayerService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var player =
                new Player()
                {
                    CoachID = _userID,
                    FullName = model.FullName,
                    PlayerPosition = model.PlayerPosition,
                    TeamID = model.TeamID
                };

            _db.Players.Add(player);
            return _db.SaveChanges() == 1;
        }

        public IEnumerable<PlayerListItem> GetPlayers()
        {
            var query =
                _db
                    .Players
                    .Where(e => e.CoachID == _userID)
                    .Select(
                        e =>
                        new PlayerListItem
                        {
                            PlayerID = e.PlayerID,
                            FullName = e.FullName,
                            PlayerPosition = e.PlayerPosition,
                            Team = e.Team.TeamName
                        }
                     );
            return query.ToArray();
        }

        public PlayerDetails GetPlayerByID(int playerID)
        {
            var entity =
                _db
                    .Players
                    .Single(e => e.PlayerID == playerID && e.CoachID == _userID);
            return new PlayerDetails
            {
                PlayerID = entity.PlayerID,
                FullName = entity.FullName,
                PlayerPosition = entity.PlayerPosition,
                Team = entity.Team.TeamName
            };
        }

        public bool UpdatePlayer(PlayerEdit player)
        {
            var entity =
                _db
                    .Players
                    .Single(e => e.PlayerID == player.PlayerID && e.CoachID == _userID);

            entity.FullName = player.FullName;
            entity.PlayerPosition = player.PlayerPosition;
            entity.Team.TeamName = player.Team;

            return _db.SaveChanges() == 1;
        }

        public bool DeletePlayer(int playerID)
        {
            var entity =
                _db
                    .Players
                    .Single(e => e.PlayerID == playerID && e.CoachID == _userID);
            _db.Players.Remove(entity);

            return _db.SaveChanges() == 1;
        }
    }
}
