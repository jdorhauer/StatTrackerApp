using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerStatModels
{
    class PlayerStatEdit
    {
        public int PlayerID { get; set; }

        [Display(Name = "Player's Name")]
        public string FullName { get; set; }

        [Display(Name = "Season Year")]
        public int YearOfSeason { get; set; }

        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }

        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Shots { get; set; }
    }
}
