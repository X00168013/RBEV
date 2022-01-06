using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models.RacquetballViewModels
{
    public class EventIndexView
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Registration> Registrations { get; set; }
        public IEnumerable<Club> Clubs { get; set; }
    }
}
