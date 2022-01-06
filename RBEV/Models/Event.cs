using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{
    public class Event
    { //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int RacquetballEventID { get; set; }

        [Display(Name = "Event name")]
        [StringLength(50, MinimumLength = 3)]
        public string EventName { get; set; }

        [Display(Name = "Event Details")]
        public string EventDetails { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event Type")]
        public string EventType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Posted Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime PostedDate { get; set; }
        public int ClubID { get; set; }

        public Club Club { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<EventAssignment> EventAssignments { get; set; }
        public ICollection<EventLocation> EventLocations { get; set; }
    }
}
