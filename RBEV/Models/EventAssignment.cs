using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{
    public class EventAssignment
    {
        public int EventCoordinatorID { get; set; }
        public int EventID { get; set; }
        public EventCoordinator EventCoordinator { get; set; }
        public Event Event { get; set; }
    }
}
