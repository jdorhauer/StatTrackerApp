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

        //public IEnumerable<PlayerStatListItem> GetStats()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var query =
        //            db
        //                .PlayerStats
        //                .Where(e => e.CoachID == _userID)
        //                .Select(
        //                    e =>
        //                    new PlayerStatListItem
        //                    {
        //                        PlayerID = e.PlayerID,
        //                        FullName = e.Player.FullName,
        //                        YearOfSeason = e.YearOfSeason,
        //                        GameNumber = e.GameNumber,

        //                    }
        //                )
        //    }
        //}
    }
}
