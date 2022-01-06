using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models.RacquetballViewModels
{
    public class RegistrationViewModel
    {
        public Event Event { get; set; }
        public Member Member { get; set; }

        public Registration Registration { get; set; }
    }
}
