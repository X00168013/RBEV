using RBEV.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RBEV.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }
        public int EventID { get; set; }
        public int MemberID { get; set; }
        [DisplayFormat(NullDisplayText = "No division")]
        public Division? Division { get; set; }

        public Event Event { get; set; }
        public Member Member { get; set; }
    }
}
