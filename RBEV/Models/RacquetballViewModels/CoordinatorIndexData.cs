using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models.RacquetballViewModels
{
    public class CoordinatorIndexData
    {
        public IEnumerable<EventCoordinator> EventCoordinators { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Registration> Registrations { get; set; }
    }
}
