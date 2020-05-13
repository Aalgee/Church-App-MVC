using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    // This class represents a user.
    public class User
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please supply a first name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please supply a last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please supply a date of birth")]
        public DateTime Dob { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please supply a phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please supply an email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please supply an address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please supply a city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please supply a state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please supply a zipcode")]
        public string Zip { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Groups { get; set; }
        public bool IsDelegate { get; set; }
    }
}
