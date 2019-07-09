using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerStatModels
{
    public class PlayerStatListItem
    {
        [Display(Name = "Player ID")]
        public int PlayerID { get; set; }

        [Display(Name = "Player Name")]
        public string FullName { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Season Year")]
        public int YearOfSeason { get; set; }

        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }
        public double Goals { get; set; }
        public int Assists { get; set; }
        public double Shots { get; set; }

        [Display(Name = "Shooting Percentage")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double ShootingPercentage
        {
            get
            {
                if (Shots != 0)
                {
                    return Goals / Shots;
                }
                return 0;
            }
        }
    }
}
