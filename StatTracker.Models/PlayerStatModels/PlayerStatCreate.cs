﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerStatModels
{
    class PlayerStatCreate
    {
        [Required]
        [Display(Name = "Player's Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Year of Season")]
        public int YearOfSeason { get; set; }

        [Required]
        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }

        [Required]
        public int Goals { get; set; }

        [Required]
        public int Assists { get; set; }

        [Required]
        public int Shots { get; set; }
    }
}