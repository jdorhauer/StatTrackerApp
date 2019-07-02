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
        public int TeamID { get; set; }

        [Required]
        public Guid CoachID { get; set; }

        public string TeamName { get; set; }


        public string TeamDivision { get; set; }
    }
}
