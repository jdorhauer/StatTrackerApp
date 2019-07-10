using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Data
{
    public class TeamStat
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Team))]
        [Display(Name = "Team")]
        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        [Key]
        [Column(Order = 1)]
        public int YearOfSeason { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GameNumber { get; set; }

        [Required]
        public Guid CoachID { get; set; }

        public double PowerPlays { get; set; }
        public double PowerPlayGoals { get; set; }
        public double PowerPlayPercentage { get; set; }
        public double PenaltyKills { get; set; }
        public double PenaltyKillGoalsAgainst { get; set; }
        public double PenaltyKillPercentage { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public bool Win { get; set; }
    }
}
