using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{
    public class EventLocation
    { 
        [Key]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter city latitude")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Please enter city longitude ")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "Please enter description ")]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}
