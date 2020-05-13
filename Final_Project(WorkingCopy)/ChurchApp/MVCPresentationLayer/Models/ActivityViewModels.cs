using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    public class ActivityDetailsViewModel
    {
        public ActivityVM Activity { get; set; }
        public List<User> Attendees { get; set; }
    }
}