using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ElectionDelegate
    {
        [Display(Name = "Delegate ID")]
        public int DelegateID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Please enter a Pin.")]
        public string Pin { get; set; }
        [Display(Name = "Has Voted For Grants")]
        public bool HasVotedForGrants { get; set; }
        [Display(Name = "Has Voted For Elections")]
        public bool HasVotedForElections { get; set; }
        public bool Active { get; set; }
    }
}
