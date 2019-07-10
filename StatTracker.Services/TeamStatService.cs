using StatTracker.Data;
using StatTracker.Models.TeamStatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Services
{
    public class TeamStatService
    {
        private readonly Guid _userID;

        public TeamStatService(Guid userID)
        {
            _userID = userID;
        }

        public IEnumerable<TeamStatListItem> SelectTeam(TeamSelect teamSelect)
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .TeamStats
                        .Where(e => e.CoachID == _userID && e.TeamID == teamSelect.TeamID && e.YearOfSeason == teamSelect.YearOfSeason)
                        .Select(
                            e =>
                            new TeamStatListItem
                            {
                                TeamID = e.TeamID,
                                TeamName = e.Team.TeamName,
                                YearOfSeason = e.YearOfSeason,
                                GameNumber = e.GameNumber,
                                PowerPlays = e.PowerPlays,
                                PowerPlayGoals = e.PowerPlayGoals,
                                PenaltyKills = e.PenaltyKills,
                                PenaltyKillGoalsAgainst = e.PenaltyKillGoalsAgainst,
                                GoalsFor = e.GoalsFor,
                                GoalsAgainst = e.GoalsAgainst
                            }
                        );
                return query.ToArray();
            }
        }

        public bool CreateTeamStats(TeamStatCreate stats)
        {
            var TeamStats =
                new TeamStat()
                {
                    CoachID = _userID,
                    TeamID = stats.TeamID,
                    YearOfSeason = stats.YearOfSeason,
                    GameNumber = stats.GameNumber,
                    PowerPlays = stats.PowerPlays,
                    PowerPlayGoals = stats.PowerPlayGoals,
                    PenaltyKills = stats.PenaltyKills,
                    PenaltyKillGoalsAgainst = stats.PenaltyKillGoalsAgainst,
                    GoalsFor = stats.GoalsFor,
                    GoalsAgainst = stats.GoalsAgainst
                };

            using (var db = new ApplicationDbContext())
            {
                db.TeamStats.Add(TeamStats);
                return db.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamStatListItem> GetTeamStats()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .TeamStats
                        .Where(e => e.CoachID == _userID)
                        .Select(
                            e =>
                            new TeamStatListItem
                            {
                                TeamID = e.TeamID,
                                TeamName = e.Team.TeamName,
                                YearOfSeason = e.YearOfSeason,
                                GameNumber = e.GameNumber,
                                PowerPlays = e.PowerPlays,
                                PowerPlayGoals = e.PowerPlayGoals,
                                PenaltyKills = e.PenaltyKills,
                                PenaltyKillGoalsAgainst = e.PenaltyKillGoalsAgainst,
                                GoalsFor = e.GoalsFor,
                                GoalsAgainst = e.GoalsAgainst
                            }
                        );
                return query.ToArray();
            }
        }

        public TeamStatDetails GetTeamStatByID(int teamID, int yearOfSeason, int gameNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .TeamStats
                        .Single(e => e.TeamID == teamID && e.YearOfSeason == yearOfSeason && e.GameNumber == gameNumber && e.CoachID == _userID);
                return new TeamStatDetails
                {
                    TeamID = entity.TeamID,
                    TeamName = entity.Team.TeamName,
                    YearOfSeason = entity.YearOfSeason,
                    GameNumber = entity.GameNumber,
                    PowerPlays = entity.PowerPlays,
                    PowerPlayGoals = entity.PowerPlayGoals,
                    PenaltyKills = entity.PenaltyKills,
                    PenaltyKillGoalsAgainst = entity.PenaltyKillGoalsAgainst,
                    GoalsFor = entity.GoalsFor,
                    GoalsAgainst = entity.GoalsAgainst
                };
            }
        }

        public bool UpdateTeamStats(TeamStatEdit teamStat)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .TeamStats
                        .Single(e => e.TeamID == teamStat.TeamID && e.YearOfSeason == teamStat.YearOfSeason && e.GameNumber == teamStat.GameNumber && e.CoachID == _userID);

                entity.TeamID = teamStat.TeamID;
                entity.YearOfSeason = teamStat.YearOfSeason;
                entity.GameNumber = teamStat.GameNumber;
                entity.PowerPlays = teamStat.PowerPlays;
                entity.PowerPlayGoals = teamStat.PowerPlayGoals;
                entity.PenaltyKills = teamStat.PenaltyKills;
                entity.PenaltyKillGoalsAgainst = teamStat.PenaltyKillGoalsAgainst;
                entity.GoalsFor = teamStat.GoalsFor;
                entity.GoalsAgainst = teamStat.GoalsAgainst;

                return db.SaveChanges() == 1;
            }
        }

        public bool DeleteTeamStats(int teamID, int yearOfSeason, int gameNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .TeamStats
                        .Single(e => e.TeamID == teamID && e.YearOfSeason == yearOfSeason && e.GameNumber == gameNumber && e.CoachID == _userID);
                db.TeamStats.Remove(entity);

                return db.SaveChanges() == 1;
            }
        }
    }
}
