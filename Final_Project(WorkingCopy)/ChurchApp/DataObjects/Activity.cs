using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    // This class is representative of an activity that is going on in the church.
    public class Activity
    {
        
        public int ActivityID { get; set; }
        
        [Display (Name = "Activity Name")]
        [Required(ErrorMessage = "Please supply an Activity Name")]
        public string ActivityName { get; set; }
        [Required(ErrorMessage = "Please supply an Activity Type")]
        [Display(Name = "Activity Type")]
        public string ActivityTypeID { get; set; }
        [Required(ErrorMessage = "Please supply a Location Name")]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        [Required(ErrorMessage = "Please supply an Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please supply a City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please supply a State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please supply a Zip")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please supply a Description")]
        public string Description { get; set; }
        public bool ActivityActive { get; set; }
    }
}
