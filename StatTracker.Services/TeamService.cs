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
    }
}
