using StatTracker.Data;
using StatTracker.Models.TeamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Services
{
    public class TeamService
    {
        private readonly Guid _userID;

        public TeamService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateTeam(TeamCreate model)
        {
            var team =
                new Team()
                {
                    CoachID = _userID,
                    TeamName = model.TeamName,
                    TeamDivision = model.TeamDivision
                };

            using (var db = new ApplicationDbContext())
            {
                db.Teams.Add(team);
                return db.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db
                        .Teams
                        .Where(e => e.CoachID == _userID)
                        .Select(
                        e => new TeamListItem
                            {
                                TeamID = e.TeamID,
                                TeamName = e.TeamName,
                                TeamDivision = e.TeamDivision
                            }
                        );
                return query.ToArray();
            }
        }

        public TeamDetails GetTeamByID(int teamID)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Teams
                        .Single(e => e.TeamID == teamID && e.CoachID == _userID);
                return new TeamDetails
                {
                    TeamID = entity.TeamID,
                    TeamName = entity.TeamName,
                    TeamDivision = entity.TeamDivision
                };
            }
        }

        public bool UpdateTeam(TeamEdit team)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Teams
                        .Single(e => e.TeamID == team.TeamID && e.CoachID == _userID);

                entity.TeamName = team.TeamName;
                entity.TeamDivision = team.TeamDivision;

                return db.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int teamID)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Teams
                        .Single(e => e.TeamID == teamID && e.CoachID == _userID);
                db.Teams.Remove(entity);

                return db.SaveChanges() == 1;
            }
        }
    }
}
