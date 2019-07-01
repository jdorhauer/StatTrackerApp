using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.TeamStatModels
{
    class TeamStatEdit
    {
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Season Year")]
        public int YearOfSeason { get; set; }

        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }

        [Display(Name = "Power Plays")]
        public int PowerPlays { get; set; }

        [Display(Name = "Power Play Goals")]
        public int PowerPlayGoals { get; set; }

        [Display(Name = "Penalty Kills")]
        public int PenaltyKills { get; set; }

        [Display(Name = "Goals Against on Penalty Kill")]
        public int PenaltyKillGoalsAgainst { get; set; }

        [Display(Name = "Goals For")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goals Against")]
        public int GoalsAgainst { get; set; }
    }
}
