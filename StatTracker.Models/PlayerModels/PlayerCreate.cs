using StatTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.PlayerModels
{
    public class PlayerCreate
    {
        [Required]
        [Display(Name = "Player's Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Position")]
        public Position PlayerPosition { get; set; }

        [Display(Name = "Team")]
        public int TeamID { get; set; }
    }
}
