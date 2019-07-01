using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Data
{
    public enum Position { Goalie, LeftWing, RightWing, Center, LeftDefense, RightDefense }

    public class Player
    {
        [Key]
        [Display(Name = "Player ID")]
        public int PlayerID { get; set; }

        [Required]
        [Display(Name = "Player's Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Position")]
        public Position PlayerPosition { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamID { get; set; }
        public virtual Team Team { get; set; }
    }
}
