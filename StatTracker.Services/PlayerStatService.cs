using StatTracker.Data;
using StatTracker.Models.PlayerStatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Services
{
    public class PlayerStatService
    {
        private readonly Guid _userID;

        public PlayerStatService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePlayerStats(PlayerStatCreate stats)
        {
            var playerStats =
                new PlayerStat()
                {
                    CoachID = _userID,
                    PlayerID = stats.PlayerID,
                    YearOfSeason = stats.YearOfSeason,
                    GameNumber = stats.GameNumber,
                    Goals = stats.Goals,
                    Assists = stats.Assists,
                    Shots = stats.Shots
                };

            using (var db = new ApplicationDbContext())
            {
                db.PlayerStats.Add(playerStats);
                return db.SaveChanges() == 1;
            }
        }

        public IEnumerable<PlayerStatListItem> GetStats()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .PlayerStats
                        .Where(e => e.CoachID == _userID)
                        .Select(
                            e =>
                            new PlayerStatListItem
                            {
                                PlayerID = e.PlayerID,
                                TeamName = e.Player.Team.TeamName,
                                FullName = e.Player.FullName,
                                YearOfSeason = e.YearOfSeason,
                                GameNumber = e.GameNumber,
                                Goals = e.Goals,
                                Assists = e.Assists,
                                Shots = e.Shots,
                                ShootingPercentage = e.Goals / e.Shots
                            }
                        );

                return query.ToArray();
            }
        }

        public PlayerStatDetails GetStatByID(int playerID, int yearOfSeason, int gameNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .PlayerStats
                        .Single(e => e.PlayerID == playerID && e.YearOfSeason == yearOfSeason && e.GameNumber == gameNumber && e.CoachID == _userID);
                return new PlayerStatDetails
                {
                    PlayerID = entity.PlayerID,
                    TeamName = entity.Player.Team.TeamName,
                    FullName = entity.Player.FullName,
                    YearOfSeason = entity.YearOfSeason,
                    GameNumber = entity.GameNumber,
                    Goals = entity.Goals,
                    Assists = entity.Assists,
                    Shots = entity.Shots,
                    ShootingPercentage = entity.Goals / entity.Shots
                };
            }
        }

        public bool UpdatePlayerStats(PlayerStatEdit playerStat)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .PlayerStats
                        .Single(e => e.PlayerID == playerStat.PlayerID && e.YearOfSeason == playerStat.YearOfSeason && e.GameNumber == playerStat.GameNumber && e.CoachID == _userID);

                entity.PlayerID = playerStat.PlayerID;
                entity.YearOfSeason = playerStat.YearOfSeason;
                entity.GameNumber = playerStat.GameNumber;
                entity.Goals = playerStat.Goals;
                entity.Assists = playerStat.Assists;
                entity.Shots = playerStat.Shots;

                return db.SaveChanges() == 1;
            }
        }

        public bool DeletePlayerStats(int playerID, int yearOfSeason, int gameNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                     .PlayerStats
                     .Single(e => e.PlayerID == playerID && e.YearOfSeason == yearOfSeason && e.GameNumber == gameNumber && e.CoachID == _userID);
                db.PlayerStats.Remove(entity);

                return db.SaveChanges() == 1;
            }
        }
    }
}
