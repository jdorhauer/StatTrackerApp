using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.TeamStatModels
{
    public class TeamStatListItem
    {
        public int TeamID { get; set; }

        [Display(Name = "Team")]
        public string TeamName { get; set; }

        [Display(Name = "Season")]
        public int YearOfSeason { get; set; }

        [Display(Name = "Game #")]
        public int GameNumber { get; set; }

        [Display(Name = "Power Plays")]
        public double PowerPlays { get; set; }

        [Display(Name = "Power Play Goals")]
        public double PowerPlayGoals { get; set; }

        [Display(Name = "Penalty Kills")]
        public double PenaltyKills { get; set; }

        [Display(Name = "GA on PK")]
        public double PenaltyKillGoalsAgainst { get; set; }

        [Display(Name = "Goals For")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goals Against")]
        public int GoalsAgainst { get; set; }

        [Display(Name = "Won Game?")]
        public bool Win
        {
            get
            {
                if (GoalsFor > GoalsAgainst)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
