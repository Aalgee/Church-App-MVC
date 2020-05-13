using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Group
    {
        [Display(Name = "Group")]
        [Required(ErrorMessage = "Please supply a group name")]
        public string GroupID { get; set; }
        [Required(ErrorMessage = "Please supply a description")]
        public string Description { get; set; }
    }
}
