using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataObjects
{
    public class Grant
    {
        public int GrantID { get; set; }
        [Display(Name = "Grant Name")]
        [Required(ErrorMessage = "Please enter a Grant Name.")]
        public string GrantName { get; set; }
        public int Points { get; set; }
        [Required(ErrorMessage = "Please enter a Grant Description")]
        public string Description { get; set; }
        [Display(Name = "Amount Asked For")]
        [Required(ErrorMessage = "Please enter an Amount Asked For")]
        public decimal AmountAskedFor { get; set; }
        [Display(Name = "Amount Recieved")]
        public decimal AmountRecieved { get; set; }
        public bool Active { get; set; }
    }
}
