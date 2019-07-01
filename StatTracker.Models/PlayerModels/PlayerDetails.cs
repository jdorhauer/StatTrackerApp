﻿using StatTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerModels
{
    class PlayerDetails
    {
        [Display(Name = "Player ID")]
        public int PlayerID { get; set; }

        [Display(Name = "Player's Name")]
        public string FullName { get; set; }

        [Display(Name = "Position")]
        public Position PlayerPosition { get; set; }
        public string Team { get; set; }
    }
}
