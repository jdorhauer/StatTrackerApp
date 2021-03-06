﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerStatModels
{
    public class PlayerStatCreate
    {
        public Guid CoachID { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerID { get; set; }

        [Required]
        [Display(Name = "Season Year")]
        public int YearOfSeason { get; set; }

        [Required]
        [Display(Name = "Game Number")]
        public int GameNumber { get; set; }

        [Required]
        public double Goals { get; set; }

        [Required]
        public int Assists { get; set; }

        [Required]
        public double Shots { get; set; }
    }
}
