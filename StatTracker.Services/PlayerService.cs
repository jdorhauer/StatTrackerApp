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

            using (var db = new ApplicationDbContext())
            {
                db.Players.Add(player);
                return db.SaveChanges() == 1;
            }
        }

        public IEnumerable<PlayerListItem> GetPlayers()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .Players
                        .Where(e => e.CoachID == _userID)
                        .Select(
                            e =>
                            new PlayerListItem
                            {
                                PlayerID = e.PlayerID,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                TeamID = e.TeamID
                            }
                         );
                return query.ToArray();
            }
        }

        public PlayerDetails GetPlayerByID(int playerID)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Players
                        .Single(e => e.PlayerID == playerID && e.CoachID == _userID);
                return new PlayerDetails
                {
                    PlayerID = entity.PlayerID,
                    FullName = entity.FullName,
                    PlayerPosition = entity.PlayerPosition,
                    TeamID = entity.TeamID
                };
            }
        }

        public bool UpdatePlayer(PlayerEdit player)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Players
                        .Single(e => e.PlayerID == player.PlayerID && e.CoachID == _userID);

                entity.FullName = player.FullName;
                entity.PlayerPosition = player.PlayerPosition;
                entity.TeamID = player.TeamID;

                return db.SaveChanges() == 1;
            }
        }

        public bool DeletePlayer(int playerID)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Players
                        .Single(e => e.PlayerID == playerID && e.CoachID == _userID);
                db.Players.Remove(entity);

                return db.SaveChanges() == 1;
            }
        }
    }
}
