using RBEV.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{

        public class Club
        {
            public int ClubID { get; set; }

            [StringLength(50, MinimumLength = 3)]
            public string Name { get; set; }

            public string County { get; set; }

            public Province Province { get; set; }

            [Display(Name = "Number of Courts")]
            public int NumberofCourts { get; set; }

            public int? EventCoordinatorID { get; set; }

            public EventCoordinator Adminstrator { get; set; }
            [Timestamp]
            public byte[] RowVersion { get; set; }
            public ICollection<Event> Events { get; set; }
        }
}
