using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.TeamStatModels
{
    class TeamStatCreate
    {
        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Required]
        [Display(Name = "Season Year")]
        public int YearOfSeason { get; set; }

        [Required]
        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }

        [Required]
        [Display(Name = "Power Plays")]
        public int PowerPlays { get; set; }

        [Required]
        [Display(Name = "Power Play Goals")]
        public int PowerPlayGoals { get; set; }

        [Required]
        [Display(Name = "Penalty Kills")]
        public int PenaltyKills { get; set; }

        [Required]
        [Display(Name = "Goals Against on Penalty Kill")]
        public int PenaltyKillGoalsAgainst { get; set; }

        [Required]
        [Display(Name = "Goals For")]
        public int GoalsFor { get; set; }

        [Required]
        [Display(Name = "Goals Against")]
        public int GoalsAgainst { get; set; }
    }
}
