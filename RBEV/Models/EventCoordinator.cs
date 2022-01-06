using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{
    public class EventCoordinator : Account
    {  
        [Display(Name = "Club Role")]
        public string ClubRole { get; set; }
        public ICollection<EventAssignment> EventAssignments { get; set; }
     }
}
