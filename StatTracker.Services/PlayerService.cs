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

        // Get ID automatically created for user at registration
        public PlayerService(Guid userID)
        {
            _userID = userID;
        }


        // Create a new player on the table
        public bool CreatePlayer(PlayerCreate model)
        {
            var player =
                new Player()
                {
                    // Setting player attributes
                    CoachID = _userID,
                    FullName = model.FullName,
                    PlayerPosition = model.PlayerPosition,
                    TeamID = model.TeamID
                };

            // add the new player to the database and save the changes to the database
            using (var db = new ApplicationDbContext())
            {
                db.Players.Add(player);
                return db.SaveChanges() == 1;
            }
        }

        // Get the list of players contained in the database
        public IEnumerable<PlayerListItem> GetPlayers()
        {
            // pull each line from the database
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .Players
                        // pull for logged in coach
                        .Where(e => e.CoachID == _userID)
                        // what we are pulling
                        .Select(
                            e =>
                            new PlayerListItem
                            {
                                PlayerID = e.PlayerID,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                TeamName = e.Team.TeamName
                            }
                         );
                // return each line as an array item
                return query.ToArray();
            }
        }

        // return info for a player based on ID that is passed into method
        public PlayerDetails GetPlayerByID(int playerID)
        {
            // pull single player from the database
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Players
                        // make sure the playerID matches the ID passed in and the coach is the one logged on
                        .Single(e => e.PlayerID == playerID && e.CoachID == _userID);
                // pull these details for display
                return new PlayerDetails
                {
                    PlayerID = entity.PlayerID,
                    FullName = entity.FullName,
                    PlayerPosition = entity.PlayerPosition,
                    TeamID = entity.TeamID,
                    TeamName = entity.Team.TeamName,
                    TeamDivision = entity.Team.TeamDivision
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
