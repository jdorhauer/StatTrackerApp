using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Data
{
    public class Team
    {
        [Key]
        [Display(Name = "Team")]
        public int TeamID { get; set; }

        [Required]
        [Display(Name = "Coach")]
        public Guid CoachID { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Division")]
        public string TeamDivision { get; set; }
    }
}
