﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Data
{
    public class PlayerStat
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Player))]
        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }

        [Key]
        [Column(Order = 1)]
        public int YearOfSeason { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GameNumber { get; set; }

        [Required]
        public Guid CoachID { get; set; }

        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Shots { get; set; }
    }
}
