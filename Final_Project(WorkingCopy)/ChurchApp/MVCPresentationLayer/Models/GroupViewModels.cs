using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    public class GroupMemberApplicantViewModel
    {
        public string GroupID { get; set; }
        public List<User> GroupMembers { get; set; }
        public List<User> GroupApplicants { get; set; }
    }
}