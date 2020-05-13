using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    class Delegate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a Pin.")]
        public string Pin { get; set; }
        public bool HasVotedForGrants { get; set; }
        public bool Active { get; set; }
    }
}
