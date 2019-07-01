using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTracker.Models.TeamModels
{
    class TeamDetails
    {
        public int TeamID { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Team Division")]
        public string TeamDivision { get; set; }
    }
}
