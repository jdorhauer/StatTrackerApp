using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerStatModels
{
    public class PlayerStatListItem
    {
        public int PlayerID { get; set; }
        public string FullName { get; set; }
        public int YearOfSeason { get; set; }
        public int GameNumber { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Shots { get; set; }
        public double ShootingPercentage
        {
            get
            {
                return Goals / Shots;
            }
        }
    }
}
