using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    class GroupVM : Group
    {
        public List<ActivityVM> Activities { get; set; }
        public List<User> Users { get; set; }
    }
}
